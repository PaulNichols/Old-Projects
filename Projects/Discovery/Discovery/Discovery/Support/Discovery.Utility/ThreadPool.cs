using System.Threading; 
using System.Collections; 
using System;
using System.Data;
using System.Diagnostics;

namespace Discovery.Utility
{
    public delegate void ThreadErrorHandlerDelegate(ThreadPoolWorkItem workItem, Exception lastError);

    public delegate object ThreadWorkDelegate(object[] objArgs);
    
    public class ThreadPoolWorkItem  
	{ 
        public bool storeOutput = false; 
        public string name = "";
        public ThreadWorkDelegate delegateToCall = null; 
		public Exception lastException = null;

        public object[] objInput = null;
        public object objOutput = null; 
        
		public ThreadPoolWorkItem() 
		{ 
        }

		public ThreadPoolWorkItem(
					string name,
                    ThreadWorkDelegate delegateToCall, 
					object[] objInput, 
					bool storeOutput) 
		{ 
            this.name               = name;
            this.delegateToCall     = delegateToCall;
            this.objInput           = objInput;
            this.storeOutput        = storeOutput; 
        } 
    } 
    
    public class DiscoveryThreadPool  
	{
        private const int MAXTHREADS = 256;
        private const int MAXQUEUESIZE = 1024;

		private decimal shutdownPauseSeconds = 1; 
		private decimal serverPauseSeconds = 1;
        
        // Thread management
        private Hashtable hashThreads = new Hashtable(MAXTHREADS); 
        private int minThreadCount = 1; 
        private int maxThreadCount = 1;

        // Work queues
        private Queue queueInput = new Queue(MAXQUEUESIZE);
        private Queue queueOutput = new Queue(MAXQUEUESIZE);

        private bool isRunning = false; 
        private Exception lastException = null;

        public event ThreadErrorHandlerDelegate ThreadError;

        public bool IsRunning
        {
            get
            {
                return isRunning;
            }
        }
            
		private void ThreadProc() 
		{ 
			// While we're runing
            while (isRunning) 
			{ 
                object obj = null; 

				// Lock this object
				Monitor.Enter(this);

				// See if there's something in the queue
				if (queueInput.Count > 0) 
				{
					// Get work item out of the queue
					obj = queueInput.Dequeue(); 
				}

				// Unlock this object
                Monitor.Exit(this); 

				// Do we have a work item
				if (obj == null) 
				{ 
					// Nothing to do
                    bool bQuit = false;
					
                    // Lock this object
                    Monitor.Enter(this); 
				
					// If we're running above the min thread count, remove the thread
                    if (hashThreads.Count > minThreadCount) 
					{ 
						// Remove the thread from the pool
                        hashThreads.Remove(Thread.CurrentThread.Name);
					
						// Quit thread loop
                        bQuit = true; 
                    } 
					// Unlock this object
                    Monitor.Exit(this); 

					// See if we're done
                    if (bQuit) 
					{ 
						// Done
						return ; 
					} 
                } 
                else 
				{
					// We have an item of work to do
                    ThreadPoolWorkItem oWorkItem = (ThreadPoolWorkItem) obj; 
                    
					try 
					{
                        // Execute the method on the work item
                        oWorkItem.objOutput = oWorkItem.delegateToCall(oWorkItem.objInput); 
                    } 
                    catch (Exception ex) 
					{ 
                        // See if we have an exception delegate to call
                        if (ThreadError != null) 
						{ 
                            // Call the thread delegate, this may throw an exception by design
                            ThreadError(oWorkItem, ex);
                        } 
                    } 

                    if (oWorkItem.storeOutput) 
					{ 
						// Lock this object
                        Monitor.Enter(queueOutput);

						// Add the competed item to the output queue
                        queueOutput.Enqueue(oWorkItem);
						
						// Unlock this object
                        Monitor.Exit(queueOutput); 
                    }
                } 

                // Don't run again for server pause seconds 
                Thread.Sleep((int)(1000 * serverPauseSeconds)); 
            } 
        } 
        
        // Default exception handler, can be overriden
        private void OnThreadError(ThreadPoolWorkItem oWorkItem, Exception oError) 
		{ 
            if (oWorkItem == null) 
			{ 
                lastException = oError; 
            } 
            else 
			{ 
                oWorkItem.lastException = oError; 
            } 
        } 
        
        public decimal ServerPauseSeconds
		{ 
            set
            {
                // Lock this resource
                Monitor.Enter(this);

                // Do work
                if (value >= 1 && value < 300)
                {
                    serverPauseSeconds = value;
                }

                // Release this resource
                Monitor.Exit(this); 
            }
        } 
        
        public decimal ShutdownPauseSeconds
		{ 
            set
            {
                // Lock this resource
                Monitor.Enter(this);

                // Do work
                if (value < 200)
                {
                    shutdownPauseSeconds = value;
                }

                // Release this resource
                Monitor.Exit(this);
            }
        } 
        
        public Exception LastException
		{ 
            get
            {
                return lastException;
            }
        } 
        
        public void InsertWorkItem(ThreadPoolWorkItem oWorkItem) 
		{ 
            try 
			{ 
				// Lock this object
                Monitor.Enter(this); 
            
				// Add the work item to the queue
				queueInput.Enqueue(oWorkItem); 
                
				if (isRunning && 
					(queueInput.Count > hashThreads.Count) && 
					(hashThreads.Count < maxThreadCount)) 
				{ 
					// Create a thread to handle the new work item
                    Thread th = new Thread(new System.Threading.ThreadStart(ThreadProc)); 
					// Generate a thread name
					th.Name = Guid.NewGuid().ToString();
					// Add the thread to the pool
                    hashThreads.Add(th.Name, th);
					// Start the thread
                    th.Start(); 
                } 
            } 
            catch (Exception oBug) 
			{ 
				// Store the exception
                lastException = oBug; 
            } 
            finally 
			{ 
				// Unlock this obkect
                Monitor.Exit(this); 
            } 
        } 
        
        public void InsertWorkItem(
					string sName, 
					ThreadWorkDelegate pMethod, 
					object[] pArgs, 
					bool bStoreOutput) 
		{ 
            InsertWorkItem(new ThreadPoolWorkItem(sName, pMethod, pArgs, bStoreOutput)); 
        } 
        
        public ThreadPoolWorkItem ExtractWorkItem() 
		{ 
            object oWorkItem = null; 
            
			Monitor.Enter(queueOutput); 
            if (queueOutput.Count > 0) 
			{ 
				oWorkItem = queueOutput.Dequeue(); 
			} 
            Monitor.Exit(queueOutput); 
            
			if (oWorkItem == null) 
			{ 
				return null; 
			} 
            
			return ((ThreadPoolWorkItem)(oWorkItem)); 
        } 
        
        public bool StartThreadPool(
					int minThreadCount,		// 5
					int maxThreadCount)		// 10
		{ 
            try 
			{ 
                // Lock this object
				Monitor.Enter(this); 
				
                // Make sure we're not already running
                if (!isRunning) 
				{ 
					// We're running
                    isRunning = true; 
					
                    // Work out the min and max thread count
                    if ((minThreadCount > 0) && (minThreadCount < MAXTHREADS)) 
					{ 
                        // Min thread count specified by user
                        this.minThreadCount = minThreadCount; 
                    } 

                    if ((maxThreadCount > minThreadCount) && (maxThreadCount < MAXTHREADS)) 
					{ 
                        // Max thread count specified by user
                        this.maxThreadCount = maxThreadCount; 
                    } 
                    else 
					{ 
                        // We need to calculate the max number of threads
                        this.maxThreadCount = 2 * this.minThreadCount; 
                    } 
                    
					// Create the minimum number of threads
                    for (int i = 1; i <= this.minThreadCount; i++) 
					{ 
						// Create the thread
                        Thread th = new Thread(new System.Threading.ThreadStart(ThreadProc)); 
						
                        // Name the thread
                        th.Name =  Guid.NewGuid().ToString(); 
						
                        // Add it to the hash
                        hashThreads.Add(th.Name, th);
						
                        // Start the thread, this just pulls work items from our queue
                        th.Start(); 
                    } 
                }

				// All ok
                return true; 
            } 
            catch (Exception oBug) 
			{ 
                isRunning = false; 
                lastException = oBug; 
                return false; 
            } 
            finally 
			{ 
                Monitor.Exit(this); 
            } 
        } 
        
        public void StopThreadPool() 
		{ 
			// Lock this object
            Monitor.Enter(this);
 
			// We're not running
            isRunning = false; 

			// See if we need to pause before shutting down
            Thread.Sleep((int)(1000 * shutdownPauseSeconds)); 

			// If we have a shutdown pause, abort each thread
			//if (shutdownPauseSeconds > 0) 
			{ 
				// Get an enumerator to each of our threads
                IDictionaryEnumerator dict = hashThreads.GetEnumerator(); 

				// Abort each of the threads that we have running
                while (dict.MoveNext()) 
				{ 
					// Get the thread
                    Thread th = (Thread) dict.Value; 

					// See if it's alive
                    if (th.IsAlive) 
                    { 
                        try 
						{ 
							// Thread alive, abort it
                            th.Abort(); 
                        } 
                        catch (Exception ex)
						{ 
							// This could be a thread exception
                        } 
                    } 
                } 
            }

			// Clear the collection of threads
            hashThreads.Clear(); 
            queueInput.Clear(); 
			queueOutput.Clear();

			// Unlock this object
            Monitor.Exit(this); 
        } 
        
        public int ThreadCount 
		{ 
			get
			{
				// Lock this object
				Monitor.Enter(this); 

                int nCount = hashThreads.Count; 

                // Release this object
				Monitor.Exit(this); 

				return nCount; 
			}
        } 
    } 
} 


using System;

using HBOS.FS.AMP.Entities;

namespace Grid_Demo
{
    /// <summary>
    /// Summary description for Fund.
    /// </summary>
    public class Fund : IEntityBase
    {
        #region Public Enumerations
        
        /// <summary>
        /// List of possible fund statuses
        /// </summary>
        public enum FundStatusType
        {
            Missing = 0 , 
            FirstLevelAuthorised = 1 , 
            FailedAMTolerance = 2 , 
            WarningAMTolerance = 4 ,
            FailedPriceTolerance = 8 ,
            WarningPriceTolerance = 16 ,
            SecondLevelAuthorised = 32 ,
            Released = 64
        };

        #endregion

        #region Private variable declaration
        //
        // Data fields
        //
        private string m_hiPortfolioCode;   
        private string m_fullName;
        private string m_shortName;
        private string m_isinCode;
        private string m_mexCode;
        private string m_policyAdministrationFundCode;
        private string m_sedolNumber;
        private decimal m_upperTolerance;
        private decimal m_lowerTolerance;
        private bool m_priceIncreaseOnly;
        private decimal m_xFactor;
        private decimal m_scalingFactor;
        private decimal m_price;
        private bool m_priceSet;
        private decimal m_previousPrice;
        private bool m_previousPriceSet;
        private FundStatusType m_fundStatus;
        private bool m_fundStatusSet;
        private DateTime m_statusChangedTime;
        private bool m_statusChangedTimeSet;
        private decimal m_predictedPrice;
        private bool m_predictedPriceSet;

        // TODO: Add the various factors
 
        // Flags for IEntityBase implementation
        private bool m_isDirty;
        private bool m_isNew;
        private bool m_isDeleted;
        private byte[] m_timeStamp;

        #endregion

        #region Constructors
        
        /// <summary>
        /// Fund object constructor used to create an empty fund object.
        /// </summary>
        public Fund()
        {
            this.m_hiPortfolioCode = string.Empty;
            this.m_fullName = string.Empty;
            this.m_shortName = string.Empty;
            this.m_isinCode = string.Empty;
            this.m_mexCode = string.Empty;
            this.m_policyAdministrationFundCode = string.Empty;
            this.m_sedolNumber = string.Empty;
            this.m_upperTolerance = 0;
            this.m_lowerTolerance = 0;
            this.m_priceIncreaseOnly = false;
            this.m_xFactor = 1;
            this.m_scalingFactor = 1;
            this.m_price = 0;
            this.m_priceSet = false;
            this.m_previousPrice = 0;
            this.m_previousPriceSet = false;
            this.m_fundStatus = FundStatusType.Missing;
            this.m_fundStatusSet = false;
            this.m_statusChangedTimeSet = false;
            this.m_predictedPrice = 0;
            this.m_predictedPriceSet = false;

            
            //Set up IEntityBase members
            m_isNew = true;
            m_isDeleted = false;
            m_timeStamp = new byte[1];
            m_isDirty = true;

            this.calculatePredictedPrice();
        }


        #endregion Constructor

        #region Fund properties

        /// <summary>
        /// Hi port fund code
        /// </summary>
        public string HiPortfolioCode
        {
            get { return m_hiPortfolioCode; }
            set 
            { 
                m_hiPortfolioCode = value; 
                this.setDirtyFlag();
            }
        }

        /// <summary>
        /// Full name of fund
        /// </summary>
        public string FullName
        {
            get { return this.m_fullName; }
            set 
            { 
                this.m_fullName = value; 
                this.setDirtyFlag();
            }
        }

        /// <summary>
        /// Short name of fund
        /// </summary>
        public string ShortName
        {
            get { return this.m_shortName; }
            set 
            { 
                this.m_shortName = value; 
                this.setDirtyFlag();
            }
        }

        /// <summary>
        /// The ISIN code.
        /// </summary>
        public string IsinCode
        {
            get
            {
                return this.m_isinCode;
            }
            set
            {
                this.m_isinCode = value;
                this.setDirtyFlag();
            }
        }

        /// <summary>
        /// The Mex code.
        /// </summary>
        public string MexCode
        {
            get
            {
                return this.m_mexCode;
            }
            set
            {
                this.m_mexCode = value;
                this.setDirtyFlag();
            }
        }

        /// <summary>
        /// The policy administration fund code.
        /// </summary>
        public string PolicyAdministrationFundCode
        {
            get
            {
                return this.m_policyAdministrationFundCode;
            }
            set
            {
                this.m_policyAdministrationFundCode = value;
                this.setDirtyFlag();
            }
        }

        /// <summary>
        /// The SEDOL number.
        /// </summary>
        public string SedolNumber
        {
            get
            {
                return this.m_sedolNumber;
            }
            set
            {
                this.m_sedolNumber = value;
                this.setDirtyFlag();
            }
        }

        /// <summary>
        /// The upper limit of price movement tolerance for this fund.
        /// </summary>
        public decimal UpperTolerance
        {
            get
            {
                return this.m_upperTolerance;
            }
            set
            {
                this.m_upperTolerance = value;
                this.setDirtyFlag();
                this.calculatePredictedPrice();
            }
        }

        /// <summary>
        /// The lower limit of price movement tolerance for this fund.
        /// </summary>
        public decimal LowerTolerance
        {
            get
            {
                return this.m_lowerTolerance;
            }
            set
            {
                this.m_lowerTolerance = value;
                this.setDirtyFlag();
                this.calculatePredictedPrice();
            }
        }

        /// <summary>
        /// Flag indicating whether the fund price is allowed to fall or not.
        /// </summary>
        public bool PriceIncreaseOnly
        {
            get
            {
                return this.m_priceIncreaseOnly;
            }
            set
            {
                this.m_priceIncreaseOnly = value;
                this.setDirtyFlag();
                this.calculatePredictedPrice();
            }
        }

        /// <summary>
        /// The X factor to use for this fund in price prediction calculations.
        /// </summary>
        public decimal XFactor
        {
            get
            {
                return this.m_xFactor;
            }
            set
            {
                this.m_xFactor = value;
                this.setDirtyFlag();
                this.calculatePredictedPrice();
            }
        }

        /// <summary>
        /// The scale factor used for this fund in price prediction calculations.
        /// </summary>
        public decimal ScalingFactor
        {
            get
            {
                return this.m_scalingFactor;
            }
            set
            {
                this.m_scalingFactor = value;
                this.setDirtyFlag();
                this.calculatePredictedPrice();
            }
        }


        /// <summary>
        /// The current imported price of the fund.
        /// </summary>
        public decimal Price
        {
            get
            {
                return m_price;
            }

            set
            {
                m_price = value;
                this.calculatePredictedPrice();
            }
        }

        /// <summary>
        /// Flag indicating whether the price field has been set or not as zero may be a valid value.
        /// </summary>
        public bool PriceSet
        {
            get
            {
                return this.m_priceSet;
            }

            set
            {
                this.m_priceSet = value;
            }
        }

        /// <summary>
        /// The predicted price for the current day.
        /// </summary>
        public decimal PredictedPrice
        {
            get
            {
                return this.m_predictedPrice;
            }
        }

        /// <summary>
        /// Flag indicating whether the predicted price has been calculated and so holds a valid 
        /// value or not.
        /// </summary>
        public bool PredictedPriceSet
        {
            get
            {
                return this.m_predictedPriceSet;
            }
        }

        /// <summary>
        /// The most recent previous price for the fund.
        /// </summary>
        public decimal PreviousPrice
        {
            get
            {
                return this.m_previousPrice;
            }

            set
            {
                this.m_previousPrice = value;
                this.calculatePredictedPrice();
            }
        }

        /// <summary>
        /// Flag indicating whether the previous price field has been set or not as zero may be a 
        /// valid value.
        /// </summary>
        public bool PreviousPriceSet
        {
            get
            {
                return this.m_previousPriceSet;
            }

            set
            {
                this.m_previousPriceSet = value;
            }
        }

        /// <summary>
        /// Current fund status
        /// </summary>
        public FundStatusType FundStatus
        {
            get 
            {
                return this.m_fundStatus;
            }
            
            set 
            {
                this.m_fundStatus = value;
            }
        }

        /// <summary>
        /// Flag indicating whether the fund status has been set.
        /// </summary>
        public bool FundStatusSet
        {
            get
            {
                return this.m_fundStatusSet;
            }

            set
            {
                this.m_fundStatusSet = value;
            }
        }
        
        /// <summary>
        /// The date and time that the status was changed (excluding tolerance status changes).
        /// </summary>
        public DateTime StatusChangedTime
        {
            get
            {
                return this.m_statusChangedTime;
            }
			
            set
            {
                this.m_statusChangedTime = value;
            }
        }

        /// <summary>
        /// Flag to indicate whether the StatusChangedTime has been set because it defaults to a 
        /// valid date.
        /// </summary>
        public bool StatusChangedTimeSet
        {
            get
            {
                return this.m_statusChangedTimeSet;
            }

            set
            {
                this.m_statusChangedTimeSet = value;
            }
        }

        /// <summary>
        /// Flag indicating that values have been modified
        /// </summary>
        public bool IsDirty
        {
            get { return this.m_isDirty; }
            set { this.m_isDirty = value; }
        }

        /// <summary>
        /// Returns true if the object is a newly created object being added to the database.
        /// </summary>
        public bool IsNew
        {
            get	{ return m_isNew; }
            set	{ m_isNew = value; }
        }

        /// <summary>
        /// Returns true if the object has been flagged as deleted.  Set to false if the
        /// data has been un-deleted
        /// </summary>
        public bool IsDeleted
        {
            get 
            {
                return m_isDeleted;
            } 

            set 
            {
                m_isDeleted = value;
            }
        }

        /// <summary>
        /// Returns the timestamp of the data 
        /// </summary>
        public byte[] TimeStamp
        {
            get
            {
                return m_timeStamp;
            }

            set
            {
                m_timeStamp = value;
            }
        }

        #endregion Fund properties

        #region Methods

        /// <summary>
        /// Set the modified flag to be true.
        /// </summary>
        private void setDirtyFlag()
        {
            //
            // TODO: Possibly check for initial loading and populating code here
            //
            this.m_isDirty = true;
        }

        /// <summary>
        /// Calculate the predicted price for the fund.
        /// </summary>
        private void calculatePredictedPrice()
        {
        }

        #endregion
    }
}

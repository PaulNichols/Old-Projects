using System;
using HBOS.FS.AMP.UPD.Types.FundGroups;

namespace HBOS.FS.AMP.UPD.DataAccess
{
	/// <summary>
	/// Dummy DB for fund groups
	/// </summary>
	public class TempDBFundGroupCollection
	{
		#region Properties
		
		FundGroupCollection m_TempFundGroupCollection;
		private static TempDBFundGroupCollection m_Instance;

		#endregion Properties
		
		#region Constructors

		/// <summary>
		/// Allows internal calling whilst not exposing to the client
		/// </summary>
		private TempDBFundGroupCollection()
		{
			m_TempFundGroupCollection = new FundGroupCollection();
			FundGroup currentFundGroup;

			//create some Asset Fund Groups via factory
			for (int i=0; i<8; i++)
			{
				currentFundGroup = FundGroupFactory.CreateFundGroup(i, "HIFM", FundGroupFactory.FundGroupTypes.Asset,
					"Asset Fund Group " + i,"Fund " + i, new byte[1]);
				m_TempFundGroupCollection.Add(currentFundGroup);
			}

			//create some Individual Fund Groups via factory
			for (int i=0; i<8; i++)
			{
				currentFundGroup = FundGroupFactory.CreateFundGroup(i+8, "HIFM", FundGroupFactory.FundGroupTypes.Individual,
					"Individual Fund Group " + i, "Fund " + i, new byte[1]);
				m_TempFundGroupCollection.Add(currentFundGroup);
			}
		}
		
		/// <summary>
		/// Make sure only one instance of the object will be instantiated 
		/// </summary>
		/// <returns></returns>
		public static TempDBFundGroupCollection GetInstance()
		{
			if (m_Instance == null)
			{
				m_Instance = new TempDBFundGroupCollection();
			}
			return m_Instance;
		}

		#endregion Constructors

		#region Methods

		/// <summary>
		/// Get a collection of FundGroupCollection
		/// </summary>
		/// <returns></returns>
		public FundGroupCollection GetFundGroupCollection()
		{
			return m_TempFundGroupCollection;
		}

		/// <summary>
		/// Persist the FundGroupCollection back to the DB
		/// </summary>
		/// <param name="fund groups"></param>
		public void SaveFundGroupCollection(FundGroupCollection FundGroupCollection)
		{
			m_TempFundGroupCollection = FundGroupCollection;
		}

		#endregion Methods
	}
}
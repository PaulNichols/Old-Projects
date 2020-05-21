using System;
using HBOS.FS.AMP.UPD.Types.AssetFunds;
using HBOS.FS.AMP.UPD.Types.Currency;
using HBOS.FS.AMP.UPD.Types.Factors;
using HBOS.FS.AMP.UPD.Types.Funds;
using NUnit.Framework;
using UT_HBOS.FS.AMP.UPD.Types.AssetFunds;

namespace UT_HBOS.FS.AMP.UPD.Types.Funds
{
	/// <summary>
	/// Base class for tests on fund calculations
	/// </summary>
	public abstract class FundCalculationTestBase
	{
		#region Initialisation

		/// <summary>
		/// Set up the fund that we'll be testing
		/// </summary>
		[SetUp]
		public virtual void SetUp()
		{

			FactorCollection fc=new FactorCollection();
			Factor[] f =new Factor[4];
			//		XFactor
			f[0]=new XFactor(0m,1,DateTime.Today,"description",new byte[0]);
		
			//		RevaluationFactor
			f[1]=new RevaluationFactor(0m,1,DateTime.Today,DateTime.Today,new byte[0]);
			//		TaxProvisionEstimate or
			f[2]=new TaxProvisionEstimate(0m,1,DateTime.Today,new byte[0]);
			//		ScalingFactor
			f[3]=new ScalingFactor(0m,1,DateTime.Today,new byte[0]);
			fc.AddRange(f);

			m_subject = MockFund.CreateInitialisedFund(fc);


			subject.PreviousPrice = 0m;

			subject.ValuationBasisEffect = 0m;
		//	subject.ScaleFactor = 0m;
		//	subject.XFactor = 0m;
		//	subject.RevalFactor = 0m;
		}

		#endregion Initialisation		

		#region ProtectedMethodsAndProperties

		// The fund we're testing
		private LinkedFund m_subject;

		/// <summary>
		/// The Fund entity that is the subject of the test(read-only) 
		/// </summary>
		protected LinkedFund subject
		{
			get { return m_subject; }
		}


		/// <summary>
		/// Makes the fund calculate itself.
		/// </summary>
		protected void makeFundCalculate()
		{
			subject.ParentAssetFund = subject.ParentAssetFund;
		}

		/// <summary>
		/// Assigns an asset fund to the fund.
		/// </summary>
		protected void assignAssetFund()
		{
			AssetMovementConstituentCollection parts = new AssetMovementConstituentCollection();

			HBOS.FS.AMP.UPD.Types.StockMarketIndex.StockMarketIndex benchMark=new HBOS.FS.AMP.UPD.Types.StockMarketIndex.StockMarketIndex(
				0,"test",0m,0m,new Currency("test",0m,0m));

			AssetMovementConstituent constituent1 = new AssetMovementConstituent(1.0m,benchMark);

			parts.Add(constituent1);
			
			subject.ParentAssetFund = AssetFundAssetMovementCalculationTests.CreateAssetFundWithBenchmarks(parts,null);
		}

		#endregion ProtectedMethodsAndProperties
	}
}
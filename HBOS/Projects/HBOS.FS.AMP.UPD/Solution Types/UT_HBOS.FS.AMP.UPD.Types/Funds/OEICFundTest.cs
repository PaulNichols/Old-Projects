using HBOS.FS.AMP.UPD.Types.AssetFunds;
using HBOS.FS.AMP.UPD.Types.Factors;
using HBOS.FS.AMP.UPD.Types.Funds;
using NUnit.Framework;
using UT_HBOS.FS.AMP.UPD.Types.AssetFunds;

namespace UT_HBOS.FS.AMP.UPD.Types.Funds
{
	/// <summary>
	/// Unit tests for OEICFund entity
	/// </summary>
	[TestFixture]
	public class OEICFund
	{
		/// <summary>
		/// Default constructor required for NUnit
		/// </summary>
		public OEICFund()
		{
		}

		private HBOS.FS.AMP.UPD.Types.Funds.OEICFund subject = null;
		private AssetFund assetFund;

		/// <summary>
		/// NUnit setup to create an initialised OEICFund
		/// </summary>
		[SetUp]
		public void SetUp()
		{
			// Create the asset fund already tested, which predicts an asset movement of 15%          
			assetFund = AssetFundAssetMovementCalculationTests.CreateAssetFundWithBenchmarks(AssetFundAssetMovementCalculationTests.CreateSingleBenchMark(),null);

			// Create an OEIC fund that has increased in price by 5% from yesterday.

			//TODO - is linked fund not an oeic fund!!

			subject= (HBOS.FS.AMP.UPD.Types.Funds.OEICFund) FundFactory.CreateFund(
				FundFactory.FundType.Oeic,
				"Hi3Code",
				"FullName",
				"ShortName",              
				"ClassOrSeriesCode",
				true, // useMidPriceAsBidPrice
				true, // onHiPortfolio3
				false,       
				assetFund,             
				0,      
				0,              
				0,              
				false, // priceIncreaseOnly
				1, // tolerancesID
				105.0m, // price - this is the "imported hi3" price
				true, // priceSet
				1, // importedFundPriceID
				0.05M, //valuationBasisEffect
				100.0m, // previousPrice
				true, //previousPriceSet
				Fund.FundStatusType.Imported, // fundStatus
				System.DateTime.Now, // statusChangedTime
				true, // statusChangedTimeSet
				false, //  usePredictedPrice
				new byte[1], // authorisedPriceTimestamp
				new byte[1], // timestamp
				false, //is life
				true, // isExDividend
				"EQAME",
				"companycode",
				"securitycode",new FactorCollection(),
                true
				);      
			/* TODO				
				(LinkedFund) FundFactory.CreateFund(
				FundFactory.FundType.Linked,
				"Hi3Code",
				"FullName",
				"ShortName",
				//null,
				"ClassOrSeriesCode",
				true, // useMidPriceAsBidPrice
				true, // onHiPortfolio3
				false, // PriceType??
				assetFund, // parentAssetFund
				0.0m, // taxProvision
				1, // taxProvisionFactorID
				true, // taxProvisionFactorIDSet
				DateTime.Now, //tpe eff date
				true, //tpe eff date set
				System.DateTime.Now, // revaluationEffectiveDate
				true, // revaluationEffectiveDateset
				7.5M, //revaluationRatioValue
				DateTime.Now, // revaluationEndDate
				true, // revaluationEndDateSet
				1, // revaluationFactorID
				true, // revaluationFactorIDSet
				0.0m, // scalingFactor
				1, // scalingFactorID
				true, // scalingFactorIDSet
				DateTime.Now.Date, //scaling factor effective date (create date)
				true, //scaling factor eff date set
				0.0m, // xFactor
				1, // xFactorID
				true, // xFactorIDSet
				DateTime.Now.Date, //X Factor eff date
				true, //X Factor eff date set
				String.Empty, //X Factor desc
				0.0m, // assetMovementTolerance
				0.0m, // upperTolerance
				0.0m, // lowerTolerance
				false, // priceIncreaseOnly
				1, // tolerancesID
				true, // tolerancesIDSet
				105.0m, // price - this is the "imported hi3" price
				true, // priceSet
				1, // importedFundPriceID
				true, // importedFundPriceIDSet
				0.05M, //valuationBasisEffect
				true, //valuationBasisEffectSet
				1, //valuationBasisID
				true,  //valuationBasisIDSet
				100.0m, // previousPrice
				true, //previousPriceSet
				Fund.FundStatusType.Imported, // fundStatus
				System.DateTime.Now, // statusChangedTime
				true, // statusChangedTimeSet
				false, //  usePredictedPrice
				new byte[1], // authorisedPriceTimestamp
				new byte[1], // timestamp
				true, // isExDividend
				new byte[1], // OEICTimestamp
				false, //is life
				false, //is initial units
				new byte[1], // RevaluationFactor timestamp
				new byte[1], // ScalingFactor timestamp
				new byte[1], // TaxProvisionFactor timestamp
				new byte[1], // XFactorTs timestamp
                "EQAME",
				null,
				null
				);
				*/
		}

		/// <summary>
		/// Test that IEntityBase is initialised.
		/// </summary>
		[Test]
		public void IEntityBaseIsInitialised()
		{
			UnitTestHelpers.OverloadedConstructorIEntityTest(subject);
		}

		/// <summary>
		/// Test the AssetUnitPrice property
		/// </summary>
		[Test]
		public void AssetUnitPrice()
		{
			decimal assetUnitPrice = assetFund.UnitPrice;
			Assert.AreEqual(assetUnitPrice, subject.AssetUnitPrice);
		}

		/// <summary>
		/// Test the AssetMovement property
		/// </summary>
		[Test]
		public void AssetMovement()
		{
			decimal assetMovement = assetFund.UnitPriceMovement;
			Assert.AreEqual(assetMovement, subject.AssetMovement);
		}

		/// <summary>
		/// Test the PredictedAssetMovement property
		/// </summary>
		[Test]
		public void PredictedAssetMovement()
		{
			decimal predictedAssetMovement = assetFund.PredictedAssetMovement;
			Assert.AreEqual(predictedAssetMovement, subject.PredictedAssetMovement);
		}


		/// <summary>
		/// Test the prediction based on asset only.
		/// </summary>
		[Test]
		public void PredictedMovementBasedOnAssetOnly()
		{
			// We have no other contributing factors to the predicted price, so the predicted price movement
			// should be made up entirely of the predicted asset movement
			decimal predictedPriceMovementPercent = assetFund.PredictedAssetMovement;
			Assert.AreEqual(predictedPriceMovementPercent, subject.PredictedPriceMovementPercent);
		}

		/// <summary>
		/// Test the prediction based on X factor only.
		/// </summary>
		[Test]
		public void PredictedMovementWithXFactor()
		{
			// Add an XFactor of 2%
			decimal xFactor = 0.02m;
			subject.XFactor = xFactor;
			Assert.AreEqual(xFactor, subject.XFactor, "X Factor not set");

			// Check that our predicted price movement has increased by 2%
			decimal predictedPriceMovementPercent = xFactor + assetFund.PredictedAssetMovement;
			Assert.AreEqual(predictedPriceMovementPercent, subject.PredictedPriceMovementPercent, "Incorrect movement calculatation");
		}

//		/// <summary>
//		/// Test the prediction based on scaling only.
//		/// </summary>
//		[Test]
//		public void PredictedMovementWithScaling()
//		{
//			// Add a Scaling Factor of -3%
//			decimal scalingFactor = -0.03m;
//			subject.ScaleFactor = scalingFactor;
//			Assert.AreEqual(scalingFactor, subject.ScaleFactor, "Scaling factor not set");
//
//			// Check that our predicted price movement has decreased by 3%
//			decimal predictedPriceMovementPercent = scalingFactor + assetFund.PredictedAssetMovement;
//			Assert.AreEqual(predictedPriceMovementPercent, subject.PredictedPriceMovementPercent);
//		}
//
//		/// <summary>
//		/// Test the prediction based on multiple factor.
//		/// </summary>
//		[Test]
//		public void PredictedMovementWithMultipleFactors()
//		{
//			decimal xFactor = 0.02m;
//			subject.XFactor = xFactor;
//			decimal scalingFactor = -0.03m;
//			subject.ScaleFactor = scalingFactor;
//
//			decimal predictedPriceMovementPercent = xFactor + scalingFactor + assetFund.PredictedAssetMovement;
//			Assert.AreEqual(predictedPriceMovementPercent, subject.PredictedPriceMovementPercent);
//		}

	}
}
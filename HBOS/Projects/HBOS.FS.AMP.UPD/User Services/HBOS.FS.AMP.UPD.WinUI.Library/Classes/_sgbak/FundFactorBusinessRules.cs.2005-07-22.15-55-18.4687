using System;

using HBOS.FS.AMP.UPD.Types.Funds;

namespace HBOS.FS.AMP.UPD.WinUI.Classes
{
	/// <summary>
	/// Provides an encapsulation of the business rules governing fund
	/// factors
	/// </summary>
	public abstract class FundFactorBusinessRules
	{
        /// <summary>
        /// Encapsulates fund business rules on XFactors
        /// </summary>
        /// <param name="fundToCheck">The fund to check</param>
        /// <returns>True if the supplied fund can set the XFactor, otherwise false</returns>
        public static bool CanSetXFactor(Fund fundToCheck)
        {
            // There should be no limitation on XFactor
            return true;
        }

        /// <summary>
        /// Encapsulates fund business rules on Tax Provision Estimate factor
        /// </summary>
        /// <param name="fundToCheck">The fund to check</param>
        /// <returns>True if the supplied fund can set the Tax Provision Estimate, otherwise false</returns>
        public static bool CanSetTaxProvisionEstimate(Fund fundToCheck)
        {
            // Not available for OEICs
            if (fundToCheck is OEICFund)
            {
                return false;
            }
            
                // only available to life-type linked funds
            else if (fundToCheck is NonOEIC)
            {
                NonOEIC fund = (NonOEIC)fundToCheck;
            
                if (fund.IsLife)
                {   
                    return true;
                }
                else    
                {
                    return false;                    
                }
            }
            
            // ... it's an unknown fund type, and we could throw an exception
            else
            {
                throw new InvalidOperationException("The Fund Factor Business Rules class does not recognise the fund type : " + fundToCheck.GetType());
            }
        }

        /// <summary>
        /// Encapsulates fund business rules on Revaluation factor
        /// </summary>
        /// <param name="fundToCheck">The fund to check</param>
        /// <returns>True if the supplied fund can set the Revaluation factor, otherwise false</returns>
        public static bool CanSetRevaluationFactor(Fund fundToCheck)
        {
            // Not available for OEICs
            if (fundToCheck is OEICFund)
            {
                return false;
            }
            
            // Available to all linked or composite funds
            else
            {
                return true;                    
            }
        }

        /// <summary>
        /// Encapsulates fund business rules on Scaling factor
        /// </summary>
        /// <param name="fundToCheck">The fund to check</param>
        /// <returns>True if the supplied fund can set the Scaling factor, otherwise false</returns>
        public static bool CanSetScalingFactor(Fund fundToCheck)
        {
            // Not available for OEICs
            if (fundToCheck is OEICFund)
            {
                return false;
            }
            else
            {
                return true;                    
            }


        }

	}
}

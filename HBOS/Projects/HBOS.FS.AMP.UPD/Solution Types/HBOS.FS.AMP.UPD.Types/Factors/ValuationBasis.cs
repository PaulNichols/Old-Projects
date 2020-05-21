using System;

namespace HBOS.FS.AMP.UPD.Types.Factors
{
	/// <summary>
	/// Summary description for ValuationBasis.
	/// </summary>
	public class ValuationBasis : Factor
	{

        #region Constructor

		/// <summary>
		/// Default constructor
		/// </summary>
        public ValuationBasis() : base()
		{
		}

        /// <summary>
        /// Constructor that initialises member variables without setting dirty flag and also initialises factors
        /// </summary>
        /// <param name="ratioValue"></param>
        public ValuationBasis (decimal ratioValue)
            : base (ratioValue)
        {
        }

        #endregion

		/// <summary>
		/// Used in Fund status property Grid, maybe ToString 
		/// could have been overriden but it looked like that 
		/// was used elsewhere for a different reason?
		/// </summary>
		public override string DisplayName 
		{
			get
			{
				return "Valuation Factor";
			}
		}	

    }
}

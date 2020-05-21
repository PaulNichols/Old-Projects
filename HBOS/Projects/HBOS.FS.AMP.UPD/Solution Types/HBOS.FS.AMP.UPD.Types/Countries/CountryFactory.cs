using System;

namespace HBOS.FS.AMP.UPD.Types.Countries
{
	/// <summary>
	/// Create a country object.
	/// </summary>
	public class CountryFactory
	{
		/// <summary>
		/// Creates a new <see cref="CountryFactory"/> instance.
		/// </summary>
		public CountryFactory()
		{
		}

        /// <summary>
        /// Get the correct country (only 1 for now, but a factory added
        /// for future-proofing)
        /// </summary>
        /// <returns>The country object of the correct type</returns>
        public static Country CreateCountry()
        {
            return new Country();
        }

        /// <summary>
        /// Get a populated country object
        /// </summary>
        /// <param name="countryCode">Three character country code</param>
        /// <param name="countryName">Name of the country</param>
        /// <returns>Populated country object</returns>
        public static Country CreateCountry(string countryCode, string countryName)
        {
            return new Country(countryCode, countryName);
        }
	}
}

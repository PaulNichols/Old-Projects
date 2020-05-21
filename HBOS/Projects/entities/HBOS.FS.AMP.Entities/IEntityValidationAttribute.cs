using System;

namespace HBOS.FS.AMP.Entities
{
    /// <summary>
    /// Interface to be implemented by any custom entity attributes that wish to work with
    /// the entity validator
    /// </summary>
    public interface IEntityValidationAttribute
    {
        /// <summary>
        /// Return true if the c
        /// </summary>
        /// <param name="valueToValidate"></param>
        /// <returns></returns>
        bool IsValid(object valueToValidate);
        string InvalidDescription{ get;}   
    }
}

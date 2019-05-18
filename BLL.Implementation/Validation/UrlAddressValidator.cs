using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interface.Validation;
using BLL.Implementation.Exception;

namespace BLL.Implementation.Validation
{
    /// <summary>
    /// Validator for url address.
    /// </summary>
    public class UrlAddressValidator : IValidator<string>
    {
        /// <summary>
        /// Validates given string.
        /// </summary>
        /// <param name="item">Given string.</param>
        /// <returns>True if url string is valid, else - false.</returns>
        public bool IsValid(string item)
        {
            if (item == null)
            {
                throw new ArgumentNullException($"Url string is null {nameof(item)}");
            }
            
            if (item == string.Empty)
            {
                throw new ArgumentException($"Url string is empty {nameof(item)}");
            }

            return Uri.TryCreate(item, UriKind.Absolute, out _); 
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JewelryStore.API.Exceptions
{
    /// <summary>
    /// Excpetion for Discount percentage
    /// </summary>
    public class InvalidDiscountPercentageException : Exception
    {
        public InvalidDiscountPercentageException() : base("Discount percentage should be in the range of 0 to 100")
        {

        }
    }
}

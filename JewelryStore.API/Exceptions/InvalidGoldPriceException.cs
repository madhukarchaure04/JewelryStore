using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JewelryStore.API.Exceptions
{
    /// <summary>
    /// Excpetion for Gold Price
    /// </summary>
    public class InvalidGoldPriceException : Exception
    {
        public InvalidGoldPriceException() : base("Gold price is invalid")
        {

        }
    }
}

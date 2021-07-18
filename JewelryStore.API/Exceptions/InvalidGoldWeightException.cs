using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JewelryStore.API.Exceptions
{
    /// <summary>
    /// Excpetion for Gold Weight
    /// </summary>
    public class InvalidGoldWeightException : Exception
    {
        public InvalidGoldWeightException() : base("Gold weight is invalid")
        {

        }
    }
}

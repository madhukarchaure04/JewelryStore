using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JewelryStore.API.Entities
{
    /// <summary>
    /// Used to reslve the dependency for IPrinterService as it has multiple registered implementation
    /// </summary>
    public enum PrintType
    {
        File,
        Paper
    }
}

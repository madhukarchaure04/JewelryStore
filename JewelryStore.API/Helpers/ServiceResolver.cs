using JewelryStore.API.Entities;
using JewelryStore.API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JewelryStore.API.Helpers
{
    /// <summary>
    /// Used to reslve the dependency for IPrinterService as it has multiple registered implementation
    /// </summary>
    /// <param name="key">Used to differentiate the IPrinterService dependencies</param>
    /// <returns></returns>
    public delegate IPrintService ServiceResolver(PrintType key);
}

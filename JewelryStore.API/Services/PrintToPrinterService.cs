using JewelryStore.API.DBModels;
using JewelryStore.API.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JewelryStore.API.Services
{
    /// <summary>
    /// Concrete implementation of IPrinterService for printing the data to paper
    /// </summary>
    public class PrintToPrinterService : IPrintService
    {
        /// <summary>
        /// Returns the byte data that can be printed on paper
        /// </summary>
        /// <param name="item">Item data to print</param>
        /// <param name="user">User data to print</param>
        /// <returns></returns>
        public byte[] Print(Item item, User user)
        {
            throw new System.NotImplementedException();
        }
    }
}

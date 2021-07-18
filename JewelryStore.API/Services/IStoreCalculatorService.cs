using JewelryStore.API.DBModels;
using JewelryStore.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JewelryStore.API.Services
{
    /// <summary>
    /// Interface defining the template required for StoreCalculatorService
    /// </summary>
    public interface IStoreCalculatorService
    {
        double CalculateTotalPrice(Item item, User user);
    }
}

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
    /// Interface defining the template required for PrintService
    /// </summary>
    public interface IPrintService
    {
        byte[] Print(Item item, User user);
    }
}

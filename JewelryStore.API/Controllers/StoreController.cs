using JewelryStore.API.Authorization;
using JewelryStore.API.DBModels;
using JewelryStore.API.Entities;
using JewelryStore.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace JewelryStore.API.Controllers
{
    /// <summary>
    /// All the Jewelry related operations will be performed here
    /// </summary>
    [Authorize]
    [ApiController]
    public class StoreController : ControllerBase
    {
        private readonly IStoreCalculatorService calculatorService;
        /// <summary>
        /// Constructor to inject the dependency for concrete implementation of Store Calculator functionality
        /// </summary>
        /// <param name="calculatorService">Service having concrete implementation for Store Calculator functionality</param>
        public StoreController(IStoreCalculatorService calculatorService)
        {
            this.calculatorService = calculatorService;
        }

        /// <summary>
        /// Method to calculate the total price of the item
        /// </summary>
        /// <param name="item">Item details to calculate the total price</param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/[controller]/calculate")]
        public IActionResult Calculate(Item item)
        {
            //Fetching the user to check if the discount is applicable or not
            var user = (User)HttpContext.Items["User"];

            try
            {
                //Calculating the total price using store calculator service
                item.TotalPrice = calculatorService.CalculateTotalPrice(item, user);
                return Ok(item);
            }
            catch (Exception ex)
            {
                //If exception occurs, returning it as a bad request
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}

using JewelryStore.API.Authorization;
using JewelryStore.API.DBModels;
using JewelryStore.API.Entities;
using JewelryStore.API.Helpers;
using JewelryStore.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JewelryStore.API.Controllers
{
    /// <summary>
    /// Controller implements REST API calls for print operation
    /// </summary>
    [Authorize]
    [ApiController]
    public class PrintController : ControllerBase
    {
        private readonly IPrintService printToFile;
        private readonly IPrintService printToPaper;
        /// <summary>
        /// Constructor to inject the dependency for concrete implementation of Print functionality
        /// </summary>
        /// <param name="resolver">Used to resolve the dependency as there are more than one implementations and registrations for IPrintService</param>
        public PrintController(ServiceResolver resolver)
        {
            //For printing the data to file 
            printToFile = resolver(Entities.PrintType.File);
            //For printing the data to paper
            printToPaper = resolver(Entities.PrintType.Paper);
        }

        /// <summary>
        /// Prints the item data to PDF file
        /// </summary>
        /// <param name="item">Data that needs to be printed</param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/[controller]/printtofile")]
        public IActionResult PrintToFile(Item item)
        {
            try
            {
                //Fetching user to print on file
                var user = (User)HttpContext.Items["User"];
                //Getting the bytes for PDF file and returing the file
                var pdf = printToFile.Print(item, user);
                return File(pdf, "application/octet-stream", "JewelryStoreEnquiry.pdf");
            }
            catch(Exception ex)
            {
                //If exception occurs, returning it as a bad request
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        ///  Prints the item data to Paper
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/[controller]/printtopaper")]
        public IActionResult PrintToPaper(Item item)
        {
            try
            {
                //Fetching user to print on Paper
                var user = (User)HttpContext.Items["User"];
                //Priting the data to paper
                printToPaper.Print(item, user);
                return Ok();
            }
            catch (Exception ex)
            {
                //If exception occurs, returning it as a bad request
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}

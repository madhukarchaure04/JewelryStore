using DinkToPdf.Contracts;
using JewelryStore.API.DBModels;
using JewelryStore.API.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DinkToPdf;

namespace JewelryStore.API.Services
{
    /// <summary>
    /// Concrete implementation of IPrinterService for printing the data to file
    /// </summary>
    public class PrintToFileService : IPrintService
    {
        private readonly IConverter converter;
        public PrintToFileService(IConverter converter)
        {
            this.converter = converter;
        }
        /// <summary>
        /// Returns the byte data that can be saved as PDF file
        /// </summary>
        /// <param name="item">Item data to print</param>
        /// <param name="user">User data to print</param>
        /// <returns></returns>
        public byte[] Print(Item item, User user)
        {
            //Gets the html to display in file
            var html = GetHtml(item, user);
            //Setting PDF file properties
            GlobalSettings globalSettings = new GlobalSettings();
            globalSettings.ColorMode = ColorMode.Color;
            globalSettings.Orientation = Orientation.Portrait;
            globalSettings.PaperSize = PaperKind.A4;
            globalSettings.Margins = new MarginSettings { Top = 30, Bottom = 30, Left = 30, Right = 30 };
            ObjectSettings objectSettings = new ObjectSettings();
            objectSettings.HtmlContent = html;
            objectSettings.WebSettings = new WebSettings() { DefaultEncoding = "utf-8" };
            HtmlToPdfDocument htmlToPdf = new HtmlToPdfDocument()
            {
                GlobalSettings = globalSettings,
                Objects = { objectSettings }
            };
            return converter.Convert(htmlToPdf);
        }

        /// <summary>
        /// Returns the data in html form
        /// </summary>
        /// <param name="item">Item data to add in html</param>
        /// <param name="user">User data to add in html</param>
        /// <returns></returns>
        private string GetHtml(Item item, User user)
        {
            return $@"
            <!DOCTYPE html>
            <html lang=""en"">
                <head>Gold Price Enquiry</head>
            <body>
                <h3>Hello {user.FirstName + " " + user.LastName} ({user.UserType.ToString()} user)</h3>
                <h4>Below are the details of your enquiry</h4>
                <p>
                    <span>Gold Price (per gram): {item.GoldPricePerGram}</span></br>
                    <span>Weight (grams): {item.WeightInGrams}</span></br>
                    <span>Discount: {(user.UserType == UserType.Privileged ? item.Discount.ToString() + "%" : "Not Applicable")}</span></br>
                    <span>Total Price: {item.TotalPrice}</span></br>
                </p>
                </br>
                </br>
                <h5>Thank you,</h6>
                <h5>Jewelry Store</h6>
            </body>
            </html>
            ";
        }
    }
}

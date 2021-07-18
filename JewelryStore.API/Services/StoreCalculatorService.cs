using JewelryStore.API.DBModels;
using JewelryStore.API.Entities;
using JewelryStore.API.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JewelryStore.API.Services
{
    /// <summary>
    /// Store calculator service defining the calculator operations
    /// </summary>
    public class StoreCalculatorService : IStoreCalculatorService
    {
        /// <summary>
        /// Calculates the total price of the item
        /// </summary>
        /// <param name="item">Details of item required for calculating total price</param>
        /// <param name="user">User deails to decide if the discount is applicable or not</param>
        /// <returns></returns>
        public double CalculateTotalPrice(Item item, User user)
        {
            //Checking if the user has passed correct gold price
            if (item.GoldPricePerGram <= 0)
                throw new InvalidGoldPriceException();
            //Checking if the user has passed correct gold weight
            if (item.WeightInGrams <= 0)
                throw new InvalidGoldWeightException();
            //Checking if the user has passed correct dicount percentage
            if (item.Discount < 0 || item.Discount > 100)
                throw new InvalidDiscountPercentageException();

            //Calculating the total for gold
            double totalPrice = item.GoldPricePerGram * item.WeightInGrams;
            //Discounts for applicable user
            if (user.UserType == UserType.Privileged)
                totalPrice -= ((totalPrice * item.Discount) / 100);

            return totalPrice;
        }
    }
}

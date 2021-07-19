using JewelryStore.API.DBModels;
using JewelryStore.API.Entities;
using JewelryStore.API.Exceptions;
using JewelryStore.API.Services;
using JewelryStore.API.Test.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace JewelryStore.API.Test.Tests
{
    public class StoreCalculatorServiceTest
    {
        private readonly IStoreCalculatorService calculatorService;
        public StoreCalculatorServiceTest()
        {
            calculatorService = new StoreCalculatorService();
        }

        [Fact(DisplayName = "Invalid gold price should throw exception")]
        public void Invalid_gold_price_should_throw_exception()
        {
            User user = MockDB.GetDBContext().Users.FirstOrDefault();
            Item item = new Item()
            {
                GoldPricePerGram = -10,
                WeightInGrams = 10,
                Discount = 5
            };
            Assert.Throws<InvalidGoldPriceException>(() => calculatorService.CalculateTotalPrice(item, user));
        }

        [Fact(DisplayName = "Invalid gold weight should throw exception")]
        public void Invalid_gold_weight_should_throw_exception()
        {
            User user = MockDB.GetDBContext().Users.FirstOrDefault();
            Item item = new Item()
            {
                GoldPricePerGram = 10,
                WeightInGrams = -10,
                Discount = 5
            };
            Assert.Throws<InvalidGoldWeightException>(() => calculatorService.CalculateTotalPrice(item, user));
        }

        [Fact(DisplayName = "Invalid discount should throw exception")]
        public void Invalid_discount_should_throw_exception()
        {
            User user = MockDB.GetDBContext().Users.FirstOrDefault();
            Item item = new Item()
            {
                GoldPricePerGram = 10,
                WeightInGrams = 10,
                Discount = 101
            };
            Assert.Throws<InvalidDiscountPercentageException>(() => calculatorService.CalculateTotalPrice(item, user));
        }

        [Fact(DisplayName = "Regular user should not get discount")]
        public void Regular_user_should_not_get_discount()
        {
            User user = MockDB.GetDBContext().Users.FirstOrDefault(u => u.UserType == UserType.Regular);
            Item item = new Item()
            {
                GoldPricePerGram = 10,
                WeightInGrams = 10,
                Discount = 5
            };
            Assert.Equal<double>(100, calculatorService.CalculateTotalPrice(item, user));
        }

        [Fact(DisplayName = "Privileged user should get discount")]
        public void RPrivileged_user_should_get_discount()
        {
            User user = MockDB.GetDBContext().Users.FirstOrDefault(u => u.UserType == UserType.Privileged);
            Item item = new Item()
            {
                GoldPricePerGram = 10,
                WeightInGrams = 10,
                Discount = 5
            };
            Assert.Equal<double>(95, calculatorService.CalculateTotalPrice(item, user));
        }
    }
}

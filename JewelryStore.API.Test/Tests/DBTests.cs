using JewelryStore.API.Test.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace JewelryStore.API.Test.Tests
{
    public class DBTests
    {
        [Fact(DisplayName = "DB should be successfully initalized")]
        public void DB_should_be_successfully_initalized()
        {
            using (var context = MockDB.GetDBContext())
            {
                Assert.True(context.Users.Count() > 0);
            }
        }

        [Fact(DisplayName = "Username and password should not be null or empty")]
        public void Username_and_password_should_not_be_null_or_empty()
        {
            bool valid = true;
            using (var context = MockDB.GetDBContext())
            {
                foreach (var user in context.Users)
                {
                    if (string.IsNullOrEmpty(user.Username) || string.IsNullOrEmpty(user.Password))
                    {
                        valid = false;
                        break;
                    }
                }
            }
            Assert.True(valid);
        }
    }
}

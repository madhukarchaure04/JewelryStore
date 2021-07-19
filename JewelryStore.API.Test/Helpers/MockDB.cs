using JewelryStore.API.DBModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace JewelryStore.API.Test.Helpers
{
    public class MockDB
    {
        public static DBContext GetDBContext()
        {
            var options = new DbContextOptionsBuilder<DBContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            var context = new DBContext(options);
            Seed(context);
            return context;
        }

        private static void Seed(DBContext context)
        {
            var user1 = new User
            {
                FirstName = "Alice",
                LastName = "S.",
                Username = "Alice",
                UserType = Entities.UserType.Regular,
                Password = "Alice"
            };

            context.Users.Add(user1);

            var user2 = new User
            {
                FirstName = "Bob",
                LastName = "K.",
                Username = "Bob",
                UserType = Entities.UserType.Privileged,
                Password = "Bob"
            };

            context.Users.Add(user2);

            context.SaveChanges();
        }
    }
}

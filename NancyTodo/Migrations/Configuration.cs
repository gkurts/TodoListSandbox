using NancyTodo.Db;
using NancyTodo.Models;

namespace NancyTodo.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<TodoContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(TodoContext context)
        {
            if (!context.TodoUserIdentities.Any(x => x.UserName == "admin"))
            {
                TodoUserIdentity greg = new TodoUserIdentity
                {
                    Created = DateTime.UtcNow,
                    Email = "admin@foo.com",
                    IsAdmin = true,
                    Name = "Admin User",
                    Password = Utils.HashSHA1("admin"),
                    UserName = "admin"
                };

                context.TodoUserIdentities.Add(greg);
                context.SaveChanges();
            }
        }
    }
}

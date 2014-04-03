using System.Data.Entity;
using NancyTodo.Models;

namespace NancyTodo.Db
{
    public class TodoContext : DbContext
    {
        public TodoContext() : base("todoconnection") {}

        public DbSet<TodoUserIdentity> TodoUserIdentities { get; set; }
        public DbSet<TodoList> TodoLists { get; set; }
        public DbSet<TodoItem> TodoItems { get; set; }
        public DbSet<SessionToken> SessionTokens { get; set; }
    }
}
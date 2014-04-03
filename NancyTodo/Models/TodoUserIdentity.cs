using System;
using System.Collections.Generic;
using System.Security;
using Nancy.Security;

namespace NancyTodo.Models
{
    public class TodoUserIdentity : IUserIdentity
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public DateTime Created { get; set; }
        public bool IsAdmin { get; set; }
        public IEnumerable<string> Claims { get; set; }
    }
}
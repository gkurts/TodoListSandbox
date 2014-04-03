using System;

namespace NancyTodo.Models
{
    public class SessionToken
    {
        public int Id { get; set; }
        public string Token { get; set; }
        public string UserName { get; set; }
        public DateTime Created { get; set; }
    }
}
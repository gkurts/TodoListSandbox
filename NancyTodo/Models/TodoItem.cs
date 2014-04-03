using System;

namespace NancyTodo.Models
{
    public class TodoItem
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int Priority { get; set; }
        public DateTime Created { get; set; }
        public int OwnerId { get; set; }
    }
}
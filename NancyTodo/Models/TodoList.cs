using System.Collections.Generic;

namespace NancyTodo.Models
{
    public class TodoList
    {
        public int Id { get; set; }
        public string ListName { get; set; }
        public List<TodoItem> TodoItems { get; set; }
        public int OwnerId { get; set; }
    }
}
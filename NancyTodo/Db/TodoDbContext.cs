using System.Collections.Generic;
using System.Linq;
using NancyTodo.Models;

namespace NancyTodo.Db
{
    public class TodoDbContext : ITodoDbContext
    {
        private TodoContext db;
        public TodoDbContext()
        {
            db = new TodoContext();
        }

        public bool DeleteTodoList(int id)
        {
            var list = db.TodoLists.Include("TodoItems").FirstOrDefault(x => x.Id == id);

            list.TodoItems.ToList().ForEach(x => db.TodoItems.Remove(x));

            db.TodoLists.Remove(list);
            db.SaveChanges();

            return true;
        }

        public TodoList GetTodoList(int listId)
        {
            return db.TodoLists.Find(listId);
        }

        public TodoItem GetTodoItem(int itemId)
        {
            return db.TodoItems.Find(itemId);
        }

        public TodoUserIdentity CreateUser(TodoUserIdentity user)
        {
            db.TodoUserIdentities.Add(user);
            db.SaveChanges();

            return user;
        }

        public bool ValidateUser(string username, string password)
        {
            return db.TodoUserIdentities.Any(x => x.UserName == username && x.Password == password);
        }

        public bool DeleteTodoItem(int id)
        {
            var todo = db.TodoItems.Find(id);
            if (todo != null)
            {
                db.TodoItems.Remove(todo);
                db.SaveChanges();
                return true;
            }

            return false;
        }

        public SessionToken CreateSessionToken(SessionToken token)
        {
            db.SessionTokens.Add(token);
            db.SaveChanges();
            return token;
        }

        public SessionToken GetSessionByToken(string token)
        {
            return db.SessionTokens.FirstOrDefault(x => x.Token == token);
        }

        public List<SessionToken> GetSessionsByUserName(string username)
        {
            return db.SessionTokens.Where(x => x.UserName == username).ToList();
        }

        public bool DeleteSession(string token)
        {
            var session = db.SessionTokens.FirstOrDefault(x => x.Token == token);
            db.SessionTokens.Remove(session);
            db.SaveChanges();

            return true;
        }

        public TodoUserIdentity GetUser(string username)
        {
            return db.TodoUserIdentities.FirstOrDefault(x => x.UserName == username);
        }

        public TodoUserIdentity GetUser(string username, string password)
        {
            return db.TodoUserIdentities.FirstOrDefault(x => x.UserName == username && x.Password == password);
        }

        public List<TodoList> GetTodoLists(int userId)
        {
            return db.TodoLists.Include("TodoItems").Where(x => x.OwnerId == userId).ToList();
        }

        public TodoList CreateTodoList(TodoList list)
        {
            db.TodoLists.Add(list);
            db.SaveChanges();

            return list;
        }

        public TodoItem CreateTodoItem(int listId, TodoItem item)
        {
            TodoList list = db.TodoLists.FirstOrDefault(x => x.Id == listId);
            list.TodoItems.Add(item);
            db.SaveChanges();
            
            return item;
        }
    }
}
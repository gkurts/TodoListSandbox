using System.Collections.Generic;
using NancyTodo.Models;

namespace NancyTodo.Db
{
    public interface ITodoDbContext
    {
        SessionToken CreateSessionToken(SessionToken token);
        SessionToken GetSessionByToken(string tokenstring);
        List<SessionToken> GetSessionsByUserName(string username);
        bool DeleteSession(string token);
        TodoUserIdentity GetUser(string username);
        TodoUserIdentity GetUser(string username, string password);
        List<TodoList> GetTodoLists(int userId);
        TodoList GetTodoList(int listId);
        TodoItem GetTodoItem(int todoId);
        TodoList CreateTodoList(TodoList list);
        TodoItem CreateTodoItem(int listId, TodoItem item);
        TodoUserIdentity CreateUser(TodoUserIdentity user);
        bool DeleteTodoItem(int id);
        bool DeleteTodoList(int id);
        bool ValidateUser(string username, string password);
    }
}
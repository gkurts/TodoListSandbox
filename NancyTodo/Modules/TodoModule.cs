using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Web;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Security;
using NancyTodo.Db;
using NancyTodo.Models;

namespace NancyTodo.Modules
{
    public class TodoModule : NancyModule
    {
        private ITodoDbContext db;
        public TodoModule(ITodoDbContext db) : base("/api/v1/todo")
        {
            this.db = db;
            this.RequiresAuthentication();

            Get["/"] = p =>
            {
                var me = this.Context.CurrentUser as TodoUserIdentity;
                var todoList = db.GetTodoLists(me.Id);
                
                return Response.AsJson(todoList);
            };

            Post["/CreateList"] = p =>
            {
                var model = this.Bind<TodoList>();
                var me = this.Context.CurrentUser as TodoUserIdentity;

                TodoList newlist = new TodoList
                {
                    ListName = model.ListName,
                    OwnerId = me.Id
                };

                db.CreateTodoList(newlist);
                
                return Response.AsJson(new {Id = newlist.Id});
            };

            Post["/CreateTodo/{ListId:int}"] = p =>
            {
                var model = this.Bind<TodoItem>();
                var me = this.Context.CurrentUser as TodoUserIdentity;
                int listId = int.Parse(p.ListId);
                if (!IsOwner(listId, new TodoList()))
                    return HttpStatusCode.Unauthorized;

                TodoItem item = new TodoItem
                {
                    Created = DateTime.Now,
                    Description = model.Description,
                    OwnerId = me.Id,
                    Priority = 0,
                };

                db.CreateTodoItem(listId, item);

                return Response.AsJson(new {Id = item.Id});
            };

            Post["/DeleteTodo/{TodoId:int}"] = p =>
            {
                var todoId = int.Parse(p.TodoId);
                if (!IsOwner(todoId, new TodoItem()))
                    return HttpStatusCode.Unauthorized;

                bool deleted = db.DeleteTodoItem(todoId);
                return deleted;
            };

            Post["/DeleteList/{ListId:int}"] = p =>
            {
                var listId = int.Parse(p.ListId);
                if (!IsOwner(listId, new TodoList()))
                    return HttpStatusCode.Unauthorized;

                bool delete = db.DeleteTodoList(listId);
                return delete;
            };

        }

        private bool IsOwner(int id, object obtype)
        {
            var me = this.Context.CurrentUser as TodoUserIdentity;
            if (obtype.GetType() == typeof (TodoItem))
            {
                var item = db.GetTodoItem(id);
                return item.OwnerId == me.Id;
            }
            if (obtype.GetType() == typeof (TodoList))
            {
                var list = db.GetTodoList(id);
                return list.OwnerId == me.Id;
            }

            return false;
        }
    }

}
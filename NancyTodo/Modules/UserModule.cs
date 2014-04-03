using System;
using System.Linq;
using Nancy;
using Nancy.ModelBinding;
using NancyTodo.Db;
using NancyTodo.Models;

namespace NancyTodo.Modules
{
    public class UserModule : NancyModule
    {
        public UserModule(ITodoDbContext db)
            : base("/api/v1")
        {
            Post["/login"] = p =>
            {
                LoginModel model = this.Bind<LoginModel>();

                var user = db.GetUser(model.UserName, Utils.HashSHA1(model.Password));

                if (user != null)
                {
                    //remove any existing sessions just to keep things tidy.
                    var existing = db.GetSessionsByUserName(user.UserName);
                    if (existing.Any())
                    {
                        foreach (var sessionToken in existing)
                        {
                            db.DeleteSession(sessionToken.Token);
                        }
                    }

                    //create a new session and save it to the database.
                    SessionToken token = new SessionToken
                    {
                        Created = DateTime.UtcNow,
                        Token = Guid.NewGuid().ToString().Replace("-", ""),
                        UserName = user.UserName
                    };

                    db.CreateSessionToken(token);
                    
                    return Response.AsJson(new { Token = token.Token, Header = "Authorization: Bearer " + token.Token });
                }

                return HttpStatusCode.Unauthorized;
            };

            Post["/register"] = p =>
            {
                RegisterModel model = this.Bind<RegisterModel>();

                if (db.GetUser(model.UserName) != null)
                {
                    return Response.AsJson(new { Error = "That UserName already exists!" });
                }

                TodoUserIdentity create = new TodoUserIdentity
                {
                    UserName = model.UserName,
                    Created = DateTime.UtcNow,
                    Email = model.Email,
                    IsAdmin = false,
                    Name = model.Name,
                    Password = Utils.HashSHA1(model.Password)
                };

                db.CreateUser(create);

                return HttpStatusCode.OK;
            };

            Post["/logout"] = p =>
            {
                string token = Request.Form.Token;

                var sessiontoken = db.GetSessionByToken(token);
                if (sessiontoken != null)
                {
                    db.DeleteSession(sessiontoken.Token);
                }

                return HttpStatusCode.OK;
            };
        }
    }
}
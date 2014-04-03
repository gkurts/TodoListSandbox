using System.Linq;
using Nancy.Security;
using NancyTodo.Db;
using NancyTodo.Models;

namespace NancyTodo.NancyLogic
{
    public class UserApiMapper : IUserApiMapper
    {
        private ITodoDbContext db;
        public UserApiMapper(ITodoDbContext db)
        {
            this.db = db;
        }

        public IUserIdentity GetUserFromAccessToken(string accessToken)
        {
            var sessionToken = db.GetSessionByToken(accessToken);

            if (sessionToken != null)
            {
                var user = db.GetUser(sessionToken.UserName);

                return user;
            }

            return null;
        }
    }
}
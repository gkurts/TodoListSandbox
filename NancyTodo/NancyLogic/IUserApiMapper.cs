using Nancy.Security;

namespace NancyTodo.NancyLogic
{
    public interface IUserApiMapper
    {
        IUserIdentity GetUserFromAccessToken(string accessToken);
    }
}
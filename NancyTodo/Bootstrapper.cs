using System.Configuration;
using Nancy;
using Nancy.Authentication.Stateless;
using Nancy.Bootstrapper;
using Nancy.Diagnostics;
using Nancy.TinyIoc;
using NancyTodo.Db;
using NancyTodo.Models;
using NancyTodo.NancyLogic;

namespace NancyTodo
{
    public class Bootstrapper : DefaultNancyBootstrapper
    {
        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            base.ApplicationStartup(container, pipelines);

            this.Conventions.ViewLocationConventions.Add((viewName, model, context) => string.Concat("Content/", viewName));
        }

        protected override DiagnosticsConfiguration DiagnosticsConfiguration
        {
            get { return new DiagnosticsConfiguration { Password = ConfigurationManager.AppSettings["DiagnosticsSecret"] }; }
        }

        protected override void ConfigureApplicationContainer(TinyIoCContainer container)
        {
            base.ConfigureApplicationContainer(container);
            container.Register<ITodoDbContext, TodoDbContext>();
            container.Register<IUserApiMapper, UserApiMapper>();
        }

        protected override void RequestStartup(TinyIoCContainer container, IPipelines pipelines, NancyContext context)
        {
            base.RequestStartup(container, pipelines, context);

            var configuration = new StatelessAuthenticationConfiguration(nancyContext =>
                {
                    const string key = "Bearer ";
                    string accessToken = null;

                    if (nancyContext.Request.Headers.Authorization.StartsWith(key))
                    {
                        accessToken = nancyContext.Request.Headers.Authorization.Substring(key.Length);
                    }

                    if (string.IsNullOrWhiteSpace(accessToken))
                        return null;


                    IUserApiMapper userMapper = container.Resolve<IUserApiMapper>();

                    return userMapper.GetUserFromAccessToken(accessToken);
                });

            StatelessAuthentication.Enable(pipelines, configuration);
        }
    }
}
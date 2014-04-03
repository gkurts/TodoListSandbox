using System;
using System.Collections.Generic;
using System.Linq;
using Nancy;
using Nancy.Routing;
using Nancy.Security;

namespace NancyTodo.Modules
{
    public class MainModule : NancyModule
    {
        public MainModule()
        {
            Get["/"] = p =>
            {
                return Response.AsText("It's a runnin'.");
            };
        }
    }
}
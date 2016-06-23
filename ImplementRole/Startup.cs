using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ImplementRole.Startup))]
namespace ImplementRole
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        { }
    }
}
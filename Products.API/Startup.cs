﻿using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Products.API.Startup))]

namespace Products.API
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
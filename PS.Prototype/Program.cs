using PipeManagementCore;
using PipeServiceCore.Extensions;
using PM.Prototype.Implements;
using PM.Prototype.Interfaces;
using PM.Prototype.Modules;
using PS.Prototype;

var builder = Host.CreateApplicationBuilder(args);

var services = builder.Services;

services
    .AddService<Worker>(builder)
    .AddTransient<Test1Module>()
    .AddTransient<Test2Module>()
    .AddTransient<IDependency, Dependency>(serviceProvider =>
    {
        // TODO: get password
        var password = Passwords.Get("MyPassword");

        return new(password);
    });

var host = builder.Build();

host.Run();

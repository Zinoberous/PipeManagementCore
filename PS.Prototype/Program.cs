using PipeManagementCore;
using PipeServiceCore.Extensions;
using PM.Prototype.Implements;
using PM.Prototype.Interfaces;
using PM.Prototype.Modules;
using PS.Prototype;

var builder = Host.CreateApplicationBuilder(args);

builder.Services
    .AddService<Worker>()
    .AddModule<Test1Module>()
    .AddModule<Test2Module>()
    .AddDependency<IDependency, Dependency>(serviceProvider =>
    {
        // TODO: get password
        var password = Passwords.Get("MyPassword");

        return new(password);
    });

var host = builder.Build();

host.Run();

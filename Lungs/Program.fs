open Lungs
open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Hosting
open Microsoft.Extensions.Hosting
open Microsoft.Extensions.DependencyInjection
open Microsoft.Extensions.Logging

open Giraffe

open Lungs.Routing
open Registrations.RegistrationConfigure
open Sessions.SessionsConfigure
open SystemHandlers.ErrorHandler


let configureApp (app : IApplicationBuilder) =
    app
        .UseGiraffeErrorHandler(errorHandler)
        .UseGiraffe routes 

let configureServices (services : IServiceCollection) =
    services
        .AddGiraffe()
        .AddRegistrations()
        .AddSessions()
    |> ignore
    
let configureLogging (logging : ILoggingBuilder) =
    logging
        .AddConsole()
        .AddDebug()
    |> ignore

[<EntryPoint>]
let main _ =
    Host.CreateDefaultBuilder()
        .ConfigureWebHostDefaults(
            fun webHostBuilder ->
                webHostBuilder
                    .Configure(configureApp)
                    .ConfigureServices(configureServices)
                    .ConfigureLogging(configureLogging)
                    |> ignore)
        .Build()
        .Run()
    0

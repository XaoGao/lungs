namespace Sessions

open Microsoft.Extensions.DependencyInjection
open Sessions.SessionsRepository
open Sessions.SessionsService

module SessionsConfigure =
    type IServiceCollection with 
        member this.AddSessions() =
            this
                .AddScoped<ISessionsService, SessionsService>()
                .AddScoped<ISessionsRepository, SessionsRepository>()


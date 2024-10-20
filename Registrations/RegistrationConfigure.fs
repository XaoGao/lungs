namespace Registrations

open Microsoft.Extensions.DependencyInjection
open Registrations.RegistrationRepository
open Registrations.RegistrationService

module RegistrationConfigure =
    type IServiceCollection with 
        member this.AddRegistrations() =
            this
                .AddScoped<IRegistrationService, RegistrationService>()
                .AddScoped<IRegistrationRepository, RegistrationRepository>()
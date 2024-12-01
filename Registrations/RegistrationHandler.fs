namespace Registrations

open Giraffe
open Microsoft.AspNetCore.Http
open Utils.Responses
open Registrations.RegistrationDomain
open RegistrationService
open Registrations.Validations

module RegistrationHandler =
    let registration =
        fun (next : HttpFunc) (ctx : HttpContext) ->
            task {
                let! requestBody = ctx.BindJsonAsync<CreateUserParams>()
                let validateResult = validateCreateUser requestBody
                match validateResult with
                | Error errs -> return! badRequestLog ctx "RegistrationHandler.Create" errs 
                | Ok _ -> 
                    let registrationService = ctx.GetService<IRegistrationService>()
                    let user = registrationService.CreateUser requestBody
                    match user with
                    | Ok _ -> return! json {| massage = "User created" |} next ctx
                    | Error err -> return! badRequestLog ctx "RegistrationHandler.Create" err
            }
            
    let editProfile =
        fun (next : HttpFunc) (ctx : HttpContext) ->
            task {
                let! requestBody = ctx.BindJsonAsync<UpdateUserParams>()
                let registrationService = ctx.GetService<IRegistrationService>()
                let userId = ctx.User.FindFirst("Id").Value
                let user = registrationService.UpdateUser userId requestBody
                match user with
                | Ok user -> return! json user next ctx
                | Error err -> return! badRequestLog ctx "RegistrationHandler.Update" err
            }
            
    let deleteProfile : HttpHandler =
        fun (next : HttpFunc) (ctx : HttpContext) ->
            task {
                let registrationService = ctx.GetService<IRegistrationService>()
                let userId = ctx.User.FindFirst("Id").Value
                let result = registrationService.DeleteUser userId 
                match result with
                | Ok user -> return! json user next ctx
                | Error err -> return! badRequestLog ctx "RegistrationHandler.Delete" err
            }
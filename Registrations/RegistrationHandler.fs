namespace Registrations

open Database.Users
open Giraffe
open RegistrationService
open Microsoft.AspNetCore.Http
open Utils.Responses

module RegistrationHandler =
    let registration =
        fun (next : HttpFunc) (ctx : HttpContext) ->
            task {
                let! requestBody = ctx.BindJsonAsync<CreateUser>()
                let registrationService = ctx.GetService<IRegistrationService>()
                let user = registrationService.CreateUser requestBody
                match user with
                | Ok _ -> return! json {| massage = "User created" |} next ctx
                | Error err -> return! logAndWriteError400 ctx "RegistrationHandler.Create" err
            }
            
    let editProfile (userId : string) : HttpHandler =
        fun (next : HttpFunc) (ctx : HttpContext) ->
            task {
                let! requestBody = ctx.BindJsonAsync<UpdateUser>()
                let registrationService = ctx.GetService<IRegistrationService>()
                let user = registrationService.UpdateUser userId requestBody
                match user with
                | Ok user -> return! json user next ctx
                | Error err -> return! logAndWriteError400 ctx "RegistrationHandler.Update" err
            }
            
    let deleteProfile (userId : string) : HttpHandler =
        fun (next : HttpFunc) (ctx : HttpContext) ->
            task {
                let registrationService = ctx.GetService<IRegistrationService>()
                let result = registrationService.DeleteUser userId 
                match result with
                | Ok user -> return! json user next ctx
                | Error err -> return! logAndWriteError400 ctx "RegistrationHandler.Delete" err
            }


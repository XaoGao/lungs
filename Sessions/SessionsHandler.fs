module Sessions.SessionsHandler

open System.Security.Claims
open Giraffe
open Microsoft.AspNetCore.Http
open Sessions.SessionsDomain
open Sessions.SessionsService
open Utils.Responses

let login =
    fun (next : HttpFunc) (ctx : HttpContext) ->
        task {
            let! requestBody = ctx.BindJsonAsync<CreateSessionParams>()
            let sessionsService = ctx.GetService<ISessionsService>()
            let token = sessionsService.NewSession requestBody
            match token with
            | Ok t -> return! json {| token = t |} next ctx
            | Error err -> return! badRequestLog ctx "SessionsHandler.Login" err
        }
       
let logout =
    fun (next : HttpFunc) (ctx : HttpContext) ->
        task {
            match ctx.User.Identity.IsAuthenticated with
            | true ->
                ctx.User <- ClaimsPrincipal()
                return! json {| massage = "User logged out" |} next ctx
            | false -> return! badRequestLog ctx "SessionsHandler.Logout" "User not authenticated"
        }
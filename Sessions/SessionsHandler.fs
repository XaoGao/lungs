module Sessions.SessionsHandler

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
        
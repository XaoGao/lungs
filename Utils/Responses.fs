namespace Utils

open System.Threading.Tasks
open Giraffe
open Microsoft.AspNetCore.Http
open Microsoft.Extensions.Logging

module Responses =
    let responseWithLog (statusCode : int) (ctx : HttpContext) (categoryName : string) (err : string) =
        task {
            let logger = ctx.GetLogger categoryName
            logger.LogError (EventId(), err)
            ctx.Response.StatusCode <- statusCode 
            do! ctx.Response.WriteAsJsonAsync {| message = err |}
            return! earlyReturn ctx
        }
        
    let internalErrorLog : HttpContext -> string -> string -> Task<HttpContext option> =
        responseWithLog 500

    let badRequestLog : HttpContext -> string -> string -> Task<HttpContext option> =
        responseWithLog 400

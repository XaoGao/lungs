namespace Utils

open System.Threading.Tasks
open Giraffe
open Microsoft.AspNetCore.Http
open Microsoft.Extensions.Logging

module Responses =
    let logAndWriteError (statusCode : int) =
        fun (ctx : HttpContext) (categoryName : string) (err : string) ->
            task {
                let logger = ctx.GetLogger categoryName
                logger.LogError (EventId(), err)
                ctx.Response.StatusCode <- statusCode 
                do! ctx.Response.WriteAsJsonAsync {| message = err |}
                return! earlyReturn ctx
            }
        
    let logAndWriteError500 : HttpContext -> string -> string -> Task<HttpContext option> =
        logAndWriteError 500

    let logAndWriteError400 : HttpContext -> string -> string -> Task<HttpContext option> =
        logAndWriteError 400

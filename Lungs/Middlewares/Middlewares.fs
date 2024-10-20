module Lungs.Middlewares

open Utils.Operations
open Lungs.Authorize
open System.Threading.Tasks
open Giraffe
open Microsoft.Extensions.Logging
open Microsoft.AspNetCore.Http

type Middleware = HttpFunc -> HttpContext -> Task<HttpContext option>
 
let authorize (next : HttpFunc) (ctx : HttpContext) : Task<HttpContext option> =
    let auth = ctx
               |> authorizationPresent
               =>> authorizationTypeBearer
               =>> validateToken
               
    match auth with
    | Ok _ -> next ctx
    | Error e ->
        let logger = ctx.GetLogger()
        logger.LogError(EventId(), e)
        setStatusCode StatusCodes.Status401Unauthorized earlyReturn ctx
        
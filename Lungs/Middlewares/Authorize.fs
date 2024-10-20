module Lungs.Authorize

open Giraffe
open Microsoft.AspNetCore.Http

let authorizationPresent (ctx : HttpContext) = ctx.GetRequestHeader "Authorization"
    
let authorizationTypeBearer (authorizationHeader : string) =
    match authorizationHeader.Split(' ').[0] = "Bearer" with
    | true -> Ok (authorizationHeader.Split(' ').[1])
    | false -> Error "Authorization type not Bearer"
    
let validateToken (token : string) =
    match token = "123" with
    | true -> Ok token
    | false -> Error "Token is not valid"

module Utils.JwtHelper

open System
open Database.Users
open Jose

let secretKey = "Some secret key"

let encode (user : User) =
    let currentTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds()
    let expirationTime = currentTime + 3600L
    let payload = dict [
        "id", user.Id.ToString() :> obj
        "name", user.Name :> obj
        "email", user.Email :> obj
        "exp", expirationTime :> obj
    ]
    JWT.Encode(payload, secretKey |> System.Text.Encoding.UTF8.GetBytes, JwsAlgorithm.HS256) |> Ok
    
let decode (token : string) =
    let secretKeyBytes = secretKey |> System.Text.Encoding.UTF8.GetBytes
    JWT.Decode(token, secretKeyBytes)
    
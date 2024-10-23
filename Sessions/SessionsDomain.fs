module Sessions.SessionsDomain

[<CLIMutable>]
type LoginModel = {
    email : string
    password : string
}
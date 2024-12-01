module Sessions.SessionsDomain

[<CLIMutable>]
type CreateSessionParams = {
    email : string
    password : string
}
module Sessions.SessionsService

open Database.Users
open Microsoft.Extensions.Logging
open Sessions.SessionsDomain
open Sessions.SessionsRepository
open Utils.JwtHelper
    
type ISessionsService =
    abstract member NewSession : LoginModel -> Result<string, string>
    abstract member DeleteSession : User -> Result<User, string>

type SessionsService(sessionsRepository : ISessionsRepository, logger : ILogger<SessionsService>) =
    interface ISessionsService with
        member _.NewSession createSession =
            let userOption = sessionsRepository.FindByEmail createSession.email
            match userOption with
            | None -> Error "User not found"
            | Some user -> encode user 
            
        member _.DeleteSession user =
            Ok user

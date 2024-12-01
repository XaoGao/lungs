module Sessions.SessionsService

open CommonDomain.Models
open Microsoft.Extensions.Logging
open Sessions.SessionsDomain
open Sessions.SessionsRepository
open Authorization
open Utils.PasswordHelper
    
type ISessionsService =
    abstract member NewSession : CreateSessionParams -> Result<string, string>

type SessionsService(sessionsRepository : ISessionsRepository, logger : ILogger<SessionsService>) =
    interface ISessionsService with
        member _.NewSession createSessionParams =
            try
                let userOption = sessionsRepository.FindByEmail createSessionParams.email
                match userOption with
                | None ->
                    logger.LogError $"User with email {createSessionParams.email} not found"
                    Error "User not found by email or password"
                | Some user ->
                    let passwordDigest = generatePasswordDigest createSessionParams.password
                    match user.PasswordDigest <> passwordDigest with
                    | false -> Error "User not found by email or password"
                    | true -> Jwt.encode user
            with ex ->
                logger.LogError(ex, "Error creating user")
                Error "Unexpected error"
            
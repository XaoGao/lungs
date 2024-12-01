module Sessions.SessionsRepository

open Database.DB
open CommonDomain.Models
open MongoDB.Driver

type ISessionsRepository =
    abstract member FindByEmail : string -> User option
    
type SessionsRepository() =
    interface ISessionsRepository with
        member _.FindByEmail email =
            let collection = conn().GetCollection<User>("users")
            let filter = Builders<User>.Filter.Eq("Email", email)
            let result = collection.Find(filter).FirstOrDefault()
            match box result with
            | null -> None
            | _ -> Some result
    
namespace Registrations

open CommonDomain.Models
open MongoDB.Bson
open MongoDB.Driver

open Database.DB

module RegistrationRepository =
    type IRegistrationRepository =
        abstract member CreateUser : User -> unit
        abstract member FindById : ObjectId -> User option
        abstract member UpdateUser : User -> User option
        // abstract member DeleteUser : ObjectId -> unit option 
        
    type RegistrationRepository() =
        interface IRegistrationRepository with
            member _.CreateUser(user) = 
                let collection = conn().GetCollection<User>("users")
                collection.InsertOne(user)
                
            member _.FindById(id) =
                let collection = conn().GetCollection<User>("users")
                
                let filter = Builders<User>.Filter.Eq("_id", id)
                let result = collection.Find(filter).FirstOrDefault()
                match box result with
                | null -> None
                | _ -> Some result
                 
            member _.UpdateUser (newUser : User) =
                let collection = conn().GetCollection<User>("users")
                let filter = Builders<User>.Filter.Eq("_id", newUser.Id)
                let result = collection.ReplaceOne(filter, newUser)
                match result.IsAcknowledged with
                | false -> None
                | true -> Some newUser
                 
            // member _.DeleteUser(id) =
            //     let collection = conn().GetCollection<User>("users")
            //     let filter = Builders<User>.Filter.Eq("_id", id)
            //     let result = collection.DeleteOne(filter)
            //     match result.IsAcknowledged with
            //     | false -> None 
            //     | true -> Some()
 
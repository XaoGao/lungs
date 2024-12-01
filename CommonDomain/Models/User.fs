module CommonDomain.Models

open MongoDB.Bson

[<CLIMutable>]
type User = {
    Id : ObjectId
    Name : string
    Email : string
    PasswordDigest : string
}

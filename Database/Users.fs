namespace Database

open MongoDB.Bson

module Users =
    [<CLIMutable>]
    type User = {
        Id : ObjectId
        Name : string
        Email : string
        PasswordDigest : string
    }

    [<CLIMutable>]
    type CreateUser = {
        Name : string
        Email : string
        Password : string
    }

    [<CLIMutable>]
    type UpdateUser = {
        Name : string
        Email : string
    }

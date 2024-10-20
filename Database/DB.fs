namespace Database

open MongoDB.Driver

module DB =
    let conn () =
        let client = MongoClient("mongodb://localhost:27017")
        client.GetDatabase("Lungs")

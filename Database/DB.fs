namespace Database

open MongoDB.Driver

module DB =
    // let private configuration () =
    //     ConfigurationBuilder()
    //         .SetBasePath(Directory.GetCurrentDirectory())
    //         .AddJsonFile("appsettings.json")
    //         .Build()
    //         
    // let private cacheConfiguration = configuration ()
    let conn () =
        let client = MongoClient "mongodb://localhost:27017"
        client.GetDatabase "Lungs"

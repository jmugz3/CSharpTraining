using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Conventions;

namespace mongoApp
{
    class Program
    {
        static void Main(string[] args)
        {

            MainAsync(args).GetAwaiter().GetResult();
            Console.WriteLine();
            Console.WriteLine("Press Enter");
            Console.ReadLine();


        }

        
        static async Task MainAsync(string[] args)
        {
            //conventionMaps instead of class maps
            var conventionPack = new ConventionPack();
            conventionPack.Add( new CamelCaseElementNameConvention());
            ConventionRegistry.Register("camelCase", conventionPack, t => true);


            //instead of attributes , you can use this: (global registry before talking to mongo)
            BsonClassMap.RegisterClassMap<Person>(cm => 
            {
                cm.AutoMap();
                cm.MapMember(x => x.Name).SetElementName("name"); //lowercase for name
            });



            //var settings = new MongoClientSettings{ };
            var connString = "mongodb://localhost:27017";
            var client = new MongoClient(connString);

            var db = client.GetDatabase("test");
            //var col = db.GetCollection<BsonDocument>("people"); //<class>
            var col = db.GetCollection<Person>("people"); //<class>


            #region Create Documents

            var doc = new BsonDocument
            {
                { "Name" ,"Jones"},
                { "Age", 29},
                { "Profession",  "Hacker"}
            };


            var doc2 = new BsonDocument
            {
                {"something",true}
            };

            var doc3 = new Person
            {
                Name = "Jose",
                Age = 29,
                Profession = "Hacker"
            };

            //var person = new Person
            //{
            //  Name          = "Jose",
            //  Age           = 29,
            //  Colors        = new List<string> {"blue", "gray", "white"},
            //  Pets          = new List<Pet> {new Pet {Name = "Scooby", Type = "Weimaraner"}, new Pet { Name = "Bentley", Type="Corgi"}},
            //  ExtraElements = new BsonDocument("anotherName", "anotherValue")

            //};
            #endregion

            #region INSERT

            //doc.Add("age",30);
            //doc["profession"] = "hacker";
            //var nestedArray = new BsonArray();
            //nestedArray.Add(new BsonDocument("color", "red"));
            //doc.Add("array", nestedArray);

            //await col.InsertOneAsync(doc);
            //await col.InsertManyAsync(new[] { doc, doc2 });
            //await col.InsertOneAsync(doc3);

            #endregion

            //Console.WriteLine(doc["array"][0]["color"]);
            //Console.WriteLine(doc);

            //not necessary unless you want to show in console
            //using (var writer = new JsonWriter(Console.Out))
            //{
            //    BsonSerializer.Serialize(writer, person);
            //}



            #region Iterate documents

            //var col3 = db.GetCollection<BsonDocument>("people");

            //using (var cursor = await col3.Find(new BsonDocument()).ToCursorAsync())
            //{
            //    while (await cursor.MoveNextAsync())
            //    {
            //        foreach (var doc4 in cursor.Current)
            //        {
            //            Console.WriteLine(doc4);
            //        }
            //    }
            //}

            ////this forces all documents to live in memory, but is cleaner and quicker to code
            //var list = await col3.Find(new BsonDocument()).ToListAsync();
            //foreach (var doc4 in list)
            //{
            //    Console.WriteLine(doc4);
            //}

            var col4 = db.GetCollection<BsonDocument>("people");
            
            await col4.Find(new BsonDocument())
                .ForEachAsync(doc5 => Console.WriteLine(doc5));


            #endregion


        }

        class Person
        {
            public ObjectId Id { get; set; }

            //[BsonElement("name")] //lowercase
            public string Name { get; set; }

            //[BsonRepresentation(BsonType.String)]
            public int Age { get; set; }
            public string Profession { get; set; }
            
            //public List<string> Colors { get; set; }
            //public List<Pet> Pets { get; set; }
            //public BsonDocument ExtraElements { get; set; }
        }

        class Pet
        {
            public string Name { get; set; }
            public string Type { get; set; }
        }

    }
}

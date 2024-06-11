using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WorkSpace___Managment.Models.SnacksModel
{
	public class Snacks
	{
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId SnackID { get; set; } = ObjectId.GenerateNewId(); // Ensure ID is generated

        public string SnackName { get; set; }

        [BsonRepresentation(BsonType.Double)]
        public float Price { get; set; } = 0;
    }
}


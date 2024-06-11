using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WorkSpace___Managment.Models.BookingsModel
{
	public class HallSetting
	{
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId HallID { get; set; } = ObjectId.GenerateNewId(); // Ensure ID is generated

        public string HallName { get; set; }
        public string BookingType { get; set; }
        [BsonRepresentation(BsonType.Double)]
        public float Price { get; set; } = 0;
    }
}


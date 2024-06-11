using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WorkSpace___Managment.Models.BookingsModel
{
	public class BookingHall
	{
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId BookingID { get; set; } = ObjectId.GenerateNewId(); // Ensure ID is generated

        [BsonRepresentation(BsonType.String)]
        public string Status { get; set; } = "Active";

        public string OrganizationName { get; set; }
        public string Tele { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string HallName { get; set; }
        public string BookingType { get; set; }


        [BsonRepresentation(BsonType.Double)]
        public float Price { get; set; } = 0;
    }
}


using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WorkSpace___Managment.Models.StudentModel
{
	public class Students
	{
		[BsonId]
		[BsonRepresentation(BsonType.ObjectId)]
		public ObjectId StudentID { get; set; }
		public string Name { get; set; }
        public string Tele { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }


    }
}


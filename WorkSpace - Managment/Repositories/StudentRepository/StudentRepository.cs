using System;
using MongoDB.Bson;
using MongoDB.Driver;
using WorkSpace___Managment.Models.StudentModel;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WorkSpace___Managment.Repositories.StudentRepository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly IMongoCollection<Students> _students;
        public StudentRepository(IMongoClient client)
        {
            var database = client.GetDatabase("WorkSpaceManagment");
            var collection = database.GetCollection<Students>(nameof(Students));

            _students = collection;
        }

        public async Task<ObjectId> Create(Students student)
        {
            await _students.InsertOneAsync(student);
            return student.StudentID;
        }

        public async Task<string> Delete(ObjectId objectId)
        {
            var filter = Builders<Students>.Filter.Eq(x => x.StudentID, objectId);
            var result = await _students.DeleteOneAsync(filter);

            if (result.DeletedCount == 1)
            {
                return "Deleted Successfully";
            }
            else
            {
                return "Delete Failed";
            }
        }

        public Task<Students> Get(ObjectId objectId)
        {
            var filter = Builders<Students>.Filter.Eq(x => x.StudentID, objectId);
            var student = _students.Find(filter).FirstOrDefaultAsync();
            return student;
        }

        public async Task<IEnumerable<Students>> GetAll()
        {
            var students = await _students.Find(_ => true).ToListAsync();
            return students;
        }

        public async Task<IEnumerable<Students>> GetAllByName(string Name)
        {
            try
            {
                var filter = Builders<Students>.Filter.Eq(x => x.Name, Name);
                var students = await _students.Find(filter).ToListAsync();
                return students;
            }
            catch (Exception ex)
            {
                // Log the exception (use a logging framework or mechanism appropriate for your application)
                Console.WriteLine($"An error occurred while retrieving the students: {ex.Message}");
                // Optionally, rethrow or return an empty list
                return new List<Students>();
            }
        }


        public async Task<Students> Update(ObjectId objectId, Students student)
        {
            var filter = Builders<Students>.Filter.Eq(x => x.StudentID, objectId);
            var update = Builders<Students>.Update
                .Set(x => x.Name, student.Name)
                .Set(x => x.Tele, student.Tele)
                .Set(x => x.Email, student.Email)
                .Set(x => x.Address, student.Address);

            var options = new FindOneAndUpdateOptions<Students>
            {
                ReturnDocument = ReturnDocument.After
            };

            try
            {
                var updatedStudent = await _students.FindOneAndUpdateAsync(filter, update, options);
                if (updatedStudent == null)
                {
                    // Handle the case where the snack was not found
                    throw new Exception("Snack not found");
                }
                return updatedStudent;
            }
            catch (Exception ex)
            {
                // Log the exception (using your preferred logging framework)
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw; // Re-throw the exception to be handled by the caller

            }
        }
    }
}
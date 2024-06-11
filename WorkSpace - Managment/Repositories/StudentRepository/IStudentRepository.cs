using System;
using MongoDB.Bson;
using WorkSpace___Managment.Models.StudentModel;

namespace WorkSpace___Managment.Repositories.StudentRepository
{
	public interface IStudentRepository
	{
		Task<ObjectId> Create(Students student);
		Task<Students> Get(ObjectId objectId);
		Task<IEnumerable<Students>> GetAll();
        Task<IEnumerable<Students>> GetAllByName(String Name);

		Task<Students> Update(ObjectId objectId, Students student);
		Task<String> Delete(ObjectId objectId);
    }
}


using System;
using MongoDB.Bson;
using WorkSpace___Managment.Models.SnacksModel;

namespace WorkSpace___Managment.Repositories.SnacksRepositories
{
	public interface ISnacksRepository
	{
        Task<ObjectId> Create(Snacks snacks);
        Task<Snacks> Get(ObjectId objectId);
        Task<IEnumerable<Snacks>> GetAll();
        Task<IEnumerable<Snacks>> GetAllByName(String SnackName);

        Task<Snacks> Update(ObjectId objectId, Snacks snacks);
        Task<String> Delete(ObjectId objectId);
    }
}


using System;
using MongoDB.Bson;
using MongoDB.Driver;
using WorkSpace___Managment.Models.SnacksModel;

namespace WorkSpace___Managment.Repositories.SnacksRepositories
{
    public class SnacksRepository : ISnacksRepository
    {

        private readonly IMongoCollection<Snacks> _snacks;
        public SnacksRepository(IMongoClient client)
        {
            var database = client.GetDatabase("WorkSpaceManagment");
            var collection = database.GetCollection<Snacks>(nameof(Snacks));

            _snacks = collection;
        }

        public async Task<ObjectId> Create(Snacks snacks)
        {
            if (snacks.SnackID == ObjectId.Empty)
            {
                snacks.SnackID = ObjectId.GenerateNewId();
            }

            await _snacks.InsertOneAsync(snacks);
            return snacks.SnackID;
        }

        public async Task<string> Delete(ObjectId objectId)
        {
            var filter = Builders<Snacks>.Filter.Eq(x => x.SnackID, objectId);
            var result = await _snacks.DeleteOneAsync(filter);

            if (result.DeletedCount == 1)
            {
                return "Deleted Successfully";
            }
            else
            {
                return "Delete Failed";
            }
        }

        public Task<Snacks> Get(ObjectId objectId)
        {
            var filter = Builders<Snacks>.Filter.Eq(x => x.SnackID, objectId);
            var snacks = _snacks.Find(filter).FirstOrDefaultAsync();
            return snacks;
        }

        public async Task<IEnumerable<Snacks>> GetAll()
        {
            var snacks = await _snacks.Find(_ => true).ToListAsync();
            return snacks;
        }

        public async Task<IEnumerable<Snacks>> GetAllByName(string SnackName)
        {
            try
            {
                var filter = Builders<Snacks>.Filter.Eq(x => x.SnackName, SnackName);
                var snacks = await _snacks.Find(filter).ToListAsync();
                return snacks;
            }
            catch (Exception ex)
            {
                // Log the exception (use a logging framework or mechanism appropriate for your application)
                Console.WriteLine($"An error occurred while retrieving the snacks: {ex.Message}");
                // Optionally, rethrow or return an empty list
                return new List<Snacks>();
            }
        }

        public async Task<Snacks> Update(ObjectId objectId, Snacks snack)
        {
            var filter = Builders<Snacks>.Filter.Eq(x => x.SnackID, objectId);
            var update = Builders<Snacks>.Update
                .Set(x => x.SnackName, snack.SnackName)
                .Set(x => x.Price, snack.Price);

            var options = new FindOneAndUpdateOptions<Snacks>
            {
                ReturnDocument = ReturnDocument.After
            };

            try
            {
                var updatedSnack = await _snacks.FindOneAndUpdateAsync(filter, update, options);
                if (updatedSnack == null)
                {
                    // Handle the case where the snack was not found
                    throw new Exception("Snack not found");
                }
                return updatedSnack;
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


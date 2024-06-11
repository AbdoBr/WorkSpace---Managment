using System;
using MongoDB.Bson;
using WorkSpace___Managment.Models.BookingsModel;

namespace WorkSpace___Managment.Repositories.Bookings
{
    public interface IHallSettingRepository
    {
        Task<ObjectId> Create(HallSetting hallSetting);
        Task<HallSetting> Get(ObjectId objectId);
        Task<IEnumerable<HallSetting>> GetAll();
        Task<IEnumerable<HallSetting>> GetAllByName(String Name);
        Task<HallSetting> GetHallByNameAndTypeAsync(string hallName, string bookingType);

        Task<HallSetting> Update(ObjectId objectId, HallSetting hallSetting);
        Task<String> Delete(ObjectId objectId);
    }
}


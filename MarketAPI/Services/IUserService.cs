using MarketAPI.Models;

namespace MarketAPI.Services
{
    public interface IUserService
    {
        public int Create(string FirstName, string LastName, int Age, string City);
        public List<User> GetAll();
        public string Update(int Id, string FirstName, string LastName, int Age, string City);
        public void Delete(int Id);
        public User Get(int Id);
    }
}

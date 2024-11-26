using MarketAPI.Data;
using MarketAPI.Services;

namespace MarketAPI.Models
{
    public class UserService : IUserService
    {
        private readonly DataContext dataContext;
        public UserService(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public int Create(string FirstName, string LastName, int Age, string City)
        {
            User user = new User()
            {
                FirstName = FirstName,
                LastName = LastName,
                Age = Age,
                City = City
            };

            dataContext.Users.Add(user);
            dataContext.SaveChanges();

            return user.Id;
        }

        public List<User> GetAll()
        {
            return dataContext.Users.ToList();
        }

        public string Update(int Id, string FirstName, string LastName, int Age, string City)
        {
            var user = dataContext.Users.FirstOrDefault(u => u.Id == Id);
            if (user is null)
            {
                return "Not found!";
            }

            user.FirstName = FirstName;
            user.LastName = LastName;
            user.Age = Age;
            user.City = City;

            dataContext.SaveChanges();

            return "Update";
        }

        public void Delete(int Id)
        {
            var user = dataContext.Users.FirstOrDefault(u => u.Id == Id);
            if(user is not null)
            {
                dataContext.Users.Remove(user);
                dataContext.SaveChanges();
            }
        }

        public User Get(int Id)
        {
            var user = dataContext.Users.FirstOrDefault(u => u.Id == Id);

            return user;
        }
    }
}

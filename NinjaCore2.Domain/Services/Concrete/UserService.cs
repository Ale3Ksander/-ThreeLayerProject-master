using NinjaCore2.Data.Entities;
using NinjaCore2.Data.Repositories.Abstract;
using NinjaCore2.Domain.Services.Abstract;
using System.Collections.Generic;

namespace NinjaCore2.Domain.Services.Concrete
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IEnumerable<User> GetUserList()
        {
            return _userRepository.GetUserList();
        }

        public User GetUser(int id)
        {
            return _userRepository.GetUser(id);
        }

        public void Create(User user)
        {
            _userRepository.Create(user);
        }

        public void Update(User user)
        {
            _userRepository.Update(user);
        }

        public void Delete(int id)
        {
            _userRepository.Delete(id);
        }
    }
}

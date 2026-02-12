using ZoheeApi.Dtos;
using ZoheeApi.Entities;

namespace ZoheeApi.Service.UserService
{
    public interface IUserService
    {
        public Task<User> CreateUserAsync(CreateUser newUser,
            int documentId,
            string documentName);
    }
}

using FIN.Service.EmailServices;
using Microsoft.EntityFrameworkCore;
using ZoheeApi.Dtos;
using ZoheeApi.Entities;
using ZoheeApi.Repository;

namespace ZoheeApi.Service.UserService
{
    public class UserService(ZoheeContext context, IEmailService emailService) : IUserService
    {
        public async Task<User> CreateUserAsync(CreateUser newUser, int documentId, string documentName)
        {
            User? user = new User()
            {
                Name = newUser.Name,
                Email = newUser.Email,
                Phone = newUser.Phone,
            };

            await context.users.AddAsync(user);
            await context.SaveChangesAsync();

            //return await context.users.Where(x => x.DocumentId == documentId && x.Email == newUser.Email).FirstOrDefaultAsync();

            await emailService.SendSignatureRequestEmailAsync(newUser.Email, documentName, documentId, user.Id);
            return user;
        }
    }
}

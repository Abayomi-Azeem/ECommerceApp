using ECommerceApp.Domain.Aggregates;
using ECommerceApp.Domain.Dtos;
using ECommerceApp.Domain.Entities;
using ECommerceApp.Domain.Enums;
using ECommerceApp.Repository.BaseRepository.Implementation;
using ECommerceApp.Repository.RepositoryManager.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace ECommerceApp.Repository.RepositoryManager.Implementation;

public class UserRepository : Repository<User>, IUserRepository
{
    private readonly ECommerceAppDbContext _dbContext;
    private const string HashSalt = "IUsethisAsASaltForMyHasher";

    public UserRepository(ECommerceAppDbContext dbContext): base(dbContext)
    {
        _dbContext = dbContext;
    }

    public User? AuthenticateUser(LoginDto loginDetails)
    {
        //check for username
        User? user =  this.GetByPredicate(x => x.Email==loginDetails.Email).FirstOrDefault();
        if(user == null) return null;
        //check if password is correct
        using(var hasher = SHA256.Create())
        {
            var passwordHashByte = hasher.ComputeHash(Encoding.UTF8.GetBytes(loginDetails.Password+HashSalt));  
            string passwordHash = Convert.ToBase64String(passwordHashByte);         
            if(user.Password == passwordHash) return user;
        }
        return null;     
    }

    public async Task<User?> CreateUser(CreateUserDto userDetails)
    {
        //check if username already exists
        var userEmail =  this.GetByPredicate(x => x.Email==userDetails.Email).FirstOrDefault();
        if(userEmail != null) return null;
        //create user
        using(var hasher = SHA256.Create())
        {
            var passwordHash = hasher.ComputeHash(Encoding.UTF8.GetBytes(userDetails.Password+HashSalt));
            userDetails.Password = Convert.ToBase64String(passwordHash);
            User user = new User().Create(userDetails);
            var result = await this.AddAsync(user) ?  user:  null;
            return result;
        }        
    }

    public async Task<bool> DeleteUser(Guid userId)
    {
        User user = this.GetById(userId);
        if(user == null) return false;
        return await this.DeleteById(userId);
    }

    public async Task<string?> ForgotPassword(string email)
    {
        User? user = this.GetByPredicate(x=> x.Email == email).FirstOrDefault();
        if(user==null) return null;
        var newPassword = GenerateRandomPassword();
        using(var hasher = SHA256.Create())
        {
            var passwordHashByte = hasher.ComputeHash(Encoding.UTF8.GetBytes(newPassword+HashSalt));  
            string passwordHash = Convert.ToBase64String(passwordHashByte);         
            user.Password = passwordHash;
            await this.UpdateAsync(user);
            return newPassword;
        }
    }

    private static string GenerateRandomPassword()
        {
            string smallLetters = "abcdefghijklmnopqrstuvwxyz";
            string capitalLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string numbers = "0123456789";
            string specialCharacters = "_#*-@&%$";
            string[] charTypes = { smallLetters, capitalLetters, numbers, specialCharacters };
            var random = new Random();
            int noOfChars = random.Next(7, 9);
            StringBuilder password = new StringBuilder();

            for (int i = 0; i < 2; i++)
            {
                int randomPos = random.Next(smallLetters.Length);
                char smallLetter = smallLetters[randomPos];
                int charPos = random.Next(password.Length + 1);
                password.Insert(charPos, smallLetter);
            }
            for (int i = 0; i < 2; i++)
            {
                int randomPos = random.Next(capitalLetters.Length);
                char capitalLetter = capitalLetters[randomPos];
                int charPos = random.Next(password.Length + 1);
                password.Insert(charPos, capitalLetter);
            }
            for (int i = 0; i < 2; i++)
            {
                int randomPos = random.Next(numbers.Length);
                char number = numbers[randomPos];
                int charPos = random.Next(password.Length + 1);
                password.Insert(charPos, number);
            }
            for (int i = 0; i < 2; i++)
            {
                int randomPos = random.Next(specialCharacters.Length);
                char specialCharacter = specialCharacters[randomPos];
                int charPos = random.Next(password.Length + 1);
                password.Insert(charPos, specialCharacter);

            }
            return password.ToString();//returns the password that was generated
        }

    public async Task<bool?> ChangePassword(ChangePasswordDto passwordDetails)
    {
        User? user = this.GetByPredicate(x=> x.Email == passwordDetails.Email).FirstOrDefault();
        if(user==null) return null;

        using(var hasher = SHA256.Create())
        {
            var passwordHashByte = hasher.ComputeHash(Encoding.UTF8.GetBytes(passwordDetails.OldPassword+HashSalt));  
            string passwordHash = Convert.ToBase64String(passwordHashByte);         
            if(user.Password == passwordHash) 
            { 
                var newPasswordHashByte = hasher.ComputeHash(Encoding.UTF8.GetBytes(passwordDetails.NewPassword+HashSalt));  
                string newPasswordHash = Convert.ToBase64String(newPasswordHashByte);     
                user.Password = newPasswordHash;
                await this.UpdateAsync(user);
                return true;
            }
            return false;
        }

    }
}
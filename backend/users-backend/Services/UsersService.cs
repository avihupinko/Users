using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using users_backend.Interfaces;
using users_backend.Models;

namespace users_backend.Services
{
    public class UsersService : IUsersService
    {
        private readonly UsersDbContext _usersDbContext;

        public UsersService(UsersDbContext usersDbContext)
        {
            _usersDbContext = usersDbContext;
        }

        // Get Table Page informations 
        public async Task<BasePageModel<UserLogicModel>> Get(string userName = null,
                                                  string phone = null,
                                                  string? userId = null,
                                                  int limit = 10,
                                                  int offset = 0)
        {
            IQueryable<User> query = (from q in this._usersDbContext.Users
                                      where !q.IsDeleted
                                      select q);
            if (!string.IsNullOrEmpty(userName))
            {
                query = (from q in query
                         where q.UserName.Contains(userName)
                         select q);
            }
            if (!string.IsNullOrEmpty(userId))
            {
                query = (from q in query
                         where q.UserId.Contains(userId)
                         select q);
            }
            if (!string.IsNullOrEmpty(phone))
            {
                query = (from q in query
                         where !string.IsNullOrEmpty(q.Phone) && q.Phone.Contains(phone)
                         select q);
            }

            return new BasePageModel<UserLogicModel>
            {
                Data = await query.OrderByDescending(x => x.Updated).Skip(offset * limit).Take(limit)
                .Select(x => new UserLogicModel
                {
                    UserId = x.UserId,
                    UserName = x.UserName,
                    BirthDate = x.BirthDate,
                    Email = x.Email,
                    Gender = x.Gender.HasValue ? x.Gender.ToString() : null,
                    Id = x.Id,
                    Phone = x.Phone,
                    Created = x.Created,
                    Updated = x.Updated
                }).ToListAsync(),
                Total = await query.CountAsync()
            };
        }

        // Get specific user
        public async Task<UserLogicModel?> GetById(Guid id)
        {
            User user = await this._usersDbContext.Users.FirstOrDefaultAsync(x=> x.Id == id && !x.IsDeleted);
            return user != null ? new UserLogicModel
            {
                UserId = user.UserId,
                UserName = user.UserName,
                BirthDate = user.BirthDate,
                Email = user.Email,
                Gender = user.Gender?.ToString(),
                Id = user.Id,
                Phone = user.Phone,
                Created = user.Created,
                Updated = user.Updated
            } : null;
        }

        public bool ValidateUsingMailAddress(string emailAddress)
        {
            try
            {
                var email = new MailAddress(emailAddress);
                return email.Address == emailAddress.Trim();
            }
            catch
            {
                return false;
            }
        }
        //Create user
        public async Task<UserLogicModel> Create(UserLogicModel model)
        {
            Gender? gender = null;
            if (!string.IsNullOrEmpty(model.Gender))
            {
                if (!Enum.TryParse(model.Gender, out Gender g))
                {
                    throw new Exception("Invalid Gender value");
                }
                gender = g;
            }
            if(!string.IsNullOrEmpty(model.Email))
            {
                if (!ValidateUsingMailAddress(model.Email))
                    throw new Exception("Invalid Email Address");
            }
            var user = new User
            {
                UserId = model.UserId,
                UserName = model.UserName,
                BirthDate = model.BirthDate,
                Email = model.Email,
                Gender = gender,
                Phone = model.Phone,
                Created = DateTime.UtcNow,
                Updated = DateTime.UtcNow
            };
            this._usersDbContext.Users.Add(user);
            await this._usersDbContext.SaveChangesAsync();
            model.Created = user.Created;
            model.Updated = user.Updated;
            model.Id = user.Id;
            return model;
        }

        //Update User
        public async Task<UserLogicModel> Update(UserLogicModel model)
        {
            var user = await this._usersDbContext.Users.FirstOrDefaultAsync(x => x.Id == model.Id && !x.IsDeleted);
            if (user == null)
                throw new Exception("User not found");

            Gender? gender = null;
            if (!string.IsNullOrEmpty(model.Gender))
            {
                if (!Enum.TryParse(model.Gender, out Gender g))
                {
                    throw new Exception("Invalid Gender value");
                }
                gender = g;
            }
            if (!string.IsNullOrEmpty(model.Email))
            {
                if (!ValidateUsingMailAddress(model.Email))
                    throw new Exception("Invalid Email Address");
            }
            user.UserId = model.UserId;
            user.UserName = model.UserName;
            user.BirthDate = model.BirthDate;
            user.Email = model.Email;
            user.Gender = gender;
            user.Phone = model.Phone;
            user.Updated = DateTime.UtcNow;
            this._usersDbContext.Users.Update(user);
            await this._usersDbContext.SaveChangesAsync();
            model.Created = user.Created;
            model.Updated = user.Updated;
            model.Id = user.Id;
            return model;
        }

        // Delete specific user
        public async Task Delete(Guid id)
        {
            User? user = await this._usersDbContext.Users.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
            if (user == null)
                throw new Exception("User not found");

            user.IsDeleted = true;
            user.Updated = DateTime.UtcNow;

            this._usersDbContext.Update(user);
            await this._usersDbContext.SaveChangesAsync();
        }
    }
}

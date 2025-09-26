using Hw12.Enteties;
using Hw12.Entities;
using Hw12.Interface;
using Hw12.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hw12.Services
{
    public class AuthenticationService
    {
        IUserRepository _userRepo = new UserRepository();
        IAdminRepository _adminRepo = new AdminRepository();
        public bool RegisterUser(string username, string password)
        {
            var existing = _userRepo.GetByUsername(username);

            if (string.IsNullOrWhiteSpace(username))
                throw new Exception("Username can't be empty!");
            if (string.IsNullOrWhiteSpace(password))
                throw new Exception("Password can't be empty!");
            if (existing != null)
                return false;

            var user = new User
            {
                UserName = username,
                Password = password,
            };

            _userRepo.Add(user);
            return true;
        }

        public bool RegisterAdmin(string username, string password)
        {
            var existing = _adminRepo.GetByUsername(username);
            if (string.IsNullOrWhiteSpace(username))
                throw new Exception("Username can't be empty!");
            if (string.IsNullOrWhiteSpace(password))
                throw new Exception("Password can't be empty!");
            if (existing != null)
                return false;

            var admin = new Admin
            {
                UserName = username,
                Password = password,
            };

            _adminRepo.Add(admin);
            return true;
        }

        public User? LoginUser(string username, string password)
        {

            var user = _userRepo.GetByUsername(username);
            if (string.IsNullOrWhiteSpace(username))
                throw new Exception("Username can't be empty!");
            if (string.IsNullOrWhiteSpace(password))
                throw new Exception("Password can't be empty!");
            return user;
        }

        public Admin? LoginAdmin(string username, string password)
        {
            var admin = _adminRepo.GetByUsername(username);
            if (string.IsNullOrWhiteSpace(username))
                throw new Exception("Username can't be empty!");
            if (string.IsNullOrWhiteSpace(password))
                throw new Exception("Password can't be empty!");
            return admin;
        }


    }
}

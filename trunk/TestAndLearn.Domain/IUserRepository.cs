using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestAndLearn.Domain.Models;

namespace TestAndLearn.Domain
{
    public interface IUserRepository
    {
        IList<UserModel> GetUsers();
        UserModel GetUser(string username);
        void CreateUser(UserModel user, string password);
        void UpdateUser(UserModel user);
        void DeleteUser(string username);
        string[] GetRoles();
        void ChangeSelectedClient(string username, int clientId);
        void ChangeSelectedTheme(string username, string theme);
    }
}

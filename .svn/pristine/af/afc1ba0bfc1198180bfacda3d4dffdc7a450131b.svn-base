using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;
using TestAndLearn.Domain.Models;

namespace TestAndLearn.Domain.Infrastructure
{
    public class UserRepository : IUserRepository
    {
        public IList<UserModel> GetUsers()
        {
            var membershipUsers = Membership.GetAllUsers();

            using (var connection = new TestAndLearnEntitiesConnection())
            {
                var list = new List<UserModel>();

                 foreach (MembershipUser user in membershipUsers)
                {

                    var role = Roles.GetRolesForUser(user.UserName).First();

                    IList<ClientModel> clients = null;

                    if (role == "Admin")
                    {
                        clients = (from c in connection.Clients
                                   select new ClientModel()
                                   {
                                       ClientId = c.ClientId,
                                       ClientName = c.ClientName,
                                       LogoImageName = c.LogoImageName
                                   }).ToList();
                    }
                    else
                    {
                        clients = (from m in connection.UserClientMaps
                                      .Include("Client")
                                   where
                                     m.UserId == (Guid)user.ProviderUserKey
                                   select
                                       new ClientModel()
                                       {
                                           ClientId = m.ClientId,
                                           ClientName = m.Client.ClientName,
                                           LogoImageName = m.Client.LogoImageName
                                       }).ToList();
                    }

                    var us = connection.UserSettings.SingleOrDefault(f => f.UserId == (Guid)user.ProviderUserKey);

                    var userSettings = new UserSettingsModel();

                    if (us == null)
                    {
                        userSettings.SelectedClientId = clients.First().ClientId;
                        userSettings.SelectedTheme = "default";
                    }
                    else
                    {
                        userSettings.SelectedClientId = us.SelectedClientId.Value;
                        userSettings.SelectedTheme = us.SelectedTheme;
                    }

                    userSettings.SelectedClientName = clients.Single(f => f.ClientId == userSettings.SelectedClientId).ClientName;

                    list.Add(new UserModel()
                    {
                        UserName = user.UserName,
                        Email = user.Email,
                        Role = role,
                        Clients = clients,
                        UserSettings = userSettings
                    });
                };

                return list;
            }
        }

        public UserModel GetUser(string username)
        {
            var user = Membership.GetUser(username);
            var role = Roles.GetRolesForUser(user.UserName).First();

            using (var connection = new TestAndLearnEntitiesConnection())
            {
                IList<ClientModel> clients = null;

                if (role == "Admin")
                {
                    clients = (from c in connection.Clients
                              select new ClientModel()
                              {
                                  ClientId = c.ClientId,
                                  ClientName = c.ClientName,
                                  LogoImageName = c.LogoImageName
                              }).ToList();
                }
                else
                {
                    clients = (from m in connection.UserClientMaps
                                  .Include("Client")
                              where
                                m.UserId == (Guid)user.ProviderUserKey
                              select
                                  new ClientModel()
                                  {
                                      ClientId = m.ClientId,
                                      ClientName = m.Client.ClientName,
                                      LogoImageName = m.Client.LogoImageName
                                  }).ToList();
                }

                var us = connection.UserSettings.SingleOrDefault(f => f.UserId == (Guid)user.ProviderUserKey);

                var userSettings = new UserSettingsModel();

                if (us == null)
                {
                    userSettings.SelectedClientId = clients.First().ClientId;
                    userSettings.SelectedTheme = "default";
                }
                else
                {
                    userSettings.SelectedClientId = us.SelectedClientId.Value;
                    userSettings.SelectedTheme = us.SelectedTheme;
                }

                userSettings.SelectedClientName = clients.Single(f => f.ClientId == userSettings.SelectedClientId).ClientName;

                return new UserModel()
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    Role = role,
                    Clients = clients,
                    UserSettings = userSettings
                };
            }
        }

        public void CreateUser(UserModel user, string password)
        {
            var u = Membership.GetUser(user.UserName);
            if (u != null)
            {
                throw new Exception("User: " + user.UserName + " already exists in the system.");
            }

            MembershipCreateStatus createStatus;
            var memberUser = Membership.CreateUser(user.UserName, password, user.Email, null, null, true, null, out createStatus);

            if (createStatus == MembershipCreateStatus.Success)
            {
                Roles.AddUserToRole(user.UserName, user.Role);

                if (user.Role == "Client")
                {
                    memberUser.Comment = "changepassword";
                    Membership.UpdateUser(memberUser);
                }
                else
                {
                    memberUser.Comment = "";
                    Membership.UpdateUser(memberUser);
                }

                if (user.Role != "Admin")
                {
                    if (user.Clients.Count <= 0)
                    {
                        Membership.DeleteUser(user.UserName);
                        throw new Exception("A Client must be selected for all non admin accounts.");
                    }

                    using (var connection = new TestAndLearnEntitiesConnection())
                    {
                        foreach (var client in user.Clients)
                        {
                            connection.UserClientMaps.Add(new UserClientMap()
                            {
                                UserId = (Guid)memberUser.ProviderUserKey,
                                ClientId = client.ClientId
                            });
                        }

                        connection.UserSettings.Add(new UserSetting() 
                        {
                            UserId = (Guid)memberUser.ProviderUserKey,
                            SelectedClientId = user.Clients.First().ClientId,
                            SelectedTheme = "default"
                        });

                        connection.SaveChanges();
                    }
                }
                else
                {
                    using (var connection = new TestAndLearnEntitiesConnection())
                    {
                        connection.UserSettings.Add(new UserSetting()
                        {
                            UserId = (Guid)memberUser.ProviderUserKey,
                            SelectedClientId = user.Clients.First().ClientId,
                            SelectedTheme = "default"
                        });

                        connection.SaveChanges();
                    }
                }
            }
            else
            {
                throw new Exception(this.ErrorCodeToString(createStatus));
            }
        }

        public void UpdateUser(UserModel user)
        {
            throw new NotImplementedException();
        }

        public void DeleteUser(string username)
        {
            using (var connection = new TestAndLearnEntitiesConnection())
            {
                var user = Membership.GetUser(username);
                var map = connection.UserClientMaps.SingleOrDefault(f => f.UserId == (Guid)user.ProviderUserKey);

                if (map != null)
                {
                    connection.UserClientMaps.Remove(map);
                }

                Membership.DeleteUser(username);

                connection.SaveChanges();
            }
        }

        public string[] GetRoles()
        {
            return Roles.GetAllRoles();
        }

        public void ChangeSelectedClient(string username, int clientId)
        {
            using (var connection = new TestAndLearnEntitiesConnection())
            {
                var user = Membership.GetUser(username);
                var userSettings = connection.UserSettings.Single(f => f.UserId == (Guid)user.ProviderUserKey);

                userSettings.SelectedClientId = clientId;

                connection.SaveChanges();
            }
        }

        public void ChangeSelectedTheme(string username, string theme) 
        {
            using (var connection = new TestAndLearnEntitiesConnection())
            {
                var user = Membership.GetUser(username);
                var userSettings = connection.UserSettings.Single(f => f.UserId == (Guid)user.ProviderUserKey);

                userSettings.SelectedTheme = theme;

                connection.SaveChanges();
            }
        }

        #region Private Methods

        private string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }

        #endregion Private Methods
    }
}

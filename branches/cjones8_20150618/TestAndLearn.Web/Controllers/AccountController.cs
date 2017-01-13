using Starcom.Authentication;
using Starcom.Common.Email;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TestAndLearn.Domain;
using TestAndLearn.Domain.Models;
using TestAndLearn.Web.Models;

namespace TestAndLearn.Web.Controllers
{
    public class AccountController : BaseController
    {
        private PublicisUserService _publicisUserService;
        private IUserRepository _userRepository;
        private IClientRepository _clientRepository;

        public AccountController(IUserRepository userRepository, IClientRepository clientRepository) : base(userRepository)
        {
            _publicisUserService = new PublicisUserService();
            _userRepository = userRepository;
            _clientRepository = clientRepository;
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View(new LogOnModel());
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LogOnModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var isValid = true;

                if (Roles.IsUserInRole(model.UserName, "Admin") || Roles.IsUserInRole(model.UserName, "Starcom"))
                {
                    //isValid = _publicisUserService.Validate(model.UserName, model.Password);
                }
                else
                {
                    isValid = Membership.ValidateUser(model.UserName, model.Password);
                }

                if (isValid)
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                    
                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                        && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "The user name or password provided is incorrect.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [HttpGet]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [CustomAuthorize(Roles = "Admin")]
        public ActionResult RegisterClientUser(RegisterClientUserModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _userRepository.CreateUser(new UserModel()
                    {
                        UserName = model.UserName,
                        Role = "Client",
                        Clients = new List<ClientModel>() 
                        {
                            new ClientModel() 
                            {
                                ClientId = model.SelectedClientId
                            }
                        },
                        Email = model.Email
                    },
                    model.Password);

                    return Json(new
                    {
                        Success = true,
                        Message = model.UserName + " has been saved to the system."
                    });
                }
                catch (Exception ex)
                {
                    return Json(new
                    {
                        Success = false,
                        Message = ex.Message
                    });
                }
            }

            var message = string.Join(" | ", ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage));

            // If we got this far, something failed, redisplay form
            return Json(new
            {
                Success = false,
                Message = message
            });
        }

        [HttpPost]
        [CustomAuthorize(Roles = "Admin")]
        public ActionResult RegisterPublicisUser(RegisterPublicisUserModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _userRepository.CreateUser(new UserModel()
                    {
                        UserName = model.UserName,
                        Role = model.Role,
                        Clients = new List<ClientModel>() 
                        {
                            new ClientModel() 
                            {
                                ClientId = model.SelectedClientId
                            }
                        },
                        Email = model.Email
                    },
                     "starcom");

                    return Json(new
                    {
                        Success = true,
                        Message = model.UserName + " has been saved to the system."
                    });
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);

                    return Json(new
                    {
                        Success = false,
                        Message = ex.Message
                    });
                }
            }

            var message = string.Join(" | ", ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage));

            // If we got this far, something failed, redisplay form
            return Json(new
            {
                Success = false,
                Message = message
            });
        }

        //
        // GET: /Account/ChangePassword

        [Authorize]
        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Account/ChangePassword

        [Authorize]
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {

                // ChangePassword will throw an exception rather
                // than return false in certain failure scenarios.
                bool changePasswordSucceeded;
                try
                {
                    MembershipUser currentUser = Membership.GetUser(User.Identity.Name, true /* userIsOnline */);
                    changePasswordSucceeded = currentUser.ChangePassword(model.OldPassword, model.NewPassword);
                    if (changePasswordSucceeded)
                    {
                        currentUser.Comment = "";
                        Membership.UpdateUser(currentUser);
                    }
                }
                catch (Exception)
                {
                    changePasswordSucceeded = false;
                }

                if (changePasswordSucceeded)
                {
                    return RedirectToAction("ChangePasswordSuccess");
                }
                else
                {
                    ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ChangePasswordSuccess

        public ActionResult ChangePasswordSuccess()
        {
            return View();
        }

        public ActionResult ResetPassword()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult ResetPassword(string email)
        {

            var userName = Membership.GetUserNameByEmail(email);

            if (string.IsNullOrEmpty(userName))
            {
                return RedirectToAction("Unauthorized", "Account");
            }

            var user = Membership.GetUser(userName);

            user.Comment = "changepassword";
            Membership.UpdateUser(user);

            string newPassword = user.ResetPassword();

            //email
            var emailer = new Emailer(
                new EmailerConfiguration()
                {
                    Host = ConfigurationManager.AppSettings["EmailerHost"],
                    PortNumber = Convert.ToInt32(ConfigurationManager.AppSettings["EmailerPortNumber"])
                });

            emailer.SendEmail(
                email,
                "starcom_test_and_learn_do_not_reply@starcomww.com",
                "Password Reset: Starcom Test and Learn",
                "You have requested a password reset for your Test and Learn Account.\n" +
                "Your new password is: " + newPassword + "\n" +
                "Please login at: http://www.smganalytics.com/testandlearn");

            return RedirectToAction("ResetPasswordSuccess", "Account");

        }

        [AllowAnonymous]
        public ActionResult ResetPasswordSuccess()
        {
            return View();
        }

        [CustomAuthorize(Roles ="Admin")]
        public ActionResult Users()
        {
            //var users = _userRepository.GetUsers();
            var clients = _clientRepository.GetClients();
            return View(new RegisterUserModels()
            {
                PublicisUserModel = new RegisterPublicisUserModel()
                {
                    Clients = clients
                },
                ClientUserModel = new RegisterClientUserModel()
                {
                    Clients = clients
                }
            });
        }

        [CustomAuthorize(Roles = "Admin")]
        public ActionResult GetUsers()
        {
            var users = _userRepository.GetUsers();

            return Json(new { rows = users }, JsonRequestBehavior.AllowGet);
        }

        [CustomAuthorize(Roles = "Admin")]
        public ActionResult DeleteUser(string username)
        {
            try
            {
                _userRepository.DeleteUser(username);

                return Json(new
                {
                    Success = true,
                    Message = "User deleted"
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    Success = false,
                    Message = ex.Message
                });
            }

        }

        [AllowAnonymous]
        public ActionResult Unauthorized()
        {
            return View();
        }

        public ActionResult SearchPublicisUsers(string term)
        {
            var users = _publicisUserService.FindUsersByName(term);

            return Json(users, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ChangeClient(int id)
        {
            _userRepository.ChangeSelectedClient(this.User.Identity.Name, id);

            return RedirectToAction("Index", "Home");
        }

        public ActionResult ChangeTheme(string theme)
        {
            _userRepository.ChangeSelectedTheme(this.User.Identity.Name, theme);
            Session["ActiveTheme"] = theme;

            return RedirectToAction("Index", "Home");
        }

        #region Status Codes
        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
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
        #endregion
    }
}
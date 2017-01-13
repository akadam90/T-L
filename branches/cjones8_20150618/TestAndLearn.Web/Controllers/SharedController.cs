using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestAndLearn.Domain;

namespace TestAndLearn.Web.Controllers
{
    public class SharedController : Controller
    {
        private IUserRepository _userRepository;

        public SharedController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public ActionResult MainNavigation(string activeSection)
        {
            var user = _userRepository.GetUser(this.User.Identity.Name);

            return View(new Models.NavigationModel()
            {
                ActiveSection = activeSection,
                CurrentUser = user
            });
        }
    }
}
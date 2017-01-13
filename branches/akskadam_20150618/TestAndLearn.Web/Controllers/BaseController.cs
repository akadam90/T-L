using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestAndLearn.Domain;

namespace TestAndLearn.Web.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        private IUserRepository _userRepository;

        public BaseController(IUserRepository userRepository)
        {
            _userRepository = userRepository;

            //if (this.User != null && Session != null)
            //{

            //    var user = _userRepository.GetUser(this.User.Identity.Name);

            //    Session["ActiveTheme"] = user.UserSettings.SelectedTheme;
            //}
        }
	}
}
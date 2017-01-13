using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestAndLearn.Domain;
using TestAndLearn.Web.Models;

namespace TestAndLearn.Web.Controllers
{
    public class HomeController : BaseController
    {
        private IUserRepository _userRepository;
        private ITestRepository _testRepository;

        public HomeController(IUserRepository userRepository, ITestRepository testRepository) : base(userRepository)
        {
            _userRepository = userRepository;
            _testRepository = testRepository;
        }

        public ActionResult Index()
        {
            var user = _userRepository.GetUser(this.User.Identity.Name);
            Session["ActiveTheme"] = user.UserSettings.SelectedTheme;

            return View(user);
        }

        public ActionResult TestForm(int id)
        {
            var testTypes = _testRepository.GetTestTypes(id);
            var successMetrics = _testRepository.GetSuccessMetrics(id);
            var user = _userRepository.GetUser(this.User.Identity.Name);

            return View(new TestFormModel() 
            {
                ClientId = id,
                SuggesstedSubmitter = user.Email,
                TestTypes = testTypes,
                SuccessMetrics = successMetrics
            });
        }
	}
}
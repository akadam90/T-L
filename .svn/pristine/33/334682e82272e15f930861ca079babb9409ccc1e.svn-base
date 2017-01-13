using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;
using TestAndLearn.Domain;
using TestAndLearn.Domain.Models;
using TestAndLearn.Web.Models;

namespace TestAndLearn.Web.Controllers
{
    public class TestApiController : BaseApiController
    {
        private ITestRepository _testRepository;

        public TestApiController(ITestRepository testRepository)
        {
            _testRepository = testRepository;
        }

        public IList<TestModel> GetTests(int clientId, int testStatusId)
        {
            return _testRepository.GetTests(clientId);
        }

        public TestModel GetTest(int id)
        {
            return _testRepository.GetTest(id);
        }

        //save test
        [HttpPost]
        public ActionResponseModel SaveTest([FromBody] TestModel test)
        {
            try
            {

                _testRepository.Save(test);

                return new ActionResponseModel()
                {
                    Success = true,
                    Message = "The test was saved successfully."
                };
            }
            catch (Exception ex)
            {
                return new ActionResponseModel()
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        //approve test

        //close test
    }
}
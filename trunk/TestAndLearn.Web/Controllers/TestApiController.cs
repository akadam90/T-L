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

        public IList<TestModel> GetTests(int clientId)
        {
            return _testRepository.GetTests(clientId);
        }


        public IList<TestModel> GetTests(int clientId, int testStatusId)
        {
            return _testRepository.GetTests(clientId, testStatusId);
        }

        [HttpGet]
        public TestModel GetTest(int id)
        {
            return _testRepository.GetTest(id);
        }

        [HttpGet]
        [HttpPost]
        public TestUrlModel GetUrls(int TestId)
        {
              TestUrlModel urlmodel = _testRepository.GetUrls(TestId);
                //int count = urlList.Count();

                //TestUrlModel urlmodel = new TestUrlModel();
                //urlmodel.TestName = urlList.ElementAt(0).TestName;
                //urlmodel.TestId = TestId;
                //if (count >= 1)
                //    urlmodel.Url1 = urlList.ElementAt(0).Url;
                //if (count >= 2)
                //    urlmodel.Url2 = urlList.ElementAt(1).Url;
                //if (count >= 3)
                //    urlmodel.Url3 = urlList.ElementAt(2).Url;
                //if (count >= 4)
                //    urlmodel.Url4 = urlList.ElementAt(3).Url;
                //if (count >= 5)
                //    urlmodel.Url5 = urlList.ElementAt(4).Url;
                //if (count >= 6)
                //    urlmodel.Url6 = urlList.ElementAt(5).Url;

                return urlmodel;

        
        }

        [HttpPost]
        public ActionResponseModel AddUrls([FromBody] TestUrlModel urls)
        {
            try
            {
                _testRepository.addUrls(urls);
                return new ActionResponseModel() { 
                Success = true,
                Message = "New Urls added successfully"
                };
            }
            catch (Exception ex)
            {
                return new ActionResponseModel() { 
                Success = false,
                Message = ex.Message
                
                };
            }
        
        }

        [HttpPost]
        public IList<TestModel> GetTests([FromBody] FilterTestModel filterModel)
        {
           return _testRepository.GetTests(filterModel);
        }

        //save tests
        [HttpPost]
        public ActionResponseModel SaveBaseTest([FromBody] TestBaseModel test)
        {
            try
            {

                _testRepository.SaveBaseTest(test);

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
        [HttpGet]
        [HttpPost]
        [CustomAuthorize(Roles = "Admin")]
        public ActionResponseModel ChangeTestStatus(int testId, int statusId)
        {
            try
            {
                _testRepository.ChangeStatus(testId, statusId);

                return new ActionResponseModel()
                {
                    Success = true,
                    Message = "Status Changed"
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

        [HttpPost]
        [CustomAuthorize(Roles = "Admin")]
        public ActionResponseModel CloseTest([FromBody] TestOutcomeModel outcomeTestModel)
        {
            try
            {
                _testRepository.CloseTest(outcomeTestModel);

                return new ActionResponseModel()
                {
                    Success = true,
                    Message = "Test Closed."
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

        [HttpPost]
        public ActionResponseModel DeleteTest([FromBody] TestBaseModel test)
        {
            try
            {
                _testRepository.DeleteTest(test.TestId);
                return new ActionResponseModel()
                {
                    Success = true,
                    Message = "Test Deleted Successfully"
                };
            }
            catch(Exception ex)
            {
                return new ActionResponseModel() 
                { 
                Success = false,
                Message = ex.Message
                };
            }

        }

        [HttpPost]
        public ActionResponseModel RankTest([FromBody] TestRankModel rank)
        {
            try
            {

                _testRepository.RankTest(rank);

                return new ActionResponseModel()
                {
                    Success = true,
                    Message = "Rank Calculated"
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

        [HttpGet]
        public int GetRank(int testId)
        {
            try
            {
                var test = _testRepository.GetTest(testId);
                return test.Rank;
                
            }
            catch (Exception ex)
            {
                return -1;
            }
        
        }

       

        [HttpPost]
        [CustomAuthorize(Roles = "Admin")]
        public ActionResponseModel SubmitRunDates([FromBody] TestRunDatesModel model)
        {
            try
            {
                _testRepository.SubmitRunDates(model);

                return new ActionResponseModel()
                {
                    Success = true,
                    Message = "Run Dates submitted."
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
    }
}
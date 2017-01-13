﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestAndLearn.Domain.Models;

namespace TestAndLearn.Domain.Infrastructure
{
    public class TestRepository : ITestRepository
    {
        public void Save(TestModel testModel)
        {
            using (var connection = new TestAndLearnEntitiesConnection())
            {
                Test test = null;
                
                if (testModel.TestId > 0)
                {
                    test = connection.Tests.SingleOrDefault(f => f.TestId == testModel.TestId);

                    if (test == null)
                    {
                        throw new Exception("Could not find Test with TestId: " + testModel.TestId + " in the database table.");
                    }
                    else
                    {
                        //update
                        //test.TestId = testModel.TestId;
                        //test.ClientId = testModel.ClientId;
                        test.Name = testModel.Name;
                        test.SubmittedBy = testModel.SubmittedBy;
                        test.Hypothesis = testModel.Hypothesis;
                        test.AdditionalNotes = testModel.AdditionalNotes;
                        test.ExpectedBusinessValue = testModel.ExpectedBusinessValue;
                        test.Quarter = testModel.Quarter;
                        test.Year = testModel.Year;
                        test.StartDate = testModel.StartDate;
                        test.EndDate = testModel.EndDate;
                        test.Outcome = testModel.Outcome;
                        test.Recommendation = testModel.Recommendation;

                        //update test types
                        for (var i = test.TestTypes.Count - 1; i >= 0; i--)
                        {
                            var testType = test.TestTypes.ElementAt(i);
                            var tt = connection.Clients.Single(f => f.ClientId == testModel.ClientId).TestTypes.SingleOrDefault(f => f.TestTypeId == testType.TestTypeId);

                            if (tt == null)
                            {
                                throw new Exception("The TestTypeId: " + testType.TestTypeId + " is not valid for client: " + testModel.ClientId);
                            }
                            else
                            {
                                if (testModel.TestTypes.SingleOrDefault(f => f.TestTypeId == tt.TestTypeId) == null)
                                {
                                    test.TestTypes.Remove(tt);
                                }
                            }
                        }

                        //update success metrics
                        for (var i = test.SuccessMetrics.Count - 1; i >= 0; i--)
                        {
                            var successMetric = test.SuccessMetrics.ElementAt(i);
                            var sm = connection.Clients
                                .Single(f => f.ClientId == testModel.ClientId)
                                .SuccessMetrics
                                .SingleOrDefault(f => f.SuccessMetricId == successMetric.SuccessMetricId);

                            if (sm == null)
                            {
                                throw new Exception("The SuccessMetricId: " + successMetric.SuccessMetricId + " is not valid for client: " + testModel.ClientId);
                            }
                            else
                            {
                                if (testModel.SuccessMetrics.SingleOrDefault(f => f.SuccessMetricId == sm.SuccessMetricId) == null)
                                {
                                    test.SuccessMetrics.Remove(sm);
                                }
                            }
                        }
                    }
                }
                else
                {
                    test = new Test()
                    {
                        ClientId = testModel.ClientId,
                        Name = testModel.Name,
                        SubmittedOn = DateTime.Today,
                        SubmittedBy = testModel.SubmittedBy,
                        Hypothesis = testModel.Hypothesis,
                        AdditionalNotes = testModel.AdditionalNotes,
                        ExpectedBusinessValue = testModel.ExpectedBusinessValue,
                        Quarter = testModel.Quarter,
                        Year = testModel.Year,
                        StartDate = testModel.StartDate,
                        EndDate = testModel.EndDate,
                        Outcome = testModel.Outcome,
                        Recommendation = testModel.Recommendation
                    };

                    testModel.Status = "Submitted";
                }

                //update status
                var status = connection.TestStatus.SingleOrDefault(f => f.Name.Trim().ToLower() == testModel.Status.Trim().ToLower());

                if (status == null)
                {
                    throw new Exception("The Status: " + testModel.Status + " does not exist in the system.");
                }
                else
                {
                    test.TestStatusId = status.TestStatusId;
                }

                foreach (var testType in testModel.TestTypes)
                {
                    //check to see if this type is allowed by this client
                    var tt = connection.Clients.Single(f => f.ClientId == testModel.ClientId).TestTypes.SingleOrDefault(f => f.TestTypeId == testType.TestTypeId);

                    if (tt == null)
                    {
                        throw new Exception("The TestTypeId: " + testType.TestTypeId + " is not valid for client: " + testModel.ClientId);
                    }
                    else
                    {
                        //now check to see if it exists on the test
                        if (test.TestTypes.Contains(tt) == false)
                        {
                            test.TestTypes.Add(tt);
                        }
                    }
                }

                foreach (var successMetric in testModel.SuccessMetrics)
                {
                    //check to see if this type is allowed by this client
                    var sm = connection.Clients
                        .Single(f => f.ClientId == testModel.ClientId)
                        .SuccessMetrics
                        .SingleOrDefault(f => f.SuccessMetricId == successMetric.SuccessMetricId);

                    if (sm == null)
                    {
                        throw new Exception("The TestTypeId: " + successMetric.SuccessMetricId + " is not valid for client: " + testModel.ClientId);
                    }
                    else
                    {
                        //now check to see if it exists on the test
                        if (test.SuccessMetrics.Contains(sm) == false)
                        {
                            test.SuccessMetrics.Add(sm);
                        }
                    }
                }

                if (testModel.TestId <= 0)
                {
                    connection.Tests.Add(test);
                }

                connection.SaveChanges();
            }
        }

        public TestModel GetTest(int testId)
        {
            using (var connection = new TestAndLearnEntitiesConnection())
            {
                var test = (from t in connection.Tests
                            .Include("Client")
                            .Include("TestStatu")
                            .Include("TestTypes")
                            .Include("SuccessMetrics")
                             where
                                 t.TestId == testId
                             select new TestModel()
                             {
                                 TestId = t.TestId,
                                 ClientId = t.ClientId,
                                 Name = t.Name,
                                 SubmittedOn = t.SubmittedOn,
                                 SubmittedBy = t.SubmittedBy,
                                 Hypothesis = t.Hypothesis,
                                 AdditionalNotes = t.AdditionalNotes,
                                 ExpectedBusinessValue = t.ExpectedBusinessValue,
                                 Quarter = t.Quarter,
                                 Year = t.Year,
                                 Status = t.TestStatu.Name,
                                 StartDate = t.StartDate,
                                 EndDate = t.EndDate,
                                 Outcome = t.Outcome,
                                 Recommendation = t.Recommendation,
                                 TestTypes = (from tt in t.TestTypes select new TestTypeModel() { TestTypeId = tt.TestTypeId, Name = tt.Name }).ToList(),
                                 SuccessMetrics = (from sm in t.SuccessMetrics select new SuccessMetricModel() { SuccessMetricId = sm.SuccessMetricId, Name = sm.Name }).ToList()
                             }).SingleOrDefault();


                return test;
            }
        }

        public IList<TestModel> GetTests(int clientId)
        {
            using (var connection = new TestAndLearnEntitiesConnection())
            {
                var tests = (from t in connection.Tests
                            .Include("Client")
                            .Include("TestStatu")
                            .Include("TestTypes")
                            .Include("SuccessMetrics")
                            where 
                                t.ClientId == clientId
                            select new TestModel()
                            {
                                TestId = t.TestId,
                                ClientId = t.ClientId,
                                Name = t.Name,
                                SubmittedOn = t.SubmittedOn,
                                SubmittedBy = t.SubmittedBy,
                                Hypothesis = t.Hypothesis,
                                AdditionalNotes = t.AdditionalNotes,
                                ExpectedBusinessValue = t.ExpectedBusinessValue,
                                Quarter = t.Quarter,
                                Year = t.Year,
                                Status = t.TestStatu.Name,
                                StartDate = t.StartDate,
                                EndDate = t.EndDate,
                                Outcome = t.Outcome,
                                Recommendation = t.Recommendation,
                                TestTypes = (from tt in t.TestTypes select new TestTypeModel() { TestTypeId = tt.TestTypeId, Name = tt.Name }).ToList(),
                                SuccessMetrics = (from sm in t.SuccessMetrics select new SuccessMetricModel() { SuccessMetricId = sm.SuccessMetricId, Name = sm.Name }).ToList()
                            }).ToList();


                return tests;
            }
        }

        public IList<SuccessMetricModel> GetSuccessMetrics(int clientId)
        {
            using (var connection = new TestAndLearnEntitiesConnection())
            {
                var successMetrics = (from sm in connection.Clients.Single(f => f.ClientId == clientId).SuccessMetrics
                                     select new SuccessMetricModel()
                                     {
                                         SuccessMetricId = sm.SuccessMetricId,
                                         Name = sm.Name
                                     }).ToList();

                return successMetrics;
            }
        }

        public IList<TestTypeModel> GetTestTypes(int clientId)
        {
            using (var connection = new TestAndLearnEntitiesConnection())
            {
                var testTypes = (from tt in connection.Clients.Single(f => f.ClientId == clientId).TestTypes
                                 select new TestTypeModel()
                                 {
                                     TestTypeId = tt.TestTypeId,
                                     Name = tt.Name
                                 }).ToList();

                return testTypes;
            }
        }

        public void ChangeStatus(int testId, int testStatusId)
        {
            using (var connection = new TestAndLearnEntitiesConnection())
            {
                var test = connection.Tests.Single(f => f.TestId == testId);
                test.TestStatusId = testStatusId;

                connection.SaveChanges();
            }
        }
    }
}

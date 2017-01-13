﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestAndLearn.Domain.Models;

namespace TestAndLearn.Domain
{
    public interface ITestRepository
    {
        void SaveBaseTest(TestBaseModel testModel);
        TestModel GetTest(int testId);
        IList<TestModel> GetTests(int clientId);
        IList<TestModel> GetTests(int clientId, int statusId);
        IList<TestModel> GetTests(IList<int> testIds);
        
        IList<TestModel> GetTests(FilterTestModel filterModel);

        IList<SuccessMetricModel> GetSuccessMetrics(int clientId);
        IList<TestTypeModel> GetTestTypes(int clientId);
        IList<PillarModel> GetPillars(int clientId);

        void ChangeStatus(int testId, int testStatusId);

        void CloseTest(TestOutcomeModel outcomeModel);

        void RankTest(TestRankModel rank);
        void SubmitRunDates(TestRunDatesModel model);
        int GetRank(int testId);

        void DeleteTest(int TestId);

        void addUrls(TestUrlModel urls);
        
        TestUrlModel GetUrls(int TestId);
    }
}

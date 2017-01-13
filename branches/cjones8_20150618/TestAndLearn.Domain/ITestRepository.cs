using System;
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

        IList<SuccessMetricModel> GetSuccessMetrics(int clientId);
        IList<TestTypeModel> GetTestTypes(int clientId);

        void ChangeStatus(int testId, int testStatusId);

        void CloseTest(TestOutcomeModel outcomeModel);
    }
}

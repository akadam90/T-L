using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestAndLearn.Domain.Models;

namespace TestAndLearn.Web.Models
{
    public class TestFormModel
    {
        public int ClientId { get; set; }
        public string SuggesstedSubmitter { get; set; }
        public IList<TestTypeModel> TestTypes { get; set; }
        public IList<SuccessMetricModel> SuccessMetrics { get; set; }
        public IList<PillarModel> Pillars { get; set; }
    }
}
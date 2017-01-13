﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAndLearn.Domain.Models
{
    public class TestBaseModel
    {
        public int TestId { get; set; }

        public int ClientId { get; set; }

        public string Name { get; set; }
        public string SubmittedBy { get; set; }
        public DateTime SubmittedOn { get; set; }

        public IList<SuccessMetricModel> SuccessMetrics { get; set; }
        public IList<TestTypeModel> TestTypes { get; set; }

        public string Hypothesis { get; set; }
        public string AdditionalNotes { get; set; }

        public string ExpectedBusinessValue { get; set; }

        public string Quarter { get; set; }
        public int? Year { get; set; }

        public string Status { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAndLearn.Domain.Models
{
    public class FilterTestModel
    {
        public int ClientId { get; set; }
        public int TestStatusId { get; set; }
        public string SubmittedOnQuery { get; set; }
        public string NameQuery { get; set; }
        public string SubmittedByQuery { get; set; }
        public string TestTypesAsStringQuery { get; set; }
        public string SuccessMetricsQuery { get; set; }
        public string QuarterQuery { get; set; }
        public string YearQuery { get; set; }
        public string RankQuery { get; set; }
        public string PillarQuery { get; set; }
        public string StartDateQuery { get; set; }
        public string EndDateQuery { get; set; }
    }
}

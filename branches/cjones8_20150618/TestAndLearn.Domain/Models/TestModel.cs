using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAndLearn.Domain.Models
{
    public class TestModel : TestBaseModel
    {
        public string Outcome { get; set; }
        public string Recommendation { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public int? LevelOfEffortToImplement { get; set; }
        public int? LevelOfEffortToReportOn { get; set; }
        public int? LevelOfBudget { get; set; }
        public int? LevelOfImpact { get; set; }

        public int Rank
        {
            get
            {
                var w = this.LevelOfEffortToImplement.HasValue ? this.LevelOfEffortToImplement.Value : 0;
                var x = this.LevelOfEffortToReportOn.HasValue ? this.LevelOfEffortToReportOn.Value : 0;
                var y = this.LevelOfBudget.HasValue ? this.LevelOfBudget.Value : 0;
                var z = this.LevelOfImpact.HasValue ? this.LevelOfImpact.Value : 0;

                return w + x + y + z;
            }
        }
    }
}

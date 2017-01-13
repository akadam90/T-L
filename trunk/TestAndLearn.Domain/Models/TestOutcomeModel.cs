using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAndLearn.Domain.Models
{
    public class TestOutcomeModel
    {
        public int TestId { get; set; }
        public string Outcome { get; set; }
        public string Recommendation { get; set; }
    }
}

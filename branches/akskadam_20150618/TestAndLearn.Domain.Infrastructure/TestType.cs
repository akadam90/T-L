//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TestAndLearn.Domain.Infrastructure
{
    using System;
    using System.Collections.Generic;
    
    public partial class TestType
    {
        public TestType()
        {
            this.Clients = new HashSet<Client>();
            this.Tests = new HashSet<Test>();
        }
    
        public int TestTypeId { get; set; }
        public string Name { get; set; }
    
        public virtual ICollection<Client> Clients { get; set; }
        public virtual ICollection<Test> Tests { get; set; }
    }
}

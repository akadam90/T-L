using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAndLearn.Domain.Models
{
    public class UserModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }

        public IList<ClientModel> Clients { get; set; }

        public string ClientsAsString
        {
            get
            {
                if (this.Clients == null)
                {
                    return "";
                }

                var s = "";

                foreach (var c in this.Clients)
                {
                    s += c.ClientName + ",";
                }

                if (!string.IsNullOrEmpty(s))
                {
                    s = s.Remove(s.Length - 1);
                }

                return s;
            }
        }

        public UserSettingsModel UserSettings { get; set; }
    }
}

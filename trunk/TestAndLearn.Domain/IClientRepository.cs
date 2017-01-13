using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestAndLearn.Domain.Models;

namespace TestAndLearn.Domain
{
    public interface IClientRepository
    {
        IList<ClientModel> GetClients();
        ClientModel GetClient(int clientId);
    }
}

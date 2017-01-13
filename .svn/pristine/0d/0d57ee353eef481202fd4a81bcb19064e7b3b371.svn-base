using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestAndLearn.Domain.Models;

namespace TestAndLearn.Domain.Infrastructure
{
    public class ClientRepository : IClientRepository
    {
        public IList<ClientModel> GetClients()
        {
            using (var connection = new TestAndLearnEntitiesConnection())
            {
                var clients = from c in connection.Clients
                              select new ClientModel()
                              {
                                  ClientId = c.ClientId,
                                  ClientName = c.ClientName,
                                  LogoImageName = c.LogoImageName
                              };

                return clients.ToList();
            }
        }


        public ClientModel GetClient(int clientId)
        {
            using (var connection = new TestAndLearnEntitiesConnection())
            {
                var client = connection.Clients
                    .Single(f => f.ClientId == clientId);

                var clientModel = new ClientModel()
                {
                    ClientId = client.ClientId,
                    ClientName = client.ClientName,
                    LogoImageName = client.LogoImageName
                };

                return clientModel;
            }
        }
    }
}

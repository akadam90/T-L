using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestAndLearn.Domain.Models;

namespace TestAndLearn.Domain.Infrastructure
{
    public class EventRepository : IEventRepository
    {
        public void SaveEvent(EventBaseModel EventModel)
        { 
        
            using (var connection = new TestAndLearnEntitiesConnection()){
                Event ev = null;

                if (EventModel.EventId > 0) //update
                {
                    ev = connection.Events.SingleOrDefault(e => e.EventId == EventModel.EventId);
                    if (ev == null)
                    {
                        throw new Exception("Could not find the Event with id "+EventModel.EventId+" in the database");
                    }
                    else
                    {
                        ev.Name = EventModel.EventName;
                        ev.ClientId = EventModel.ClientId;
                        ev.Description = EventModel.EventDescription;
                        ev.StartDate = EventModel.StartDate;
                        ev.EndDate = EventModel.EndDate;

                    }


                }
                else     //create new Event
                {

                    ev = new Event();
                    ev.ClientId = EventModel.ClientId;
                    ev.Name = EventModel.EventName;
                    ev.Description = EventModel.EventDescription;
                    ev.StartDate = EventModel.StartDate;
                    ev.EndDate = EventModel.EndDate;

                    connection.Events.Add(ev);

                }

                connection.SaveChanges();
            };
        }

        public EventBaseModel GetEvent(int EventId)
        {
            using (var connection = new TestAndLearnEntitiesConnection()) {

                var ev = (from e in connection.Events
                          where (e.EventId == EventId)
                          select new EventBaseModel() { 
                          EventId = e.EventId,
                          ClientId = e.ClientId,
                          EventDescription = e.Description,
                          EventName = e.Name,
                          StartDate = e.StartDate,
                          EndDate = e.EndDate
                          }).SingleOrDefault();
                return ev;
                
            };


        }



        public IList<EventBaseModel> GetEvents(int ClientId)
        {

            using (var connection = new TestAndLearnEntitiesConnection()) {

                var events = (from e in connection.Events
                              where (e.ClientId == ClientId)
                              select new EventBaseModel() { 
                              EventId = e.EventId,
                              ClientId = e.ClientId,
                              EventName = e.Name,
                              EventDescription = e.Description,
                              StartDate = e.StartDate,
                              EndDate = e.EndDate
                              }).ToList();

                return events;
            };
        }

        public void DeleteEvent(int id)
        {
            using (var connection = new TestAndLearnEntitiesConnection()) {

                var ev = connection.Events.SingleOrDefault(e=>e.EventId==id);
                connection.Events.Remove(ev);
                
                connection.SaveChanges();
            };

        }
    }
}

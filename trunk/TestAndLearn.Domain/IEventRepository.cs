﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestAndLearn.Domain.Models;

namespace TestAndLearn.Domain
{
    public interface IEventRepository
    {

        void SaveEvent(EventBaseModel ev);
        IList<EventBaseModel> GetEvents(int ClientId);
        EventBaseModel GetEvent(int EventId);
        void DeleteEvent(int id);
    
    }
}

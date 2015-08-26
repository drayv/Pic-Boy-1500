using System.Collections.Generic;
using PicBoy.Core.DataAccess;
using PicBoy.Core.Models;

namespace PicBoy.Core.Logic
{
    /// <summary>
    /// Represents business logic of creating and reading events.
    /// </summary>
    public class EventWorker
    {
        public List<Event> GetAll()
        {
            using (var repo = new EventRepository())
            {
                return repo.GetAll();
            }
        }   
    }
}
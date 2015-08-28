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
        /// <summary>
        /// Gets events from storage.
        /// </summary>
        /// <returns>Event list.</returns>
        public List<Event> GetAll()
        {
            using (var repo = new EventRepository())
            {
                return repo.GetAll();
            }
        }

        /// <summary>
        /// Saves event to storage.
        /// </summary>
        /// <param name="newEvent">Entity to save.</param>
        /// <returns>The number of events, that does not have saved. Return 0 if everising OK.</returns>
        public int AddEvent(Event newEvent)
        {
            using (var repo = new EventRepository())
            {
                repo.Insert(newEvent);
                return repo.Save();
            }
        }
    }
}
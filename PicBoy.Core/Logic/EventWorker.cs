using System;
using System.Collections.Generic;
using System.Linq;
using PicBoy.Core.DataAccess;
using PicBoy.Core.Models;

namespace PicBoy.Core.Logic
{
    /// <summary>
    /// Represents business logic of creating, changing and reading events.
    /// </summary>
    public class EventWorker
    {
        const string DateFormat = "dd:MM:yyyy HH:mm";

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
            ValidateEvent(newEvent);
            using (var repo = new EventRepository())
            {
                repo.Insert(newEvent);
                return repo.Save();
            }
        }

        public int DeleteEvent(Event eventToDelete)
        {
            using (var repo = new EventRepository())
            {
                repo.Delete(eventToDelete);
                return repo.Save();
            }
        }

        public int SaveEventChanges(Event eventToChange)
        {
            ValidateEvent(eventToChange);
            using (var repo = new EventRepository())
            {
                repo.Update(eventToChange);
                return repo.Save();
            }
        }

        public string CheckIntersectionDates(Event newEvent)
        {
            using (var repo = new EventRepository())
            {
                var intersectDates = repo.GetAll(e => (e.DateBegin < newEvent.DateEnd && newEvent.DateBegin < e.DateEnd));

                if (intersectDates.Count == 0)
                {
                    return string.Empty;
                }

                var message = "Some events already exists in this period." + Environment.NewLine + "...";
                message = intersectDates.Take(5).Aggregate(message, (current, eventDate) => current +
                (eventDate.Name + " " + eventDate.DateBegin.ToString(DateFormat) + " - " + eventDate.DateEnd.ToString(DateFormat) + ", "));
                if (intersectDates.Count > 5)
                {
                    message += "and others, ";
                }
                message = message.Remove(message.Length - 2, 2) + ".";

                message += Environment.NewLine + Environment.NewLine + "Do you really want to save another event on this period?";
                return message;
            }
        }

        private static void ValidateEvent(Event eventToValidate)
        {
            var hasError = false;
            var errorMessage = "";

            if (eventToValidate.Name.Length > 50)
            {
                AddNewLineIfNotEmpty(ref errorMessage);
                errorMessage += "Event name can't be larger than 50 symbols!";
                hasError = true;
            }

            if (eventToValidate.Name.Equals(string.Empty))
            {
                AddNewLineIfNotEmpty(ref errorMessage);
                errorMessage += "Please fill the name of the event!";
                hasError = true;
            }

            if (eventToValidate.DateBegin < DateTime.Now)
            {
                AddNewLineIfNotEmpty(ref errorMessage);
                errorMessage += "The event must starts later than the current date: " + DateTime.Now.ToString(DateFormat) + "!";
                hasError = true;
            }

            if (eventToValidate.DateEnd < eventToValidate.DateBegin)
            {
                AddNewLineIfNotEmpty(ref errorMessage);
                errorMessage += "End date of the event must be greater than the start date!";
                hasError = true;
            }

            if (hasError)
            {
                throw new EventValidationException(errorMessage);
            }
        }

        private static void AddNewLineIfNotEmpty(ref string message)
        {
            if (!message.Equals(string.Empty))
            {
                message += Environment.NewLine;
            }
        }

        public class EventValidationException : Exception
        {
            public EventValidationException(string message) : base(message)
            {
            }
        }
    }
}
using System;

namespace PicBoy.Core.Models
{
    /// <summary>
    /// Event and information about it.
    /// </summary>
    public class Event : IEquatable<Event>
    {
        /// <summary>
        /// Event Id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Event name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Date and time when event starts.
        /// </summary>
        public DateTime DateBegin { get; set; }

        /// <summary>
        /// Date and time when event ends.
        /// </summary>
        public DateTime DateEnd { get; set; }

        /// <summary>
        /// Determines whether the specified event is equal to the current event.
        /// </summary>
        /// <param name="other">Event to equal.</param>
        /// <returns>True if the specified event is equal to the current event; otherwise, false.</returns>
        public bool Equals(Event other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id == other.Id && string.Equals(Name, other.Name) && DateBegin.Equals(other.DateBegin) && DateEnd.Equals(other.DateEnd);
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current event.
        /// </summary>
        /// <param name="obj">Object to equal.</param>
        /// <returns>True if the specified object is equal to the current event; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((Event) obj);
        }

        /// <summary>
        /// Serves as the  hash function.
        /// </summary>
        /// <returns>Event object hash.</returns>
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Id;
                hashCode = (hashCode*397) ^ (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ DateBegin.GetHashCode();
                hashCode = (hashCode*397) ^ DateEnd.GetHashCode();
                return hashCode;
            }
        }

        /// <summary>
        /// Overrides "==".
        /// </summary>
        public static bool operator ==(Event left, Event right)
        {
            return Equals(left, right);
        }

        /// <summary>
        /// Overrides "!=".
        /// </summary>
        public static bool operator !=(Event left, Event right)
        {
            return !Equals(left, right);
        }
    }
}
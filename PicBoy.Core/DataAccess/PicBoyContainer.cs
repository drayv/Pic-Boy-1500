using System.Data.Entity;

namespace PicBoy.Core.DataAccess
{
    /// <summary>
    /// Represents a Unit Of Work with PicBoy database.
    /// </summary>
    class PicBoyContainer : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PicBoyContainer"/> class.
        /// </summary>
        public PicBoyContainer()
            : base("name=PicBoy")
        {
        }
    }
}
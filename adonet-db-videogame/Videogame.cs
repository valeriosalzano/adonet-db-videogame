using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace adonet_db_videogame
{
    internal class Videogame
    {
        private string name;
        public long Id { get; private set; }
        public string Name {
            get { return name; }
            private set 
            {
                if (string.IsNullOrEmpty(value) || value.Trim() == "")
                    throw new Exception("Videogame name is required.");
                else name = value;
            } 
        }
        public string Overview { get; private set; } = "";
        public DateTime ReleaseDate { get; private set; }

        public long SoftwareHouseId { get; private set; }

        public Videogame(long id, string name, string overview, DateTime releaseDate, long softwareHouseId)
        {
            Id = id;
            Name = name;
            Overview = overview;
            ReleaseDate = releaseDate;
            SoftwareHouseId = softwareHouseId;
        }

        public override string ToString()
        {
            return $"ID: {Id}, {Name} - released on {ReleaseDate.ToString("dd/MM/yyyy")} by SH ID: {SoftwareHouseId}.";
        }
    }
}

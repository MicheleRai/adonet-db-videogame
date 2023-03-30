using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace adonet_db_videogame
{
     public record Videogame
    {
        public Videogame(long? id, string name, string overview, DateTime release_date, int softweare_house_id)
        {
            Id = id;
            Name = name;
            Overview = overview;
            Release_date = release_date;
            Softweare_house_id = softweare_house_id;
        }

        public long? Id { get; set; }
        public string Name { get; set; }
        public string Overview { get; set; }
        public DateTime Release_date { get; set; }
        public int Softweare_house_id { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace WeatherLiz.Models
{
    public class Favoritos
    {
        [PrimaryKey, AutoIncrement]

        public int Id { get; set; }
        public string City { get; set; }
    
        public string Country { get; set; }

        public string Temperature { get; set; } 
        public string Icon { get; set; }    

        public string Description { get; set;  } 

        public string Time { get; set; } 
    }
}

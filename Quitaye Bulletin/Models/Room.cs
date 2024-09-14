using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quitaye_School.Models
{
    public class Room
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<TimeSlot> Availability { get; set; }
    }
}

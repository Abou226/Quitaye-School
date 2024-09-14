using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Calendrier_Classe
    {
        public int TeacherId { get; set; }
        public int RoomId { get; set; }
        public int GradeId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quitaye_School.Models
{
    public class TimetableManager
    {
        private List<Professeur> teachers;
        private List<Room> rooms;
        private List<Calendrier_Classe> schedule;

        public TimetableManager()
        {
            teachers = new List<Professeur>();
            rooms = new List<Room>();
            schedule = new List<Calendrier_Classe>();
        }

        public void AddTeacher(Professeur teacher)
        {
            teachers.Add(teacher);
        }

        public void AddRoom(Room room)
        {
            rooms.Add(room);
        }

        public bool ScheduleClass(int teacherId, int roomId, DateTime startTime, DateTime endTime)
        {
            // Check teacher and room availability
            var teacher = teachers.Find(t => t.Id == teacherId);
            var room = rooms.Find(r => r.Id == roomId);

            if (teacher == null || room == null)
            {
                Console.WriteLine("Prof ou class non trouvé.");
                return false;
            }

            // Check if the time slot is available for both teacher and room
            bool isTeacherAvailable = teacher.Availability.Exists(slot =>
                slot.StartTime <= startTime && slot.EndTime >= endTime);
            bool isRoomAvailable = room.Availability.Exists(slot =>
                slot.StartTime <= startTime && slot.EndTime >= endTime);

            if (!isTeacherAvailable || !isRoomAvailable)
            {
                Console.WriteLine("Prof ou class n'est pas available en ce moment.");
                return false;
            }

            // Schedule the class
            var classSchedule = new Calendrier_Classe
            {
                TeacherId = teacherId,
                RoomId = roomId,
                StartTime = startTime,
                EndTime = endTime
            };

            schedule.Add(classSchedule);

            return true; // Return true if the class was scheduled successfully.
        }
    }
}

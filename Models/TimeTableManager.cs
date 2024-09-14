using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class TimetableManager
    {
        private List<Professeur> teachers;
        private List<Room> rooms;
        private List<Calendrier_Classe> schedule;
        private List<Classe> grades;

        public TimetableManager()
        {
            teachers = new List<Professeur>();
            rooms = new List<Room>();
            schedule = new List<Calendrier_Classe>();
            grades = new List<Classe>();
        }

        public void AddTeacher(Professeur teacher)
        {
            teachers.Add(teacher);
        }

        public void AddRoom(Room room)
        {
            rooms.Add(room);
        }

        public void AddGrade(Classe grade)
        {
            grades.Add(grade);
        }

        public List<Calendrier_Classe> GetTeacherTimetable(int teacherId)
        {
            return schedule.Where(scheduleItem => scheduleItem.TeacherId == teacherId).ToList();
        }

        public List<Calendrier_Classe> GetRoomTimetable(int roomId)
        {
            return schedule.Where(scheduleItem => scheduleItem.RoomId == roomId).ToList();
        }

        public List<Calendrier_Classe> GetGradeTimetable(int gradeId)
        {
            return schedule.Where(scheduleItem => scheduleItem.GradeId == gradeId).ToList();
        }

        public bool ScheduleClass(int teacherId, int roomId, int gradeId, DateTime startTime, DateTime endTime)
        {
            // Check teacher, room, and grade existence
            Professeur teacher = teachers.Find(t => t.Id == teacherId);
            Room room = rooms.Find(r => r.Id == roomId);
            Classe grade = grades.Find(g => g.Id == gradeId);

            if (teacher == null || room == null || grade == null)
            {
                Console.WriteLine("Teacher, room, or grade not found.");
                return false;
            }

            // Check if the time slot is available for teacher, room, and grade
            bool isTeacherAvailable = teacher.Availability.Exists(slot =>
                slot.StartTime <= startTime && slot.EndTime >= endTime);

            bool isRoomAvailable = room.Availability.Exists(slot =>
                slot.StartTime <= startTime && slot.EndTime >= endTime);

            bool isGradeAvailable = !schedule.Exists(scheduleItem =>
                scheduleItem.GradeId == gradeId &&
                ((scheduleItem.StartTime <= startTime && scheduleItem.EndTime >= startTime) ||
                (scheduleItem.StartTime <= endTime && scheduleItem.EndTime >= endTime)));

            if (!isTeacherAvailable || !isRoomAvailable || !isGradeAvailable)
            {
                Console.WriteLine("Teacher, room, or grade is not available at that time.");
                return false;
            }

            // Schedule the class
            Calendrier_Classe classSchedule = new Calendrier_Classe
            {
                TeacherId = teacherId,
                RoomId = roomId,
                GradeId = gradeId,
                StartTime = startTime,
                EndTime = endTime
            };

            schedule.Add(classSchedule);
            Console.WriteLine("Class scheduled successfully.");
            return true; // Return true if the class was scheduled successfully.
        }
    }
}

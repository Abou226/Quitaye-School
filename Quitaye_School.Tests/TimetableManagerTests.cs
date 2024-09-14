using Models;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quitaye_School.Tests
{
    public class TimetableManagerTests
    {
        public static IEnumerable<object[]> SuccessfulTestData()
        {
            yield return new object[]
            {
            new Professeur { Id = 1, Name = "Teacher 1", Availability = new List<TimeSlot>
            {
                new TimeSlot { StartTime = DateTime.Parse("2023-09-26 08:00"), EndTime = DateTime.Parse("2023-09-26 12:00") }
            }},
            new Room { Id = 1, Name = "Room 101", Availability = new List<TimeSlot>
            {
                new TimeSlot { StartTime = DateTime.Parse("2023-09-26 08:00"), EndTime = DateTime.Parse("2023-09-26 12:00") }
            }},
            new Classe { Id = 1, Name = "Grade X" },
            DateTime.Parse("2023-09-26 09:00"),
            DateTime.Parse("2023-09-26 11:00"),
            true // Expected result
            };
        }

        [Theory]
        [MemberData(nameof(SuccessfulTestData))]
        public void ShouldAddSchedule_WhenTeacherAndRoom_AreAvailable(Professeur teacher, Room room, Classe grade, DateTime startTime, DateTime endTime, bool expectedResult)
        {
            // Arrange
            TimetableManager timetableManager = new TimetableManager();
            timetableManager.AddTeacher(teacher);
            timetableManager.AddRoom(room);
            timetableManager.AddGrade(grade);

            // Act
            bool result = timetableManager.ScheduleClass(teacher.Id, room.Id, grade.Id, startTime, endTime);

            // Assert
            Assert.Equal(expectedResult, result);
        }

        public static IEnumerable<object[]> FailureTestData()
        {
            yield return new object[]
            {
            new Professeur { Id = 1, Name = "Teacher 1", Availability = new List<TimeSlot>() },
            new Room { Id = 1, Name = "Room 101", Availability = new List<TimeSlot>() },
            new Classe { Id = 1, Name = "Grade X" },
            DateTime.Parse("2023-09-26 09:00"),
            DateTime.Parse("2023-09-26 11:00"),
            false // Expected result
            };
        }

        [Theory]
        [MemberData(nameof(FailureTestData))]
        public void ShouldNotAddSchedule_WhenTeacherOrRoom_AreNotAvailable(Professeur teacher, Room room, Classe grade, DateTime startTime, DateTime endTime, bool expectedResult)
        {
            // Arrange
            TimetableManager timetableManager = new TimetableManager();
            timetableManager.AddTeacher(teacher);
            timetableManager.AddRoom(room);
            timetableManager.AddGrade(grade);

            // Act
            bool result = timetableManager.ScheduleClass(teacher.Id, room.Id, grade.Id, startTime, endTime);

            // Assert
            Assert.Equal(expectedResult, result);
        }

        public static IEnumerable<object[]> TeacherTimetableTestData()
        {
            // Test data: teacherId, roomId, gradeId, startTime, endTime, expectedClassCount
            yield return new object[] { 1, 1, 1, DateTime.Parse("2023-09-26 09:00"), DateTime.Parse("2023-09-26 11:00"), 1 };
            yield return new object[] { 2, 1, 1, DateTime.Parse("2023-09-26 10:00"), DateTime.Parse("2023-09-26 12:00"), 0 };
            // Add more test cases as needed
        }

        [Theory]
        [MemberData(nameof(TeacherTimetableTestData))]
        public void GetTeacherTimetableForSpecificRoom_ShouldReturnTeacherTimetableInRoom(int teacherId, int roomId, int gradeId, DateTime startTime, DateTime endTime, int expectedClassCount)
        {
            // Arrange
            var timetableService = new TimetableManager();
            Professeur teacher = new Professeur
            {
                Id = 1,
                Name = "Teacher 1",
                Availability = new List<TimeSlot>
        {
            new TimeSlot { StartTime = DateTime.Parse("2023-09-26 08:00"), EndTime = DateTime.Parse("2023-09-26 12:00") }
        }
            };
            Room room = new Room
            {
                Id = 1,
                Name = "Room 101",
                Availability = new List<TimeSlot>
        {
            new TimeSlot { StartTime = DateTime.Parse("2023-09-26 08:00"), EndTime = DateTime.Parse("2023-09-26 12:00") }
        }
            };
            var grade = new Classe { Id = 1, Name = "Grade X" };

            teacher.Availability.Add(new TimeSlot { StartTime = DateTime.Parse("2023-09-26 08:00"), EndTime = DateTime.Parse("2023-09-26 12:00") });
            room.Availability.Add(new TimeSlot { StartTime = DateTime.Parse("2023-09-26 08:00"), EndTime = DateTime.Parse("2023-09-26 12:00") });
            timetableService.AddTeacher(teacher);
            timetableService.AddRoom(room);
            timetableService.AddGrade(grade);

            // Schedule a class
            timetableService.ScheduleClass(teacherId, roomId, gradeId, startTime, endTime);

            // Act
            List<Calendrier_Classe> teacherTimetableInRoom = timetableService.GetTeacherTimetable(teacherId);

            // Assert
            Assert.Equal(expectedClassCount, teacherTimetableInRoom.Count);
        }

        public static IEnumerable<object[]> GradeTimetableTestData()
        {
            // Test data: gradeId, roomId, startTime, endTime, expectedClassCount
            yield return new object[] { 1, 1, DateTime.Parse("2023-09-26 09:00"), DateTime.Parse("2023-09-26 11:00"), 1 };
            yield return new object[] { 1, 2, DateTime.Parse("2023-09-26 10:00"), DateTime.Parse("2023-09-26 12:00"), 0 };
            // Add more test cases as needed
        }

        [Theory]
        [MemberData(nameof(GradeTimetableTestData))]
        public void GetGradeTimetableForSpecificRoom_ShouldReturnGradeTimetableInRoom(int gradeId, int roomId, DateTime startTime, DateTime endTime, int expectedClassCount)
        {
            // Arrange
            var timetableService = new TimetableManager();
            var teacher = new Professeur
            {
                Id = 1,
                Name = "Teacher 1",
                Availability = new List<TimeSlot>
        {
            new TimeSlot { StartTime = DateTime.Parse("2023-09-26 08:00"), EndTime = DateTime.Parse("2023-09-26 12:00") }
        }
            };
            Room room = new Room
            {
                Id = 1,
                Name = "Room 101",
                Availability = new List<TimeSlot>
        {
            new TimeSlot { StartTime = DateTime.Parse("2023-09-26 08:00"), EndTime = DateTime.Parse("2023-09-26 12:00") }
        }
            };
            var grade = new Classe { Id = 1, Name = "Grade X" };

            teacher.Availability.Add(new TimeSlot { StartTime = DateTime.Parse("2023-09-26 08:00"), EndTime = DateTime.Parse("2023-09-26 12:00") });
            room.Availability.Add(new TimeSlot { StartTime = DateTime.Parse("2023-09-26 08:00"), EndTime = DateTime.Parse("2023-09-26 12:00") });
            timetableService.AddTeacher(teacher);
            timetableService.AddRoom(room);
            timetableService.AddGrade(grade);

            // Schedule a class
            timetableService.ScheduleClass(1, roomId, gradeId, startTime, endTime);

            // Act
            var gradeTimetableInRoom = timetableService.GetGradeTimetable(gradeId);

            // Assert
            Assert.Equal(expectedClassCount, gradeTimetableInRoom.Count);
        }
    }
}

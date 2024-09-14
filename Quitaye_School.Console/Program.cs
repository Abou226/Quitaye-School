// See https://aka.ms/new-console-template for more information

// Create a TimetableManager
using Models;

TimetableManager timetableManager = new TimetableManager();

// Add teachers and rooms with their availability
Professeur teacher1 = new Professeur { Id = 1, Name = "Teacher 1", Availability = new List<TimeSlot>() };
Room room1 = new Room { Id = 1, Name = "Room 101", Availability = new List<TimeSlot>() };

// Set availability for teacher and room
teacher1.Availability.Add(new TimeSlot { StartTime = DateTime.Parse("2023-09-26 08:00"), EndTime = DateTime.Parse("2023-09-26 12:00") });
room1.Availability.Add(new TimeSlot { StartTime = DateTime.Parse("2023-09-26 08:00"), EndTime = DateTime.Parse("2023-09-26 12:00") });

timetableManager.AddTeacher(teacher1);
timetableManager.AddRoom(room1);

// Schedule a class
DateTime classStartTime = DateTime.Parse("2023-09-26 09:00");
DateTime classEndTime = DateTime.Parse("2023-09-26 11:00");

//timetableManager.ScheduleClass(1, 1, classStartTime, classEndTime);



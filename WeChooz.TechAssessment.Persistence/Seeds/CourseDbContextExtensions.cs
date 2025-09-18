using Microsoft.EntityFrameworkCore;
using WeChooz.TechAssessment.Domain.Entities;
using WeChooz.TechAssessment.Domain.Enums;

namespace WeChooz.TechAssessment.Persistence.Seeds;

public static class CourseDbContextExtensions
{
    public static void CreateDatabase(this CourseDbContext dbContext)
    {
        dbContext.Database.Migrate();

        if (dbContext.Trainers.Any())
        {
            return;
        }

        var (trainer1, trainer2, trainer3) = SeedTrainers(dbContext.Trainers);
        dbContext.SaveChanges();

        var (course1, course2, course3) = SeedCourses(dbContext.Courses, trainer1, trainer2, trainer3);
        dbContext.SaveChanges();

        SeedSessions(dbContext.Sessions, course1, course2, course3);
        dbContext.SaveChanges();
        
        var sessions = dbContext.Sessions.ToList();
        
        SeedParticipants(dbContext.Participants, sessions);
        dbContext.SaveChanges();
    }

    private static void SeedParticipants(DbSet<Participant> participants, IEnumerable<Session> sessions)
    {
        var counter = 1;

        foreach (var session in sessions)
        {
            participants.Add(new Participant
            {
                SessionId = session.Id,
                FirstName = $"Alice{counter}",
                LastName = "Johnson",
                Email = $"alice{counter}@example.com",
                CompanyName = "TechCorp"
            });

            counter++;

            participants.Add(new Participant
            {
                SessionId = session.Id,
                FirstName = $"Bob{counter}",
                LastName = "Smith",
                Email = $"bob{counter}@example.com",
                CompanyName = "InnoSoft"
            });

            counter++;

            participants.Add(new Participant
            {
                SessionId = session.Id,
                FirstName = $"Charlie{counter}",
                LastName = "Brown",
                Email = $"charlie{counter}@example.com",
                CompanyName = "GlobalSolutions"
            });

            counter++;
        }
    }

    private static void SeedSessions(DbSet<Session> sessions, Course course1, Course course2, Course course3)
    {
        // Sessions for course1
        sessions.Add(new Session
        {
            CourseId = course1.Id,
            StartDate = new DateTime(2025, 10, 1, 9, 0, 0),
            Duration = 180, // 3 hours in minutes
            DeliveryMode = DeliveryMode.Remote
        });
        sessions.Add(new Session
        {
            CourseId = course1.Id,
            StartDate = new DateTime(2025, 10, 8, 9, 0, 0),
            Duration = 180,
            DeliveryMode = DeliveryMode.OnSite
        });
        sessions.Add(new Session
        {
            CourseId = course1.Id,
            StartDate = new DateTime(2025, 10, 15, 9, 0, 0),
            Duration = 180,
            DeliveryMode = DeliveryMode.Remote
        });

        // Sessions for course2
        sessions.Add(new Session
        {
            CourseId = course2.Id,
            StartDate = new DateTime(2025, 11, 1, 10, 0, 0),
            Duration = 120, // 2 hours in minutes
            DeliveryMode = DeliveryMode.OnSite
        });
        sessions.Add(new Session
        {
            CourseId = course2.Id,
            StartDate = new DateTime(2025, 11, 8, 10, 0, 0),
            Duration = 120,
            DeliveryMode = DeliveryMode.Remote
        });
        sessions.Add(new Session
        {
            CourseId = course2.Id,
            StartDate = new DateTime(2025, 11, 15, 10, 0, 0),
            Duration = 120,
            DeliveryMode = DeliveryMode.OnSite
        });

        // Sessions for course3
        sessions.Add(new Session
        {
            CourseId = course3.Id,
            StartDate = new DateTime(2025, 12, 1, 14, 0, 0),
            Duration = 240, // 4 hours in minutes
            DeliveryMode = DeliveryMode.Remote
        });
        sessions.Add(new Session
        {
            CourseId = course3.Id,
            StartDate = new DateTime(2025, 12, 8, 14, 0, 0),
            Duration = 240,
            DeliveryMode = DeliveryMode.OnSite
        });
        sessions.Add(new Session
        {
            CourseId = course3.Id,
            StartDate = new DateTime(2025, 12, 15, 14, 0, 0),
            Duration = 240,
            DeliveryMode = DeliveryMode.Remote
        });
    }


    private static (Course course1, Course course2, Course course3) SeedCourses(DbSet<Course> courses, Trainer trainer1, Trainer trainer2, Trainer trainer3)
    {
        var course1 = courses.Add(new Course
        {
            Name = "Mastering Asynchronous Programming and Parallelism in C#",
            Description = new Description()
            {
                Short = "Learn to write efficient, non-blocking C# code using async/await, Tasks, and parallel programming techniques.",
                Long =
                    "This course dives deep into **asynchronous programming** in C#, teaching you how to leverage `async` and `await`, **Task parallelism**, and the **Task Parallel Library (TPL)** to build responsive, high-performance applications.  \n\nYou’ll explore real-world scenarios, including **I/O-bound** and **CPU-bound** operations, and learn best practices for avoiding common pitfalls such as **deadlocks** and **race conditions**.  \n\nBy the end of this course, you’ll be able to write **scalable** and **efficient** C# applications that handle concurrency like a pro.\n",
            },
            Duration = 5,
            TargetAudience = TargetAudience.WorksCouncilPresident,
            MaxParticipants = 10,
            TrainerId = trainer1.Id
        }).Entity;

        var course2 = courses.Add(new Course
        {
            Name = "Advanced Design Patterns and Architecture with .NET",
            Description = new Description()
            {
                Short = "Master design patterns and architectural principles to build maintainable, robust C# applications.",
                Long =
                    "Take your C# development skills to the next level by learning how to apply **advanced design patterns** and **software architecture principles** in .NET.  \n\nThis course covers patterns such as **Factory**, **Observer**, **Dependency Injection**, and **Repository**, along with **SOLID principles**, **Clean Architecture**, and **microservices design**.  \n\nYou’ll gain **hands-on experience** designing **flexible**, **scalable**, and **testable applications** that adhere to **industry best practices**, preparing you for complex enterprise projects.\n"
            },
            Duration = 3,
            TargetAudience = TargetAudience.WorksCouncilPresident,
            MaxParticipants = 20,
            TrainerId = trainer1.Id
        }).Entity;

        var course3 = courses.Add(new Course
        {
            Name = "High-Performance C# and Memory Management Techniques",
            Description = new Description()
            {
                Short = "Optimize C# applications for speed and efficiency while mastering memory management.",
                Long =
                    "In this course, you’ll learn how to write **high-performance C# applications** that make the most of system resources.  \n\nTopics include **memory management**, **garbage collection**, **value vs. reference types**, **Span<T>** and **Memory<T>** structures, and **profiling tools** to identify performance bottlenecks.  \n\nThrough practical examples, you’ll discover techniques to **reduce memory allocations**, **improve throughput**, and ensure your applications run **efficiently** under heavy workloads.  \n\nIdeal for developers who want to **maximize performance** in real-world C# applications.\n"
            },
            Duration = 14,
            TargetAudience = TargetAudience.WorksCouncilPresident,
            MaxParticipants = 50,
            TrainerId = trainer1.Id
        }).Entity;

        return (course1, course2, course3);
    }

    private static (Trainer trainer1, Trainer trainer2, Trainer trainer3) SeedTrainers(DbSet<Trainer> trainers)
    {
        var trainter1 = trainers.Add(new Trainer
        {
            FirstName = "Ethan",
            LastName = "Ramirez",
        }).Entity;

        var trainer2 = trainers.Add(new Trainer
        {
            FirstName = "Sophia",
            LastName = "Bennett"
        }).Entity;

        var trainer3 = trainers.Add(new Trainer
        {
            FirstName = "Marcus",
            LastName = "Klein"
        }).Entity;

        return (trainter1, trainer2, trainer3);
    }
}
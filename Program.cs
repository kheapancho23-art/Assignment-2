using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Assignment2
{
    class Student
    {
        public int studentID { get; set; }
        public string name { get; set; }
        public string course { get; set; }
        public int grade { get; set; }

        // Constructor
        public Student(int id, string name, string course, int grade)
        {
            this.studentID = id;
            this.name = name;
            this.course = course;
            this.grade = grade;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            string filePath = "students.txt";

            // =========================
            // TASK 1 — CREATE & WRITE FILE
            // =========================
            List<Student> students = new List<Student>()
            {
                new Student(20261,"John","BSIT",90),
                new Student(20262,"Maria","BSTCM",92),
                new Student(20263,"Paul","BSIT",77),
                new Student(20264,"Ana","EMT",88),
                new Student(20265,"Mark","BSIT",89)
            };

            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (var s in students)
                {
                    writer.WriteLine($"{s.studentID},{s.name},{s.course},{s.grade}");
                }
            }

            Console.WriteLine("File created and data written successfully.\n");

            // =========================
            // TASK 2 — READ FILE + LINQ
            // =========================
            var lines = File.ReadAllLines(filePath);

            List<Student> studentList = lines
                .Select(line =>
                {
                    var parts = line.Split(',');
                    return new Student(
                        int.Parse(parts[0]),
                        parts[1],
                        parts[2],
                        int.Parse(parts[3]));
                }).ToList();

            // LINQ 1 — Grade > 85
            var highGrades = studentList
                .Where(s => s.grade > 85);

            Console.WriteLine("Students with Grade > 85");
            foreach (var s in highGrades)
                Console.WriteLine($"{s.name} - {s.grade}");

            Console.WriteLine();

            // LINQ 2 — Sort Descending
            var sorted = studentList
                .OrderByDescending(s => s.grade);

            Console.WriteLine("Sorted by Grade (Descending)");
            foreach (var s in sorted)
                Console.WriteLine($"{s.name} - {s.grade}");

            Console.WriteLine();

            // LINQ 3 — Names Only (Projection)
            var names = studentList
                .Select(s => s.name);

            Console.WriteLine("Student Names");
            foreach (var n in names)
                Console.WriteLine(n);

            Console.WriteLine();

            // LINQ 4 — Average Grade
            var average = studentList
                .Average(s => s.grade);

            Console.WriteLine($"Average Grade: {average:F2}");
        }
    }
}

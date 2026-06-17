using System;
using System.Collections.Generic;
using System.Linq;

namespace StudentManagementConsole
{
    //base Class:person  
    public abstract class Person
    {
        //encapsulation:private field and public property
        private string _name;

        public string Name
        {
            get { return _name; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Name cannot be empty.");
                _name = value;
            }
        }

        public string Email { get; set; }

        //polymorphism:abstract method  who override the child class 
        public abstract void DisplayDetails();
    }

    // derived Class:student inherits from Person
    public class Student : Person
    {
        public string StudentId { get; set; }

        //polymorphism base class override
        public override void DisplayDetails()
        {
            Console.WriteLine($"[Student] ID: {StudentId} | Name: {Name} | Email: {Email}");
        }
    }
    //interface design  for maintain standard
    public interface IStudentManager
    {
        void AddStudent(Student student);
        void ViewAllStudents();
        void SearchStudent(string studentId);
    }
    //this Class implement the  interface  
    public class StudentManager : IStudentManager
    {
        //encapsulation:list now private , no one can modify this 
        private readonly List<Student> _students = new List<Student>();

        public void AddStudent(Student student)
        {
            _students.Add(student);
            Console.WriteLine("\n[Success] Student added successfully!");
        }

        public void ViewAllStudents()
        {
            Console.WriteLine("  All Students ");
            if (_students.Count == 0)
            {
                Console.WriteLine("No students found in the system.");
                return;
            }

            foreach (var student in _students)
            {
                student.DisplayDetails();
            }
        }

        public void SearchStudent(string studentId)
        {
            Console.WriteLine(" Search Student");

            Student foundStudent = null;
            foreach (var student in _students)
            {
                if (student.StudentId == studentId)
                {
                    foundStudent = student;
                    break;
                }
            }

            if (foundStudent != null)
            {
                Console.Write("[Found] ");
                foundStudent.DisplayDetails();
            }
            else
            {
                Console.WriteLine($"\n[Not Found] No student exists with ID: {studentId}");
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            //made object using interface (Loosely coupled)
            IStudentManager studentManager = new StudentManager();

            //initial dmmy data
            studentManager.AddStudent(new Student { StudentId = "22-48508-3", Name = "Ifti", Email = "ifti@aiub.edu" });

            bool isRunning = true;

            while (isRunning)
            {
                Console.WriteLine(" Student Management System   ");
                Console.WriteLine("1. Add New Student");
                Console.WriteLine("2. View All Students");
                Console.WriteLine("3. Search Student by ID");
                Console.WriteLine("4. Exit");
                Console.Write("Select an option (1-4): ");

                string choice = Console.ReadLine();

                try
                {
                    switch (choice)
                    {
                        case "1":
                            Console.WriteLine(" Add Student ");
                            Console.Write("Enter Student ID: ");
                            string id = Console.ReadLine();

                            Console.Write("Enter Student Name: ");
                            string name = Console.ReadLine();

                            Console.Write("Enter Student Email: ");
                            string email = Console.ReadLine();

                            //object creation
                            Student newStudent = new Student
                            {
                                StudentId = id,
                                Name = name,
                                Email = email
                            };

                            studentManager.AddStudent(newStudent);
                            break;

                        case "2":
                            studentManager.ViewAllStudents();
                            break;

                        case "3":
                            Console.Write("\nEnter Student ID to search: ");
                            string searchId = Console.ReadLine();
                            studentManager.SearchStudent(searchId);
                            break;

                        case "4":
                            isRunning = false;
                            Console.WriteLine("Exiting program... Good bye!");
                            break;

                        default:
                            Console.WriteLine("Invalid option. Please enter a number between 1 and 4.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\n[Error] {ex.Message}");
                }
            }
        }
    }
}

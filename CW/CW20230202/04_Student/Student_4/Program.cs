using StudentLibrary;
using System;
using static System.Console;

namespace SimpleProject
{
    class Program
    {
        static void Main(string[] args)
        {
            Student student = new Student
            {
                FirstName = "John",
                LastName = "Miller",
                DateOfBirth = new DateTime(1997, 3, 12)
            };
            WriteLine(student.ToString());
        }
    }
}

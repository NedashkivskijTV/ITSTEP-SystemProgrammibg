using System;

namespace SampleLibrary
{
    public enum PersonMaritalStatus
    {
        Merried,
        Single
    }

    public class Person
    {
        string Name;
        string LastName;
        int Age;
        PersonMaritalStatus MaritsalStatus;

        public Person() : this("Unknow", "Unknow", 0, PersonMaritalStatus.Single)
        { }

        public Person(string Name, string Lastname, int Age) : this(Name, Lastname, Age, PersonMaritalStatus.Single)
        { }

        public Person(string Name, string Lastname, int Age, PersonMaritalStatus status)
        {
            this.Name = Name;
            this.LastName = Lastname;
            this.Age = Age;
            this.MaritsalStatus = status;
        }

        public void Print()
        {
            Console.WriteLine($"{nameof(Person)}:\nName: {Name} \nLastname: {LastName} \nAge: {Age} \nMaritsalStatus: {MaritsalStatus}");
        }
    }

    public class Employee : Person
    {
        string Position;
        decimal Salary;

        public Employee() : base()
        {
            this.Position = "Unknow";
            this.Salary = 0;
        }

        public Employee(string Name, string Lastname, int Age, string Position, decimal Salary) : base(Name, Lastname, Age, PersonMaritalStatus.Single)
        {
            this.Position = Position;
            this.Salary = Salary;
        }

        public Employee(string Name, string Lastname, int Age, string status, string Position, decimal Salary):
            base(Name, Lastname, Age, (PersonMaritalStatus)Enum.Parse(typeof(PersonMaritalStatus), status))
        {
            this.Position = Position;
            this.Salary = Salary;
        }

        public new void Print()
        {
            base.Print();
            Console.WriteLine($"{nameof(Employee)}:\nPosition: {Position} \nSalary: {Salary} \n");
        }
    }
}

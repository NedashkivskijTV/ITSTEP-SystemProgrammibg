using System;
namespace StudentLibrary
{
    /// <summary>
    /// Класс Студент
    /// Содержит поля 
    /// FirstName <see cref="string"/>, 
    /// LastName <see cref="string"/>, 
    /// DateOfBirth<see cref="DateTime"/>
    /// </summary>
    public class Student
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }

        /// <summary>
        /// Перегрузка вывода на экран полей класса.
        /// </summary>
        /// <returns>Возвращает строку из Фамилии Имени Даты рождения.
        /// </returns>

        public override string ToString()
        {
            return $"Surname: {LastName}, Name: {FirstName}, Date of Birth: {DateOfBirth.ToLongDateString()}";
        }
    }
}
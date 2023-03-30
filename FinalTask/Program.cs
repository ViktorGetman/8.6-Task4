using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace FinalTask
{
    [Serializable]
    class Student
    {
        public string Name { get; set; }
        public string Group { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Student(string name, string group, DateTime dateOfBirth)
        {
            Name = name;
            Group = group;
            DateOfBirth = dateOfBirth;
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Укажите путь к файлу");
            string folderPath = Console.ReadLine();

            string[] groups;
            Student[] students;
            Directory.CreateDirectory("C:\\Users\\1\\Desktop\\Students");

            BinaryFormatter formatter = new BinaryFormatter();
            using (var fs = new FileStream(folderPath, FileMode.OpenOrCreate))
            {
                students = (Student[])formatter.Deserialize(fs);
                foreach (var student in students)
                {
                    Console.WriteLine("Объект десериализован");
                    Console.WriteLine($"Имя: {student.Name} --- Возраст: {student.DateOfBirth} --- Группа {student.Group}");
                }

            }
            int i = 0;
            groups = new string[students.Length];
            foreach (var student in students)
            {
                if (!groups.Contains(student.Group))
                {
                    groups[i] = student.Group;
                    i++;
                }

            }
            foreach (string group in groups)
            {
                if (group == null)
                {
                    break;
                }
                using (var fs = new StreamWriter($"C:\\Users\\1\\Desktop\\Students\\{group}.txt"))
                {
                    foreach (var student in students)
                    {
                        if (student.Group == group)
                        {
                            fs.WriteLine(student.Name + " " + student.DateOfBirth);
                        }
                    }

                }

            }

            Console.ReadLine();
        }
    }
}

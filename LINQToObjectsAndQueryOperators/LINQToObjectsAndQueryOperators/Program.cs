using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQToObjectsAndQueryOperators
{
    class Program
    {
        static void Main(string[] args)
        {
            UniversityManager um = new UniversityManager();

            um.MaleStudents();
            um.FemaleStudents();
            um.SortStudentsByAge();
            um.AllStudentsFromIllinoisStateUniversity();

            um.StudentAndUniversityNameCollection();

            /* This is a way of sorting:
            int[] someInt = { 30, 12, 4, 3, 12 };
            IEnumerable<int> sortedInts = from i in someInt orderby i select i;
            IEnumerable<int> reversedInts = sortedInts.Reverse();

            foreach(int i in reversedInts)
            {
                Console.WriteLine(i);
            }

            IEnumerable<int> reversedSortedInts = from i in someInt orderby i descending select i;

            foreach (int i in reversedSortedInts)
            {
                Console.WriteLine(i);
            }
            */
            
            /*
            Console.WriteLine("Which University to see?");
            try
            {
                string inputUniversity = Console.ReadLine();
                int universityInput = Convert.ToInt32(inputUniversity);
                um.AllStudentsFromThatUniversity(universityInput);
            }
            catch (Exception)
            {
                Console.WriteLine("Wrong Value");
            }
            */
            Console.ReadKey();
        }
    }

    class UniversityManager
    {
        public List<University> universities;
        public List<Student> students;

        // Constructor
        public UniversityManager()
        {
            universities = new List<University>();
            students = new List<Student>();

            // Let's add some Universities
            universities.Add(new University { Id = 1, Name = "Yale" });
            universities.Add(new University { Id = 2, Name = "Illinois State University" });

            // Let's add some Students
            students.Add(new Student { Id = 1, Name = "Carla", Gender = "Female", Age = 17, UniversityId = 1 });
            students.Add(new Student { Id = 2, Name = "Toni", Gender = "Male", Age = 23, UniversityId = 2 });
            students.Add(new Student { Id = 3, Name = "Leyla", Gender = "Female", Age = 18, UniversityId = 1 });
            students.Add(new Student { Id = 4, Name = "Frank", Gender = "Male", Age = 23, UniversityId = 1 });
            students.Add(new Student { Id = 5, Name = "James", Gender = "Male", Age = 19, UniversityId = 2 });
            students.Add(new Student { Id = 6, Name = "Linda", Gender = "Female", Age = 20, UniversityId = 1 });
        }

        public void MaleStudents()
        {
            // Linq
            IEnumerable<Student> maleStudents = from student in students where student.Gender == "Male" select student;
            Console.WriteLine("Male - Students: ");

            foreach (Student student in maleStudents)
            {
                student.Print();
            }
        }

        public void FemaleStudents()
        {
            // Linq
            IEnumerable<Student> femaleStudents = from student in students where student.Gender == "Female" select student;
            Console.WriteLine("Female - Students: ");

            foreach (Student student in femaleStudents)
            {
                student.Print();
            }
        }

        public void SortStudentsByAge()
        {
            var sortedStudents = from student in students orderby student.Age select student;

            Console.WriteLine("Students sorted by Age: ");

            foreach(Student student in sortedStudents)
            {
                student.Print();
            }
        }

        public void AllStudentsFromIllinoisStateUniversity()
        {
            IEnumerable<Student> ISUStudents = from student in students
                                               join university in universities on student.UniversityId equals university.Id
                                               where university.Name == "Illinois State University"
                                               select student;

            Console.WriteLine("Students from Illinois State University");
            foreach(Student student in ISUStudents)
            {
                student.Print();
            }
        }

        public void AllStudentsFromThatUniversity(int Id)
        {
            IEnumerable<Student> universityStudents = from student in students
                                               join university in universities on student.UniversityId equals university.Id
                                               where university.Id == Id
                                               select student;

            Console.WriteLine("Students from {0}", Id);
            foreach (Student student in universityStudents)
            {
                student.Print();
            }
        }

        public void StudentAndUniversityNameCollection()
        {
            var newCollection = from student in students
                                join university in universities on student.UniversityId equals university.Id
                                orderby student.Name
                                select new { StudentName = student.Name, UniversityName = university.Name };
            Console.WriteLine("New Collection ");

            foreach(var col in newCollection)
            {
                Console.WriteLine("Student {0} from University {1}", col.StudentName, col.UniversityName);
            }
        }
    }

    class University
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public void Print()
        {
            Console.WriteLine("University: {0} with id: {1}", Name, Id);
        }
    }

    class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }

        // Foreign Key
        public int UniversityId { get; set; }

        public void Print()
        {
            Console.WriteLine("Student {0} with Id: {1}, Gender: {2}, and " +
                "Age {3} from University with the Id {4}", Name, Id, Gender, Age, UniversityId);
        }
    }
}

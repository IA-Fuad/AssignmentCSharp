using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AssignmentCSharp
{
    class ProcessStudentInfo
    {
        string path;
        private delegate void InputValidity(string input);
        private IList<StudentInfo> studentInfos;
        private IList<StudentResultInfo> studentResultInfos;

        public ProcessStudentInfo()
        {
            path = Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory())));
            studentInfos = new List<StudentInfo>();
            studentResultInfos = new List<StudentResultInfo>();
        }

        public void CalculateAndSortStudentsByCgpaFromFileInput()
        {
            PopulateStudentListFromInput();
            PopulateStudentResultListFromInput();

            PrintStudentBySortedCG();
        }

        void ReadFile(string path, InputValidity inputValidity)
        {
            StreamReader reader = new StreamReader(path);

            try
            {
                while (reader.Peek() != -1)
                {
                    inputValidity(reader.ReadLine());
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                reader.Close();
            }
        }

        void ValidateAndGetStudentInput(string input)
        {
            bool valid = true;

            string name = "";
            int roll = -1, age = -1;

            string[] data = input.Split(',').Select(x => x.Trim()).ToArray();
            if (data.Length != 3)
            {
                valid = false;
            }
            else
            {
                try
                {
                    roll = Int32.Parse(data[0]);
                    name = data[1];
                    age = Int32.Parse(data[2]);
                }
                catch
                {
                    valid = false;
                }
            }

            if (valid)
            {
                studentInfos.Add(new StudentInfo() { Roll = roll, Name = name, Age = age });
            }
        }

        void ValidateAndGetResultInput(string input)
        {
            bool valid = true;

            string subject = "";
            int roll = -1;
            double gp = 0;

            string[] data = input.Split(',').Select(x => x.Trim()).ToArray();
            if (data.Length != 3)
            {
                valid = false;
            }
            else
            {
                try
                {
                    roll = Int32.Parse(data[0]);
                    subject = data[1];
                    gp = Double.Parse(data[2]);
                }
                catch
                {
                    valid = false;
                }
            }

            if (valid)
            {
                studentResultInfos.Add(new StudentResultInfo() { Roll = roll, Subject = subject, GP = gp });
            }
        }


        void PopulateStudentListFromInput()
        {
            InputValidity inputValidity = ValidateAndGetStudentInput;
            ReadFile(path + "\\Input\\StudentInfo.txt", inputValidity);
        }

        void PopulateStudentResultListFromInput()
        {
            InputValidity inputValidity = ValidateAndGetResultInput;
            ReadFile(path + "\\Input\\StudentResultInfo.txt", inputValidity);
        }


        void PrintStudentBySortedCG()
        {
            var sortedList = studentInfos.Join(studentResultInfos, stud => stud.Roll, result => result.Roll,
                                        (stud, result) => new { stud.Name, stud.Roll, result.GP }).GroupBy(g => g.Roll).Select(s => new { Roll = s.Key, Name = s.Select(g => g.Name).First(), CGPA = Math.Round(s.Select(g => g.GP).Average(), 2) }).OrderByDescending(s => s.CGPA).ThenBy(s => s.Roll).ThenBy(s => s.Name);

            StreamWriter sw = new StreamWriter(path + "\\Output\\SortedList.txt");

            foreach (var stud in sortedList)
            {
                sw.WriteLine($"{stud.Name}, {stud.Roll}, {stud.CGPA}");
            }
            sw.Close();
        }
    }
}

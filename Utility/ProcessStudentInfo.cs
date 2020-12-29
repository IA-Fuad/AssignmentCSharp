using AssignmentCSharp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AssignmentCSharp
{
    class ProcessStudentInfo
    {
        public static void PrintStudentBySortedCG(IList<StudentInfo> studentInfos, IList<StudentResultInfo> studentResultInfos, string path)
        {
            var joinStudentAndResult = studentInfos.Join(studentResultInfos, stud => stud.Roll, result => result.Roll,
                                        (stud, result) => new { stud.Name, stud.Roll, result.GP });
            
            var group = joinStudentAndResult.GroupBy(g => g.Roll).Select(s => new StudentOutputInfo{ Name = s.Select(g => g.Name).First(), Roll = s.Key, CGPA = Math.Round(s.Select(g => g.GP).Average(), 2)});
            var sortedList = group.OrderByDescending(s => s.CGPA).ThenBy(s => s.Roll).ThenBy(s => s.Name);

            StreamWriter sw = new StreamWriter(path);

            foreach (StudentOutputInfo stud in sortedList)
            {
                sw.WriteLine($"{stud.Name}, {stud.Roll}, {stud.CGPA}");
            }
            sw.Close();
        }
    }
}

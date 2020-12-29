using AssignmentCSharp.Utility;
using System.Collections.Generic;
using System.IO;

namespace AssignmentCSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory())));
            IList<StudentInfo> studentInfos = ReadFile.ValidateAndGetStudentInput(path + "\\Input\\StudentInfo.txt");
            IList<StudentResultInfo> studentResultInfos = ReadFile.ValidateAndGetResultInput(path + "\\Input\\StudentResultInfo.txt");

            ProcessStudentInfo.PrintStudentBySortedCG(studentInfos, studentResultInfos, path + "\\Output\\SortedList.txt");
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AssignmentCSharp.Utility
{
    class ReadFile
    {
        public static IList<StudentInfo> ValidateAndGetStudentInput(string path)
        {
            StreamReader reader = new StreamReader(path);
            IList<StudentInfo> studentInfos = new List<StudentInfo>();

            string name = "";
            int roll = -1, age = -1;

            try
            {
                while (reader.Peek() != -1)
                {
                    string input = reader.ReadLine();
                    string[] data = input.Split(',').Select(x => x.Trim()).ToArray();

                    if (data.Length == 3)
                    {
                        try
                        {
                            roll = Int32.Parse(data[0]);
                            name = data[1];
                            age = Int32.Parse(data[2]);

                            studentInfos.Add(new StudentInfo() { Roll = roll, Name = name, Age = age });
                        }
                        catch
                        {
                            Console.WriteLine("Not valid input");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Not valid input");
                    }
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

            return studentInfos;
        }

        public static IList<StudentResultInfo> ValidateAndGetResultInput(string path)
        {
            StreamReader reader = new StreamReader(path);
            IList<StudentResultInfo> studentResultInfos = new List<StudentResultInfo>();

            string subject = "";
            int roll = -1;
            double gp = 0;

            try
            {
                while (reader.Peek() != -1)
                {
                    string input = reader.ReadLine();
                    string[] data = input.Split(',').Select(x => x.Trim()).ToArray();

                    if (data.Length == 3)
                    {
                        try
                        {
                            roll = Int32.Parse(data[0]);
                            subject = data[1];
                            gp = Double.Parse(data[2]);

                            studentResultInfos.Add(new StudentResultInfo() { Roll = roll, Subject = subject, GP = gp });
                        }
                        catch
                        {
                            Console.WriteLine("Not valid input");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Not valid input");
                    }
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

            return studentResultInfos;
        }
    }
}

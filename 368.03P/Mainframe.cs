using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _368._03P
{
    class Mainframe
    {
        static int CloudStorage = 500;
        static int NetworkSpeed = 10000;

        static bool GetIntFromUser(out int number, int min, int max, int DEFAULT)
        {
            int inp;
            bool b;
            Console.WriteLine($"Enter a number between {min} and {max}.");
            inp = Convert.ToInt32(Console.ReadLine());
            if (inp >= min && inp <= max)
            {
                number = inp;
                b = true;
            }
            else
            {
                number = DEFAULT;
                b = false;
            }
            return b;
        }

        static bool DoubleIntNotPastMax(ref int number, int max, bool setToMax)
        {
            bool b = false;
            if ((number * 2) <= max)
            {
                number *= 2;
                b = true;
            }
            else if ((number * 2) > max)
            {
                if (setToMax)
                {
                    number = max;
                    b = false;
                }
                else
                {
                    b = false;
                }
            }
            return b;
        }

        static bool HalveValueNotPastMin(ref int number, int min, bool setToMin)
        {
            bool b = false;
            if ((number / 2) >= min)
            {
                number /= 2;
                b = true;
            }
            else if ((number / 2) < min)
            {
                if (setToMin)
                {
                    number = min;
                    b = false;
                }
                else
                {
                    b = false;
                }
            }
            return b;
        }

        static Computer userPrototype = null;
        static Computer hardPrototype = new Computer("001")
        {
            HasAntenna = false,
            HardDCapacity = 250,
            RAM = 1000,
            SoftwareLic = null
        };

        static void Main(string[] args)
        {
            int comps;
            Console.WriteLine("What is the maximum number of computers " +
                "you will need to track?");

            bool goodInput = GetIntFromUser(out comps, 5, 20, 10);

            if (!goodInput)
                Console.WriteLine("Invalid input; max set to ten");
            Computer[] comparr = new Computer[comps];
            while (!Console.ReadLine().Equals("qwerty"))
            {
                Console.WriteLine("Enter the number preceding the option you wish to select:" +
                    "1- add computer; " +
                    "2- specify user prototype computer; " +
                    "3- remove user prototype computer; " +
                    "4- upgrade your cloud storage; " +
                    "5- downgrade your cloud storage; " +
                    "6- upgrade your network speed; " +
                    "7- downgrade your network speed; " +
                    "8- get a summary of one of the computers; " +
                    "9- get statistics on all computers; " +
                    "10- get statistics on a specific range of computers. " +
                    "Enter 'qwerty' to exit. ");
                int s = Convert.ToInt32(Console.ReadLine());

                if (s == 1)
                {
                    Console.WriteLine("Enter ID");
                    Computer newComp = new Computer(Console.ReadLine());
                    Console.WriteLine("Enter true or false for antenna or anything else if n/a:");
                    string antInp = Console.ReadLine();
                    newComp.HasAntenna = (antInp.Equals("true") || antInp.Equals("false")) ? (bool?)Convert.ToBoolean(antInp) : null;
                    Console.WriteLine("Enter hard drive capacity or a negative number if n/a");
                    newComp.HardDCapacity = Convert.ToDouble(Console.ReadLine());
                    Console.WriteLine("Enter RAM");
                    newComp.RAM = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Enter an array of number of licenses per software, if software is installed " +
                        "(Otherwise enter -1); enter 0 if the software has no licenses; enter 'null' if " +
                        "the software is not installed; separate each element with a comma. ");
                    string str1 = Console.ReadLine();
                    if (!str1.Equals("-1"))
                    {
                        String[] tokens1 = str1.Split(',');
                        for (int i = 0; i < tokens1.Length; i++)
                        {
                            newComp.SoftwareLic[i] = tokens1[i].Equals("null") ? null :
                                (int?)Convert.ToInt32(tokens1[i]);
                        }
                    }
                    else
                        newComp.SoftwareLic = null;

                    comparr[Array.FindIndex(comparr, i => i == null)] = newComp;
                }

                else if (s == 2)
                {
                    Console.WriteLine("Enter ID");
                    userPrototype = new Computer(Console.ReadLine());
                    Console.WriteLine("Enter true or false for antenna or nothing if n/a:");
                    userPrototype.HasAntenna = Convert.ToBoolean(Console.ReadLine());
                    Console.WriteLine("Enter hard drive capacity or nothing if n/a:");
                    userPrototype.HardDCapacity = Convert.ToDouble(Console.ReadLine());
                    Console.WriteLine("Enter RAM");
                    userPrototype.RAM = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Enter an array of number of licenses per software, if software is installed; " +
                        "enter -1 if there is no extra software; enter 0 if the software has no licenses; " +
                        "enter 'null' " +
                        "if that specific software is not installed; separate each element with a comma.");
                    string str1 = Console.ReadLine();
                    if (!str1.Equals("-1"))
                    {
                        String[] tokens1 = str1.Split(',');
                        for (int i = 0; i < tokens1.Length; i++)
                        {
                            userPrototype.SoftwareLic[i] = tokens1[i].Equals("null") ? null :
                                (int?)Convert.ToInt32(tokens1[i]);
                        }
                    }
                    comparr[Array.FindIndex(comparr, i => i == null)] = userPrototype;
                }
                else if (s == 3)
                {
                    try
                    {
                        comparr[Array.FindIndex(comparr, i => i.Equals(userPrototype))] = null;
                    }
                    catch (Exception e) { Console.WriteLine("There is no prototype computer in the array."); }
                }
                else if (s == 4)
                {
                    if (DoubleIntNotPastMax(ref CloudStorage, 16000, false))
                        Console.WriteLine($"Cloud Storage successfully doubled to {CloudStorage}");
                    else
                        Console.WriteLine("You have too much already to upgrade.");
                }

                else if (s == 5)
                {
                    if (HalveValueNotPastMin(ref CloudStorage, 500, true))
                        Console.WriteLine($"Downgrade successful, halved to {CloudStorage}");
                    else
                        Console.WriteLine("You have too little storage to halve it; cloud storage set to 500");
                }
                else if (s == 6)
                {
                    if (DoubleIntNotPastMax(ref NetworkSpeed, 250000, true))
                        Console.WriteLine($"Network Speed successfully doubled to {NetworkSpeed} ");
                    else
                        Console.WriteLine("You have too much speed already, Network Speed set to 250000");
                }
                else if (s == 7)
                {
                    if (HalveValueNotPastMin(ref NetworkSpeed, 10000, false))
                        Console.WriteLine($"Network Speed downgraded to {NetworkSpeed} ");
                    else
                        Console.WriteLine("Downgrade unsuccessful- too little speed");
                }

                else if (s == 8)
                {
                    Console.WriteLine("Enter the 0 based index of the computer you wish to view:");
                    int i = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine((comparr.ElementAt(i) ?? hardPrototype).ToString() + $"Cloud Storage - {CloudStorage}, " +
                                $"Network Speed - {NetworkSpeed}");
                }
                else if (s == 9)
                {
                    double RAMaverage = comparr.Where(e => e != null).ToList().Select(e => e.RAM).Average();
                    List<bool?> Antennable = comparr.Where(c => c != null).Where(c => c.HasAntenna != null)
                        .Select(c => c.HasAntenna).ToList();
                    int percentAntenna = Antennable.Where(c => (bool)c).Count() / Antennable.Count * 100;
                    double? hardDAverage = comparr.ToList().Where(c => c != null)
                        .Where(c => c.HardDCapacity != null).Select(e => e.HardDCapacity).Average();
                    //get List of all computer software license arrays
                    List<int?[]> compLic = comparr.Where(m => m != null).Where(m => m.SoftwareLic != null)
                        .Select(m => m.SoftwareLic).ToList();
                    //create List of total licenses for each computer 
                    List<int?> licensesPerM = new List<int?>();
                    foreach (var machine in compLic)
                    {
                        int machineLic = 0;
                        foreach (int? software in machine)
                        {
                            machineLic += software ?? 0;
                        }
                        licensesPerM.Add(machineLic);
                    }
                    //Average of all machine licenses
                    int averageLic = (int)licensesPerM.Average();
                    //Average of all machine licenses per program-
                    //get array of each software on all computers, use previous List of software arrays, and get averages 
                    int averagePerProgram0 = (int)compLic.Select(m => m[0]).Where(m => m.HasValue).Average();
                    int averagePerProgram1 = (int)compLic.Select(m => m[1]).Where(m => m.HasValue).Average();
                    int averagePerProgram2 = (int)compLic.Select(m => m[2]).Where(m => m.HasValue).Average();
                    int averagePerProgram3 = (int)compLic.Select(m => m[3]).Where(m => m.HasValue).Average();
                    int averagePerProgram4 = (int)compLic.Select(m => m[4]).Where(m => m.HasValue).Average();

                    Console.WriteLine($"Average computer RAM - {RAMaverage}; percentage of computers " +
                        $"with antenna - {percentAntenna}; average hard drive capacity - {hardDAverage}; " +
                        $"average total software licenses per computer - {averageLic}; average of number of " +
                        $"software licenses per software, from softwares[0] to [4] - {averagePerProgram0}; " +
                        $"{averagePerProgram1}; {averagePerProgram2}; {averagePerProgram3}; {averagePerProgram4}." +
                        $"Your current cloud storage is {CloudStorage} and your network speed is {NetworkSpeed}.");
                }
             
                else if (s == 10)
                {
                    Computer deFault = userPrototype ?? hardPrototype;

                    Console.WriteLine("Enter first index");
                    int first = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Enter the second index");
                    int second = Convert.ToInt32(Console.ReadLine());

                    int filled = comparr.Count(com => com != null);
                    Computer[] comparr002 = new Computer[comps];
                    for(int i = 0; i < filled; i++)
                    {
                        comparr002[i] = comparr[i];
                    }
                    for(int i = filled; i< comps; i++)
                    {
                        comparr002[i] = userPrototype ?? hardPrototype;
                    }

                        List<Computer> Antennable = comparr002.Where(e => e != null).Skip(first).Take(second - first + 1).Where(c => !c.HasAntenna.Equals(null)).ToList();

                        double RAMaverage = comparr002.Skip(first).Take(second - first + 1).Where(e => e != null).ToList().Average(e => e.RAM);

                        int percentAntenna = (Antennable.Where(c => (bool)c.HasAntenna).Count() /
                        Antennable.Count()) * 100;

                        double? hardDAverage = comparr002.Where(e => e != null).Skip(first).Take(second - first + 1)
                            .ToList().Where(c => !c.HardDCapacity.Equals(null))
                            .Select(e => e.HardDCapacity).Average();
                        //get List of all computer software license arrays
                        List<Computer> compLicFirst = comparr002.Where(e => e != null).Skip(first).Take(second - first + 1).ToList();
                        List<Computer> compLicSecond = new List<Computer>();
                        for (int i = 0; i < compLicFirst.Count(); i++)
                        {
                            compLicSecond[i] = compLicFirst[i] ?? deFault;
                        }

                        List<int?[]> compLic = compLicSecond.Where(m => m.SoftwareLic != null).Select(m => m.SoftwareLic).ToList();
                    //create List of total licenses for each computer 
                    List<int?> licensesPerM = new List<int?>();
                    foreach (var machine in compLic)
                    {
                        int machineLic = 0;
                        foreach (int? software in machine)
                        {
                            machineLic += software ?? 0;
                        }
                        licensesPerM.Add(machineLic);
                    }
                    //Average of all machine licenses
                    int averageLic = (int)licensesPerM.Average();
                        //Average of all machine licenses per program-
                        //get array of each software on all computers, use previous List of software arrays, and get averages 
                        int averagePerProgram0 = (int)compLic.Select(m => m[0]).Where(m => m.HasValue).Average();
                        int averagePerProgram1 = (int)compLic.Select(m => m[1]).Where(m => m.HasValue).Average();
                        int averagePerProgram2 = (int)compLic.Select(m => m[2]).Where(m => m.HasValue).Average();
                        int averagePerProgram3 = (int)compLic.Select(m => m[3]).Where(m => m.HasValue).Average();
                        int averagePerProgram4 = (int)compLic.Select(m => m[4]).Where(m => m.HasValue).Average();

                        Console.WriteLine($"Average computer RAM - {RAMaverage}; percentage of computers " +
                            $"with antenna - {percentAntenna}; average hard drive capacity - {hardDAverage}; " +
                            $"average total software licenses per computer - {averageLic}; average of number of " +
                            $"software licenses per software, from softwares[0] to [4] - {averagePerProgram0}; " +
                            $"{averagePerProgram1}; {averagePerProgram2}; {averagePerProgram3}; {averagePerProgram4}." +
                            $"Your current cloud storage is {CloudStorage} and your network speed is {NetworkSpeed}.");
                    }    
                }
            }
        }
    }
} 

using System;
using System.Linq;

class Computer
{
    private readonly string iD;

    public string GetID()
    {
        return iD;
    }

    private bool? hasAntenna;
    public bool? HasAntenna
    {
        get
        {
            return hasAntenna;
        }
        set
        {
            hasAntenna = value.Equals(true || false) ? value : null;
        } 
    }

    private double? hardDCapacity;
    public double? HardDCapacity
    {
        get
        {
            return hardDCapacity;
        }
        set
        {
            hardDCapacity = (value > 0) ?  value : null;
        }
    }

    private int?[] softwareLic = new int?[5];
    public int?[] SoftwareLic
    {
        set
        {
            softwareLic = value;
        }

        get
        {
            return softwareLic;
        }
    }

    private int ram;
    public int RAM
    {
        get
        {
            int available = ram;
            if (HasAntenna == true)
                available -= 100;
            else
                available -= 50;
            try
            {
                foreach (int? software in softwareLic)
                {
                    if (software > 0)
                        available -= 10;
                }
            }
            catch (Exception e) { Console.WriteLine("There is no Computer here"); }
            return available;
        }

        set
        {
            ram = (value >= 1000) ? value : 1000;
        }
    }

    public Computer(string id)
    {
        this.iD = id;
    }

    public Computer()
    {
    }


    public override String ToString()
    {
        string printArr = "[no software]";
        try
        {
            printArr = string.Join(",", SoftwareLic);
        }
        catch (Exception e) { Console.WriteLine(e.Message); }
        String s = ($"ID - {iD}, Has Antenna - {HasAntenna}, Hard Drive Capacity - {HardDCapacity}, Software Licences - " +
        $"{printArr}, Ram - {RAM} ");
        return s;
    }
}




using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using ManagementClassCreator;

namespace ProcessorInfo
{
    internal class Program
    {
        static void Main(string[] args)
        {

            CpuInfoGetter myCPU = new CpuInfoGetter();

            // Виведення усієї доступної інф по процесору одним методом
            //Console.WriteLine("----- CPU Info -------------------------------");
            //Console.WriteLine(myCPU.GetAllCpuInfo());
            //Console.WriteLine("------------------------------------");

            // Виведення інф по процесору окремими методами
            Console.WriteLine("\nProcessor Info :");
            Console.WriteLine("\t" + myCPU.GetCpuId());
            Console.WriteLine("\t" + myCPU.GetCpuName());
            Console.WriteLine("\t" + myCPU.GetCpuDescription());
            Console.WriteLine("\t" + myCPU.GetCpuAddressWidth());
            Console.WriteLine("\t" + myCPU.GetCpuDataWidth());
            Console.WriteLine("\t" + myCPU.GetCpuArchitecture());
            Console.WriteLine("\t" + myCPU.GetCpuL2CacheSize());
            Console.WriteLine("\t" + myCPU.GetCpuL3CacheSize());
            Console.WriteLine("\t" + myCPU.GetCpuNumberOfCores());
            Console.WriteLine("\t" + myCPU.GetCpuThreadCount());


            OsInfoGetter myOS = new OsInfoGetter();

            // Виведення усієї доступної інф по ОС одним методом
            //Console.WriteLine("\n\n----- OS Info -------------------------------");
            //Console.WriteLine(myOS.GetAllOsInfo());
            //Console.WriteLine("------------------------------------");

            // Виведення інф по ОС окремими методами
            Console.WriteLine("\nOS Info :");
            Console.WriteLine("\t" + myOS.GetOsName());
            Console.WriteLine("\t" + myOS.GetOsCaption());
            Console.WriteLine("\t" + myOS.GetOsVersion());
            Console.WriteLine("\t" + myOS.GetOsMaxNumberOfProcesses());
            Console.WriteLine("\t" + myOS.GetOsMaxProcessMemorySize());
            Console.WriteLine("\t" + myOS.GetOsArchitecture());
            Console.WriteLine("\t" + myOS.GetOsSerialNumber());
            Console.WriteLine("\t" + myOS.GetOsBuildNumber());

            Console.WriteLine();
        }
    }
}

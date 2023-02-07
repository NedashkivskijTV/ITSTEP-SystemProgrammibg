using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using ManagementClassCreator;

namespace ProcessorInfo
{
    public class CpuInfoGetter
    {
        private ManagementClass myManagementClass = null;
        private ManagementObject myManagementObject = null;
        static String cpuPath = "Win32_Processor";

        public ManagementClass ManagementClassGetter() {
            if(myManagementClass == null)
            {
                myManagementClass = MCCreator.GetManagementClass(cpuPath);
            }
            return myManagementClass;
        }

        public ManagementObject ManagementObjectGetter() {
            if(myManagementObject == null)
            {
                myManagementObject = MCCreator.GetManagementObject(cpuPath);
            }
            return myManagementObject;
        }


        public String GetAllCpuInfo()
        {
            String cpuInfo = "\nProcessor Info :";
            StringBuilder sb = new StringBuilder();
            sb.Append(cpuInfo);

            ManagementObjectCollection myManagementCollection = ManagementClassGetter().GetInstances();
            PropertyDataCollection myProperties = ManagementClassGetter().Properties;

            foreach (var obj in myManagementCollection)
            {
                foreach (var myProperty in myProperties)
                {
                    sb.Append("\n\t" + myProperty.Name + " = " + obj.Properties[myProperty.Name].Value);
                }
            }

            return sb.ToString();
        }

        public String GetCpuId()
        {
            ManagementObject cpu = ManagementObjectGetter();
            return "ProcessorID = " + cpu["ProcessorID"];
        }

        public String GetCpuName()
        {
            ManagementObject cpu = ManagementObjectGetter();
            return "Name = " + cpu["Name"];
        }

        public String GetCpuDescription()
        {
            ManagementObject cpu = ManagementObjectGetter();
            return "Description = " + cpu["Description"];
        }

        public String GetCpuAddressWidth()
        {
            ManagementObject cpu = ManagementObjectGetter();
            return "AddressWidth = " + cpu["AddressWidth"];
        }

        public String GetCpuDataWidth()
        {
            ManagementObject cpu = ManagementObjectGetter();
            return "DataWidth = " + cpu["DataWidth"];
        }

        public String GetCpuArchitecture()
        {
            ManagementObject cpu = ManagementObjectGetter();
            return "Architecture = " + cpu["Architecture"];
        }

        public String GetCpuNumberOfCores()
        {
            ManagementObject cpu = ManagementObjectGetter();
            return "NumberOfCores = " + cpu["NumberOfCores"];
        }

        public String GetCpuThreadCount()
        {
            ManagementObject cpu = ManagementObjectGetter();
            return "ThreadCount = " + cpu["ThreadCount"];
        }

        public String GetCpuL2CacheSize()
        {
            ManagementObject cpu = ManagementObjectGetter();
            return "L2CacheSize = " + cpu["L2CacheSize"];
        }

        public String GetCpuL3CacheSize()
        {
            ManagementObject cpu = ManagementObjectGetter();
            return "L3CacheSize = " + cpu["L3CacheSize"];
        }

    }
}

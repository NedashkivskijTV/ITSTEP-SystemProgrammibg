using ManagementClassCreator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace ProcessorInfo
{
    public class OsInfoGetter
    {
        private ManagementClass myManagementClass = null;
        private ManagementObject myManagementObject = null;
        static String osPath = "Win32_OperatingSystem";

        public ManagementClass ManagementClassGetter()
        {
            if (myManagementClass == null)
            {
                myManagementClass = MCCreator.GetManagementClass(osPath);
            }
            return myManagementClass;
        }

        public ManagementObject ManagementObjectGetter()
        {
            if (myManagementObject == null)
            {
                myManagementObject = MCCreator.GetManagementObject(osPath);
            }
            return myManagementObject;
        }


        public String GetAllOsInfo()
        {
            String osInfo = "\nOS Info :";
            StringBuilder sb = new StringBuilder();
            sb.Append(osInfo);

            ManagementObjectCollection myManagementCollection = ManagementClassGetter().GetInstances();
            PropertyDataCollection myProperties = ManagementClassGetter().Properties;

            //Console.WriteLine("\nProcessor Info :");
            foreach (var obj in myManagementCollection)
            {
                foreach (var myProperty in myProperties)
                {
                    sb.Append("\n\t" + myProperty.Name + " = " + obj.Properties[myProperty.Name].Value);
                }
            }

            return sb.ToString();
        }

        public String GetOsName()
        {
            ManagementObject os = ManagementObjectGetter();
            return "Name = " + os["Name"];
        }

        public String GetOsVersion()
        {
            ManagementObject os = ManagementObjectGetter();
            return "Version = " + os["Version"];
        }

        public String GetOsMaxNumberOfProcesses()
        {
            ManagementObject os = ManagementObjectGetter();
            return "MaxNumberOfProcesses = " + os["MaxNumberOfProcesses"];
        }

        public String GetOsMaxProcessMemorySize()
        {
            ManagementObject os = ManagementObjectGetter();
            return "MaxProcessMemorySize = " + os["MaxProcessMemorySize"];
        }

        public String GetOsArchitecture()
        {
            ManagementObject os = ManagementObjectGetter();
            return "OSArchitecture = " + os["OSArchitecture"];
        }

        public String GetOsSerialNumber()
        {
            ManagementObject os = ManagementObjectGetter();
            return "SerialNumber = " + os["SerialNumber"];
        }

        public String GetOsCaption()
        {
            ManagementObject os = ManagementObjectGetter();
            return "Caption = " + os["Caption"];
        }

        public String GetOsBuildNumber()
        {
            ManagementObject os = ManagementObjectGetter();
            return "BuildNumber = " + os["BuildNumber"];
        }
    }
}

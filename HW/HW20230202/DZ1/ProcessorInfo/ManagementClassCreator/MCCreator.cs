using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace ManagementClassCreator
{
    public class MCCreator
    {

        public static ManagementClass GetManagementClass(String path)
        {
            return new ManagementClass(path);
        }

        public static ManagementObject GetManagementObject(String path)
        {
            return new ManagementObjectSearcher("select * from " + path).Get().Cast<ManagementObject>().First();
        }
    }
}

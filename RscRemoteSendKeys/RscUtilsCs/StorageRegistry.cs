using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Win32;

namespace Ressive.Utils
{
    public static class StorageRegistry
    {

        const string csCOMPANY = "Ressive.Hu";

        public static string m_sAppName = "Unknown";

        public static void Write(string sName, string sValue)
        {
            RegistryKey key = Registry.CurrentUser.CreateSubKey("SOFTWARE\\" + csCOMPANY + "\\" + m_sAppName);
            key.SetValue(sName, sValue);
            key.Dispose();
        }

        public static void Write(string sName, int iValue)
        {
            RegistryKey key = Registry.CurrentUser.CreateSubKey("SOFTWARE\\" + csCOMPANY + "\\" + m_sAppName);
            key.SetValue(sName, iValue);
            key.Dispose();
        }

        public static string Read(string sName, string sDefaultValue)
        {
            RegistryKey key = Registry.CurrentUser.CreateSubKey("SOFTWARE\\" + csCOMPANY + "\\" + m_sAppName);
            string sValue = (string)key.GetValue(sName, sDefaultValue);
            key.Dispose();

            return sValue;
        }

        public static int Read(string sName, int iDefaultValue)
        {
            RegistryKey key = Registry.CurrentUser.CreateSubKey("SOFTWARE\\" + csCOMPANY + "\\" + m_sAppName);
            int iValue = (int)key.GetValue(sName, iDefaultValue);
            key.Dispose();

            return iValue;
        }

    }
}

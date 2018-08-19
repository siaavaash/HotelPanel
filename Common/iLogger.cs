using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    /// <summary>
    /// iLogger is a method for user action tracking
    /// </summary>
    public class iLogger
    {
        /// <summary>
        /// Storage Location
        /// </summary>
        private string Storage = AppSettings.GlobalTempPath + "iLogger";
        /// <summary>
        /// Check Exist Current Directory
        /// </summary>
        /// <returns>True Or False</returns>
        public Boolean CheckExistDirectory()
        {
            try
            {
                if (Directory.Exists(Storage))
                    return true;
            }
            catch
            {

            }
            return false;
        }
        /// <summary>
        /// Create Directory Path
        /// </summary>
        /// <returns>True Or False</returns>
        public Boolean CreateDirectory()
        {
            try
            {
                if (!CheckExistDirectory())
                    System.IO.Directory.CreateDirectory(System.IO.Path.Combine(Storage));
            }
            catch
            {

            }
            return false;
        }
        /// <summary>
        /// Create New File For Log
        /// </summary>
        /// <param name="ContractID">Contract ID</param>
        /// <param name="UserID">Current User</param>
        /// <returns>True Or False</returns>
        public Boolean CreateNewfile()
        {
            try
            {
                File.WriteAllText(Storage+"Log"+".txt", "");
                return true;
            }
            catch
            {

            }
            return false;
        }
        /// <summary>
        /// Append Log
        /// </summary>
        /// <param name="Text">Text Of Log</param>
        /// <param name="ContractID">Contract ID</param>
        /// <param name="UserID">Current User</param>
        /// <returns>True Or False</returns>
        public Boolean Append(string Text)
        {
            try
            {
                string path = Storage + "Log" + ".txt";
                if (File.Exists(path))
                {
                    using (StreamWriter sw = File.AppendText(path))
                    {
                        sw.WriteLine(Text + " " + DateTime.Now.ToString());
                    }
                }
                else
                {
                    CreateNewfile();
                    using (StreamWriter sw = File.AppendText(path))
                    {
                        sw.WriteLine(Text + " " + DateTime.Now.ToString());
                    }
                }
                return true;
            }
            catch
            {

            }
            return false;
        }
    }
}

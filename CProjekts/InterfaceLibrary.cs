using System;
using System.IO;
using System.Windows.Forms;

namespace CProjekts
{
    public interface ICheck
    {
        bool Check(string text);
    }
    public class FileCheck : ICheck
    {
        public bool Check(string text)
        {           
            string location = Application.StartupPath;
            string FilePath = string.Format(location + @"\{0}.txt", text);
            return File.Exists(FilePath);
        }
    }
    public class ConvertionToDoubleCheck : ICheck
    {
        public bool Check(string text)
        {
            return double.TryParse(text, out double offset);
        }
    }

    public class IsLargerThanZeroCheck : ICheck
    {
        public bool Check(string text)
        {
            return Convert.ToDouble(text)>=0;
        }
    }
}

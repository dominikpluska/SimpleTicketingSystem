using SimpleLogger.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleLogger
{
    public class SimpleLogger : ISimpleLogger
    {
        private readonly DateTime _dateTime = DateTime.Now;
        private readonly string _filePath;
        private readonly string _fileName;

        public SimpleLogger(string filePath, string fileName)
        {
            _filePath = filePath;
            _fileName = fileName;
        }


        public void Log(string message)
        {
            try
            {
                using (var fileWriter = new StreamWriter(_filePath + _fileName))
                {
                    fileWriter.WriteLine($"{_dateTime.ToString()} : {message}");
                }
            }
            catch (ArgumentNullException ex)
            {
                throw new ArgumentNullException(ex.Message);
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(ex.Message);
            }

        }
    }
}

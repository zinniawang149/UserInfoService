using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserInfoService.Services
{
    public class ConsolePrinter : IPrinter
    {

        public void PrintByDelimiter(IEnumerable<string> obj, char delimiter, out string? result)
        {
            var stringBuilder = new StringBuilder();
            foreach (var item in obj)
            {
                stringBuilder.Append($"{item}{delimiter}");
            }
            if (stringBuilder.Length > 0) stringBuilder.Remove(stringBuilder.Length - 1, 1); //Remove the last delimiter
            result = stringBuilder.ToString();
            Console.WriteLine(result);
        }

        public void PrintByDelimiter(IEnumerable<string> obj, char delimiter)
        {
           PrintByDelimiter(obj, delimiter, out _);
        }
    }
}

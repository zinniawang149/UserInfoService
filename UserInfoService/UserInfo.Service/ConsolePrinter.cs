using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserInfoService.Services
{
    public class ConsolePrinter : IPrinter
    {

        public void PrintByDelimiter(IEnumerable<string> obj, char delimiter)
        {
            var stringBuilder = new StringBuilder();
            foreach (var item in obj)
            {
                stringBuilder.Append($"{item}{delimiter}");
            }
            if (stringBuilder.Length > 0) stringBuilder.Remove(stringBuilder.Length - 1, 1); //Remove the last delimiter
            Console.WriteLine(stringBuilder.ToString());
        }
    }
}

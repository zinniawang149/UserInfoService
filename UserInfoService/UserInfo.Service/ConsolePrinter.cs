using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserInfoService.Services
{
    public class ConsolePrinter : IPrinter
    {
        public void Print(string InfoToPrint) {
            Console.WriteLine(InfoToPrint);
        }
    }
}

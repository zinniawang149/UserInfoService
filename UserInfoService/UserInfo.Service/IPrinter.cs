﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserInfoService.Services
{
    public interface IPrinter
    {
        void PrintByDelimiter(IEnumerable<string> obj, char delimiter);
    }
}

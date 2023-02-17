using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace UserInfoService.Models
{

    public class User
    {
        public int Id { get; set; }
        public string? First { get; set; }
        public string? Last { get; set; }
        public int Age { get; set; }

        public Gender Gender { get; set; }
    }

    public enum Gender
    {
        M,
        F
    }
}


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
        F,
        Unknown
    }
}

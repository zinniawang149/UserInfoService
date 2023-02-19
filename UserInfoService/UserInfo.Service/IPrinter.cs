
namespace UserInfoService.Services
{
    public interface IPrinter
    {
        void PrintByDelimiter(IEnumerable<string> obj, char delimiter);
    }
}

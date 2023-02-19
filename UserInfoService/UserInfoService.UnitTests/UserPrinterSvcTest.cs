using Moq;
using UserInfoService.Models;
using UserInfoService.Services;

namespace UserInfo.Service.UnitTests
{
    public class UserPrinterSvcTest
    {
        private readonly Mock<IPrinter> _printerMock;
        private readonly UserPrinterSvc _userPrinterSvc;
        private readonly List<User> _users;

        public UserPrinterSvcTest()
        {
            _printerMock = new Mock<IPrinter>();
            _userPrinterSvc = new UserPrinterSvc(_printerMock.Object);
            _users = new List<User> {
                new User(){
                   Id=53,
                   First = "Bill",
                   Last = "Bryson",
                   Age = 23,
                   Gender = Gender.M
                },
                new User(){
                   Id=41,
                   First = "John",
                   Last = "Travolta",
                   Age = 23,
                   Gender = Gender.M
                },
                new User()
                {
                   Id=10,
                   First = "Zinnia",
                   Last = "Wang",
                   Age = 18,
                   Gender = Gender.F
                },
                new User()
                {
                   Id=10,
                   First = "Anna",
                   Last = "Meredith",
                   Age = 18,
                   Gender = Gender.F
                }
            };
        }
        [Fact(DisplayName = "The PrintUsersFullNames method should be filter by condition and call PrintByDelimiter")]
        public void PrintUsersFullNames_ShouldFilterByCondition_AndCallPrintByDelimiter()
        {
            // Arrange
            var expectCollectionById = new List<string> { "Zinnia Wang", "Anna Meredith" };
            var expectCollectionByAge = new List<string> { "Bill Bryson", "John Travolta" };

            // Act
            _userPrinterSvc.PrintUsersFullNames(_users, u => u.Id == 10);
            _userPrinterSvc.PrintUsersFullNames(_users, u => u.Age == 23);

            // Assert
            _printerMock.Verify(svc => svc.PrintByDelimiter(expectCollectionById, '\n'), Times.Once);
            _printerMock.Verify(svc => svc.PrintByDelimiter(expectCollectionByAge, '\n'), Times.Once);
        }

        [Fact(DisplayName = "The PrintUsersFirstNames method should be filter by condition and call PrintByDelimiter")]
        public void PrintUsersFirstNames_ShouldFilterByCondition_AndCallPrintByDelimiter()
        {
            // Arrange
            var expectCollectionById = new List<string> { "Zinnia", "Anna" };
            var expectCollectionByAge = new List<string> { "Bill", "John" };

            // Act
            _userPrinterSvc.PrintUsersFirstNames(_users, u => u.Id == 10);
            _userPrinterSvc.PrintUsersFirstNames(_users, u => u.Age == 23);

            // Assert
            _printerMock.Verify(svc => svc.PrintByDelimiter(expectCollectionById, ','), Times.Once);
            _printerMock.Verify(svc => svc.PrintByDelimiter(expectCollectionByAge, ','), Times.Once);
        }

        [Fact(DisplayName = "The PrintUsersStats method should be run correctly and call PrintByDelimiter")]
        public void PrintUsersStats_ShouldCallPrintByDelimiter_WithCorrectResult()
        {
            // Arrange
            var expectCollectionById = new List<string> { "Age:18 Female:2 Male:0", "Age:23 Female:0 Male:2" };

            // Act
            _userPrinterSvc.PrintUsersStats(_users);

            // Assert
            _printerMock.Verify(svc => svc.PrintByDelimiter(expectCollectionById, '\n'), Times.Once);
        }
    }
}
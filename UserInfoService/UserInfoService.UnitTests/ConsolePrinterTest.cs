using UserInfoService.Services;

namespace UserInfo.Service.UnitTests
{
    public class ConsolePrinterTest
    {

        [Theory(DisplayName = "PrintByDelimiter should print the result with dilimiter")]
        [InlineData(',')]
        [InlineData('\n')]
        public void PrintByDelimiter_ShouldPrintResultWithDelimiter(char delimiter)
        {
            // Arrange
            var objList = new List<string> { 
            "testString1", "testString2", "testString3"
            };
            var resultExpectation = $"testString1{delimiter}testString2{delimiter}testString3";
            var consolePrinter = new ConsolePrinter();
            // Act
            consolePrinter.PrintByDelimiter(objList,delimiter, out string result);
            // Assert
            Assert.Equal(resultExpectation, result);
        }
    }
}

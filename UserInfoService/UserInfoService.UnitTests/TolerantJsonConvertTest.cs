using UserInfo.Service.Helper;

namespace UserInfo.Service.UnitTests
{
    public class TolerantJsonConvertTest
    {
        [Theory(DisplayName = "DeserializeCollectionObject method should try to fix the invalid Json and produce usable data")]
        //Include \r\n, \n, additional comma, missing double quote on keys, key case insensitive
        [InlineData("[{ \"id\": 53, \"Name\": \"Bill\", \"type\":\"Invalid\", \"gender\":\"M\" },\r\n{ \"id\": 10, \"name\": \"Zinnia\", \"type\":\"Valid\", gender:\"F\"\n },]")] 
        //Include invalid type of value
        [InlineData("[{ \"id\": \"abc\", \"name\": \"Bill\", \"type\":\"Invalid\", \"gender\":\"M\" },{ \"id\": 53, \"name\": \"Bill\", \"type\":\"Invalid\", \"gender\":\"M\" },{ \"id\": 10, \"name\": \"Zinnia\", \"type\":\"Valid\", gender:\"F\" }]")] 
        //Include out of scope key name
        [InlineData("[{ \"id\": 53, \"name\": \"Bill\", \"type\":\"Invalid\", \"gender\":\"M\",\"outOfScope\":\"Invalid\" },{ \"id\": 10, \"name\": \"Zinnia\", \"type\":\"Valid\", gender:\"F\" }]")] 
        //Include not finished json
        [InlineData("[{ \"id\": 53, \"name\": \"Bill\", \"type\":\"Invalid\", \"gender\":\"M\" },{ \"id\": 10, \"name\": \"Zinnia\", \"type\":\"Valid\", gender:\"F\" }, { \"id\": 25, \"name\": \"Winne\", \"type\":\"V")] 
        public void DeserializeCollectionObject_ShouldDeserializeObjectCorrectly(string inputJsonString)
        {
            //Arrange
            var expectObjList = new List<object> { 
                new TestObject(){ 
                   Id=53,
                   Name = "Bill",
                   Type = Type.Invalid,
                   Gender = Gender.M
                },
                new TestObject()
                {
                   Id=10,
                   Name = "Zinnia",
                   Type = Type.Valid,
                   Gender = Gender.F
                }
            };

            //Act
            var objList = TolerantJsonConvert.DeserializeCollectionObject<TestObject>(inputJsonString)!.ToList();

            //Assert
            Assert.Equal(expectObjList, objList);

        }
        [Fact(DisplayName = "By passing TolerantEnumConverter into DeserializeCollectionObject, it should convert unknown Enum property")]
        public void TolerantEnumConverter_ShouldConvertUnknownEnum()
        {
            //Arrange
            var inputJsonString = "[{ \"id\": 53, \"name\": \"Bill\", \"type\":\"Invalid\", \"gender\":\"Y\" },{ \"id\": 10, \"name\": \"Zinnia\", \"type\":\"ICanFly\", gender:\"F\" }]";
            var tolerantEnumConverter = new TolerantEnumConverter();
            var expectObjList = new List<object> {
                new TestObject(){
                   Id=53,
                   Name = "Bill",
                   Type = Type.Invalid,
                   Gender = Gender.Unknown
                },
                new TestObject()
                {
                   Id=10,
                   Name = "Zinnia",
                   Type = null,
                   Gender = Gender.F
                }
            };
            //Act
            var objList = TolerantJsonConvert.DeserializeCollectionObject<TestObject>(inputJsonString, tolerantEnumConverter)!.ToList();
            //Assert
            Assert.Equal(expectObjList, objList);
        }

        class TestObject : IEquatable<TestObject>
        {
            public int Id { get; set; }
            public string? Name { get; set; }
            public Gender Gender { get; set; }
            public Type? Type { get; set; }

            public bool Equals(TestObject? other)
            {
                if (other == null)
                    return false;

                var result = Id.Equals(other.Id) && Gender.Equals(other.Gender) && Type.Equals(other.Type);
                if (Name != null) { 
                    result = result && Name.Equals(other.Name); 
                }
                return result;
            }
        }

        enum Gender
        {
            M,
            F,
            Unknown
        }
        enum Type
        {
            Valid,
            Invalid,
        }
    }


}
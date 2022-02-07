using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EDiaryUnitTests
{
    public class EDiaryConsole
    {
        [Fact]
        public void ValidateDateStringShouldPassWithValidString()
        {
            //Arrange
            DateValidationModel expected = new DateValidationModel(true, "The format is as expected");
            string dateString = "06:02:2022";
            //Act
            DateValidationModel actual = EDiaryApp.Operation.ValidateDateString(dateString);
            //Assert
            Assert.Equal(expected.IsValid, actual.IsValid);    
            Assert.Equal(expected.ValidationMessage, actual.ValidationMessage);
            
        }
    }
}

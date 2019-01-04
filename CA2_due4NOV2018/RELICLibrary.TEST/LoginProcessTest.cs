using RelicClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
namespace RELICLibrary.TEST
{
    public class LoginProcessTest
    {
        
        [Theory]
        [InlineData ("J","C",true)]     // normal login
        [InlineData("J", "", false)]    // no password provided
        [InlineData("", "C", false)] //no username provided
        [InlineData("twobigavalue", "C", false)] //username too big
        [InlineData("C","twobigavalue", false)] //password too big

        public void ValidateUserInput_stringsShouldVerify(string username, string password, bool expected)
        {
            //Arrange
            LoginProcess loginProcess = new LoginProcess();           

            //Act
            bool actual = loginProcess.ValidateLoginData(username, password);
            //assert
            Assert.Equal(expected, actual);
        }
    }
}

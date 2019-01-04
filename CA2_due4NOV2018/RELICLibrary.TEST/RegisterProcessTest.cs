using RelicClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace RELICLibrary.TEST
{
    public class RegisterProcessTest
    {

        [Theory]
        [InlineData("kjamieson", "password", 5006, "Keith", "Jamieson", 2, "P", "AP", "I", "0222222222", "Me@here.com", true)] //should pass
        [InlineData("kjamieson", "password", 1, "Keith", "Jamieson", 2, "P", "AP", "I", "0222222222", "Me@here.com", false)] //airc_id exists
        [InlineData("", "password", 1, "Keith", "Jamieson", 2, "P", "AP", "I", "0222222222", "Me@here.com", false)] //no username
        [InlineData("kjamieson", "", 1, "Keith", "Jamieson", 2, "P", "AP", "I", "0222222222", "Me@here.com", false)] //no password
        [InlineData("kjamieson", "password", 5007, "", "Jamieson", 2, "P", "AP", "I", "0222222222", "Me@here.com", false)] //no firstname
        [InlineData("kjamieson", "password", 5007, "Keith", "", 2, "P", "AP", "I", "0222222222", "Me@here.com", false)] //no lastname
        [InlineData("kjamieson", "password", 5007, "Keith", "Jamieson", 0, "P", "AP", "I", "0222222222", "Me@here.com", false)] //no club
        [InlineData("kjamieson", "password", 5007, "Keith", "Jamieson", 2, "", "AP", "I", "0222222222", "Me@here.com", false)] //no dressage grade
        [InlineData("kjamieson", "password", 5007, "Keith", "Jamieson",2, "P", "", "I", "0222222222", "Me@here.com", false)] //no showjump grade
        [InlineData("kjamieson", "password", 5007, "Keith", "Jamieson", 2, "P", "AP", "", "0222222222", "Me@here.com", false)] //no cross-country grade
        [InlineData("kjamieson", "password", 5007, "Keith", "Jamieson", 2, "AI", "O", "AO", "", "Me@here.com", true)] //no phone
        [InlineData("kjamieson", "password", 5007, "Keith", "Jamieson", 2, "AP", "I", "AI", "0222222222", "", true)] //no email
        [InlineData("kjamieson", "password", 5007, "Keith", "Jamieson", 2, "AP", "I", "AI", "012345678901234567890123456789", "", false)] //phone too big
        [InlineData("kjamieson", "password", 5007, "Keith", "Jamieson", 2, "AP", "I", "AI", "This electronic email address is far larger than the 40 characters that are allowed", "", false)] //email too big
        public void ValidateUserInput_stringsShouldVerify(string username, string password, int airc_id, string firstname, string lastname, int club_id, string DR, string SJ, string XC, string phone, string email, bool expected)
        {
      
        //Arrange
        RegisterProcess registerProcess = new RegisterProcess();


            //Act
            //    bool actual = registerProcess.ValidateRegistrationData("jconnolly", "password",5006,"James","Connolly",2,"P","AP","I","0222222222","Me@here.com");
            bool actual = registerProcess.ValidateRegistrationData(
                username, password, airc_id, firstname,  lastname,  club_id,  DR, SJ, XC, phone, email);
            //assert
            Assert.Equal(expected, actual);
        }
    }
}
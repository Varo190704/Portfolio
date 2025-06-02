using Domain.Utilities;
using Domain.Validators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assert = NUnit.Framework.Assert;

namespace BusinessLogicTest.Utilities;

[TestClass]
public class UserTest
{
    [TestMethod]
    public void create_valid_user()
    {
        User user = new User("Alvaro", "Redo", "example@gmail.com", "$Ab123456", new DateOnly(2004, 07, 19));
        Assert.AreEqual("Alvaro", user.Name);
        Assert.AreEqual("Redo", user.Surname);
        Assert.AreEqual("example@gmail.com", user.Email);
        Assert.AreEqual("$Ab123456", user.Password);
        Assert.IsNotNull(user.BirthDate);
    }

    [TestMethod]
    public void create_invalid_user_bcName()
    {
        Assert.Throws<ArgumentException>(() =>
            {
                User user = new User("", "Redo", "example@gmail.com", "$Aa123456", new DateOnly(2004, 07, 19));
            }
        );
    }
    
    [TestMethod]
    public void create_invalid_user_bcSurname()
    {
        Assert.Throws<ArgumentException>(() =>
            {
                User user = new User("Alvaro", "", "example@gmail.com", "$Aa123456", new DateOnly(2004, 07, 19));
            }
        );
    }
    
    [TestMethod]
    public void create_invalid_user_bcEmail()
    {
        Assert.Throws<ArgumentException>(() =>
            {
                User user = new User("Alvaro", "Redo", "", "$Aa123456", new DateOnly(2004, 07, 19));
            }
        );
    }
    
    //i make only the invalid pass bc if u put null or "" password the userClass will throw 
    //an exception and if u put some invalid pass it should throw another dif exception.

    [TestMethod]
    public void password_get_invalid_bcNotSpecialChar()
    {
        //no especial charac
        Assert.Throws<ArgumentException>(() =>
            {
                User user = new User("Alvaro", "Redo", "example@gmail.com", "Ab123456", new DateOnly(2004, 07, 19));
            }
        );
    }
    
    [TestMethod]
    public void password_get_invalid_bcNotUpper()
    {
        //No upper
        Assert.Throws<ArgumentException>(() =>
            {
                User user = new User("Alvaro", "Redo", "example@gmail.com", "$b123456", new DateOnly(2004, 07, 19));
            }
        );
    }

    [TestMethod]
    public void password_get_invalid_bcNotNum()
    {
        //No num
        Assert.Throws<ArgumentException>(() =>
            {
                User user = new User("Alvaro", "Redo", "example@gmail.com", "$Abcdefg", new DateOnly(2004, 07, 19));
            }
        );
    }
    
    [TestMethod]
    public void password_get_invalid_bcNotLower()
    {
        //No lower
        Assert.Throws<ArgumentException>(() =>
            {
                User user = new User("Alvaro", "Redo", "example@gmail.com", "$A1234567", new DateOnly(2004, 07, 19));
            }
        );
    }
    
    //i will make all types of mail validations
    
    [TestMethod]
    public void email_get_valid()
    {
        Assert.AreEqual(true, Validator.validEmail("example@gmail.com"));
    }
    //no user
    [TestMethod]
    public void email_get_invalid_bcNotfirstPart()
    {
        Assert.AreEqual(false, Validator.validEmail(" @gmail.com"));    
    }
    //no domain first part
    [TestMethod]
    public void email_get_invalid_bcNotsecondPart()
    {
        Assert.AreEqual(false, Validator.validEmail("example@.com"));    
    }
    //no valid domaind secondpart
    [TestMethod]
    public void email_get_invalid_bcNotthirdPart()
    {
        Assert.AreEqual(false, Validator.validEmail("example@gmail.a"));    
    }
    //no dot
    [TestMethod]
    public void email_get_invalid_bcNotDot()
    {
        Assert.AreEqual(false, Validator.validEmail("example@gmail"));    
    }
    //no @
    [TestMethod]
    public void email_get_invalid_bcNotAt()
    {
        Assert.AreEqual(false, Validator.validEmail("examplegmail.com"));    
    }
    
}
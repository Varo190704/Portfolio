using Domain.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace BusinessLogicTest.Utilities;

[TestClass]
public class Alert_Test
{
    [TestMethod]
    public void create_valid_alert()
    {
        Alert alert = new Alert("This is a test", 1, "email@gmail.com");
        Assert.AreEqual("This is a test", alert.Message);
        Assert.AreEqual(1, alert.Id);
    }
    
    [TestMethod]
    public void create_invalid_alert_bcId()
    {
        Assert.Throws<ArgumentException>(() =>
            {
                Alert alert = new Alert("This is a test", 0, "email@gmail.com");
            }
        );
    }
        
    [TestMethod]
    public void create_invalid_alert_bcMessage()
    {
        Assert.Throws<ArgumentException>(() =>
            {
                Alert alert = new Alert("", 0, "email@gmail.com");
            }
        );
    }
    
    [TestMethod]
    public void create_invalid_alert_bcMessageWhiteSpace()
    {
        Assert.Throws<ArgumentException>(() =>
            {
                Alert alert = new Alert("  ", 0, "email@gmail.com");
            }
        );
    }
}
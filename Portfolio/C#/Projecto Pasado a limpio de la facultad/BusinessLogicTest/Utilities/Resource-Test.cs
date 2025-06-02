using Domain.Utilities;
using Domain.Enum;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace BusinessLogicTest.Utilities;

[TestClass]
public class Resource_Test
{
    [TestMethod]
    public void create_resource()
    {
        Resource resourse = new Resource("Develop", TaskType.Human, "Software developer");
        Assert.AreEqual("Develop", resourse.Name);
        Assert.AreEqual(TaskType.Human, resourse.TaskType);
        Assert.AreEqual("Software developer", resourse.Description);
    }

    [TestMethod]
    public void create_invalid_resource()
    {
        Assert.Throws<ArgumentException>(() =>
        {
            Resource resource = new Resource("", TaskType.Human, "Not a valid resource");
        });
    }
    
    //User and dev cant create a resource with a type !include on
    //TaskType.cs
    
}
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
        Resource resourse = new Resource("Develop", TaskType.Human, "Software developer", "Develop");
        Assert.AreEqual("Develop", resourse.Name);
        Assert.AreEqual(TaskType.Human, resourse.TaskType);
        Assert.AreEqual("Software developer", resourse.Description);
    }

    [TestMethod]
    public void create_invalid_resource()
    {
        Assert.Throws<ArgumentException>(() =>
        {
            Resource resource = new Resource("", TaskType.Human, "Not a valid resource", "");
        });
    }
    
    [TestMethod]
    public void assign_resource_should_add_to_both_lists()
    {
        var resource = new Resource("Dev", TaskType.Human, "Backend dev", "Develop");
        var start = new DateOnly(2025, 6, 1);
        var end = new DateOnly(2025, 6, 5);

        resource.assigned("Task A", "Project X", start, end);

        Assert.AreEqual(1, resource.resourceUsed.Count);
        Assert.AreEqual(1, resource.ResourceUsedComp.Count);
        Assert.AreEqual("Task A", resource.resourceUsed[0].Task);
        Assert.AreEqual("Project X", resource.resourceUsed[0].Project);
    }

    [TestMethod]
    public void get_task_assigned_should_return_correct_assignment()
    {
        var resource = new Resource("QA", TaskType.Human, "Tester", "QA");
        resource.assigned("Task B", "Project Y", new DateOnly(2025, 6, 2), new DateOnly(2025, 6, 3));

        var assignment = resource.GetTaskAssigned("Task B");

        Assert.IsNotNull(assignment);
        Assert.AreEqual("Project Y", assignment.Project);
    }

    [TestMethod]
    public void is_resource_used_should_detect_overlap()
    {
        var resource = new Resource("DB", TaskType.Human, "DBA", "DB");
        resource.assigned("Task C", "Project Z", new DateOnly(2025, 6, 10), new DateOnly(2025, 6, 20));

        bool result = resource.IsResourceUsed(new DateOnly(2025, 6, 15), new DateOnly(2025, 6, 25));
    
        Assert.IsTrue(result);
    }

    [TestMethod]
    public void get_resource_used_should_return_correct_resource()
    {
        var resource = new Resource("SysAdmin", TaskType.Human, "Infra work", "IT");
        resource.assigned("Task D", "Project I", new DateOnly(2025, 7, 1), new DateOnly(2025, 7, 5));

        var resUsed = resource.GetResourceUsed(new DateOnly(2025, 7, 2), new DateOnly(2025, 7, 3));
    
        Assert.IsNotNull(resUsed);
        Assert.AreEqual("Task D", resUsed.Task);
    }

}
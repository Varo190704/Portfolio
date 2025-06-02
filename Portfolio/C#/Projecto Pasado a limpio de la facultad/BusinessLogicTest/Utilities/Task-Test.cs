using Domain.Enum;
using Domain.Utilities;
using Task = Domain.Utilities.Task;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace BusinessLogicTest.Utilities;

[TestClass]
public class Task_Test
{
    [TestMethod]
    public void create_valid_Task()
    {
        Task task = new Task("Task", "Descrip Task", new DateOnly(2024, 07,19), 10);
        Assert.AreEqual("Task", task.Name);
        Assert.AreEqual("Descrip Task", task.Description);
        Assert.IsNotNull(task.StartDate);
        Assert.AreEqual(task.Duration, 10);
        Assert.IsNotNull(task.EndDate);
        Assert.IsNotNull(task.Status);
    }

    [TestMethod]
    public void create_invalid_Task_bcName()
    {
        Assert.Throws<ArgumentException>(() =>
        {
            Task task = new Task("", "Descrip Task", new DateOnly(2024, 07, 19), 10);
        }); 
    }
    
    [TestMethod]
    public void  create_invalid_Task_bcDuration()
    {
        Assert.Throws<ArgumentException>(() =>
        {
            Task task = new Task("Task", "Descrip Task", new DateOnly(2024, 07, 19), 0);
        });
    }
    
    [TestMethod]
    public void add_dependency()
    {
        Task task = new Task("Task", "Descrip Task", new DateOnly(2024, 07, 19), 1);
        Task task2 = new Task("Task2", "desc", new DateOnly(2024, 07, 14), 3);
        task.AddDepen(task2);
        Assert.AreEqual(1, task.Dependencies.Count);
    }
    
    [TestMethod]
    public void invalid_add_dependency_bcNullDepend()
    {
        Task task = new Task("Task", "Descrip Task", new DateOnly(2024, 07, 19), 1);
        Assert.Throws<ArgumentException>(() =>
        {
            task.AddDepen(null);
        });
    }
    
    [TestMethod]
    public void add_duplicate_dependency()
    {
        Task task = new Task("Task", "Descrip Task", new DateOnly(2024, 07, 19), 1);
        Task task2 = new Task("Task2", "desc", new DateOnly(2024, 07, 14), 3);
        task.AddDepen(task2);
        Assert.Throws<ArgumentException>(() =>
        {
            task.AddDepen(task2);
        });
    }
    
    [TestMethod]
public void ctor_with_status_initializes_properties_correctly()
{
    var start = new DateOnly(2025, 1, 1);
    var task = new Task("T1", "Desc", start, 5, TaskProgress.InProgress);
    Assert.AreEqual("T1", task.Name);
    Assert.AreEqual("Desc", task.Description);
    Assert.AreEqual(5, task.Duration);
    Assert.AreEqual(start.AddDays(5), task.EndDate);
    Assert.AreEqual(TaskProgress.InProgress, task.Status);
    Assert.AreEqual(0, task.Slak);
}

[TestMethod]
public void name_setter_accepts_valid_string()
{
    var t = new Task();
    t.Name = "Proyecto X";
    Assert.AreEqual("Proyecto X", t.Name);
}

[TestMethod]
public void name_setter_rejects_null_or_empty()
{
    var t = new Task();
    Assert.ThrowsException<ArgumentException>(() => t.Name = null);
    Assert.ThrowsException<ArgumentException>(() => t.Name = "");
}

[TestMethod]
public void duration_setter_accepts_positive_value()
{
    var t = new Task();
    t.Duration = 7;
    Assert.AreEqual(7, t.Duration);
}

[TestMethod]
public void duration_setter_rejects_invalid_values()
{
    var t = new Task();
    Assert.ThrowsException<ArgumentException>(() => t.Duration = 0);
    Assert.ThrowsException<ArgumentException>(() => t.Duration = -3);
}

[TestMethod]
public void add_and_remove_dependency_happy_path()
{
    var t1 = new Task("A", "", new DateOnly(2025, 1, 1), 1);
    var dep = new Task("B", "", new DateOnly(2025, 1, 2), 2);
    t1.AddDepen(dep);
    Assert.AreEqual(1, t1.Dependencies.Count);
    t1.RemoveDependency(dep);
    Assert.AreEqual(0, t1.Dependencies.Count);
}

[TestMethod]
public void add_dependency_throws_on_null_or_duplicate()
{
    var t1 = new Task("A", "", new DateOnly(2025, 1, 1), 1);
    Assert.ThrowsException<ArgumentException>(() => t1.AddDepen(null));
    var dep = new Task("B", "", new DateOnly(2025, 1, 2), 2);
    t1.AddDepen(dep);
    Assert.ThrowsException<ArgumentException>(() => t1.AddDepen(dep));
}

[TestMethod]
public void remove_dependency_throws_on_null()
{
    var t1 = new Task("A", "", new DateOnly(2025, 1, 1), 1);
    Assert.ThrowsException<ArgumentException>(() => t1.RemoveDependency(null));
}

[TestMethod]
public void remove_dependency_throws_when_missing()
{
    var t1 = new Task("A", "", new DateOnly(2025, 1, 1), 1);
    var dep = new Task("B", "", new DateOnly(2025, 1, 2), 2);
    Assert.ThrowsException<ArgumentException>(() => t1.RemoveDependency(dep));
}

[TestMethod]
public void add_and_remove_resource_happy_path()
{
    var t = new Task("X", "", new DateOnly(2025, 2, 1), 3);
    var r = new Resource("R1", TaskType.Human, "Desc");
    t.AddResource(r);
    Assert.AreEqual(1, t.Resources.Count);
    t.RemoveResource(r);
    Assert.AreEqual(0, t.Resources.Count);
}

[TestMethod]
public void add_resource_throws_on_null_or_duplicate()
{
    var t = new Task("X", "", new DateOnly(2025, 2, 1), 3);
    Assert.ThrowsException<ArgumentException>(() => t.AddResource(null));
    var r = new Resource("R1", TaskType.Software, "Desc");
    t.AddResource(r);
    Assert.ThrowsException<ArgumentException>(() => t.AddResource(r));
}

[TestMethod]
public void remove_resource_throws_on_null()
{
    var t = new Task("Y", "", new DateOnly(2025, 3, 1), 2);
    Assert.ThrowsException<ArgumentException>(() => t.RemoveResource(null));
}

[TestMethod]
public void remove_resource_no_error_when_missing()
{
    var t = new Task("Y", "", new DateOnly(2025, 3, 1), 2);
    var r = new Resource("R2", TaskType.Infrastructure, "Desc");
    t.RemoveResource(r);
    Assert.AreEqual(0, t.Resources.Count);
}
   
}
using Domain.Validators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assert = NUnit.Framework.Assert;
using Task = Domain.Utilities.Task;

namespace BusinessLogicTest.Validators;

[TestClass]
public class criticalPath_Test
{
    [TestMethod]
    public void throw_exception_if_task_list_is_null()
    {
        Assert.Throws<ArgumentException>(() =>
        {
            CriticalPathCalculator.Calculate(null);
        });
    }
    
    [TestMethod]
    public void criticPath()
    {
        //same date on task1, task 2 and task 3 bc my criticalPath class change startdates.
        //Put EndDates of dependencies as StartDate dependants 
        Task task1 = new Task("1", "Start", new DateOnly(2024, 1, 1), 1);
        Task task2 = new Task("2", "Middle", new DateOnly(2024, 1, 1), 1);
        Task task3 = new Task("3", "End", new DateOnly(2024, 1, 1), 1);

        task2.AddDepen(task1);
        task3.AddDepen(task2);

        List<Task> tasks = new List<Task>();
        tasks.Add(task1);
        tasks.Add(task2);
        tasks.Add(task3);
        
        CriticalPathCalculator.Calculate(tasks);

        Assert.AreEqual(0, task1.Slak);
        Assert.AreEqual(0, task2.Slak);
        Assert.AreEqual(0, task3.Slak);
    }
    
    [TestMethod]
    public void critical_path_with_task_with_slak()
    {
        Task task1 = new Task("A", "Critical start", new DateOnly(2024, 1, 1), 2);
        Task task2 = new Task("B", "Parallel shorter", new DateOnly(2024, 1, 1), 1);
        Task task3 = new Task("C", "Join", new DateOnly(2024, 1, 1), 1);

        task3.AddDepen(task1);
        task3.AddDepen(task2);

        List<Task> tasks = new List<Task>();
        tasks.Add(task1);
        tasks.Add(task2);
        tasks.Add(task3);

        CriticalPathCalculator.Calculate(tasks);
        //A has duration 2, B has duration 1 and C depends from A and B. \
        //C starts on time 2 so B has 1 extra time to finish 
        Assert.AreEqual(0, task1.Slak);
        Assert.AreEqual(1, task2.Slak);
        Assert.AreEqual(0, task3.Slak);
    }
    
    [TestMethod]
    public void lateDates_same_Early()
    {
        Task task1 = new Task("1", "Start", new DateOnly(2024, 1, 1), 1);
        Task task2 = new Task("2", "Middle", new DateOnly(2024, 1, 1), 1);
        Task task3 = new Task("3", "End", new DateOnly(2024, 1, 1), 1);

        task2.AddDepen(task1);
        task3.AddDepen(task2);

        List<Task> tasks = new List<Task>();
        tasks.Add(task1);
        tasks.Add(task2);
        tasks.Add(task3);

        CriticalPathCalculator.Calculate(tasks);

        // Path + times Task1[1,2] -> Task2[2,3] -> Task2[3, 4] 

        Assert.AreEqual(new DateOnly(2024, 1, 3), task3.StartDate);
        Assert.AreEqual(new DateOnly(2024, 1, 4), task3.EndDate);
        Assert.AreEqual(task3.EndDate, task3.LateEndDate);
        Assert.AreEqual(task3.LateEndDate.AddDays(-1), task3.LateStartDate);

        Assert.AreEqual(task2.EndDate, task2.LateEndDate);
        Assert.AreEqual(task2.LateEndDate.AddDays(-1), task2.LateStartDate);
        Assert.AreEqual(task3.StartDate, task2.EndDate);
        
        Assert.AreEqual(task1.EndDate, task1.LateEndDate);
        Assert.AreEqual(task1.LateEndDate.AddDays(-1), task1.LateStartDate);
        Assert.AreEqual(task2.StartDate, task1.EndDate);
    }


}
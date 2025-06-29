using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Aplication.Services;
using Domain.Enum;
using Domain.Infrastructure.SqlConecc;
using Domain.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using Task = Domain.Utilities.Task;

namespace BusinessLogicTest.Services;

[TestClass]
public class TaskManager_Test
{
    [TestMethod]
    public void getAllTasks_returns_all_tasks()
    {
        var repo = new TaskInSQL();
        var mgr = new TaskManager(repo);
        var t1 = new Task("A", "", new DateOnly(2025, 1, 1), 1);
        var t2 = new Task("B", "", new DateOnly(2025, 1, 2), 2);
        repo.Add(t1);
        repo.Add(t2);
        var all = mgr.getAllTasks().ToList();
        Assert.AreEqual(2, all.Count);
        Assert.IsTrue(all.Contains(t1));
        Assert.IsTrue(all.Contains(t2));
    }

    [TestMethod]
    public void getTasksByName_returns_entity_or_null_and_throws_on_invalid()
    {
        var repo = new TaskInSQL();
        var mgr = new TaskManager(repo);
        var t = new Task("X", "", new DateOnly(2025, 1, 1), 1);
        repo.Add(t);
        Assert.AreSame(t, mgr.getTasksByName("X"));
        Assert.IsNull(mgr.getTasksByName("Y"));
        Assert.ThrowsException<ArgumentException>(() => mgr.getTasksByName(null));
    }

    [TestMethod]
    public void getTaskByName_behaves_same_as_getTasksByName()
    {
        var repo = new TaskInSQL();
        var mgr = new TaskManager(repo);
        var t = new Task("Y", "", new DateOnly(2025, 1, 1), 1);
        repo.Add(t);
        Assert.AreSame(t, mgr.getTaskByName("Y"));
        Assert.IsNull(mgr.getTaskByName("Z"));
        Assert.ThrowsException<ArgumentException>(() => mgr.getTaskByName(null));
    }

    [TestMethod]
    public void createTask_valid_adds_and_invalid_throws()
    {
        var repo = new TaskInSQL();
        var mgr = new TaskManager(repo);
        Assert.ThrowsException<ArgumentException>(() => mgr.createTask(null));
        var t = new Task("C", "", new DateOnly(2025, 1, 1), 1);
        mgr.createTask(t);
        Assert.AreEqual(1, repo.GetAll().Count());
        Assert.ThrowsException<ArgumentException>(() => mgr.createTask(t));
    }

    [TestMethod]
    public void deleteTask_valid_removes_and_invalid_throws()
    {
        var repo = new TaskInSQL();
        var mgr = new TaskManager(repo);
        Assert.ThrowsException<ArgumentException>(() => mgr.deleteTask(null));
        var t = new Task("D", "", new DateOnly(2025, 1, 1), 1);
        Assert.ThrowsException<ArgumentException>(() => mgr.deleteTask(t));
        mgr.createTask(t);
        mgr.deleteTask(t);
        Assert.AreEqual(0, repo.GetAll().Count());
    }

    [TestMethod]
    public void updateTask_throws_when_missing_and_updates_when_exists()
    {
        var repo = new TaskInSQL();
        var mgr = new TaskManager(repo);
        var t = new Task("E", "Old", new DateOnly(2025, 1, 1), 1);
        Assert.ThrowsException<ArgumentException>(() => mgr.updateTask(t));
        mgr.createTask(t);
        var updated = new Task("E", "New", new DateOnly(2025, 1, 2), 2);
        mgr.updateTask(updated);
        var inRepo = repo.getByName("E");
        Assert.AreEqual("New", inRepo.Description);
        Assert.AreEqual(2, inRepo.Duration);
    }

    [TestMethod]
    public void AddDependency_and_RemoveDependency_behave_correctly()
    {
        var repo = new TaskInSQL();
        var mgr = new TaskManager(repo);
        var t = new Task("F", "", new DateOnly(2025, 1, 1), 1);
        var dep = new Task("G", "", new DateOnly(2025, 1, 2), 2);
        repo.Add(t);
        Assert.ThrowsException<ArgumentException>(() => mgr.AddDependency(null, dep));
        mgr.AddDependency(t, dep);
        Assert.AreEqual(1, t.Dependencies.Count);
        mgr.RemoveDependency(t, dep);
        Assert.AreEqual(0, t.Dependencies.Count);
    }

    [TestMethod]
    public void AssResource_and_DeleteResource_behave_correctly()
    {
        var repo = new TaskInSQL();
        var mgr = new TaskManager(repo);
        var t = new Task("H", "", new DateOnly(2025, 1, 1), 1);
        var r = new Resource("R", TaskType.Human, "D", "R");
        repo.Add(t);
        mgr.AssResource(t, r);
        Assert.AreEqual(1, t.Resources.Count);
        mgr.DeleteResource(t, r);
        Assert.AreEqual(0, t.Resources.Count);
    }

    [TestMethod]
    public void RecalculateCriticalPath_does_not_throw_and_preserves_tasks()
    {
        var repo = new TaskInSQL();
        var mgr = new TaskManager(repo);
        mgr.RecalculateCriticalPath();
        var t = new Task("I", "", new DateOnly(2025, 1, 1), 3);
        repo.Add(t);
        mgr.RecalculateCriticalPath();
        Assert.AreEqual(1, repo.GetAll().Count());
    }

    [TestMethod]
    public void AlwaysUpdateTasks_filters_resources_and_updates_repo()
    {
        var repo = new TaskInSQL();
        var mgr = new TaskManager(repo);
        var task = new Task("T", "", new DateOnly(2025, 1, 1), 1);
        repo.Add(task);
        var r1 = new Resource("R1", TaskType.Software, "Old", "R1");
        var r2 = new Resource("R2", TaskType.Human, "Old2", "R2");
        task.Resources.Add(r1);
        task.Resources.Add(r2);
        var updatedResources = new List<Resource> { new Resource("T", TaskType.Infrastructure, "New", "T") };
        mgr.AlwaysUpdateTasks(updatedResources, new List<Task> { task });
        Assert.AreEqual(0, task.Resources.Count);
    }
}

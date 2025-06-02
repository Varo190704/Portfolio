using Domain.Infrastructure.Persistence;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using Task = Domain.Utilities.Task;

namespace BusinessLogicTest.Infrastructure;

[TestClass]
public class RepositoryMemory_Test
{
    [TestMethod]
    public void getall_initially_empty()
    {
        var repo = new RepositoryInMemory<Task>();
        Assert.AreEqual(0, repo.GetAll().Count());
    }

    [TestMethod]
    public void add_entity_increases_count()
    {
        var repo = new RepositoryInMemory<Task>();
        var t = new Task("T", "", new DateOnly(2025,1,1),1);
        repo.Add(t);
        Assert.AreEqual(1, repo.GetAll().Count());
        Assert.AreSame(t, repo.GetAll().First());
    }

    [TestMethod]
    public void getByName_valid_name_returns_entity()
    {
        var repo = new RepositoryInMemory<Task>();
        var t = new Task("T", "", new DateOnly(2025,1,1),1);
        repo.Add(t);
        var found = repo.getByName("T");
        Assert.AreSame(t, found);
    }

    [TestMethod]
    public void getByName_null_or_whitespace_throws()
    {
        var repo = new RepositoryInMemory<Task>();
        Assert.ThrowsException<ArgumentException>(() => repo.getByName(null));
        Assert.ThrowsException<ArgumentException>(() => repo.getByName(""));
        Assert.ThrowsException<ArgumentException>(() => repo.getByName(" "));
    }

    [TestMethod]
    public void getByName_not_found_returns_null()
    {
        var repo = new RepositoryInMemory<Task>();
        var found = repo.getByName("X");
        Assert.IsNull(found);
    }

    [TestMethod]
    public void update_null_entity_throws()
    {
        var repo = new RepositoryInMemory<Task>();
        Assert.ThrowsException<ArgumentException>(() => repo.Update(null));
    }

    [TestMethod]
    public void update_nonexistent_entity_throws()
    {
        var repo = new RepositoryInMemory<Task>();
        var t = new Task("T", "", new DateOnly(2025,1,1),1);
        Assert.ThrowsException<ArgumentException>(() => repo.Update(t));
    }

    [TestMethod]
    public void update_existing_entity_sets_properties()
    {
        var repo = new RepositoryInMemory<Task>();
        var t = new Task("T", "Desc", new DateOnly(2025,1,1),1);
        repo.Add(t);
        var updated = new Task("T", "NewDesc", new DateOnly(2025,1,2),2);
        repo.Update(updated);
        var inRepo = repo.getByName("T");
        Assert.AreEqual("NewDesc", inRepo.Description);
        Assert.AreEqual(2, inRepo.Duration);
        Assert.AreEqual(new DateOnly(2025,1,2), inRepo.StartDate);
        Assert.AreEqual(new DateOnly(2025,1,2).AddDays(2), inRepo.EndDate);
    }

    [TestMethod]
    public void remove_existing_entity_decreases_count()
    {
        var repo = new RepositoryInMemory<Task>();
        var t = new Task("T", "", new DateOnly(2025,1,1),1);
        repo.Add(t);
        repo.Remove(t);
        Assert.AreEqual(0, repo.GetAll().Count());
    }

    [TestMethod]
    public void remove_nonexistent_entity_does_not_throw()
    {
        var repo = new RepositoryInMemory<Task>();
        var t = new Task("T", "", new DateOnly(2025,1,1),1);
        repo.Remove(t);
        Assert.AreEqual(0, repo.GetAll().Count());
    }
}

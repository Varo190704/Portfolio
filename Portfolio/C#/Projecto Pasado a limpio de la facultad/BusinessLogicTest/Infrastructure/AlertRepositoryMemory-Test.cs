using Domain.Infrastructure.Persistence;
using Domain.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using Task = Domain.Utilities.Task;

namespace BusinessLogicTest.Infrastructure;

[TestClass]
public class AlertRepositoryMemory_Test
{
    [TestMethod]
    public void getById_invalid_id_throws()
    {
        var repo = new AlertRepositoryInMemory();
        Assert.ThrowsException<ArgumentException>(() => repo.getById(0));
        Assert.ThrowsException<ArgumentException>(() => repo.getById(-5));
    }

    [TestMethod]
    public void getById_returns_null_when_not_found()
    {
        var repo = new AlertRepositoryInMemory();
        var result = repo.getById(1);
        Assert.IsNull(result);
    }

    [TestMethod]
    public void getById_returns_entity_when_exists()
    {
        var repo = new AlertRepositoryInMemory();
        var alert = new Alert { Id = 1, Message = "Hello" };
        repo.Add(alert);
        var result = repo.getById(1);
        Assert.AreSame(alert, result);
    }

    [TestMethod]
    public void update_null_alert_throws()
    {
        var repo = new AlertRepositoryInMemory();
        Assert.ThrowsException<ArgumentException>(() => repo.Update(null));
    }

    [TestMethod]
    public void update_nonexistent_alert_throws()
    {
        var repo = new AlertRepositoryInMemory();
        var alert = new Alert { Id = 1, Message = "Hello" };
        Assert.ThrowsException<ArgumentException>(() => repo.Update(alert));
    }

    [TestMethod]
    public void update_existing_alert_changes_message()
    {
        var repo = new AlertRepositoryInMemory();
        var alert = new Alert { Id = 1, Message = "Old" };
        repo.Add(alert);
        var updated = new Alert { Id = 1, Message = "New" };
        repo.Update(updated);
        var result = repo.getById(1);
        Assert.AreEqual("New", result.Message);
    }
}
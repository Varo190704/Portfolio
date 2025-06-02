using System;
using System.Linq;
using Domain.Infrastructure.Persistence;
using Domain.Utilities;
using Domain.Aplication.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace BusinessLogicTest.Services;

[TestClass]
public class AlertManager_Test
{
    [TestMethod]
    public void getAllAlerts_returns_empty_when_none()
    {
        var repo = new AlertRepositoryInMemory();
        var mgr = new AlertManager(repo);
        Assert.AreEqual(0, mgr.getAllAlerts().Count());
    }

    [TestMethod]
    public void getAllAlerts_returns_all()
    {
        var repo = new AlertRepositoryInMemory();
        var mgr = new AlertManager(repo);
        var a1 = new Alert { Id = 1, Message = "M1" };
        var a2 = new Alert { Id = 2, Message = "M2" };
        repo.Add(a1);
        repo.Add(a2);
        var all = mgr.getAllAlerts().ToList();
        Assert.AreEqual(2, all.Count);
        Assert.IsTrue(all.Contains(a1));
        Assert.IsTrue(all.Contains(a2));
    }

    [TestMethod]
    public void getAlertsById_invalid_id_throws()
    {
        var repo = new AlertRepositoryInMemory();
        var mgr = new AlertManager(repo);
        Assert.ThrowsException<ArgumentException>(() => mgr.getAlertsById(0));
        Assert.ThrowsException<ArgumentException>(() => mgr.getAlertsById(-1));
    }

    [TestMethod]
    public void getAlertsById_returns_null_when_not_found()
    {
        var repo = new AlertRepositoryInMemory();
        var mgr = new AlertManager(repo);
        Assert.IsNull(mgr.getAlertsById(1));
    }

    [TestMethod]
    public void getAlertsById_returns_entity_when_exists()
    {
        var repo = new AlertRepositoryInMemory();
        var mgr = new AlertManager(repo);
        var a = new Alert { Id = 3, Message = "Hello" };
        repo.Add(a);
        Assert.AreSame(a, mgr.getAlertsById(3));
    }

    [TestMethod]
    public void createAlert_adds_entity()
    {
        var repo = new AlertRepositoryInMemory();
        var mgr = new AlertManager(repo);
        var a = new Alert { Id = 4, Message = "New" };
        mgr.createAlert(a);
        Assert.AreEqual(1, mgr.getAllAlerts().Count());
        Assert.AreSame(a, mgr.getAlertsById(4));
    }

    [TestMethod]
    public void deleteAlert_null_throws()
    {
        var repo = new AlertRepositoryInMemory();
        var mgr = new AlertManager(repo);
        Assert.ThrowsException<ArgumentNullException>(() => mgr.deleteAlert(null));
    }

    [TestMethod]
    public void deleteAlert_nonexistent_throws()
    {
        var repo = new AlertRepositoryInMemory();
        var mgr = new AlertManager(repo);
        var a = new Alert { Id = 5, Message = "X" };
        Assert.ThrowsException<ArgumentException>(() => mgr.deleteAlert(a));
    }

    [TestMethod]
    public void deleteAlert_valid_removes()
    {
        var repo = new AlertRepositoryInMemory();
        var mgr = new AlertManager(repo);
        var a = new Alert { Id = 6, Message = "Y" };
        mgr.createAlert(a);
        mgr.deleteAlert(a);
        Assert.AreEqual(0, mgr.getAllAlerts().Count());
    }

    [TestMethod]
    public void updateAlert_nonexistent_throws()
    {
        var repo = new AlertRepositoryInMemory();
        var mgr = new AlertManager(repo);
        var a = new Alert { Id = 7, Message = "Z" };
        Assert.ThrowsException<ArgumentException>(() => mgr.updateAlert(a));
    }

    [TestMethod]
    public void updateAlert_valid_updates_message()
    {
        var repo = new AlertRepositoryInMemory();
        var mgr = new AlertManager(repo);
        var a = new Alert { Id = 8, Message = "Old" };
        mgr.createAlert(a);
        var updated = new Alert { Id = 8, Message = "New" };
        mgr.updateAlert(updated);
        Assert.AreEqual("New", mgr.getAlertsById(8).Message);
    }
}

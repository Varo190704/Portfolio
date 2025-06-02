using System;
using System.Linq;
using Domain.Aplication.Services;
using Domain.Infrastructure.Persistence;
using Domain.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace BusinessLogicTest.Services;

[TestClass]
public class UserManager_Test
{
    [TestMethod]
    public void constructor_adds_admin()
    {
        var repo = new UserRepositoryInMemory();
        var mgr = new UserManager(repo);
        var all = mgr.getAllUsers().ToList();
        Assert.AreEqual(1, all.Count);
        var admin = mgr.getUserByName("admin@sistema.com");
        Assert.IsNotNull(admin);
        Assert.AreEqual("Administrador", admin.Name);
        Assert.AreEqual("Sistema", admin.Surname);
        Assert.IsTrue(admin.AdminSistem);
        Assert.IsTrue(admin.login);
    }

    [TestMethod]
    public void constructor_does_not_duplicate_admin()
    {
        var repo = new UserRepositoryInMemory();
        repo.Add(new User { Name = "Administrador", Surname = "Sistema", Email = "admin@sistema.com", Password = "$Admin123", BirthDate = new DateOnly(2004, 10, 10), AdminSistem = true, login = true });
        var mgr = new UserManager(repo);
        Assert.AreEqual(1, mgr.getAllUsers().Count());
    }

    [TestMethod]
    public void getUserByName_invalid_throws()
    {
        var repo = new UserRepositoryInMemory();
        var mgr = new UserManager(repo);
        Assert.ThrowsException<ArgumentException>(() => mgr.getUserByName(null));
        Assert.ThrowsException<ArgumentException>(() => mgr.getUserByName(""));
        Assert.ThrowsException<ArgumentException>(() => mgr.getUserByName(" "));
    }

    [TestMethod]
    public void getUserByName_not_found_returns_null()
    {
        var repo = new UserRepositoryInMemory();
        var mgr = new UserManager(repo);
        Assert.IsNull(mgr.getUserByName("noone@example.com"));
    }

    [TestMethod]
    public void createUser_valid_adds()
    {
        var repo = new UserRepositoryInMemory();
        var mgr = new UserManager(repo);
        var user = new User { Name = "User", Surname = "One", Email = "user1@example.com", Password = "$Admin123", BirthDate = new DateOnly(1990, 1, 1) };
        mgr.createUser(user);
        Assert.AreEqual(2, mgr.getAllUsers().Count());
        Assert.AreSame(user, mgr.getUserByName("user1@example.com"));
    }

    [TestMethod]
    public void createUser_duplicate_throws()
    {
        var repo = new UserRepositoryInMemory();
        var mgr = new UserManager(repo);
        var user = new User { Name = "User", Surname = "One", Email = "user2@example.com", Password = "$Admin123", BirthDate = new DateOnly(1990, 1, 1) };
        mgr.createUser(user);
        Assert.ThrowsException<ArgumentException>(() => mgr.createUser(user));
    }

    [TestMethod]
    public void deleteUser_valid_removes()
    {
        var repo = new UserRepositoryInMemory();
        var mgr = new UserManager(repo);
        var user = new User { Name = "User", Surname = "Two", Email = "user3@example.com", Password = "$Admin123", BirthDate = new DateOnly(1990, 1, 1) };
        mgr.createUser(user);
        mgr.deleteUser(user);
        Assert.IsNull(mgr.getUserByName("user3@example.com"));
        Assert.AreEqual(1, mgr.getAllUsers().Count());
    }

    [TestMethod]
    public void deleteUser_nonexistent_throws()
    {
        var repo = new UserRepositoryInMemory();
        var mgr = new UserManager(repo);
        var user = new User { Name = "User", Surname = "Three", Email = "user4@example.com", Password = "$Admin123", BirthDate = new DateOnly(1990, 1, 1) };
        Assert.ThrowsException<ArgumentException>(() => mgr.deleteUser(user));
    }

    [TestMethod]
    public void updateUser_valid_updates()
    {
        var repo = new UserRepositoryInMemory();
        var mgr = new UserManager(repo);
        var user = new User { Name = "User", Surname = "Four", Email = "user5@example.com", Password = "$Admin123", BirthDate = new DateOnly(1990, 1, 1) };
        mgr.createUser(user);
        var updated = new User { Name = "UserNew", Surname = "FourNew", Email = "user5@example.com", Password = "$Admin12322", BirthDate = new DateOnly(1990, 1, 2) };
        mgr.updateUser(updated);
        var u = mgr.getUserByName("user5@example.com");
        Assert.AreEqual("UserNew", u.Name);
        Assert.AreEqual("FourNew", u.Surname);
        Assert.AreEqual("$Admin12322", u.Password);
        Assert.AreEqual(new DateOnly(1990, 1, 2), u.BirthDate);
    }

    [TestMethod]
    public void updateUser_nonexistent_throws()
    {
        var repo = new UserRepositoryInMemory();
        var mgr = new UserManager(repo);
        var user = new User { Name = "User", Surname = "Five", Email = "user6@example.com", Password = "$Admin123", BirthDate = new DateOnly(1990, 1, 1) };
        Assert.ThrowsException<ArgumentException>(() => mgr.updateUser(user));
    }
}

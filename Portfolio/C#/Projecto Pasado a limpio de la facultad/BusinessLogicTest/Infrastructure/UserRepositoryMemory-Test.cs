using Domain.Infrastructure.Persistence;
using Domain.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using Task = Domain.Utilities.Task;

namespace BusinessLogicTest.Infrastructure;

[TestClass]
public class UserRepositoryMemory_Test
{
    [TestMethod]
    public void getByName_invalid_email_throws()
    {
        var repo = new UserRepositoryInMemory();
        Assert.ThrowsException<ArgumentException>(() => repo.getByName(null));
        Assert.ThrowsException<ArgumentException>(() => repo.getByName(""));
        Assert.ThrowsException<ArgumentException>(() => repo.getByName(" "));
    }

    [TestMethod]
    public void getByName_not_found_returns_null()
    {
        var repo = new UserRepositoryInMemory();
        var result = repo.getByName("a@b.com");
        Assert.IsNull(result);
    }

    [TestMethod]
    public void getByName_returns_entity_when_exists()
    {
        var repo = new UserRepositoryInMemory();
        var user = new User { Email = "a@b.com", Name = "Alice" };
        repo.Add(user);
        var result = repo.getByName("a@b.com");
        Assert.AreSame(user, result);
    }

    [TestMethod]
    public void update_null_user_throws()
    {
        var repo = new UserRepositoryInMemory();
        Assert.ThrowsException<ArgumentException>(() => repo.Update(null));
    }

    [TestMethod]
    public void update_nonexistent_user_throws()
    {
        var repo = new UserRepositoryInMemory();
        var user = new User { Email = "a@b.com", Name = "Alice" };
        Assert.ThrowsException<ArgumentException>(() => repo.Update(user));
    }
}

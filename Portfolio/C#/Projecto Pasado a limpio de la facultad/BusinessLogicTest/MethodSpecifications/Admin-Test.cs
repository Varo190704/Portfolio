using Domain.Specification;
using Domain.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace BusinessLogicTest.MethodSpecifications;

[TestClass]
public class Admin_Test
{
    [TestMethod]
    public void create_valid_admin()
    {
        User user = AdminSistem.Create("Juan", "Pérez", "juan@example.com", "$Ab12345", new DateOnly(1990, 1, 1));
        Assert.AreEqual("Juan", user.Name);
        Assert.AreEqual("Pérez", user.Surname);
        Assert.AreEqual("juan@example.com", user.Email);
        Assert.AreEqual("$Ab12345", user.Password);
        Assert.AreEqual(new DateOnly(1990, 1, 1), user.BirthDate);
    }
    
    [TestMethod]
    public void delet_user_should_remove_user_from_list()
    {
        User user1 = new User("A", "B", "a@a.com", "$Ab12345", new DateOnly(2000, 1, 1));
        User user2 = new User("C", "D", "b@b.com", "$Ab12345", new DateOnly(2000, 2, 2));
        List<User> users = new List<User>();
        users.Add(user1);
        users.Add(user2);
        
        List<User> result = AdminSistem.Delete(users, user1);

        Assert.AreEqual(1, result.Count);
        Assert.AreEqual(false, result.Contains(user1));
        Assert.AreEqual(true, result.Contains(user2));
    }
    
    [TestMethod]
    public void cannot_delete_null_user()
    {
        Assert.Throws<ArgumentException>(() =>
            {
                AdminSistem.Delete(null, null);
            }
        );
    }

    [TestMethod]
    public void cannot_delete_null_list()
    {
        User user1 = new User("A", "B", "a@a.com", "$Ab12345", new DateOnly(2000, 1, 1));
        Assert.Throws<ArgumentException>(() =>
            {
                AdminSistem.Delete(null, user1);
            }
        );
    }
    
    [TestMethod]
    public void update_user_should_modify_existing_user()
    {
        User user1 = new User("A", "B", "a@a.com", "$Ab12345", new DateOnly(2000, 1, 1));
        List<User> users = new List<User>();
        users.Add(user1);
        
        User updated = new User("C", "D", "a@a.com", "$Ab12345", new DateOnly(2000, 2, 2));
        updated.AdminSistem = true;

        AdminSistem.Update(users, updated);

        Assert.AreEqual("C", user1.Name);
        Assert.AreEqual("D", user1.Surname);
        Assert.AreEqual("$Ab12345", user1.Password);
        Assert.AreEqual(new DateOnly(2000, 2, 2), user1.BirthDate);
        Assert.AreEqual(true, user1.AdminSistem);
    }
}
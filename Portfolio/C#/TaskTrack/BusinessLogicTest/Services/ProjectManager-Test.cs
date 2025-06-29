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
public class ProjectManager_Test
{
    private User MakeUser(string email = "u@x.com")
    {
        var u = new User
        {
            Name = "N",
            Surname = "S",
            Email = email,
            Password = "$Admin123",
            BirthDate = new DateOnly(2000, 1, 1),
            AdminSistem = false,
            login = false
        };
        u.Types = new List<UserType>();
        u.projects = new List<Project>();
        u.tasks = new List<Task>();
        return u;
    }

    [TestMethod]
    public void getAllProjects_empty_and_nonempty()
    {
        var repo = new ProjectInSQL();
        var mgr = new ProjectManager(repo);
        Assert.AreEqual(0, mgr.getAllProjects().Count());
        var admin = MakeUser();
        var p = new Project("P","D", admin, new DateOnly(2025,1,1));
        repo.Add(p);
        Assert.AreEqual(1, mgr.getAllProjects().Count());
    }

    [TestMethod]
    public void getProjectsByName_returns_and_throws_on_invalid()
    {
        var repo = new ProjectInSQL();
        var mgr = new ProjectManager(repo);
        var admin = MakeUser();
        var p = new Project("X","D", admin, new DateOnly(2025,1,1));
        repo.Add(p);
        Assert.AreSame(p, mgr.getProjectsByName("X"));
        Assert.IsNull(mgr.getProjectsByName("Y"));
        Assert.ThrowsException<ArgumentException>(() => mgr.getProjectsByName(null));
    }

    [TestMethod]
    public void createProject_null_and_duplicate_and_valid()
    {
        var repo = new ProjectInSQL();
        var mgr = new ProjectManager(repo);
        Assert.ThrowsException<ArgumentException>(() => mgr.createProject(null));
        var admin = MakeUser();
        var p = new Project("A","D", admin, new DateOnly(2025,1,1));
        mgr.createProject(p);
        Assert.ThrowsException<ArgumentException>(() => mgr.createProject(p));
    }

    [TestMethod]
    public void deleteProject_nonexistent_and_valid()
    {
        var repo = new ProjectInSQL();
        var mgr = new ProjectManager(repo);
        var admin = MakeUser();
        var p = new Project("B","D", admin, new DateOnly(2025,1,1));
        Assert.ThrowsException<ArgumentException>(() => mgr.deleteProject(p));
        mgr.createProject(p);
        mgr.deleteProject(p);
        Assert.AreEqual(0, repo.GetAll().Count());
    }

    [TestMethod]
    public void updateProject_always_calls_repo()
    {
        var repo = new ProjectInSQL();
        var mgr = new ProjectManager(repo);
        var admin = MakeUser();
        var p = new Project("U","Old", admin, new DateOnly(2025,1,1));
        repo.Add(p);
        p.Description = "New";
        mgr.updateProject(p);
        Assert.AreEqual("New", repo.getByName("U").Description);
    }

    [TestMethod]
    public void assignMember_valid_and_null()
    {
        var repo = new ProjectInSQL();
        var mgr = new ProjectManager(repo);
        var admin = MakeUser();
        var member = MakeUser("m@x.com");
        var p = new Project("M","D", admin, new DateOnly(2025,1,1));
        repo.Add(p);
        mgr.assignMember(p, member);
        Assert.IsTrue(p.Members.Contains(member));
        Assert.IsTrue(member.projects.Contains(p));
        Assert.IsTrue(member.Types.Contains(UserType.ProjectMember));
        Assert.ThrowsException<ArgumentException>(() => mgr.assignMember(p, null));
    }

    [TestMethod]
    public void assAlert_valid_and_null()
    {
        var repo = new ProjectInSQL();
        var mgr = new ProjectManager(repo);
        var admin = MakeUser();
        var p = new Project("A","D", admin, new DateOnly(2025,1,1));
        repo.Add(p);
        var alert = new Alert { Id = 1, Message = "msg" };
        mgr.assAlert(p, alert);
        Assert.AreSame(alert, p.Alerts.First());
        Assert.ThrowsException<ArgumentException>(() => mgr.assAlert(p, null));
    }

    [TestMethod]
    public void getCriticalPath_simple()
    {
        var repo = new ProjectInSQL();
        var mgr = new ProjectManager(repo);
        var admin = MakeUser();
        var p = new Project("C","D", admin, new DateOnly(2025,1,1));
        repo.Add(p);
        var t = new Task("C","", new DateOnly(2025,1,1),1);
        p.Tasks.Add(t);
        var path = mgr.getCriticalPath(p);
        Assert.AreEqual(1, path.Count);
        Assert.AreSame(t, path.First());
    }

    [TestMethod]
    public void assTaskToMember_and_deleteTaskToMember()
    {
        var repo = new ProjectInSQL();
        var mgr = new ProjectManager(repo);
        var admin = MakeUser();
        var member = MakeUser("m2@x.com");
        var p = new Project("TM","D", admin, new DateOnly(2025,1,1));
        repo.Add(p);
        mgr.assignMember(p, member);
        var t = new Task("TM","", new DateOnly(2025,1,1),1);
        mgr.assTaskToMember(p, admin, member, t);
        Assert.IsTrue(member.tasks.Contains(t));
        mgr.deleteTaskToMember(p, admin, member, t);
        Assert.IsFalse(member.tasks.Contains(t));
    }
    
    [TestMethod]
    public void assTaskAdmin_should_add_task_and_update_repo()
    {
        var repo = new ProjectInSQL();
        var mgr = new ProjectManager(repo);
        var admin = MakeUser();
        var p = new Project("X", "D", admin, new DateOnly(2025, 1, 1));
        repo.Add(p);
    
        var task = new Task("T1", "desc", new DateOnly(2025, 1, 2), 3);
    
        mgr.assTaskAdmin(p, admin, task);

        Assert.AreEqual(1, p.Tasks.Count);
        Assert.AreEqual("T1", p.Tasks[0].Name);
    }

    [TestMethod]
    public void assTask_should_add_task_without_admin_validation()
    {
        var repo = new ProjectInSQL();
        var mgr = new ProjectManager(repo);
        var admin = MakeUser();
        var p = new Project("Y", "D", admin, new DateOnly(2025, 1, 1));
        repo.Add(p);
    
        var task = new Task("T2", "desc", new DateOnly(2025, 1, 2), 3);
    
        mgr.assTask(p, task);

        Assert.AreEqual(1, p.Tasks.Count);
        Assert.AreEqual("T2", p.Tasks[0].Name);
    }

    [TestMethod]
    public void updateTaskAdmin_should_modify_existing_task()
    {
        var repo = new ProjectInSQL();
        var mgr = new ProjectManager(repo);
        var admin = MakeUser();
        var p = new Project("Z", "D", admin, new DateOnly(2025, 1, 1));
        var task = new Task("T3", "desc", new DateOnly(2025, 1, 1), 2);
        p.Tasks.Add(task);
        repo.Add(p);
    
        var updated = new Task("T3", "updated", new DateOnly(2025, 1, 1), 5);
    
        mgr.updateTaskAdmin(p, admin, updated);

        Assert.AreEqual(5, p.Tasks[0].Duration);
        Assert.AreEqual("updated", p.Tasks[0].Description);
    }

    [TestMethod]
    public void deleteTask_should_remove_task_directly()
    {
        var repo = new ProjectInSQL();
        var mgr = new ProjectManager(repo);
        var admin = MakeUser();
        var p = new Project("D", "desc", admin, new DateOnly(2025, 1, 1));
        var t = new Task("T4", "desc", new DateOnly(2025, 1, 1), 1);
        p.Tasks.Add(t);
        repo.Add(p);
    
        mgr.deleteTask(p, t);

        Assert.AreEqual(0, p.Tasks.Count);
    }
    
    [TestMethod]
    public void updateStatus_should_change_task_status()
    {
        var repo = new ProjectInSQL();
        var mgr = new ProjectManager(repo);
        var admin = MakeUser();
        var p = new Project("E", "desc", admin, new DateOnly(2025, 1, 1));
        var t = new Task("T5", "desc", new DateOnly(2025, 1, 1), 1);
        p.Tasks.Add(t);
        repo.Add(p);
    
        bool[] status = { false, true, false }; // InProgress
        mgr.updateStatus(p, admin, t, status);

        Assert.AreEqual(TaskProgress.InProgress, p.Tasks[0].Status);
    }
    
    [TestMethod]
    public void assResource_should_add_resource_to_task()
    {
        var repo = new ProjectInSQL();
        var mgr = new ProjectManager(repo);
        var admin = MakeUser();
        var p = new Project("F", "desc", admin, new DateOnly(2025, 1, 1));
        var t = new Task("T6", "desc", new DateOnly(2025, 1, 1), 1);
        var r = new Resource("Dev", TaskType.Human, "developer", "Dev");
        p.Admin = admin.Email;
        p.Tasks.Add(t);
        repo.Add(p);

        mgr.assResource(p, admin, t, r);

        Assert.IsTrue(t.Resources.Contains(r));
    }

    [TestMethod]
    public void deleteResource_should_remove_resource_from_task()
    {
        var repo = new ProjectInSQL();
        var mgr = new ProjectManager(repo);
        var admin = MakeUser();
        var p = new Project("G", "desc", admin, new DateOnly(2025, 1, 1));
        var t = new Task("T7", "desc", new DateOnly(2025, 1, 1), 1);
        var r = new Resource("Dev", TaskType.Human, "developer", "Dev");
        p.Admin = admin.Email;
        t.Resources.Add(r);
        p.Tasks.Add(t);
        repo.Add(p);

        mgr.deleteResource(p, admin, t, r);

        Assert.IsFalse(t.Resources.Contains(r));
    }

    [TestMethod]
    public void assAdmin_should_assign_admin_and_update_repo()
    {
        var repo = new ProjectInSQL();
        var mgr = new ProjectManager(repo);
        var admin = MakeUser("admin@x.com");
        var p = new Project("H", "desc", new DateOnly(2025, 1, 1));
        repo.Add(p);

        mgr.assAdmin(admin, p);

        Assert.AreEqual(admin.Email, p.Admin);
        Assert.IsTrue(p.Members.Contains(admin));
    }
}
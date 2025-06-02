using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Aplication.Services;
using Domain.Enum;
using Domain.Infrastructure.Persistence;
using Domain.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using Task = Domain.Utilities.Task;

namespace BusinessLogicTest.Services
{
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
            var repo = new RepositoryInMemory<Project>();
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
            var repo = new RepositoryInMemory<Project>();
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
            var repo = new RepositoryInMemory<Project>();
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
            var repo = new RepositoryInMemory<Project>();
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
            var repo = new RepositoryInMemory<Project>();
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
            var repo = new RepositoryInMemory<Project>();
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
            var repo = new RepositoryInMemory<Project>();
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
        public void assTaskAdmin_non_admin_and_valid()
        {
            var repo = new RepositoryInMemory<Project>();
            var mgr = new ProjectManager(repo);
            var admin = MakeUser();
            var other = MakeUser("o@x.com");
            var p = new Project("T","D", admin, new DateOnly(2025,1,1));
            repo.Add(p);
            var t = new Task("T","D", new DateOnly(2025,1,1),1);
            Assert.ThrowsException<ArgumentException>(() => mgr.assTaskAdmin(p, other, t));
            mgr.assTaskAdmin(p, admin, t);
            Assert.IsTrue(p.Tasks.Contains(t));
        }

        [TestMethod]
        public void assTask_null_and_duplicate()
        {
            var repo = new RepositoryInMemory<Project>();
            var mgr = new ProjectManager(repo);
            var admin = MakeUser();
            var p = new Project("TT","D", admin, new DateOnly(2025,1,1));
            repo.Add(p);
            Assert.ThrowsException<ArgumentException>(() => mgr.assTask(p, null));
            var t = new Task("TT","D", new DateOnly(2025,1,1),1);
            mgr.assTask(p, t);
            Assert.ThrowsException<ArgumentException>(() => mgr.assTask(p, t));
        }

        [TestMethod]
        public void updateStatus_valid_and_invalid()
        {
            var repo = new RepositoryInMemory<Project>();
            var mgr = new ProjectManager(repo);
            var admin = MakeUser();
            var p = new Project("S","D", admin, new DateOnly(2025,1,1));
            repo.Add(p);
            var t = new Task("S","D", new DateOnly(2025,1,1),1);
            mgr.assTask(p, t);
            mgr.updateStatus(p, admin, t, new[] { false, true, false });
            Assert.AreEqual(TaskProgress.InProgress, t.Status);
            Assert.ThrowsException<ArgumentException>(() => mgr.updateStatus(p, admin, t, new bool[]{false}));
            Assert.ThrowsException<ArgumentException>(() => mgr.updateStatus(p, null, t, new[] { true,false,false }));
        }

        [TestMethod]
        public void getCriticalPath_simple()
        {
            var repo = new RepositoryInMemory<Project>();
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
            var repo = new RepositoryInMemory<Project>();
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
        public void AlwaysUpdateTasks_and_AlwaysUpdateUsers()
        {
            var repo = new RepositoryInMemory<Project>();
            var mgr = new ProjectManager(repo);
            var admin = MakeUser();
            var member1 = MakeUser("u1@x.com");
            var member2 = MakeUser("u2@x.com");
            var p = new Project("AU","D", admin, new DateOnly(2025,1,1));
            repo.Add(p);
            mgr.assignMember(p, member1);
            mgr.assignMember(p, member2);
            var t1 = new Task("AT1","Old", new DateOnly(2025,1,1),1);
            var t2 = new Task("AT2","Old2", new DateOnly(2025,1,2),2);
            p.Tasks.Add(t1);
            p.Tasks.Add(t2);
            mgr.AlwaysUpdateTasks(new List<Task> { new Task("AT1","New", new DateOnly(2025,1,1),1) }, new List<Project> { p });
            Assert.AreEqual(1, p.Tasks.Count);
            mgr.AlwaysUpdateUsers(new List<User> { new User {
                Name = member1.Name, Surname = "NewS", Email = member1.Email,
                Password = member1.Password, BirthDate = member1.BirthDate
            , AdminSistem = member1.AdminSistem, login = member1.login, projects = new List<Project>(), tasks = new List<Task>(), Types = new List<UserType>() } }, new List<Project> { p });
            Assert.AreEqual(3, p.Members.Count);
            Assert.AreEqual("NewS", p.Members.First().Surname);
        }
    }
}

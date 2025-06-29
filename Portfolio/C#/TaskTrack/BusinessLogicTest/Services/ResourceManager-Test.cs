using System.Linq;
using Domain.Aplication.Services;
using Domain.Enum;
using Domain.Infrastructure.SqlConecc;
using Domain.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace BusinessLogicTest.Services
{
    [TestClass]
    public class ResourceManager_Test
    {
        [TestMethod]
        public void getAllResources_returns_all()
        {
            var repo = new ResourceInSQL();
            var mgr = new ResourceManager(repo);
            var r1 = new Resource("R1", TaskType.Human, "Desc1", "R1");
            var r2 = new Resource("R2", TaskType.Software, "Desc2", "R2");
            repo.Add(r1);
            repo.Add(r2);

            var all = mgr.getAllResources().ToList();
            Assert.AreEqual(2, all.Count);
            Assert.IsTrue(all.Contains(r1));
            Assert.IsTrue(all.Contains(r2));
        }

        [TestMethod]
        public void getResourcesByName_returns_existing()
        {
            var repo = new ResourceInSQL();
            var mgr = new ResourceManager(repo);
            var r = new Resource("RX", TaskType.Infrastructure, "DescX", "RX");
            repo.Add(r);

            var found = mgr.getResourcesByName("RX");
            Assert.AreSame(r, found);
        }

        [TestMethod]
        public void getResourcesByName_returns_null_when_not_exists()
        {
            var repo = new ResourceInSQL();
            var mgr = new ResourceManager(repo);

            var found = mgr.getResourcesByName("NoExiste");
            Assert.IsNull(found);
        }

        [TestMethod]
        public void createResource_null_throws()
        {
            var repo = new ResourceInSQL();
            var mgr = new ResourceManager(repo);
            Assert.ThrowsException<ArgumentException>(() => mgr.createResource(null));
        }

        [TestMethod]
        public void createResource_duplicate_throws()
        {
            var repo = new ResourceInSQL();
            var mgr = new ResourceManager(repo);
            var r = new Resource("DUP", TaskType.Human, "Desc", "DUP");
            mgr.createResource(r);
            Assert.ThrowsException<ArgumentException>(() => mgr.createResource(r));
        }

        [TestMethod]
        public void createResource_valid_adds()
        {
            var repo = new ResourceInSQL();
            var mgr = new ResourceManager(repo);
            var r = new Resource("NEW", TaskType.Software, "DescNew", "NEW");
            mgr.createResource(r);

            Assert.AreEqual(1, repo.GetAll().Count());
            Assert.AreSame(r, repo.getByName("NEW"));
        }

        [TestMethod]
        public void deleteResource_null_throws()
        {
            var repo = new ResourceInSQL();
            var mgr = new ResourceManager(repo);
            Assert.ThrowsException<ArgumentException>(() => mgr.deleteResource(null));
        }

        [TestMethod]
        public void deleteResource_not_exists_throws()
        {
            var repo = new ResourceInSQL();
            var mgr = new ResourceManager(repo);
            var r = new Resource("X", TaskType.Human, "Desc", "X");
            Assert.ThrowsException<ArgumentException>(() => mgr.deleteResource(r));
        }

        [TestMethod]
        public void deleteResource_valid_removes()
        {
            var repo = new ResourceInSQL();
            var mgr = new ResourceManager(repo);
            var r = new Resource("DEL", TaskType.Infrastructure, "DescDel", "DEL");
            mgr.createResource(r);
            mgr.deleteResource(r);

            Assert.AreEqual(0, repo.GetAll().Count());
        }

        [TestMethod]
        public void updateResource_not_exists_throws()
        {
            var repo = new ResourceInSQL();
            var mgr = new ResourceManager(repo);
            var r = new Resource("U1", TaskType.Human, "Desc", "U1");
            Assert.ThrowsException<ArgumentException>(() => mgr.updateResource(r));
        }

        [TestMethod]
        public void updateResource_valid_updates()
        {
            var repo = new ResourceInSQL();
            var mgr = new ResourceManager(repo);
            var r = new Resource("U2", TaskType.Software, "OldDesc", "U2");
            mgr.createResource(r);

            var updated = new Resource("U2", TaskType.Software, "NewDesc", "U2");
            mgr.updateResource(updated);

            var inRepo = repo.getByName("U2");
            Assert.AreEqual(TaskType.Software, inRepo.TaskType);
            Assert.AreEqual("NewDesc", inRepo.Description);
        }
    }
}
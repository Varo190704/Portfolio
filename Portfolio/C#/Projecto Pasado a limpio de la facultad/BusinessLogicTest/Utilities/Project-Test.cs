using Domain.Enum;
using Domain.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using Task = Domain.Utilities.Task;

namespace BusinessLogicTest.Utilities;

[TestClass]
public class Project_Test
{
    [TestMethod]
    public void create_valid_project()
    {
        User admin = new User("Alvaro", "Redo", "example@gmail.com", "$Ab23456", new DateOnly(2004, 07,19 ));
        Project project = new Project("Name", "A description", admin,  new DateOnly(2024, 07, 19));
		Assert.AreEqual("Name", project.Name);
        Assert.AreEqual("A description", project.Description);
        Assert.AreEqual(admin, project.Admin);
        Assert.IsNotNull(project.StartDate);
    }

    //consider that a description can be 0 char

    [TestMethod]
    public void create_invalid_project_bcName()
    {
        User admin = new User("Alvaro", "Redo", "example@gmail.com", "$Ab23456", new DateOnly(2004, 07,19 ));
        Assert.Throws<ArgumentException>(() =>
            {
                Project project = new Project("", "A description", admin,  new DateOnly(2024, 07, 19));
            }
        );
    }

    //I delete this function bc in the end i dont consider that the start of a project has to be youngeer than the admin

    /*public void create_invalid_project2()
    {
        User admin = new User("Alvaro", "Redo", "example@gmail.com", "$Ab23456", new DateOnly(2004, 07,19 ));
        Assert.Throws<ArgumentException>(() =>
            {
                Project project = new Project("Name", "A description", admin, new DateOnly (2003, 07, 19));
            }
        );
    }*/

    [TestMethod]
    public void create_invalid_project_bcDescription()
    {
        User admin = new User("Alvaro", "Redo", "example@gmail.com", "$Ab23456", new DateOnly(2004, 07,19 ));
        Assert.Throws<ArgumentException>(() =>
            {
                Project project = new Project("Name", 
                "In today’s dynamic business environment, marked by constant digital transformation, organizations that aim to remain competitive must adopt a strategic vision centered on continuous innovation. This requires not only integrating new technologies but also reconfiguring internal processes, investing in ongoing talent development, and cultivating resilient organizational cultures. Companies that successfully align their operational goals with an adaptable and proactive mindset toward change don’t just survive — they thrive in volatile contexts, positioning themselves as leaders in their industries.",
                 admin, new DateOnly (2003, 07, 19));
            }
        );
    }   

    [TestMethod]
    public void assign_admin()
    {
        User admin = new User("Alvaro", "Redo", "example@gmail.com", "$Ab23456", new DateOnly(2004, 07,19 ));
        Project project = new Project("Name", "A description",  new DateOnly(2024, 07, 19));
		project.assAdmin(admin);
    	Assert.IsNotNull(project.Admin);
	}
    
    [TestMethod]
    public void assign_member()
    {
        User member = new User("Alvaro", "Redo", "example@gmail.com", "$Ab23456", new DateOnly(2004, 07,19 ));
        Project project = new Project("Name", "A description",  new DateOnly(2024, 07, 19));
        project.assMember(member);
    }

    [TestMethod]
    public void assign_Task()
    {
        Task task = new Task("Task", "Descrip Task", new DateOnly(2024, 07, 19), 1);
        Project project = new Project("Name", "A description",  new DateOnly(2024, 07, 19));
        User member = new User("Alvaro", "Redo", "example@gmail.com", "$Ab23456", new DateOnly(2004, 07,19 ));
        project.Admin = member;
        project.assTaskAdmin(member, task);
    }
    
    [TestMethod]
    public void assign_Alert()
    {
        Alert alert = new Alert("This is a test", 1, "email@gmail.com");
        Project project = new Project("Name", "A description",  new DateOnly(2024, 07, 19));
        project.assAlert(alert);
    }
    
    [TestMethod]
	public void notassign_admin()
    {
		Project project = new Project("Name", "A description",  new DateOnly(2024, 07, 19));
		Assert.Throws<ArgumentException>(() =>
            {
				project.assAdmin(null);
            }
        );
	}

    [TestMethod]
    public void assign_member_invalid()
    {
        Project project = new Project("Name", "A description",  new DateOnly(2024, 07, 19));
        Assert.Throws<ArgumentException>(() =>
            {
                project.assMember(null);
            }
        );
    }
    
    [TestMethod]
    public void assign_task_invalid_bcNull_task()
    {
        User member = new User("Alvaro", "Redo", "example@gmail.com", "$Ab23456", new DateOnly(2004, 07,19 ));
        Project project = new Project("Name", "A description",  new DateOnly(2024, 07, 19));
        project.Admin = member;
        Assert.Throws<ArgumentException>(() =>
            {
                project.assTaskAdmin(member, null);
            }
        );
    }

    [TestMethod]
    public void assign_task_invalid_bcNotAdmin()
    {
        Task task = new Task("Task", "Descrip Task", new DateOnly(2024, 07, 19), 1);
        User member = new User("Alvaro", "Redo", "example@gmail.com", "$Ab23456", new DateOnly(2004, 07,19 ));
        Project project = new Project("Name", "A description",  new DateOnly(2024, 07, 19));
        //idk why its works with UnauthorizedAccessException
        Assert.Throws<ArgumentException>(() =>
            {
                project.assTaskAdmin(member, task);
            }
        );
    }

    [TestMethod]
    public void assign_task_invalid_bcAdmin_null()
    {
        Task task = new Task("Task", "Descrip Task", new DateOnly(2024, 07, 19), 1);
        Project project = new Project("Name", "A description",  new DateOnly(2024, 07, 19));
        Assert.Throws<ArgumentException>(() =>
            {
                project.assTaskAdmin(null, task);
            }
        );
    }

    [TestMethod]
    public void assign_task_invalid_bcSame_Task()
    {
        Task task = new Task("Task", "Descrip Task", new DateOnly(2024, 07, 19), 1);
        User member = new User("Alvaro", "Redo", "example@gmail.com", "$Ab23456", new DateOnly(2004, 07,19 ));
        Project project = new Project("Name", "A description",  new DateOnly(2024, 07, 19));
        project.Admin = member;
        project.assTaskAdmin(member, task);
        Assert.Throws<ArgumentException>(() =>
            {
                project.assTaskAdmin(member, task);
            }
        );
    }
    
    [TestMethod]
    public void update_Task()
    {
        Task task = new Task("Task", "Descrip Task", new DateOnly(2024, 07, 19), 1);
        Task task2 = new Task("Task", "Descrip Task with changes", new DateOnly(2024, 07, 30), 5);
        User admin = new User("Alvaro", "Redo", "example@gmail.com", "$Ab23456", new DateOnly(2004, 07,19 ));
        Project project = new Project("Name", "A description",  new DateOnly(2024, 07, 19));
        project.Admin = admin;
        project.assTaskAdmin(admin, task);
        project.updateTaskAdmin(admin, task2);
        Assert.AreEqual("Descrip Task with changes", task.Description);
        Assert.AreEqual(new DateOnly(2024, 07, 30), task.StartDate);
        Assert.AreEqual(5, task.Duration);
        Assert.AreEqual(0, task.Dependencies.Count);
        Assert.AreEqual(0, task.Resources.Count);
        Assert.AreEqual(0, task.Slak);
    }

    [TestMethod]
    public void update_Task_invalid_bcNull_task()
    {
        Task task = new Task("Task", "Descrip Task", new DateOnly(2024, 07, 19), 1);
        User member = new User("Alvaro", "Redo", "example@gmail.com", "$Ab23456", new DateOnly(2004, 07,19 ));
        Project project = new Project("Name", "A description",  new DateOnly(2024, 07, 19));
        project.Admin = member;
        project.assTaskAdmin(member, task);
        Assert.Throws<ArgumentException>(() =>
            {
                project.updateTaskAdmin(member, null);
            }
        );
    }
    
    [TestMethod]
    public void update_Task_invalid_bcNull_admin()
    {
        Task task = new Task("Task", "Descrip Task", new DateOnly(2024, 07, 19), 1);
        Task task2 = new Task("Task", "Descrip Task with changes", new DateOnly(2024, 07, 30), 5);
        User member = new User("Alvaro", "Redo", "example@gmail.com", "$Ab23456", new DateOnly(2004, 07,19 ));
        Project project = new Project("Name", "A description",  new DateOnly(2024, 07, 19));
        project.Admin = member;
        project.assTaskAdmin(member, task);
        Assert.Throws<ArgumentException>(() =>
            {
                project.updateTaskAdmin(null, task2);
            }
        );
    }
    
    [TestMethod]
    public void update_Task_invalid_bcNotAdmin()
    {
        Task task = new Task("TaskNot", "Descrip Task", new DateOnly(2024, 07, 19), 1);
        Task task2 = new Task("Task", "Descrip Task with changes", new DateOnly(2024, 07, 30), 5);
        User member = new User("Alvaro", "Redo", "example@gmail.com", "$Ab23456", new DateOnly(2004, 07,19 ));
        Project project = new Project("Name", "A description",  new DateOnly(2024, 07, 19));
        project.Admin = member;
        project.assTaskAdmin(member, task);
        User member2 = new User("Alvaro2", "Redo", "example@gmail.com", "$Ab23456", new DateOnly(2004, 07,19 ));
        Assert.Throws<ArgumentException>(() =>
            {
                project.updateTaskAdmin(member2, task2);
            }
        );
    }
    
    [TestMethod]
    public void update_Task_invalid_bcNotExisting_Task()
    {
        Task task = new Task("Task", "Descrip Task", new DateOnly(2024, 07, 19), 1);
        Task task2 = new Task("Task", "Descrip Task with changes", new DateOnly(2024, 07, 30), 5);
        User member = new User("Alvaro", "Redo", "example@gmail.com", "$Ab23456", new DateOnly(2004, 07,19 ));
        Project project = new Project("Name", "A description",  new DateOnly(2024, 07, 19));
        project.Admin = member;
        project.assTaskAdmin(member, task);
        User member2 = new User("Alvaro2", "Redo", "example@gmail.com", "$Ab23456", new DateOnly(2004, 07,19 ));
        Assert.Throws<ArgumentException>(() =>
            {
                project.updateTaskAdmin(member2, task2);
            }
        );
    }

    [TestMethod]
    public void delete_Task()
    {
        Task task = new Task("Task", "Descrip Task", new DateOnly(2024, 07, 19), 1);
        User admin = new User("Alvaro", "Redo", "example@gmail.com", "$Ab23456", new DateOnly(2004, 07,19 ));
        Project project = new Project("Name", "A description",  new DateOnly(2024, 07, 19));
        project.Admin = admin;
        project.assTaskAdmin(admin, task);
        project.deleteTaskAdmin(admin, task);
        Assert.AreEqual(0, project.Tasks.Count);
    }
    
    [TestMethod]
    public void delete_Task_dependencies()
    {
        Task task = new Task("Task", "Descrip Task", new DateOnly(2024, 07, 19), 1);
        Task task2 = new Task("Task2", "Descrip Task2", new DateOnly(2024, 07, 20), 1);
        User admin = new User("Alvaro", "Redo", "example@gmail.com", "$Ab23456", new DateOnly(2004, 07,19 ));
        Project project = new Project("Name", "A description",  new DateOnly(2024, 07, 19));
        project.Admin = admin;
        task2.Dependencies.Add(task);
        project.assTaskAdmin(admin, task);
        project.assTaskAdmin(admin, task2);
        project.deleteTaskAdmin(admin, task);
        Assert.AreEqual(1, project.Tasks.Count);
        Assert.AreEqual(0, task2.Dependencies.Count);
    }

    [TestMethod]
    public void delete_Task_invalid_bcNull_Admin()
    {
        Task task = new Task("Task", "Descrip Task", new DateOnly(2024, 07, 19), 1);
        User admin = new User("Alvaro", "Redo", "example@gmail.com", "$Ab23456", new DateOnly(2004, 07,19 ));
        Project project = new Project("Name", "A description",  new DateOnly(2024, 07, 19));
        project.Admin = admin;
        project.assTaskAdmin(admin, task);
        Assert.Throws<ArgumentException>(() =>
            {
                project.deleteTaskAdmin(null, task);
            }
        );
    }

    [TestMethod]
    public void delete_Task_invalid_bcNotAdmin()
    {
        Task task = new Task("Task", "Descrip Task", new DateOnly(2024, 07, 19), 1);
        User admin = new User("Alvaro", "Redo", "example@gmail.com", "$Ab23456", new DateOnly(2004, 07,19 ));
        Project project = new Project("Name", "A description",  new DateOnly(2024, 07, 19));
        project.Admin = admin;
        User member2 = new User("Alvaro", "Redo", "example@gmail.com", "$Ab23456", new DateOnly(2004, 07,19 ));
        project.assTaskAdmin(admin, task);
        Assert.Throws<ArgumentException>(() =>
            {
                project.deleteTaskAdmin(member2, task);
            }
        );
    }
    
    [TestMethod]
    public void delete_Task_invalid_bcTask_null()
    {
        User admin = new User("Alvaro", "Redo", "example@gmail.com", "$Ab23456", new DateOnly(2004, 07,19 ));
        Project project = new Project("Name", "A description",  new DateOnly(2024, 07, 19));
        project.Admin = admin;
        Assert.Throws<ArgumentException>(() =>
            {
                project.deleteTaskAdmin(admin, null);
            }
        );
    }
    
    [TestMethod]
    public void delete_Task_invalid_bcTaskNotExisting()
    {
        Task task = new Task("Task", "Descrip Task", new DateOnly(2024, 07, 19), 1);
        User admin = new User("Alvaro", "Redo", "example@gmail.com", "$Ab23456", new DateOnly(2004, 07,19 ));
        Project project = new Project("Name", "A description",  new DateOnly(2024, 07, 19));
        project.Admin = admin;
        Assert.Throws<ArgumentException>(() =>
            {
                project.deleteTaskAdmin(admin, task);
            }
        );
    }

    [TestMethod]
    public void update_Status()
    {
        Task task = new Task("Task", "Descrip Task", new DateOnly(2024, 07, 19), 1);
        User admin = new User("Alvaro", "Redo", "example@gmail.com", "$Ab23456", new DateOnly(2004, 07,19 ));
        Project project = new Project("Name", "A description",  new DateOnly(2024, 07, 19));
        project.Admin = admin;
        project.assTaskAdmin(admin, task);
        bool[] bools = { false, true, false };
        project.updateStatusAdmin(bools, admin, task);
        Assert.AreEqual(TaskProgress.InProgress, task.Status);
    }
    
    [TestMethod]
    public void update_Status_invalid_bcNull_Admin()
    {
        Task task = new Task("Task", "Descrip Task", new DateOnly(2024, 07, 19), 1);
        User admin = new User("Alvaro", "Redo", "example@gmail.com", "$Ab23456", new DateOnly(2004, 07,19 ));
        Project project = new Project("Name", "A description",  new DateOnly(2024, 07, 19));
        project.Admin = admin;
        project.assTaskAdmin(admin, task);
        bool[] bools = { false, true, false };
        Assert.Throws<ArgumentException>(() =>
            {
                project.updateStatusAdmin(bools, null, task);
            }
        );
    }
    
    [TestMethod]
    public void update_Status_invalid_bcNotValidArrayStatus()
    {
        Task task = new Task("Task", "Descrip Task", new DateOnly(2024, 07, 19), 1);
        User admin = new User("Alvaro", "Redo", "example@gmail.com", "$Ab23456", new DateOnly(2004, 07,19 ));
        Project project = new Project("Name", "A description",  new DateOnly(2024, 07, 19));
        project.Admin = admin;
        project.assTaskAdmin(admin, task);
        User member = new User("Alvaro2", "Redo", "example@gmail.com", "$Ab23456", new DateOnly(2004, 07,19 ));
        bool[] bools = { false, true};
        Assert.Throws<ArgumentException>(() =>
            {
                project.updateStatusAdmin(bools, member, task);
            }
        );
    }
    
    [TestMethod]
    public void update_Status_invalid_bcTaskNull()
    {
        User admin = new User("Alvaro", "Redo", "example@gmail.com", "$Ab23456", new DateOnly(2004, 07,19 ));
        Project project = new Project("Name", "A description",  new DateOnly(2024, 07, 19));
        project.Admin = admin;
        bool[] bools = { false, true, false };
        Assert.Throws<ArgumentException>(() =>
            {
                project.updateStatusAdmin(bools, admin, null);
            }
        );
    }
    
    [TestMethod]
    public void update_Status_invalid_bcTaskNotExisting()
    {
        Task task = new Task("Task", "Descrip Task", new DateOnly(2024, 07, 19), 1);
        User admin = new User("Alvaro", "Redo", "example@gmail.com", "$Ab23456", new DateOnly(2004, 07,19 ));
        Project project = new Project("Name", "A description",  new DateOnly(2024, 07, 19));
        project.Admin = admin;
        bool[] bools = { false, true, false };
        Assert.Throws<ArgumentException>(() =>
            {
                project.updateStatusAdmin(bools, admin, task);
            }
        );
    }

    
    [TestMethod]
    public void assign_alert_invalid()
    {
        Project project = new Project("Name", "A description",  new DateOnly(2024, 07, 19));
        Assert.Throws<ArgumentException>(() =>
            {
                project.assMember(null);
            }
        );
    }

    
    [TestMethod]
    public void add_resource()
    {
        Task task = new Task("Task", "Descrip Task", new DateOnly(2024, 07, 19), 1);
        Resource resource = new Resource("Dev", TaskType.Human, "Frontend dev");
        User admin = new User("Alvaro", "Redo", "example@gmail.com", "$Ab23456", new DateOnly(2004, 07,19 ));
        Project project = new Project("Name", "A description",  new DateOnly(2024, 07, 19));
        project.Admin = admin;
        project.assTaskAdmin(admin, task);
        project.assResourceAdmin(admin, task, resource);
        Assert.AreEqual(1, task.Resources.Count);
    }

    [TestMethod]
    public void invalid_add_resource_bcNUllAdmin()
    {
        Task task = new Task("Task", "Descrip Task", new DateOnly(2024, 07, 19), 1);
        Resource resource = new Resource("Dev", TaskType.Human, "Frontend dev");
        User admin = new User("Alvaro", "Redo", "example@gmail.com", "$Ab23456", new DateOnly(2004, 07,19 ));
        Project project = new Project("Name", "A description",  new DateOnly(2024, 07, 19));
        project.Admin = admin;
        project.assTaskAdmin(admin, task);
        Assert.Throws<ArgumentException>(() =>
        {
            project.assResourceAdmin(null, task, resource);
        });
    }
    
    [TestMethod]
    public void invalid_add_resource_bcNotAdmin()
    {
        Task task = new Task("Task", "Descrip Task", new DateOnly(2024, 07, 19), 1);
        Resource resource = new Resource("Dev", TaskType.Human, "Frontend dev");
        User admin = new User("Alvaro", "Redo", "example@gmail.com", "$Ab23456", new DateOnly(2004, 07,19 ));
        Project project = new Project("Name", "A description",  new DateOnly(2024, 07, 19));
        project.Admin = admin;
        User member = new User("Alvaro2", "Redo", "example@gmail.com", "$Ab23456", new DateOnly(2004, 07,19 ));
        project.assTaskAdmin(admin, task);
        Assert.Throws<ArgumentException>(() =>
        {
            project.assResourceAdmin(member, task, resource);
        });
    }
    
    [TestMethod]
    public void invalid_add_resource_bcTasknull()
    {
        Task task = new Task("Task", "Descrip Task", new DateOnly(2024, 07, 19), 1);
        Resource resource = new Resource("Dev", TaskType.Human, "Frontend dev");
        User admin = new User("Alvaro", "Redo", "example@gmail.com", "$Ab23456", new DateOnly(2004, 07,19 ));
        Project project = new Project("Name", "A description",  new DateOnly(2024, 07, 19));
        project.Admin = admin;
        project.assTaskAdmin(admin, task);
        Assert.Throws<ArgumentException>(() =>
        {
            project.assResourceAdmin(admin, null, resource);
        });
    }
    
    [TestMethod]
    public void invalid_add_resource_bcResourceNull()
    {
        Task task = new Task("Task", "Descrip Task", new DateOnly(2024, 07, 19), 1);
        User admin = new User("Alvaro", "Redo", "example@gmail.com", "$Ab23456", new DateOnly(2004, 07,19 ));
        Project project = new Project("Name", "A description",  new DateOnly(2024, 07, 19));
        project.Admin = admin;
        project.assTaskAdmin(admin, task);
        Assert.Throws<ArgumentException>(() =>
        {
            project.assResourceAdmin(admin, task, null);
        });
    }
    
    [TestMethod]
    public void invalid_add_resource_bcTaskNotExisting()
    {
        Task task = new Task("Task", "Descrip Task", new DateOnly(2024, 07, 19), 1);
        Resource resource = new Resource("Dev", TaskType.Human, "Frontend dev");
        User admin = new User("Alvaro", "Redo", "example@gmail.com", "$Ab23456", new DateOnly(2004, 07,19 ));
        Project project = new Project("Name", "A description",  new DateOnly(2024, 07, 19));
        project.Admin = admin;
        Assert.Throws<ArgumentException>(() =>
        {
            project.assResourceAdmin(admin, task, resource);
        });
    }
    
    [TestMethod]
    public void invalid_add_resource_bcResourceAlreadyAssigned()
    {
        Task task = new Task("Task", "Descrip Task", new DateOnly(2024, 07, 19), 1);
        Resource resource = new Resource("Dev", TaskType.Human, "Frontend dev");
        User admin = new User("Alvaro", "Redo", "example@gmail.com", "$Ab23456", new DateOnly(2004, 07,19 ));
        Project project = new Project("Name", "A description",  new DateOnly(2024, 07, 19));
        project.Admin = admin;
        project.assTaskAdmin(admin, task);
        project.assResourceAdmin(admin, task, resource);
        Assert.Throws<ArgumentException>(() =>
        {
            project.assResourceAdmin(admin, task, resource);
        });
    }
    
    [TestMethod]
    public void delete_resource()
    {
        Task task = new Task("Task", "Descrip Task", new DateOnly(2024, 07, 19), 1);
        Resource resource = new Resource("Dev", TaskType.Human, "Frontend dev");
        User admin = new User("Alvaro", "Redo", "example@gmail.com", "$Ab23456", new DateOnly(2004, 07,19 ));
        Project project = new Project("Name", "A description",  new DateOnly(2024, 07, 19));
        project.Admin = admin;
        project.assTaskAdmin(admin, task);
        project.assResourceAdmin(admin, task, resource);
        project.deleteResourceAdmin(admin, task, resource);
        Assert.AreEqual(0, task.Resources.Count);
    }

    [TestMethod]
    public void invalid_delete_resource_bcNUllAdmin()
    {
        Task task = new Task("Task", "Descrip Task", new DateOnly(2024, 07, 19), 1);
        Resource resource = new Resource("Dev", TaskType.Human, "Frontend dev");
        User admin = new User("Alvaro", "Redo", "example@gmail.com", "$Ab23456", new DateOnly(2004, 07,19 ));
        Project project = new Project("Name", "A description",  new DateOnly(2024, 07, 19));
        project.Admin = admin;
        project.assTaskAdmin(admin, task);
        project.assResourceAdmin(admin, task, resource);
        Assert.Throws<ArgumentException>(() =>
        {
            project.deleteResourceAdmin(null, task, resource);
        });
    }
    
    [TestMethod]
    public void invalid_delete_resource_bcNotAdmin()
    {
        Task task = new Task("Task", "Descrip Task", new DateOnly(2024, 07, 19), 1);
        Resource resource = new Resource("Dev", TaskType.Human, "Frontend dev");
        User admin = new User("Alvaro", "Redo", "example@gmail.com", "$Ab23456", new DateOnly(2004, 07,19 ));
        Project project = new Project("Name", "A description",  new DateOnly(2024, 07, 19));
        project.Admin = admin;
        User member = new User("Alvaro2", "Redo", "example@gmail.com", "$Ab23456", new DateOnly(2004, 07,19 ));
        project.assTaskAdmin(admin, task);
        project.assResourceAdmin(admin, task, resource);
        Assert.Throws<ArgumentException>(() =>
        {
            project.deleteResourceAdmin(member, task, resource);
        });
    }
    
    [TestMethod]
    public void invalid_delete_resource_bcTasknull()
    {
        Task task = new Task("Task", "Descrip Task", new DateOnly(2024, 07, 19), 1);
        Resource resource = new Resource("Dev", TaskType.Human, "Frontend dev");
        User admin = new User("Alvaro", "Redo", "example@gmail.com", "$Ab23456", new DateOnly(2004, 07,19 ));
        Project project = new Project("Name", "A description",  new DateOnly(2024, 07, 19));
        project.Admin = admin;
        project.assTaskAdmin(admin, task);
        project.assResourceAdmin(admin, task, resource);
        Assert.Throws<ArgumentException>(() =>
        {
            project.deleteResourceAdmin(admin, null, resource);
        });
    }
    
    [TestMethod]
    public void invalid_delete_resource_bcResourceNull()
    {
        Task task = new Task("Task", "Descrip Task", new DateOnly(2024, 07, 19), 1);
        Resource resource = new Resource("Dev", TaskType.Human, "Frontend dev");
        User admin = new User("Alvaro", "Redo", "example@gmail.com", "$Ab23456", new DateOnly(2004, 07,19 ));
        Project project = new Project("Name", "A description",  new DateOnly(2024, 07, 19));
        project.Admin = admin;
        project.assTaskAdmin(admin, task);
        project.assResourceAdmin(admin, task, resource);
        Assert.Throws<ArgumentException>(() =>
        {
            project.deleteResourceAdmin(admin, task, null);
        });
    }
    
    [TestMethod]
    public void invalid_delete_resource_bcTaskNotExisting()
    {
        Task task = new Task("Task", "Descrip Task", new DateOnly(2024, 07, 19), 1);
        Resource resource = new Resource("Dev", TaskType.Human, "Frontend dev");
        User admin = new User("Alvaro", "Redo", "example@gmail.com", "$Ab23456", new DateOnly(2004, 07,19 ));
        Project project = new Project("Name", "A description",  new DateOnly(2024, 07, 19));
        project.Admin = admin;
        Assert.Throws<ArgumentException>(() =>
        {
            project.deleteResourceAdmin(admin, task, resource);
        });
    }
    
    [TestMethod]
    public void invalid_delete_resource_bcResourceIsNotAssigned()
    {
        Task task = new Task("Task", "Descrip Task", new DateOnly(2024, 07, 19), 1);
        Resource resource = new Resource("Dev", TaskType.Human, "Frontend dev");
        User admin = new User("Alvaro", "Redo", "example@gmail.com", "$Ab23456", new DateOnly(2004, 07,19 ));
        Project project = new Project("Name", "A description",  new DateOnly(2024, 07, 19));
        project.Admin = admin;
        project.assTaskAdmin(admin, task);
        Assert.Throws<ArgumentException>(() =>
        {
            project.deleteResourceAdmin(admin, task, resource);
        });
    }

    [TestMethod]
    public void assign_TaskToMember()
    {
        Task task = new Task("Task", "Descrip Task", new DateOnly(2024, 07, 19), 1);
        Project project = new Project("Name", "A description",  new DateOnly(2024, 07, 19));
        User member = new User("Alvaro", "Redo", "example@gmail.com", "$Ab23456", new DateOnly(2004, 07,19 ));
        User member2 = new User("Alvaro2", "Redo", "example@gmail.com", "$Ab23456", new DateOnly(2004, 07,19 ));
        project.Admin = member;
        project.assMember(member2);
        project.assTaskToMemberAdmin(member, member2, task);
        Assert.AreEqual(1, member2.tasks.Count);
    }
    
    [TestMethod]
    public void assign_TaskToMember_invalid_bcNUllAdmin()
    {
        Task task = new Task("Task", "Descrip Task", new DateOnly(2024, 07, 19), 1);
        Project project = new Project("Name", "A description",  new DateOnly(2024, 07, 19));
        User member2 = new User("Alvaro2", "Redo", "example@gmail.com", "$Ab23456", new DateOnly(2004, 07,19 ));
        project.assMember(member2);
        Assert.Throws<ArgumentException>(() =>
        {
            project.assTaskToMemberAdmin(null, member2, task);
        });
    }

    [TestMethod]
    public void assign_TaskToMember_invalid_bcNUllUser()
    {
        Task task = new Task("Task", "Descrip Task", new DateOnly(2024, 07, 19), 1);
        User member = new User("Alvaro", "Redo", "example@gmail.com", "$Ab23456", new DateOnly(2004, 07,19 ));
        Project project = new Project("Name", "A description",  new DateOnly(2024, 07, 19));
        project.Admin = member;
        Assert.Throws<ArgumentException>(() =>
        {
            project.assTaskToMemberAdmin(member, null, task);
        });
    }

    [TestMethod]
    public void assign_TaskToMember_invalid_bcNotAdmin()
    {
        Task task = new Task("Task", "Descrip Task", new DateOnly(2024, 07, 19), 1);
        User member = new User("Alvaro", "Redo", "example@gmail.com", "$Ab23456", new DateOnly(2004, 07,19 ));
        User member2 = new User("Alvaro2", "Redo", "example@gmail.com", "$Ab23456", new DateOnly(2004, 07,19 ));
        Project project = new Project("Name", "A description",  new DateOnly(2024, 07, 19));
        project.assMember(member2);
        Assert.Throws<ArgumentException>(() =>
        {
            project.assTaskToMemberAdmin(member, member2, task);
        });
    }
    
    [TestMethod]
    public void assign_TaskToMember_invalid_bcNullTask()
    {
        User member = new User("Alvaro", "Redo", "example@gmail.com", "$Ab23456", new DateOnly(2004, 07,19 ));
        User member2 = new User("Alvaro2", "Redo", "example@gmail.com", "$Ab23456", new DateOnly(2004, 07,19 ));
        Project project = new Project("Name", "A description",  new DateOnly(2024, 07, 19));
        project.Admin = member;
        project.assMember(member2);
        Assert.Throws<ArgumentException>(() =>
        {
            project.assTaskToMemberAdmin(member, member2, null);
        });
    }
    
    [TestMethod]
    public void assign_TaskToMember_invalid_bcMember2NotAssigned()
    {
        Task task = new Task("Task", "Descrip Task", new DateOnly(2024, 07, 19), 1);
        User member = new User("Alvaro", "Redo", "example@gmail.com", "$Ab23456", new DateOnly(2004, 07,19 ));
        User member2 = new User("Alvaro2", "Redo", "example@gmail.com", "$Ab23456", new DateOnly(2004, 07,19 ));
        Project project = new Project("Name", "A description",  new DateOnly(2024, 07, 19));
        project.Admin = member;
        Assert.Throws<ArgumentException>(() =>
        {
            project.assTaskToMemberAdmin(member, member2, task);
        });
    }
    
    [TestMethod]
    public void assign_TaskToMember_invalid_bcTaskIsAssigned()
    {
        Task task = new Task("Task", "Descrip Task", new DateOnly(2024, 07, 19), 1);
        User member = new User("Alvaro", "Redo", "example@gmail.com", "$Ab23456", new DateOnly(2004, 07,19 ));
        User member2 = new User("Alvaro2", "Redo", "example@gmail.com", "$Ab23456", new DateOnly(2004, 07,19 ));
        Project project = new Project("Name", "A description",  new DateOnly(2024, 07, 19));
        project.Admin = member;
        project.assMember(member2);
        member2.tasks.Add(task);
        Assert.Throws<ArgumentException>(() =>
        {
            project.assTaskToMemberAdmin(member, member2, task);
        });
    }
    
    [TestMethod]
    public void assign_TaskToMember_valid_but_TaskIsInTasks()
    {
        Task task = new Task("Task", "Descrip Task", new DateOnly(2024, 07, 19), 1);
        Project project = new Project("Name", "A description",  new DateOnly(2024, 07, 19));
        User member = new User("Alvaro", "Redo", "example@gmail.com", "$Ab23456", new DateOnly(2004, 07,19 ));
        User member2 = new User("Alvaro2", "Redo", "example@gmail.com", "$Ab23456", new DateOnly(2004, 07,19 ));
        project.Admin = member;
        project.assMember(member2);
        project.assTaskAdmin(member, task);
        project.assTaskToMemberAdmin(member, member2, task);
        Assert.AreEqual(1, member2.tasks.Count);
    }

    [TestMethod]
    public void delete_TaskFromMember()
    {
        Task task = new Task("Task", "Descrip Task", new DateOnly(2024, 07, 19), 1);
        Task task2 = new Task("Task2", "Descrip Task", new DateOnly(2024, 07, 19), 1);
        Project project = new Project("Name", "A description",  new DateOnly(2024, 07, 19));
        User member = new User("Alvaro", "Redo", "example@gmail.com", "$Ab23456", new DateOnly(2004, 07,19 ));
        User member2 = new User("Alvaro2", "Redo", "example@gmail.com", "$Ab23456", new DateOnly(2004, 07,19 ));
        project.Admin = member;
        project.assMember(member2);
        project.assTaskToMemberAdmin(member, member2, task);
        project.assTaskToMemberAdmin(member, member2, task2);
        project.deleteTaskToMemberAdmin(member, member2, task);
        Assert.AreEqual(1, member2.tasks.Count);
        Assert.AreEqual(task2, member2.tasks[0]);
    }

    [TestMethod]
    public void delete_TaskFromMember_invalid_bcNUllAdmin()
    {
        Task task = new Task("Task", "Descrip Task", new DateOnly(2024, 07, 19), 1);
        Task task2 = new Task("Task2", "Descrip Task", new DateOnly(2024, 07, 19), 1);
        Project project = new Project("Name", "A description",  new DateOnly(2024, 07, 19));
        User member = new User("Alvaro", "Redo", "example@gmail.com", "$Ab23456", new DateOnly(2004, 07,19 ));
        User member2 = new User("Alvaro2", "Redo", "example@gmail.com", "$Ab23456", new DateOnly(2004, 07,19 ));
        project.Admin = member;
        project.assMember(member2);
        project.assTaskToMemberAdmin(member, member2, task);
        project.assTaskToMemberAdmin(member, member2, task2);
        Assert.Throws<ArgumentException>(() =>
        {
            project.deleteTaskToMemberAdmin(null, member2, task);
        });
    }

    [TestMethod]
    public void delete_TaskFromMember_invalid_bcNUllUser()
    {
        Task task = new Task("Task", "Descrip Task", new DateOnly(2024, 07, 19), 1);
        Task task2 = new Task("Task2", "Descrip Task", new DateOnly(2024, 07, 19), 1);
        Project project = new Project("Name", "A description",  new DateOnly(2024, 07, 19));
        User member = new User("Alvaro", "Redo", "example@gmail.com", "$Ab23456", new DateOnly(2004, 07,19 ));
        User member2 = new User("Alvaro2", "Redo", "example@gmail.com", "$Ab23456", new DateOnly(2004, 07,19 ));
        project.Admin = member;
        project.assMember(member2);
        project.assTaskToMemberAdmin(member, member2, task);
        project.assTaskToMemberAdmin(member, member2, task2);
        Assert.Throws<ArgumentException>(() =>
        {
            project.deleteTaskToMemberAdmin(member, null, task);
        });
    }

    [TestMethod]
    public void delete_TaskFromMember_invalid_bcNotAdmin()
    {
        Task task = new Task("Task", "Descrip Task", new DateOnly(2024, 07, 19), 1);
        Task task2 = new Task("Task2", "Descrip Task", new DateOnly(2024, 07, 19), 1);
        Project project = new Project("Name", "A description",  new DateOnly(2024, 07, 19));
        User member = new User("Alvaro", "Redo", "example@gmail.com", "$Ab23456", new DateOnly(2004, 07,19 ));
        User member2 = new User("Alvaro2", "Redo", "example@gmail.com", "$Ab23456", new DateOnly(2004, 07,19 ));
        project.Admin = member;
        project.assMember(member2);
        project.assTaskToMemberAdmin(member, member2, task);
        project.assTaskToMemberAdmin(member, member2, task2);
        Assert.Throws<ArgumentException>(() =>
        {
            project.deleteTaskToMemberAdmin(member2, member, task);
        });
    }

    [TestMethod]
    public void delete_TaskFromMember_invalid_bcNullTask()
    {
        Task task = new Task("Task", "Descrip Task", new DateOnly(2024, 07, 19), 1);
        Task task2 = new Task("Task2", "Descrip Task", new DateOnly(2024, 07, 19), 1);
        Project project = new Project("Name", "A description",  new DateOnly(2024, 07, 19));
        User member = new User("Alvaro", "Redo", "example@gmail.com", "$Ab23456", new DateOnly(2004, 07,19 ));
        User member2 = new User("Alvaro2", "Redo", "example@gmail.com", "$Ab23456", new DateOnly(2004, 07,19 ));
        project.Admin = member;
        project.assMember(member2);
        project.assTaskToMemberAdmin(member, member2, task);
        project.assTaskToMemberAdmin(member, member2, task2);
        Assert.Throws<ArgumentException>(() =>
        {
            project.deleteTaskToMemberAdmin(member2, member, null);
        });
    }

    [TestMethod]
    public void delete_TaskFromMember_invalid_bcTaskIsNotAssigned()
    {
        Task task = new Task("Task", "Descrip Task", new DateOnly(2024, 07, 19), 1);
        Project project = new Project("Name", "A description",  new DateOnly(2024, 07, 19));
        User member = new User("Alvaro", "Redo", "example@gmail.com", "$Ab23456", new DateOnly(2004, 07,19 ));
        User member2 = new User("Alvaro2", "Redo", "example@gmail.com", "$Ab23456", new DateOnly(2004, 07,19 ));
        project.assMember(member2);
        Assert.Throws<ArgumentException>(() =>
        {
            project.deleteTaskToMemberAdmin(member, member2, task);
        });
    }

    [TestMethod]
    public void delete_TaskFromMember_invalid_bcTaskIsNotAssignedOnMember()
    {
        Task task = new Task("Task", "Descrip Task", new DateOnly(2024, 07, 19), 1);
        Task task2 = new Task("Task2", "Descrip Task", new DateOnly(2024, 07, 19), 1);
        Project project = new Project("Name", "A description",  new DateOnly(2024, 07, 19));
        User member = new User("Alvaro", "Redo", "example@gmail.com", "$Ab23456", new DateOnly(2004, 07,19 ));
        User member2 = new User("Alvaro2", "Redo", "example@gmail.com", "$Ab23456", new DateOnly(2004, 07,19 ));
        project.Admin = member;
        project.assMember(member2);
        project.assTaskAdmin(member, task);
        project.assTaskAdmin(member, task2);
        Assert.Throws<ArgumentException>(() =>
        {
            project.deleteTaskToMemberAdmin(member2, member, task);
        });
    }
    
    [TestMethod]
    public void getCriticalPath()
    {
        Task task = new Task("Task", "Descrip Task", new DateOnly(2024, 07, 19), 1);
        Task task2 = new Task("Task", "Descrip Task", new DateOnly(2024, 07, 19), 8);
        Task task3 = new Task("Task", "Descrip Task", new DateOnly(2024, 07, 19), 2);
        Task task4 = new Task("Task", "Descrip Task", new DateOnly(2024, 07, 19), 1);
        Project project = new Project("Name", "A description",  new DateOnly(2024, 07, 19));
        User member = new User("Alvaro", "Redo", "example@gmail.com", "$Ab23456", new DateOnly(2004, 07,19 ));
        project.Admin = member;
        task2.AddDepen(task);
        task3.AddDepen(task2);
        project.assTaskAdmin(member, task);
        project.assTaskAdmin(member, task2);
        project.assTaskAdmin(member, task3);
        project.assTaskAdmin(member, task4);
        Assert.AreEqual(task, project.getCriticalPath()[0]);
        Assert.AreEqual(3, project.getCriticalPath().Count);
    }
    
    [TestMethod]
    public void getCriticalPath2()
    {
        Task task = new Task("Task", "Descrip Task", new DateOnly(2024, 07, 19), 1);
        Task task2 = new Task("Task", "Descrip Task", new DateOnly(2024, 07, 19), 8);
        Task task3 = new Task("Task", "Descrip Task", new DateOnly(2024, 07, 19), 2);
        Task task4 = new Task("Task", "Descrip Task", new DateOnly(2024, 07, 19), 1);
        Task task5 = new Task("Task", "Descrip Task", new DateOnly(2024, 07, 19), 20);
        Project project = new Project("Name", "A description",  new DateOnly(2024, 07, 19));
        User member = new User("Alvaro", "Redo", "example@gmail.com", "$Ab23456", new DateOnly(2004, 07,19 ));
        project.Admin = member;
        task2.AddDepen(task);
        task3.AddDepen(task2);
        project.assTaskAdmin(member, task);
        project.assTaskAdmin(member, task5);
        project.assTaskAdmin(member, task2);
        project.assTaskAdmin(member, task3);
        project.assTaskAdmin(member, task4);
        Assert.AreEqual(task5, project.getCriticalPath()[0]);
        Assert.AreEqual(1, project.getCriticalPath().Count);
    }
    /*
     commented this bc idk how to put a dateOnly nullable, in stackOverflow says that we can work with dateOnly? (it accepts null)  
    public void set_invalid_endDate()
    {
        Project project = new Project("Name", "A description",  new DateOnly(2024, 07, 19));
        Assert.Throws<ArgumentException>(() =>
            {
                project.setEndDate(date);
            }
        );
    }
    */
}
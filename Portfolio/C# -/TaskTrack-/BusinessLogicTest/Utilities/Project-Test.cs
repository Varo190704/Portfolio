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
        Assert.AreEqual(admin.Email, project.Admin);
        Assert.IsNotNull(project.StartDate);
    }

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
        project.Admin = member.Email;
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
        project.Admin = member.Email;
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
    public void update_Task_invalid_bcNull_task()
    {
        Task task = new Task("Task", "Descrip Task", new DateOnly(2024, 07, 19), 1);
        User member = new User("Alvaro", "Redo", "example@gmail.com", "$Ab23456", new DateOnly(2004, 07,19 ));
        Project project = new Project("Name", "A description",  new DateOnly(2024, 07, 19));
        project.Admin = member.Email;
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
        project.Admin = member.Email;
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
        project.Admin = member.Email;
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
        project.Admin = member.Email;
        project.assTaskAdmin(member, task);
        User member2 = new User("Alvaro2", "Redo", "example@gmail.com", "$Ab23456", new DateOnly(2004, 07,19 ));
        Assert.Throws<ArgumentException>(() =>
            {
                project.updateTaskAdmin(member2, task2);
            }
        );
    }

    [TestMethod]
    public void delete_Task_invalid_bcNull_Admin()
    {
        Task task = new Task("Task", "Descrip Task", new DateOnly(2024, 07, 19), 1);
        User admin = new User("Alvaro", "Redo", "example@gmail.com", "$Ab23456", new DateOnly(2004, 07,19 ));
        Project project = new Project("Name", "A description",  new DateOnly(2024, 07, 19));
        project.Admin = admin.Email;
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
        project.Admin = admin.Email;
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
        project.Admin = admin.Email;
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
        project.Admin = admin.Email;
        Assert.Throws<ArgumentException>(() =>
            {
                project.deleteTaskAdmin(admin, task);
            }
        );
    }
    
    [TestMethod]
    public void update_Status_invalid_bcNull_Admin()
    {
        Task task = new Task("Task", "Descrip Task", new DateOnly(2024, 07, 19), 1);
        User admin = new User("Alvaro", "Redo", "example@gmail.com", "$Ab23456", new DateOnly(2004, 07,19 ));
        Project project = new Project("Name", "A description",  new DateOnly(2024, 07, 19));
        project.Admin = admin.Email;
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
        project.Admin = admin.Email;
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
        project.Admin = admin.Email;
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
        project.Admin = admin.Email;
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
    public void invalid_add_resource_bcNUllAdmin()
    {
        Task task = new Task("Task", "Descrip Task", new DateOnly(2024, 07, 19), 1);
        Resource resource = new Resource("Dev", TaskType.Human, "Frontend dev", "Dev");
        User admin = new User("Alvaro", "Redo", "example@gmail.com", "$Ab23456", new DateOnly(2004, 07,19 ));
        Project project = new Project("Name", "A description",  new DateOnly(2024, 07, 19));
        project.Admin = admin.Email;
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
        Resource resource = new Resource("Dev", TaskType.Human, "Frontend dev", "Dev");
        User admin = new User("Alvaro", "Redo", "example@gmail.com", "$Ab23456", new DateOnly(2004, 07,19 ));
        Project project = new Project("Name", "A description",  new DateOnly(2024, 07, 19));
        project.Admin = admin.Email;
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
        Resource resource = new Resource("Dev", TaskType.Human, "Frontend dev", "Dev");
        User admin = new User("Alvaro", "Redo", "example@gmail.com", "$Ab23456", new DateOnly(2004, 07,19 ));
        Project project = new Project("Name", "A description",  new DateOnly(2024, 07, 19));
        project.Admin = admin.Email;
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
        project.Admin = admin.Email;
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
        Resource resource = new Resource("Dev", TaskType.Human, "Frontend dev", "Dev");
        User admin = new User("Alvaro", "Redo", "example@gmail.com", "$Ab23456", new DateOnly(2004, 07,19 ));
        Project project = new Project("Name", "A description",  new DateOnly(2024, 07, 19));
        project.Admin = admin.Email;
        Assert.Throws<ArgumentException>(() =>
        {
            project.assResourceAdmin(admin, task, resource);
        });
    }
    
    [TestMethod]
    public void invalid_delete_resource_bcTaskNotExisting()
    {
        Task task = new Task("Task", "Descrip Task", new DateOnly(2024, 07, 19), 1);
        Resource resource = new Resource("Dev", TaskType.Human, "Frontend dev", "Dev");
        User admin = new User("Alvaro", "Redo", "example@gmail.com", "$Ab23456", new DateOnly(2004, 07,19 ));
        Project project = new Project("Name", "A description",  new DateOnly(2024, 07, 19));
        project.Admin = admin.Email;
        Assert.Throws<ArgumentException>(() =>
        {
            project.deleteResourceAdmin(admin, task, resource);
        });
    }
    
    [TestMethod]
    public void invalid_delete_resource_bcResourceIsNotAssigned()
    {
        Task task = new Task("Task", "Descrip Task", new DateOnly(2024, 07, 19), 1);
        Resource resource = new Resource("Dev", TaskType.Human, "Frontend dev", "Dev");
        User admin = new User("Alvaro", "Redo", "example@gmail.com", "$Ab23456", new DateOnly(2004, 07,19 ));
        Project project = new Project("Name", "A description",  new DateOnly(2024, 07, 19));
        project.Admin = admin.Email;
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
        project.Admin = member.Email;
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
        project.Admin = member.Email;
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
        project.Admin = member.Email;
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
        project.Admin = member.Email;
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
        project.Admin = member.Email;
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
        project.Admin = member.Email;
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
        project.Admin = member.Email;
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
        project.Admin = member.Email;
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
        project.Admin = member.Email;
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
        project.Admin = member.Email;
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
        project.Admin = member.Email;
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
        project.Admin = member.Email;
        project.assMember(member2);
        project.assTaskAdmin(member, task);
        project.assTaskAdmin(member, task2);
        Assert.Throws<ArgumentException>(() =>
        {
            project.deleteTaskToMemberAdmin(member2, member, task);
        });
    }
    
    [TestMethod]
    public void assign_lider_should_set_lider_and_update_user()
    {
        User lider = new User("Líder", "Lider", "lider@correo.com", "$Pa123456", new DateOnly(2000, 01, 01));
        Project project = new Project("Project", "Desc", new DateOnly(2025, 6, 1));

        project.assLider(lider);

        Assert.AreEqual(lider.Email, project.Lider);
        Assert.IsTrue(project.Members.Contains(lider));
        Assert.IsTrue(lider.Types.Contains(UserType.ProjectLider));
        Assert.IsTrue(lider.projects.Contains(project));
    }

    [TestMethod]
    public void assign_lider_should_throw_if_null()
    {
        Project project = new Project("Project", "Description", new DateOnly(2025, 6, 1));
        Assert.ThrowsException<ArgumentException>(() =>
        {
            project.assLider(null);
        });
    }
    
    [TestMethod]
    public void update_task_should_modify_task_properties()
    {
        Task original = new Task("Deploy", "Initial deployment", new DateOnly(2025, 6, 1), 3);
        original.Slak = 2;
        original.Status = TaskProgress.Pending;

        Project project = new Project("Release Project", "Deploy pipeline", new DateOnly(2025, 6, 1));
        project.Tasks.Add(original);

        Task updated = new Task("Deploy", "Final deployment to prod", new DateOnly(2025, 6, 1), 5);
        updated.Status = TaskProgress.Completed;
        updated.Slak = 2;

        project.UpdateTask(updated);

        Task t = project.Tasks[0];
        Assert.AreEqual("Final deployment to prod", t.Description);
        Assert.AreEqual(5, t.Duration);
        Assert.AreEqual(TaskProgress.Completed, t.Status);
        Assert.AreEqual(t.StartDate.AddDays(5), t.EndDate);
        Assert.AreEqual(t.EndDate.AddDays(2), t.LateEndDate);
    }
}
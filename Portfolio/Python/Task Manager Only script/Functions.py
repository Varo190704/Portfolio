def addTask(task):
    
    with open('Task Manager Only script\Task\TasksToComplete', 'a') as file:
        file.write(task + '\n')
    print("Task added successfully.")

def removeTask(task):

    try: 
        with open('Task Manager Only script\Task\TasksToComplete', 'r') as file:
            taskList = file.readlines()
        boolean = True
        with open('Task Manager Only script\Task\TasksToComplete', 'w') as file:
            counter = 0
            for i in taskList:
                if (task+'\n') == i:
                    if boolean:
                        del taskList[counter]
                        file.writelines(taskList)
                        boolean = False
                        print("Task removed successfully.")
                counter += 1
        if boolean == False:
            with open('Task Manager Only script\Task\TasksRemoved', 'a') as file:
                file.write(task + '\n')
    
    except FileNotFoundError:
        print("No tasks with that name in the database.")

def completeTask(task):

    try: 
        with open('Task Manager Only script\Task\TasksToComplete', 'r') as file:
            taskList = file.readlines()
        with open('Task Manager Only script\Task\TasksToComplete', 'w') as file:
            counter = 0
            boolean = True
            for i in taskList:
                if(boolean):
                    if (task+'\n') == i:
                            del taskList[counter]
                            file.writelines(taskList)
                            with open('Task Manager Only script\Task\CompletedTasks', 'a') as file:
                                file.write(task + '\n')
                            boolean = False
                            print("Task added successfully to the completed tasks file.")
                            print("Task removed successfully from the tasks to complete file.")
                    counter += 1
                    
    except FileNotFoundError:
        print("No tasks with that name in the database.")

def showPendingTasks():

    try: 
        
        with open('Task Manager Only script\Task\TasksToComplete', 'r') as file:
            pendingTasksList = file.readlines()
        print("   ")    
        print("*******************************"+ "\n"+ "\n")
        counter = 1
        
        for i in pendingTasksList:
            print(str(counter) + "- " + i)
            counter += 1
        print("   ")    
        print("*******************************"+ "\n"+ "\n")    

    except FileNotFoundError:
        print("No pending tasks in the database.")

def showCompletedTasks():

    try: 
    
        with open('Task Manager Only script\Task\CompletedTasks', 'r') as file:
            completedTasksList = file.readlines()
        print("   ")    
        print("*******************************"+ "\n"+ "\n")
        counter = 1
        
        for i in completedTasksList:
            print(str(counter) + "- " + i)
            counter += 1
        print("   ")    
        print("*******************************"+ "\n"+ "\n")    

    except FileNotFoundError:
        print("No completed tasks in the database.")

def showAllTasks():

    try: 
    
        with open('Task Manager Only script\Task\CompletedTasks', 'r') as file:
            completedTasksList = file.readlines()
        with open('Task Manager Only script\Task\TasksToComplete', 'r') as file:
            pendingTasksList = file.readlines()
        with open('Task Manager Only script\Task\TasksRemoved', 'r') as file:
            removedTasksList = file.readlines()
        print("   ")    
        print("*******************************"+ "\n"+ "\n")
        print("Completed Tasks")    
        counter = 1
        for i in completedTasksList:
            print(str(counter) + " - " + i)
            counter += 1
        print("   ")    
        print("*******************************"+ "\n"+ "\n")     
        print("Pending Tasks"+ "\n"+ "\n")
        counter = 1
        for i in pendingTasksList:
            print(str(counter) + " - " + i)
            counter += 1
        print("   ")    
        print("*******************************"+ "\n"+ "\n")    
        print("Removed Tasks")
        counter = 1
        for i in removedTasksList:
            print(str(counter) + " - " + i)
            counter += 1
        print("   ")    
        print("*******************************"+ "\n"+ "\n")     
        
    except FileNotFoundError:
        print("No pending tasks in the database.")

def clearRemovedList():
    with open('Task Manager Only script\Task\TasksRemoved', 'r') as file:
        removedTasksList = file.readlines()
    with open('Task Manager Only script\Task\TasksRemoved', 'w') as file:
        for i in removedTasksList:
            del removedTasksList

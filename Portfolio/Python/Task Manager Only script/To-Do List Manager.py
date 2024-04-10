import Functions

exitVariable = True

while exitVariable:
    print("*** Welcome User ***" + "\n" + "\n")
    print("Enter your choice: ")
    print("1. Main Menu (Actions based on numbers)")
    print("2. Exit from Orders" + "\n" + "\n")

    userChoice = input("Enter an option: ")

    if userChoice == '1':

        exitVariable2 = True
        while exitVariable2:
            print("*** Main Menu ***" + "\n" + "\n")
            print("1. Add a Task")
            print("2. Remove a Task")
            print("3. Mark a Task as Completed")
            print("4. List All Pending Tasks")
            print("5. List All Completed Tasks")
            print("6. List All Tasks")
            print("7. Exit to Welcome" + "\n" + "\n")
            option = input("Enter an option: ")

            if option == '1':

                task = input("Enter the name of the task to add: ")
                Functions.addTask(task)

            elif option == '2':

                task = input("Enter the name of the task to remove: ")
                Functions.removeTask(task)

            elif option == '3':

                task = input("Enter the name of the completed task: ")
                Functions.completeTask(task)

            elif option == '4':

                Functions.showPendingTasks()

            elif option == '5':

                Functions.showCompletedTasks()

            elif option == '6':

                Functions.showAllTasks()

            elif option == '7':

                print("Exiting to Welcome...")
                exitVariable2 = False
                Functions.clearRemovedList()

            else:
                print("Invalid option")

    elif userChoice == '2':
        exitVariable = False

    else:
        print("Invalid option")

print("   ")
print("*******************************" + "\n" + "\n")
print("Exiting the program")
print("   " + "\n")
print("*******************************" + "\n")

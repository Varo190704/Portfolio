# Start of commands

# Imports
import tkinter as tk
from tkinter import messagebox

# End of imports

# First menu

def integrate_first_menu():
    widgets = root.winfo_children()
    for widget in widgets:
        widget.destroy()
    menu_1_button = tk.Button(root, text="Main Menu", bg="blue", command=integrate_second_menu, height="10", width="25")
    exit_button = tk.Button(root, text="Exit App", bg="blue", command=confirm_exit, height="10", width="25")
    
    exit_button.place(x=900, y=270)
    menu_1_button.place(x=100, y= 270)

# Close window
 
def confirm_exit():
    widgets = root.winfo_children()
    for widget in widgets:
        widget.destroy()
    root.geometry("600x400")
    label = tk.Label(root, text="The list of deleted tasks will be erased", font=("Arial", 14), bg="#80CBC4")
    yes_button = tk.Button(root, text="I am sure" + '\n'+"to exit",  font=("Arial", 12), bg="blue", command=delete_deleted_list ,height="5", width="13")
    no_button = tk.Button(root, text="Back to"+'\n'+"main menu",  font=("Arial", 12), bg="blue", command=integrate_first_menu ,height="5", width="13")

    yes_button.place(x=20, y=150)
    no_button.place(x=450, y= 150)
    label.place(x=160, y= 80) 

def delete_deleted_list():
    with open('Python\Task manager\Task\TasksRemoved', 'w') as file:
        file.write('')
    close_window()

def clear_deleted():
    with open('Python\Task manager\Task\TasksRemoved', 'w') as file:
        file.write('')

def close_window():
    root.destroy()

def confirm_close():
    if messagebox.askokcancel("Exit", "Are you sure you want to exit?"):
        clear_deleted()
        close_window()

# End of close window

# End of first menu

# Second menu    

def integrate_second_menu():
    root.geometry("1200x825")
    widgets = root.winfo_children()
    for widget in widgets:
        widget.destroy()
    string1 ="Mark task" + '\n' + "as" + '\n'+"completed"
    string2 ="List all" +'\n' +"tasks"
    string3 ="List only" + '\n' + "completed" + '\n' +"tasks"
    string4 ="List only" + '\n' + "pending" + '\n' +"tasks"
    string5 = "Mark task" + '\n' + "deleted"
    string6 = "Exit to" + '\n' + "main" + '\n' + "menu"
    add_button =tk.Button(root, text="Add task", bg="blue", command=add_task, height="5", width="12")
    remove_button = tk.Button(root, text="Remove task", bg="blue", command=remove_task ,height="5", width="12") 
    mark_button = tk.Button(root, text=string1, command=complete_task, bg="blue", height="5", width="12")
    list_all_button = tk.Button(root, text=string2, bg="blue", command=list_all_tasks, height="5", width="12")
    list_comp_button = tk.Button(root, text=string3, bg="blue",command=list_completed_tasks, height="5", width="12")
    list_pen_button = tk.Button(root, text=string4, bg="blue",command=list_pending_tasks, height="5", width="12")
    list_del_button = tk.Button(root, text=string5, bg="blue",command=list_deleted_tasks, height="5", width="12")
    exit_menu1 = tk.Button(root, text=string6, bg="blue", command=integrate_first_menu, height="5", width="12")

    add_button.place(x=550, y= 20)
    remove_button.place(x=550, y= 120)
    mark_button.place(x=550, y= 220)
    list_all_button.place(x=550, y= 320)
    list_comp_button.place(x=550, y= 420)
    list_pen_button.place(x=550, y= 520)
    list_del_button.place(x=550, y= 620)
    exit_menu1.place(x=550, y= 720)

# End of second menu

# Add task
    
def add_task():
    widgets = root.winfo_children()
    for widget in widgets:
        widget.destroy()
    root.geometry("600x350")
    text_area = tk.Text(root, height=3, width=60)
    text_area.bind("<Key>", limit_characters)
    title = tk.Label(root, text="Please enter the name of the task to add:", font=(18), bg="#80CBC4")
    get_text_button = tk.Button(root, text="Add", command=lambda: get_text_add(text_area), bg= "Yellow")
    return_button = tk.Button(root, text="Return", command=integrate_second_menu, bg= "Yellow")
    return_button.place(x=240, y=290) 
    get_text_button.place(x=300, y= 290)
    text_area.place(x=60, y= 150) 
    title.place(x=85,y=40)
    
def get_text_add(text_area):
    text = text_area.get("1.0", "end-1c")
    with open('Python\Task manager\Task\TasksToComplete', 'a') as file:
        file.write(text + '\n')
    alert_correction()
    integrate_second_menu()

def limit_characters(event):
    max_characters = 60
    if len(event.widget.get("1.0", "end-1c")) >= max_characters:
        event.widget.config(state="disabled") 
        messagebox.showinfo("Task Manager", "Maximum task name size should not exceed 60 characters")
    elif event.keysym == "BackSpace":
        event.widget.config(state="normal")

# End of add task

# Remove task

def remove_task():
    widgets = root.winfo_children()
    for widget in widgets:
        widget.destroy()
    root.geometry("600x350")
    text_area2 = tk.Text(root, height=3, width=60)
    title = tk.Label(root, text="Please enter the name of the task to remove:", font=(18), bg="#80CBC4")
    get_text_button = tk.Button(root, text="Delete", command=lambda: get_text_delete(text_area2), bg= "Yellow")
    return_button = tk.Button(root, text="Return", command=integrate_second_menu, bg= "Yellow")
    return_button.place(x=240, y=270)
    get_text_button.place(x=300, y= 270)
    text_area2.place(x=60, y= 150) 
    title.place(x=85,y=40)
    
def get_text_delete(text_area2):
    text2 = text_area2.get("1.0", "end-1c") 
    delete_task(text2)

def delete_task(text2):
    try:
        with open('Python\Task manager\Task\TasksToComplete', 'r') as file:
            task_list = file.readlines()
        boolean = True
        with open('Python\Task manager\Task\TasksToComplete', 'w') as file:
            for i in task_list:
                if boolean:
                    if text2 == i.strip():
                        task_list.remove(i)
                        file.writelines(task_list)
                        boolean = False
                        alert_correction()
                        integrate_second_menu()
            if boolean:
                file.writelines(task_list)
                alert_failed()
                integrate_second_menu()
        if not boolean:
            with open('Python\Task manager\Task\TasksRemoved', 'a') as file:
                file.write(text2 + '\n')

    except FileNotFoundError:
        alert_failed()
        integrate_second_menu()

# End of remove task

# Complete task
            
def complete_task():
    widgets = root.winfo_children()
    for widget in widgets:
        widget.destroy()
    root.geometry("600x350")
    text_area3 = tk.Text(root, height=3, width=60)
    title = tk.Label(root, text="Please enter the name of the task to complete:", font=(18), bg="#80CBC4")
    get_text_button = tk.Button(root, text="Mark as completed", command=lambda: get_text_complete(text_area3), bg= "Yellow")
    return_button = tk.Button(root, text="Return", command=integrate_second_menu, bg= "Yellow")
    return_button.place(x=200, y=290) 
    get_text_button.place(x=260, y= 290)
    text_area3.place(x=60, y= 150) 
    title.place(x=85,y=40)


def get_text_complete(text_area3):
    text3 = text_area3.get("1.0", "end-1c") 
    complete_task(text3)

def complete_task(text3):
    try:
        with open('Python\Task manager\Task\TasksToComplete', 'r') as file:
            task_list = file.readlines()
        boolean = True
        with open('Python\Task manager\Task\TasksToComplete', 'w') as file:
            for i in task_list:
                if boolean:
                    if text3 == i.strip():
                        task_list.remove(i)
                        file.writelines(task_list)
                        boolean = False
                        alert_correction()
                        integrate_second_menu()
            if boolean:
                file.writelines(task_list)
                alert_failed()
                integrate_second_menu()

        if not boolean:
            with open('Python\Task manager\Task\CompletedTasks', 'a') as file:
                file.write(text3 + '\n')

    except FileNotFoundError:
        alert_failed()
        integrate_second_menu()

# End of complete task

# List all tasks
        
def list_all_tasks():
    widgets = root.winfo_children()
    for widget in widgets:
        widget.destroy()
    root.geometry("600x800")
    root.resizable(0,1)
    list_all_tasks_func()

def list_all_tasks_func():
    try: 
        with open('Python\Task manager\Task\TasksToComplete', 'r') as file:
            pending_tasks_list = file.readlines()
        count = 0
        z=0
        c=20
        validator=0
        string = " ***  Pending tasks *** "
        title_p = tk.Label(root, text=string, font=(20), bg="#80CBC4")
        title_p.place(x=0, y=c)
        if len(pending_tasks_list)>0:        
            for i in pending_tasks_list:
                c+=50
                put_label(i, count,z, c)
                count+=1
            c+=70
        else:
            validator+=1
        with open('Python\Task manager\Task\CompletedTasks', 'r') as file:
            completed_tasks_list = file.readlines()
        count=0
        string = " ***  Completed tasks *** "
        title_c = tk.Label(root, text=string, font=(20), bg="#80CBC4")
        title_c.place(x=0, y=c)
        if len(completed_tasks_list)>0:        
            for i in completed_tasks_list:
                c+=50
                put_label(i, count,z, c)
                count+=1
            c+=70
        else:
            validator+=1  
        with open('Python\Task manager\Task\TasksRemoved', 'r') as file:
            deleted_tasks_list = file.readlines()
        count=0
        string = " ***  Deleted tasks *** "
        title_d = tk.Label(root, text=string, font=(20), bg="#80CBC4")
        title_d.place(x=0, y=c)
        if len(deleted_tasks_list)>0:        
            for i in deleted_tasks_list:
                c+=50
                put_label(i, count,z, c)
                count+=1
            c+=70 
        else:
            validator+=1                    
        if validator==3:
            alert_fail_list()   
        exit_button = tk.Button(root, text="Exit to main menu", command=integrate_second_menu, bg= "Yellow")
        exit_button.place(x=225, y= c)
    
    except FileNotFoundError:
        alert_fail_list()   

# End list all tasks

# List completed tasks

def list_completed_tasks():
    widgets = root.winfo_children()
    for widget in widgets:
        widget.destroy()
    root.geometry("600x800")
    root.resizable(0,1)
    list_completed_tasks_func()    

def list_completed_tasks_func():
    try: 
        with open('Python\Task manager\Task\CompletedTasks', 'r') as file:
                completed_tasks_list = file.readlines()
        count=0
        z=0
        c=20
        string = " ***  Completed tasks *** "
        title_c = tk.Label(root, text=string, font=(20), bg="#80CBC4")
        title_c.place(x=0, y=c)
        if len(completed_tasks_list)>0:        
            for i in completed_tasks_list:
                c+=50
                put_label(i, count,z, c)
                count+=1
            c+=70 
        exit_button = tk.Button(root, text="Exit to main menu", command=integrate_second_menu, bg= "Yellow")
        exit_button.place(x=225, y= c)
    except FileNotFoundError:
        alert_fail_list()              

# End list completed tasks

# List pending tasks
        
def list_pending_tasks():
    widgets = root.winfo_children()
    for widget in widgets:
        widget.destroy()
    root.geometry("600x800")
    root.resizable(0,1)
    list_pending_tasks_func()    

def list_pending_tasks_func():
    try: 
        with open('Python\Task manager\Task\TasksToComplete', 'r') as file:
                pending_tasks_list = file.readlines()
        count=0
        z=0
        c=20
        string = " ***  Pending tasks *** "
        title_c = tk.Label(root, text=string, font=(20), bg="#80CBC4")
        title_c.place(x=0, y=c)
        if len(pending_tasks_list)>0:        
            for i in pending_tasks_list:
                c+=50
                put_label(i, count,z, c)
                count+=1
            c+=70 
        exit_button = tk.Button(root, text="Exit to main menu", command=integrate_second_menu, bg= "Yellow")
        exit_button.place(x=225, y= c)
    except FileNotFoundError:
        alert_fail_list()              

# End list pending tasks

# List deleted tasks
    
def list_deleted_tasks():
    widgets = root.winfo_children()
    for widget in widgets:
        widget.destroy()
    root.geometry("600x800")
    root.resizable(0,1)
    list_deleted_tasks_func()    

def list_deleted_tasks_func():
    try: 
        with open('Python\Task manager\Task\TasksRemoved', 'r') as file:
                deleted_tasks_list = file.readlines()
        count=0
        z=0
        c=20
        string = " ***  Deleted tasks *** "
        title_c = tk.Label(root, text=string, font=(20), bg="#80CBC4")
        title_c.place(x=0, y=c)
        if len(deleted_tasks_list)>0:        
            for i in deleted_tasks_list:
                c+=50
                put_label(i, count,z, c)
                count+=1
            c+=70 
        exit_button = tk.Button(root, text="Exit to main menu", command=integrate_second_menu, bg= "Yellow")
        exit_button.place(x=225, y= c)
    except FileNotFoundError:
        alert_fail_list() 

# End list deleted tasks

# End of second menu
    
# Alerts

def alert_correction():
    widgets = root.winfo_children()
    for widget in widgets:
        widget.destroy()
    messagebox.showinfo("Task Manager", "Operation completed successfully")
    
def alert_failed():
    widgets = root.winfo_children()
    for widget in widgets:
        widget.destroy()
    messagebox.showinfo("Task Manager", "Operation failed. Explanation: There is no task with that name")

def alert_fail_list():
    widgets = root.winfo_children()
    for widget in widgets:
        widget.destroy()
    messagebox.showinfo("Task Manager", "Operation failed. Explanation: There are no tasks to display")

# End of alerts
    
# Labels

def put_label(i, count, z, c):
    string = str((count+1)) + "- " + i
    task_label = tk.Label(root, text=string, bg="#80CBC4")
    task_label.place(x=z, y=c)

# End of labels

# Definition of root and tk commands
    
root = tk.Tk()
root.title("Task Manager")
root.geometry("1200x725")
root.iconbitmap("Python\Task manager\code\InterfazG\Imagenes\logoPort.ico") 
root.config(bg="#80CBC4")
root.resizable(0,0)
root.protocol("WM_DELETE_WINDOW", confirm_close)

# End of root and tk commands 

# Main function

integrate_first_menu()

# End of main function

root.mainloop()

# End of commands

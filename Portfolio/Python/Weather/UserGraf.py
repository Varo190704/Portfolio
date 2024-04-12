#imports

import tkinter as tk
from tkinter import messagebox
from tkinter import *
from tkcalendar import Calendar
from datetime import datetime
import weather_functions

#End of imports

#main menu

def main_menu():

    delete_widgets()
    string = "Menu based"+'\n'+"on current day"
    current_day_menu = tk.Button(root, text=string, bg="#b1ccc4", command= current_day, height="6", width="14", font=(20))
    exit_button = tk.Button(root, text="Exit", command=exit_app, bg="#b1ccc4", height="2", width="7", font=(20))
    exit_button.place(x=350, y=380)
    current_day_menu.place(x=320, y=200)

#End main menu

#current day menu 

def current_day():
    root.geometry("800x600")

    #date updates by changing the calendar

    def update_dynamic_date(event):

        selected_date = cal.get_date()
        date_label.config(text="Selected date: " + selected_date)

    #End date updates by changing the calendar

    delete_widgets()
    current_date_us = datetime.now()
    current_date_str = "Selected date: " + current_date_us.strftime("%m/%d/%y")
    string = "Enter the day" + '\n' + "you are interested in for weather info"
    enter_label = tk.Label(root, text=string, bg="#fff5d6", font=(30))
    back_button = tk.Button(root, text="         Back         ", command=main_menu, bg="#b1ccc4")
    date_label = tk.Label(root, text=current_date_str , bg="#fff5d6", font=(20))
    
#messagebox

    messagebox.showinfo("App message", "The date will be displayed in mm/dd/yy style")
    messagebox.showinfo("App message", "The API only shows weather for the current date even if you choose a future or past date")
    messagebox.showinfo("App message", "The date set in the calendar from the beginning is the user's current date")

#End of messagebox

    text = tk.Text(root, height=1, width=50)
    text.bind("<Key>", limit_characters)
    get_weather_button = tk.Button(root, text="Get weather", command=lambda: get_weather(text, cal), bg="#b1ccc4")


    cal = Calendar(root, selectmode="day", year=current_date_us.year, month=current_date_us.month, day=current_date_us.day)
    cal.bind("<<CalendarSelected>>", update_dynamic_date)

    enter_label.place(x=250, y=20)
    text.place(x=190, y=350)
    date_label.place(x=260, y=300)  #Add label to layout
    cal.place(x=250, y=100)
    back_button.place(x=290, y=400)
    get_weather_button.place(x=390, y=400) 

#End of current day menu

#Show weather

def get_weather(text, cal):
        
    root.geometry("400x300")


    city = text.get("1.0", "end-1c")
    date = cal.get_date()
    weather = weather_functions.get_current_weather(city, date)
    delete_widgets()
    result_label = tk.Label(root, text="", bg="#fff5d6", font=(20)) 

    if weather:
        result_label.config(text=weather)

    else:
        result_label.config(text="Error fetching weather forecast")

    back_button = tk.Button(root, text="         Back         ", command=current_day, bg="#b1ccc4")
    back_button.place(x=150, y=200)
    result_label.place(x=60, y=130)   

#End of Show weather

#Confirm exit

def confirm_exit():
    if messagebox.askokcancel("Exit Warning", "Are you sure you want to exit?"):
        exit_app()

#End of confirm exit

#Exit window

def exit_app():
    root.destroy()

#End of exit window

#Delete widget

def delete_widgets():
    widgets = root.winfo_children()
    for widget in widgets:
        widget.destroy()

#End of delete widget

#Limit characters in each text

def limit_characters(event):
    max_characters = 50
    if len(event.widget.get("1.0", "end-1c")) >= max_characters:
        event.widget.config(state="disabled") 
        messagebox.showinfo("Task Manager", "              Maximum city name length" +'\n' + "should not exceed 50 characters")
    elif event.keysym == "BackSpace":
        event.widget.config(state="normal")

#End of limit characters in each text


#initialize root
root = tk.Tk()
root.geometry("800x600")
root.title("Weather App")
root.iconbitmap("Python\Weather\img\Opera-Snapshot_2024-04-01_103739_smashinglogo.com.ico") 
root.config(bg="#fff5d6")
root.resizable(0,0)
root.protocol("WM_DELETE_WINDOW", confirm_exit)

main_menu()

root.mainloop()
#End of root initialization

#imports

import tkinter as tk
from tkinter import messagebox
from tkinter import*
from tkcalendar import Calendar
from datetime import datetime
import funciones_clima

#fin de imports

#menu principal

def menu_principal():

    borrar_widget()
    string= "Menu basado"+'\n'+"en día actual"
    men_d = tk.Button(root, text=string, bg="#b1ccc4", command=menu_dia_actual, height="6", width="14", font=(20))
    boton_salir = tk.Button(root, text="Salir", command=salir, bg="#b1ccc4", height="2", width="7", font=(20))
    boton_salir.place(x=350, y=380)
    men_d.place(x=320, y=200)

#Fin menu principal

#menu dia actual 

def menu_dia_actual():
    root.geometry("800x600")

    #fecha se actualiza cambiando el calendario

    def actualizar_fecha_dinamic(event):

        fecha_seleccionada = cal.get_date()
        etiqueta_fecha.config(text="La fecha seleccionada: " + fecha_seleccionada)

    #Fin fecha se actualiza cambiando el calendario

    borrar_widget()
    fecha_actual_us = datetime.now()
    fecha_actual_str = "La fecha seleccionada: " + fecha_actual_us.strftime("%m/%d/%y")
    string = "Ingrese día en el que" + '\n' + "está interesado en saber el clima"
    ingresar_label = tk.Label(root, text=string, bg="#fff5d6", font=(30))
    boton_volver = tk.Button(root, text="         Volver         ", command=menu_principal, bg="#b1ccc4")
    etiqueta_fecha = tk.Label(root, text=fecha_actual_str , bg="#fff5d6", font=(20))
    
#messagebox

    messagebox.showinfo("Mensaje de la app", "La fecha estará presentada en estilo mm/dd/aa")
    messagebox.showinfo("Mensaje de la app", "La api solo muestra el clima de la fecha actual por mas que se le de una posterior o anterior a la misma")
    messagebox.showinfo("Mensaje de la app", "La fecga establecida en el calendar desde un inicio es la actual del usuario")

#Fin de messagebox

    text = tk.Text(root, height=1, width=50)
    text.bind("<Key>", limitar_caracteres)
    boton_enviar_info = tk.Button(root, text="Conseguir clima", command=lambda: obtener_clima(text, cal), bg="#b1ccc4")


    cal = Calendar(root, selectmode="day", year=fecha_actual_us.year, moth=fecha_actual_us.month, day=fecha_actual_us.day)
    cal.bind("<<CalendarSelected>>", actualizar_fecha_dinamic)

    ingresar_label.place(x=250, y=20)
    text.place(x=190, y=350)
    etiqueta_fecha.place(x=260, y=300)  #Agregar la etiqueta al layout
    cal.place(x=250, y=100)
    boton_volver.place(x=290, y=400)
    boton_enviar_info.place(x=390, y=400) 

#Fin del menu del dia actual

#Mostrar clima

def obtener_clima(text, cal):
        
    root.geometry("400x300")


    ciudad = text.get("1.0", "end-1c")
    fecha = cal.get_date()
    clima = funciones_clima.obtener_clima_actual(ciudad, fecha)
    borrar_widget()
    resultado_label = tk.Label(root, text="", bg="#fff5d6", font=(20)) 

    if clima:
        resultado_label.config(text=clima)

    else:
        resultado_label.config(text="Error al obtener el pronóstico del clima")

    boton_volver = tk.Button(root, text="         Volver         ", command=menu_dia_actual, bg="#b1ccc4")
    boton_volver.place(x=150, y=200)
    resultado_label.place(x=60, y=130)   

#Fin de mostrar clima

#confirmar salida

def confirmar_salida():
    if messagebox.askokcancel("Advertencia de salida", "¿Está seguro que desea salir?"):
        salir()

#fin de confirmar salida

#salir de la ventana

def salir():
    root.destroy()

#fin de salir de la ventana

#borrar widget

def borrar_widget():
    widget = root.winfo_children()
    for i in widget:
        i.destroy()

#finalizar borrar widget

#Limitar caracteres en cada text

def limitar_caracteres(event):
    max_caracteres = 50
    if len(event.widget.get("1.0", "end-1c")) >= max_caracteres:
        event.widget.config(state="disabled") 
        messagebox.showinfo("Gestor de Tareas", "              Tamaño máximo del nombre" +'\n' + "de la ciudad no debe superar los 50 caracteres")
    elif event.keysym == "BackSpace":
        event.widget.config(state="normal")

#finalizar limitacion de caracteres en cada text


#inicio la root
root = tk.Tk()
root.geometry("800x600")
root.title("App de clima")
root.iconbitmap("Python\Clima\imagenes\Opera-Snapshot_2024-04-01_103739_smashinglogo.com.ico") 
root.config(bg="#fff5d6")
root.resizable(0,0)
root.protocol("WM_DELETE_WINDOW", confirmar_salida)

menu_principal()

root.mainloop()
#finalizo la inicializacion del root
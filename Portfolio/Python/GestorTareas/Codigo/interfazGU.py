#Inicio de comandos

#Imports

import tkinter as tk
from tkinter import messagebox

#Fin de imports

#Primer menu

def integrar_primer_menu():
    widget = root.winfo_children()
    for i in widget:
        i.destroy()
    menu_1_boton = tk.Button(root, text="Menu Principal", bg="blue", command=integrar_segundo_menu, height="10", width="25")
    salida_boton = tk.Button(root, text="Salida de la App", bg="blue", command=avisar_se_esta_cerrar ,height="10", width="25")
    
    salida_boton.place(x=900, y=270)
    menu_1_boton.place(x=100, y= 270)

#Cerrar ventana
 
def avisar_se_esta_cerrar():
    widget = root.winfo_children()
    for i in widget:
        i.destroy()
    root.geometry("600x400")
    Label = tk.Label(root, text="Se borrará la lista de eliminados", font=("Arial", 14), bg="#80CBC4")
    si = tk.Button(root, text="Estoy seguro" + '\n'+"de salir",  font=("Arial", 12), bg="blue", command=eliminar_lista_eliminadas ,height="5", width="13")
    no = tk.Button(root, text="Volver a"+'\n'+" menu principal",  font=("Arial", 12), bg="blue", command=integrar_primer_menu ,height="5", width="13")

    si.place(x=20, y=150)
    no.place(x=450, y= 150)
    Label.place(x=160, y= 80) 

def eliminar_lista_eliminadas():
    with open('GestorTareas\Tareas\TareasEliminadas', 'w') as file:
        file.write('')
    salir_de_ventana()

def borrar_eliminados():
    with open('GestorTareas\Tareas\TareasEliminadas', 'w') as file:
        file.write('')

def salir_de_ventana():
    root.destroy()

def confirmar_salida():
    if messagebox.askokcancel("Salir", "¿Está seguro que desea salir?"):
        borrar_eliminados()
        salir_de_ventana()

#Fin de cerrar ventana

#Fin de primer menu

#Segundo menu    

def integrar_segundo_menu():
    root.geometry("1200x825")
    widget = root.winfo_children()
    for i in widget:
        i.destroy()
    string1 ="Marcar tarea" + '\n' + "como" + '\n'+"completada"
    string2 ="Listar todas" +'\n' +"las tareas"
    string3 ="Listar solo" + '\n' + "las tareas" + '\n' +"completas"
    string4 ="Listar solo" + '\n' + "las tareas" + '\n' +"pendientes"
    string5 = "Marcar tarea" + '\n' + "eliminadas"
    string6 = "Salir a" + '\n' + "menu" + '\n' + "principal"
    agregar_t =tk.Button(root, text="Agregar tarea", bg="blue", command=agregar_tarea, height="5", width="12")
    quit_t = tk.Button(root, text="Quitar tarea", bg="blue", command=quitar_tarea ,height="5", width="12") 
    mark_t = tk.Button(root, text=string1, command=completada_tarea, bg="blue", height="5", width="12")
    list_t_t = tk.Button(root, text=string2, bg="blue", command=listar_todas_tareas, height="5", width="12")
    list_c_t = tk.Button(root, text=string3, bg="blue",command=listar_comp_tareas, height="5", width="12")
    list_p_t = tk.Button(root, text=string4, bg="blue",command=listar_pen_tareas, height="5", width="12")
    list_e_t = tk.Button(root, text=string5, bg="blue",command=listar_eli_tareas, height="5", width="12")
    salir_menu1 = tk.Button(root, text=string6, bg="blue", command=integrar_primer_menu, height="5", width="12")

    agregar_t.place(x=550, y= 20)
    quit_t.place(x=550, y= 120)
    mark_t.place(x=550, y= 220)
    list_t_t.place(x=550, y= 320)
    list_c_t.place(x=550, y= 420)
    list_p_t.place(x=550, y= 520)
    list_e_t.place(x=550, y= 620)
    salir_menu1.place(x=550, y= 720)

#Fin de segundo menu

#Agregar tarea
    
def agregar_tarea():
    widget = root.winfo_children()
    for i in widget:
        i.destroy()
    root.geometry("600x350")
    text_area = tk.Text(root, height=3, width=60)
    text_area.bind("<Key>", limitar_caracteres)
    titulo_t = tk.Label(root, text="Ingrese nombre de la tarea a agregar por favor:", font=(18), bg="#80CBC4")
    boton_obtener_texto = tk.Button(root, text="Agregar", command=lambda: conseguir_texto_agregar(text_area), bg= "Yellow")
    boton_volver = tk.Button(root, text="Volver", command=integrar_segundo_menu, bg= "Yellow")
    boton_volver.place(x=240, y=290) 
    boton_obtener_texto.place(x=300, y= 290)
    text_area.place(x=60, y= 150) 
    titulo_t.place(x=85,y=40)
    
def conseguir_texto_agregar(text_area):
    texto = text_area.get("1.0", "end-1c")
    with open('GestorTareas\Tareas\TareasACompletar', 'a') as file:
        file.write(texto + '\n')
    salte_correccion()
    integrar_segundo_menu()

def limitar_caracteres(event):
    max_caracteres = 60
    if len(event.widget.get("1.0", "end-1c")) >= max_caracteres:
        event.widget.config(state="disabled") 
        messagebox.showinfo("Gestor de Tareas", "Tamaño máximo del nombre de la tarea no debe superar los 60 caracteres")
    elif event.keysym == "BackSpace":
        event.widget.config(state="normal")

#Fin de agregar tarea

#Quitar tarea

def quitar_tarea():
    widget = root.winfo_children()
    for i in widget:
        i.destroy()
    root.geometry("600x350")
    text_area2 = tk.Text(root, height=3, width=60)
    titulo_t = tk.Label(root, text="Ingrese nombre de la tarea a agregar por favor:", font=(18), bg="#80CBC4")
    boton_obtener_texto = tk.Button(root, text="Eliminar", command=lambda: conseguir_texto_q(text_area2), bg= "Yellow")
    boton_volver = tk.Button(root, text="Volver", command=integrar_segundo_menu, bg= "Yellow")
    boton_volver.place(x=240, y=270)
    boton_obtener_texto.place(x=300, y= 270)
    text_area2.place(x=60, y= 150) 
    titulo_t.place(x=85,y=40)
    
def conseguir_texto_q(text_area2):
    texto2 = text_area2.get("1.0", "end-1c") 
    eliminar_tarea(texto2)

def eliminar_tarea(texto2):
    try:
        with open('GestorTareas\Tareas\TareasACompletar', 'r') as file:
            listaTareas1 = file.readlines()
        boolean = True
        with open('GestorTareas\Tareas\TareasACompletar', 'w') as file:
            for i in listaTareas1:
                if boolean:
                    if texto2 == i.strip():
                        listaTareas1.remove(i)
                        file.writelines(listaTareas1)
                        boolean = False
                        salte_correccion()
                        integrar_segundo_menu()
            if boolean:
                file.writelines(listaTareas1)
                salte_fallido()
                integrar_segundo_menu()
        if boolean == False:
            with open('GestorTareas\Tareas\TareasEliminadas', 'a') as file:
                file.write(texto2 + '\n')

    except FileNotFoundError:
        salte_fallido()
        integrar_segundo_menu()

#Fin de eliminar tarea

#Completar tarea
            
def completada_tarea():
    widget = root.winfo_children()
    for i in widget:
        i.destroy()
    root.geometry("600x350")
    text_area3 = tk.Text(root, height=3, width=60)
    titulo_t = tk.Label(root, text="Ingrese nombre de la tarea a agregar por favor:", font=(18), bg="#80CBC4")
    boton_obtener_texto = tk.Button(root, text="Marcar como completada", command=lambda: conseguir_texto_c(text_area3), bg= "Yellow")
    boton_volver = tk.Button(root, text="Volver", command=integrar_segundo_menu, bg= "Yellow")
    boton_volver.place(x=200, y=290) 
    boton_obtener_texto.place(x=260, y= 290)
    text_area3.place(x=60, y= 150) 
    titulo_t.place(x=85,y=40)


def conseguir_texto_c(text_area3):
    texto3 = text_area3.get("1.0", "end-1c") 
    completar_tarea(texto3)

def completar_tarea(texto3):
    try:
        with open('GestorTareas\Tareas\TareasACompletar', 'r') as file:
            listaTareas1 = file.readlines()
        boolean = True
        with open('GestorTareas\Tareas\TareasACompletar', 'w') as file:
            for i in listaTareas1:
                if boolean:
                    if texto3 == i.strip():
                        listaTareas1.remove(i)
                        file.writelines(listaTareas1)
                        boolean = False
                        salte_correccion()
                        integrar_segundo_menu()
            if boolean:
                file.writelines(listaTareas1)
                salte_fallido()
                integrar_segundo_menu()

        if boolean == False:
            with open('GestorTareas\Tareas\TareasCompletadas', 'a') as file:
                file.write(texto3 + '\n')

    except FileNotFoundError:
        salte_fallido()
        integrar_segundo_menu()

#Fin de completar tarea

#Listar tareas total
        
def listar_todas_tareas():
    widget = root.winfo_children()
    for i in widget:
        i.destroy()
    root.geometry("600x800")
    root.resizable(0,1)
    listar_tareas_t()

def listar_tareas_t():
    try: 
        with open('GestorTareas\Tareas\TareasACompletar', 'r') as file:
            listas_tot_a = file.readlines()
        contador=0
        z=0
        c=20
        validador=0
        string = " ***  Tareas pendientes *** "
        titulo_p = tk.Label(root, text=string, font=(20), bg="#80CBC4")
        titulo_p.place(x=0, y=c)
        if len(listas_tot_a)>0:        
            for i in listas_tot_a:
                c+=50
                poner_label(i, contador,z, c)
                contador+=1
            c+=70
        else:
            validador+=1
        with open('GestorTareas\Tareas\TareasCompletadas', 'r') as file:
            listas_tot_c = file.readlines()
        contador=0
        string = " ***  Tareas comletadas *** "
        titulo_c = tk.Label(root, text=string, font=(20), bg="#80CBC4")
        titulo_c.place(x=0, y=c)
        if len(listas_tot_c)>0:        
            for i in listas_tot_c:
                c+=50
                poner_label(i, contador,z, c)
                contador+=1
            c+=70
        else:
            validador+=1  
        with open('GestorTareas\Tareas\TareasEliminadas', 'r') as file:
            listas_tot_e = file.readlines()
        contador=0
        string = " ***  Tareas eliminadas *** "
        titulo_e = tk.Label(root, text=string, font=(20), bg="#80CBC4")
        titulo_e.place(x=0, y=c)
        if len(listas_tot_e)>0:        
            for i in listas_tot_e:
                c+=50
                poner_label(i, contador,z, c)
                contador+=1
            c+=70 
        else:
            validador+=1                    
        if validador==3:
            salte_fallo_lista()   
        boton_salida = tk.Button(root, text="Salir a menu principal", command=integrar_segundo_menu, bg= "Yellow")
        boton_salida.place(x=225, y= c)
    
    except FileNotFoundError:
        salte_fallo_lista()   

#Fin listar tareas total

#Listar tareas completadas

def listar_comp_tareas():
    widget = root.winfo_children()
    for i in widget:
        i.destroy()
    root.geometry("600x800")
    root.resizable(0,1)
    listar_tareas_complet()    

def listar_tareas_complet():
    try: 
        with open('GestorTareas\Tareas\TareasCompletadas', 'r') as file:
                listas_tot_c = file.readlines()
        contador=0
        z=0
        c=20
        string = " ***  Tareas comletadas *** "
        titulo_c = tk.Label(root, text=string, font=(20), bg="#80CBC4")
        titulo_c.place(x=0, y=c)
        if len(listas_tot_c)>0:        
            for i in listas_tot_c:
                c+=50
                poner_label(i, contador,z, c)
                contador+=1
            c+=70 
        boton_salida = tk.Button(root, text="Salir a menu principal", command=integrar_segundo_menu, bg= "Yellow")
        boton_salida.place(x=225, y= c)
    except FileNotFoundError:
        salte_fallo_lista()              

#Fin listar tareas completadas

#Listar tareas pendientes
        
def listar_pen_tareas():
    widget = root.winfo_children()
    for i in widget:
        i.destroy()
    root.geometry("600x800")
    root.resizable(0,1)
    listar_tareas_pendien()    

def listar_tareas_pendien():
    try: 
        with open('GestorTareas\Tareas\TareasACompletar', 'r') as file:
                listas_tot_c = file.readlines()
        contador=0
        z=0
        c=20
        string = " ***  Tareas pendientes *** "
        titulo_c = tk.Label(root, text=string, font=(20), bg="#80CBC4")
        titulo_c.place(x=0, y=c)
        if len(listas_tot_c)>0:        
            for i in listas_tot_c:
                c+=50
                poner_label(i, contador,z, c)
                contador+=1
            c+=70 
        boton_salida = tk.Button(root, text="Salir a menu principal", command=integrar_segundo_menu, bg= "Yellow")
        boton_salida.place(x=225, y= c)
    except FileNotFoundError:
        salte_fallo_lista()              

#Fin de listar tareas pendientes

#Listar tareas eliminadas
    
def listar_eli_tareas():
    widget = root.winfo_children()
    for i in widget:
        i.destroy()
    root.geometry("600x800")
    root.resizable(0,1)
    listar_tareas_elimi()    

def listar_tareas_elimi():
    try: 
        with open('GestorTareas\Tareas\TareasEliminadas', 'r') as file:
                listas_tot_c = file.readlines()
        contador=0
        z=0
        c=20
        string = " ***  Tareas eliminadas *** "
        titulo_c = tk.Label(root, text=string, font=(20), bg="#80CBC4")
        titulo_c.place(x=0, y=c)
        if len(listas_tot_c)>0:        
            for i in listas_tot_c:
                c+=50
                poner_label(i, contador,z, c)
                contador+=1
            c+=70 
        boton_salida = tk.Button(root, text="Salir a menu principal", command=integrar_segundo_menu, bg= "Yellow")
        boton_salida.place(x=225, y= c)
    except FileNotFoundError:
        salte_fallo_lista() 

#Fin de listar tareas eliminadas

#Fin de segundo menu
    
#Alertas

def salte_correccion():
    widget = root.winfo_children()
    for i in widget:
        i.destroy()
    messagebox.showinfo("Gestor de Tareas", "Operación realizada con éxito")
    
def salte_fallido():
    widget = root.winfo_children()
    for i in widget:
        i.destroy()
    messagebox.showinfo("Gestor de Tareas", "Operación fallida. Explicación: No existe tarea con dicho nombre")

def salte_fallo_lista():
    widget = root.winfo_children()
    for i in widget:
        i.destroy()
    messagebox.showinfo("Gestor de Tareas", "Operación fallida. Explicación: No hay tareas para mostrar")

#Fin de las alertas
    
#Labels

def poner_label(i, contador, z, c):
    string = str((contador+1)) + "- " + i
    tarea_lista = tk.Label(root, text=string, bg="#80CBC4")
    tarea_lista.place(x=z, y=c)

#Fin de labels

#Definicion de root y comandos de tk
    
root = tk.Tk()
root.title("Gestor de Tareas")
root.geometry("1200x725")
root.iconbitmap("GestorTareas\code\InterfazG\Imagenes\logoPort.ico") 
root.config(bg="#80CBC4")
root.resizable(0,0)
root.protocol("WM_DELETE_WINDOW", confirmar_salida)

#Fin de root y comandos de tk 

#Funcion principal

integrar_primer_menu()

#Fin de funcion principal

root.mainloop()

#Fin de comandos
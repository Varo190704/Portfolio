import argparse

def agregarTarea(tarea):
    
    with open('Gestor de tareas scrip\Tareas\TareasACompletar', 'a') as file:
        file.write(tarea + '\n')
    print("Tarea agregada correctamente.")

def eliminarTarea(tarea):

    try: 
        with open('Gestor de tareas scrip\Tareas\TareasACompletar', 'r') as file:
            listaTareas = file.readlines()
        boolean = True
        with open('Gestor de tareas scrip\Tareas\TareasACompletar', 'w') as file:
            contador=0
            for i in listaTareas:
                if (tarea+'\n') == i:
                    if boolean:
                        del listaTareas[contador]
                        file.writelines(listaTareas)
                        boolean = False
                        print("Tarea eliminada correctamente.")
                contador+=1
        if boolean == False:
            with open('Gestor de tareas scrip\Tareas\TareasEliminadas', 'a') as file:
                file.write(tarea + '\n')
    
    except FileNotFoundError:
        print("No hay tareas con ese nombre en la base de datos.")


def completadaTarea(tarea):

    try: 
        with open('Gestor de tareas scrip\Tareas\TareasACompletar', 'r') as file:
            listaTareas = file.readlines()
        with open('Gestor de tareas scrip\Tareas\TareasACompletar', 'w') as file:
            contador=0
            boolean = True
            for i in listaTareas:
                if(boolean):
                    if (tarea+'\n') == i:
                            del listaTareas[contador]
                            file.writelines(listaTareas)
                            with open('Gestor de tareas scrip\Tareas\TareasCompletadas', 'a') as file:
                                file.write(tarea + '\n')
                            boolean = False
                            print("Tarea agregada correctamente al archivo de tareas completada.")
                            print("Tarea eliminada correctamente del archivo de tareas acompletar.")
                    contador+=1
                    
    except FileNotFoundError:
        print("No hay tareas con ese nombre en la base de datos.")


def mostrarTareasACompletar():

    try: 
        
        with open('Gestor de tareas scrip\Tareas\TareasACompletar', 'r') as file:
            listaTareasPendientes = file.readlines()
        print("   ")    
        print("*******************************"+ "\n"+ "\n")
        contator = 1
        
        for i in listaTareasPendientes:
            print(str(contator) + "- " + i)
            contator+=1
        print("   ")    
        print("*******************************"+ "\n"+ "\n")    

    except FileNotFoundError:
        print("No hay tareas pendientes en la base de datos.")


def mostrarTareasComp():

    try: 
    
        with open('Gestor de tareas scrip\Tareas\TareasCompletadas', 'r') as file:
            listaTareasCompletas = file.readlines()
        print("   ")    
        print("*******************************"+ "\n"+ "\n")
        contator = 1
        
        for i in listaTareasCompletas:
            print(str(contator) + "- " + i)
            contator+=1
        print("   ")    
        print("*******************************"+ "\n"+ "\n")    

    except FileNotFoundError:
        print("No hay tareas pendientes en la base de datos.")


def mostrarTareasTota():

    try: 
    
        with open('Gestor de tareas scrip\Tareas\TareasCompletadas', 'r') as file:
            listaTareasCompletas = file.readlines()
        with open('Gestor de tareas scrip\Tareas\TareasACompletar', 'r') as file:
            listaTareasPendientes = file.readlines()
        with open('Gestor de tareas scrip\Tareas\TareasEliminadas', 'r') as file:
            listaTareasEliminadas = file.readlines()
        print("   ")    
        print("*******************************"+ "\n"+ "\n")
        print("Tareas Completadas")    
        contator = 1
        for i in listaTareasCompletas:
            print(str(contator) + " - " + i)
            contator+=1
        print("   ")    
        print("*******************************"+ "\n"+ "\n")     
        print("Tareas Pendientes"+ "\n"+ "\n")
        contator = 1
        for i in listaTareasPendientes:
            print(str(contator) + " - " + i)
            contator+=1
        print("   ")    
        print("*******************************"+ "\n"+ "\n")    
        print("Tareas Eliminadas")
        contator = 1
        for i in listaTareasEliminadas:
            print(str(contator) + " - " + i)
            contator+=1
        print("   ")    
        print("*******************************"+ "\n"+ "\n")     
        
    except FileNotFoundError:
        print("No hay tareas pendientes en la base de datos.")

def eliminarListaElim():
    with open('Gestor de tareas scrip\Tareas\TareasEliminadas', 'r') as file:
        listaTareas1 = file.readlines()
    with open('Gestor de tareas scrip\Tareas\TareasEliminadas', 'w') as file:
        for i in listaTareas1:
            del listaTareas1

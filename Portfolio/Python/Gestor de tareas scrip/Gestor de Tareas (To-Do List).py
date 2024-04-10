import Funciones

variableSal = True

while variableSal:
    print("*** Bienvenido Usuario ***"+"\n" + "\n")
    print("Ingrese qué opción quiere tomar: ")
    print("1. Menu Principal (Acciones a base de numeros)")
    print("2. Salir de Pedidos" + "\n"+ "\n")

    opcP = input("Ingrese una opción: ")
    
    if opcP == '1':

        variableSal2 = True
        while variableSal2:
            print("*** Menu Principal ***"+ "\n"+ "\n")
            print("1. Agregar una Tarea")
            print("2. Quitar una Tarea")
            print("3. Marcar como Completada una Tarea")
            print("4. Listar Todas las Tareas Pendientes")
            print("5. Listar Todas las Tareas Completadas")
            print("6. Listar Todas las Tareas")
            print("7. Salir a la Bienvenida"+ "\n"+ "\n")
            opc = input("Ingrese una opción: ")
            
            if opc == '1':

                tarea = input("Ingrese nombre de la tarea a ingresar: ")
                Funciones.agregarTarea(tarea)

            elif opc == '2':
                
                tarea = input("Ingrese nombre de la tarea que quiere eliminar: ")
                Funciones.eliminarTarea(tarea)

            elif opc == '3':

                tarea = input("Ingrese nombre de la Tarea Completada: ")
                Funciones.completadaTarea(tarea)

            elif opc == '4':

                Funciones.mostrarTareasACompletar()
            
            elif opc == '5':

                Funciones.mostrarTareasComp()

           
            elif opc == '6':

                Funciones.mostrarTareasTota()
            
            elif opc == '7':

                print("Saliendo a Bienvenida...")
                variableSal2 = False
                Funciones.eliminarListaElim()

            else:
                print("Opción inválida")

    elif opcP == '2':
        variableSal = False
    
    else:
            print("Opción inválida")

print("   ")    
print("*******************************"+ "\n"+ "\n")
print("Saliendo del programa")
print("   " + "\n" )    
print("*******************************"+ "\n")
/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Main.java to edit this template
 */
package modelo;

import java.util.*;

/**
 *
 *  @author Alvaro Redondo Motta
 * 
 */

public class Obligatorio {

    public static void saleDelMenu(String opcion, Scanner entrada, Tablero tablero, Pantalla pantalla, Lista lista) {
        while (!opcion.equalsIgnoreCase("e")) { 
            opcion = primerOpcion(entrada, pantalla);
            if (opcion.equalsIgnoreCase("a")) {            
                System.out.println("Opcion a seleccionada");
                agregarJugador(entrada, lista);
            }
            if(opcion.equalsIgnoreCase("b")){  
                System.out.println("Opcion b seleccionada");
                int dimension = dimension(entrada);
                int cantAutos = cantAutos(entrada);
                tablero.setDimensionPropio(dimension);
                crearTableroPropio(cantAutos, entrada, lista, tablero);
                System.out.println("");
            }
            if(lista.tamañoListaJugadores() <= 1 && opcion.equalsIgnoreCase("c")) { 
                System.out.println("Opcion c seleccionada");
                System.out.println("Se necesita tener 2 Jugadores registrados.");
                System.out.println("Por favor ingrese al menu de registar un jugador");
            }      
            if(lista.tamañoListaJugadores() > 1 && opcion.equalsIgnoreCase("c")) { 
                System.out.println("Opcion c seleccionada");
                saleDelSegundoMenu(entrada, tablero, pantalla, lista);
            }
            if(opcion.equalsIgnoreCase("d")) { 
                System.out.println("Opcion d seleccionada");
                pantalla.actualRanking(tablero, lista);
                System.out.println(pantalla.getRanking());
            }
            if(opcion.equalsIgnoreCase("e")){
                System.out.println("Opcion e seleccionada");
            }
        }
    }

    public static String primerOpcion(Scanner entrada, Pantalla pantalla) {
        System.out.println(pantalla.primerMenu(entrada));
        System.out.println("Ingrese opcion deseada: ");
        String opcion = entrada.nextLine();
        System.out.println("");
        if (!opcion.equalsIgnoreCase("a") && !opcion.equalsIgnoreCase("b") && !opcion.equalsIgnoreCase("c") && 
            !opcion.equalsIgnoreCase("d") && !opcion.equalsIgnoreCase("e")) {
            System.out.println("Opcion no valida");
        }
        return opcion;
    }
    
    public static int segundaOpcion(Scanner entrada, Pantalla pantalla) { 
        System.out.println(pantalla.segundoMenu(entrada));
        System.out.println("Ingrese opcion deseada");
        System.out.println("");
        int opcion2;
        while (true) {
            try {
                opcion2 = Integer.parseInt(entrada.nextLine());
                break;
            } catch (NumberFormatException e) {
                System.out.println("Ingrese una opciona valida (numero entero):");
            }
        }
        if ( opcion2!=1 && opcion2!=2 && opcion2!=3) {
            System.out.println("Opcion no valida");
            primerOpcion(entrada, pantalla);
        }
        return opcion2;
    }

    public static void agregarJugador(Scanner entrada, Lista lista) { 
        String nombre = "";
        while (nombre.isEmpty()) {
            System.out.println("Ingrese su nombre (no puede estar vacio):");
            nombre = entrada.nextLine().trim();
        }
        System.out.println("Ingrese su edad");
        int edad;
        while (true) {
            try {
                edad = Integer.parseInt(entrada.nextLine());
                if (edad >= 0 && edad <= 130) {
                    break;
                } else {
                    System.out.println("La edad debe estar entre 0 y 130. Ingrese nuevamente:");
                }
            } catch (NumberFormatException e) {
                System.out.println("Ingrese una edad valida (numero entero):");
            }
        }
        String alias = "";
        while (alias.isEmpty()) {
           System.out.println("Ingrese su Alias (no puede estar vacio ni puede superar los 9 caracteres):");
           alias = entrada.nextLine().trim(); 
        }
        if(alias.length()>9){
            System.out.println("Alias no puede superar los 9 caracteres");
            agregarJugador(entrada, lista);
        }else{
            if (!lista.verificarAlias(alias)) {
                Jugador jugador = new Jugador(alias, lista, nombre, edad);
                lista.agregarJugador(jugador);
                lista.agregarAlias(jugador);
                System.out.println("Jugador agregado correctamente");
            } 
            else {
                System.out.println("Alias ya tomado por otro jugador");
                agregarJugador(entrada, lista);
            }
        }
    }

    public static void saleDelSegundoMenu(Scanner entrada, Tablero tablero, Pantalla pantalla, Lista lista) { 
        int opcion2 = segundaOpcion(entrada, pantalla);
        int tipo = 1;
        switch (opcion2) {
            //if(opcion2==1){
            case 1: // Al azae
                System.out.println("Opcion 1 seleccionada");
                int dimension = dimension(entrada);
                int cantAutos = cantAutos(entrada);
                tablero.tableroAzar(lista, cantAutos, dimension);
                mostrarTablero(tablero, pantalla);
                jugar(entrada, lista, tablero, pantalla, tipo);
                lista.eliminarListaAutos();
                lista.eliminarListaAutosJugables();
                lista.eliminarListaJugadoresActuales();
                break;
            //}if( opcion2==2){
            case 2: //SPropio
                tipo = 0;
                System.out.println("Opcion 2 seleccionada");
                if(tablero.getExiste()==true){
                    tablero.setDimension(tablero.getDimensionPropio());
                    mostrarTableroPropio(tablero, pantalla);
                    jugar(entrada, lista, tablero, pantalla, tipo);
                    lista.eliminarListaAutosPropio();
                    lista.eliminarListaAutosJugables();
                    lista.eliminarListaJugadoresActuales();

                }
                else{
                    System.out.println("Primero cree un tablero propio");
                }
                break;

            //}if(opcion2==3){
            case 3: // Predeterminado
                System.out.println("Opcion 3 seleccionada");
                tablero.tableroPredefinido(lista);
                mostrarTablero(tablero, pantalla);
                jugar(entrada, lista, tablero, pantalla, tipo);
                lista.eliminarListaAutos();
                lista.eliminarListaAutosJugables();
                lista.eliminarListaJugadoresActuales();
                break;
            //}
        }    
    }

    public static int cantAutos(Scanner entrada) { 
        System.out.println("Ingrese cantidad de autos deseada,");
        System.out.println("Debe ser entre 3 (inclusive) y 12 (inclusive)");
        int cant;
        while (true) {
            try {
                cant = Integer.parseInt(entrada.nextLine());
                break;
            } catch (NumberFormatException e) {
                System.out.println("Ingrese una cantidad de autos valida (numero entero):");
            }
        }
        if (cant < 3 || cant > 12) {
            System.out.println("Opcion no valida");
            cantAutos(entrada);
        }
        return cant;
    }
 
    public static int dimension(Scanner entrada) { 
        System.out.println("Ingrese la dimension deseada del tablero");
        System.out.println("Opciones validas: 5, 6, 7");
        int dimen;
        while (true) {
            try {
                dimen = Integer.parseInt(entrada.nextLine());
                if (dimen >= 5 && dimen <= 7) {
                    return dimen;
                } else {
                    System.out.println("Opcion no valida");
                }
            } catch (NumberFormatException e) {
                System.out.println("Ingrese una dimension valida (numero entero):");
            }
        }
    }
    
    public static void mostrarTablero(Tablero tablero, Pantalla pantalla){ 
        pantalla.actualTablero(tablero.getMatriz());
        System.out.println(pantalla.getTablero());
    }
    
    public static void mostrarTableroPropio(Tablero tablero, Pantalla pantalla){
        pantalla.actualTablero(tablero.getPropio());
        System.out.println(pantalla.getTablero());
    }

    public static void jugadoresActuales(Scanner entrada, Lista lista) {
        for (int g = 0; g < 2; g++) {
            System.out.println("Ingrese alias del Jugador " + (g + 1));
            String alias = entrada.nextLine();
            boolean jugadorEncontrado = false;

            for (int i = 0; i < lista.tamañoListaJugadores(); i++) {
                if (alias.equals(lista.getAliasDeLista(i))) {
                    jugadorEncontrado = true;

                    if (lista.jugadorEstaJugando(alias)) {
                        System.out.println("Este Alias pertenece a un jugador que esta jugando.");
                        System.out.println("Ingrese Alias valido para continuar.");
                        g--;
                        break;
                    }

                    if (lista.tamañoListaJugadoresActuales() < 2) {
                        lista.agregarJugadorActual(lista.getJugador(i));
                    } else {
                        System.out.println("Ya se han agregado dos jugadores.");
                    }

                    break;
                }
            }

            if (!jugadorEncontrado) {
                System.out.println("Este Alias no pertenece a ningun jugador.");
                System.out.println("Ingrese Alias valido para continuar.");
                g--;
            }
        }
    }

    public static boolean realizaJugada(Scanner entrada, Lista lista, Tablero tablero, boolean abandona, Pantalla pantalla, int jugador, int tipo) {
        System.out.println(pantalla.getTablero());
        int dimension = tablero.getDimension();
        System.out.println("Jugador: " + lista.getJugadorActual(jugador).getAlias() + ", eliga una de las siguientes opciones");
        System.out.println(pantalla.tercerMenu(entrada));
        String opcion = entrada.nextLine();
        if (opcion.equals("1")) {
            String[] letras = { "A", "B", "C", "D", "E", "F", "G" };
            System.out.println("Ingrese fila y columna del auto a mover");
            String auto = entrada.nextLine();
            if ((Integer.parseInt(Character.toString(auto.charAt(1)))) > dimension || !Arrays.asList(letras).contains(Character.toString(auto.charAt(0)))) {
                System.out.println("Una de los datos ingresados no es validos");
                realizaJugada(entrada, lista, tablero, abandona, pantalla, jugador, tipo);
            }
            tablero.moverAuto(auto, lista, tipo);
            if (tipo == 1) {
                pantalla.actualTablero(tablero.getMatriz());
            } else {
                pantalla.actualTablero(tablero.getPropio());
            }
        }
        if (opcion.equalsIgnoreCase("S")) {
            lista.getJugadorActual(jugador).aumentarAyudas();
            tablero.agregarAutosJugables(lista, tipo);
            System.out.println(pantalla.listaAyuda(lista));
            abandona = realizaJugada(entrada,lista,tablero,abandona, pantalla, jugador, tipo);
        }
        if (opcion.equalsIgnoreCase("X")) {
            abandona = true;
        }
        if (opcion.equalsIgnoreCase("R")) {
            tablero.girarTablero(lista, tipo);
            if (tipo == 1) {
                pantalla.actualTablero(tablero.getMatriz());
            } else {
                pantalla.actualTablero(tablero.getPropio());
            }
        }
        if (!opcion.equalsIgnoreCase("R") && !opcion.equalsIgnoreCase("S") && !opcion.equalsIgnoreCase("X") && !opcion.equalsIgnoreCase("1")) {
            System.out.println("Opcion no valida");
            realizaJugada(entrada, lista, tablero, false, pantalla, jugador, tipo);
        }
                    
        return abandona;
    }
 
    public static void jugar(Scanner entrada, Lista lista, Tablero tablero, Pantalla pantalla, int tipo){
        jugadoresActuales(entrada, lista);
        boolean booleano = true;
        int jugador = 0;
        boolean abandona = false;
        while(booleano && !abandona){
            abandona = realizaJugada(entrada, lista, tablero, abandona, pantalla, jugador, tipo);
            jugador = (jugador+1)%2;
            if(tipo==1){
                booleano = tablero.validarProxJugada(lista, tablero.getMatriz(), tipo);
            }else{
                booleano = tablero.validarProxJugada(lista, tablero.getPropio(), tipo);
            }
        }
        System.out.println(pantalla.getTablero());
        if(abandona){
            lista.getJugadorActual(jugador).aumentarGanadas();  
            lista.getJugadorActual(jugador).aumentarJugadas();                          
            lista.getJugadorActual(jugador).actualizarPuntaje();                          
            lista.getJugadorActual((jugador+1)%2).aumentarAbandonadas();
            lista.getJugadorActual((jugador+1)%2).aumentarJugadas();                          
            lista.getJugadorActual((jugador+1)%2).actualizarPuntaje();
            System.out.println("Gana: " + lista.getJugadorActual(jugador).getAlias());
            System.out.println("Abandona: " + lista.getJugadorActual((jugador+1)%2).getAlias());
        }else{
            lista.getJugadorActual(jugador).aumentarPerdidas();
            lista.getJugadorActual(jugador).aumentarJugadas();                          
            lista.getJugadorActual(jugador).actualizarPuntaje();                          
            lista.getJugadorActual((jugador+1)%2).aumentarGanadas();
            lista.getJugadorActual((jugador+1)%2).aumentarJugadas();                          
            lista.getJugadorActual((jugador+1)%2).actualizarPuntaje();       
            System.out.println("Gana: " + lista.getJugadorActual((jugador+1)%2).getAlias());
            System.out.println("Pierde: " + lista.getJugadorActual(jugador).getAlias());
        }
    }
    
    public static void crearTableroPropio(int cantAutos, Scanner entrada, Lista listaAutito, Tablero tablero){
        listaAutito.eliminarListaAutosPropio();
        String[] filas = { "A", "B", "C", "D", "E", "F", "G" };
        Auto[][] matrizT = new Auto[tablero.getDimension()][tablero.getDimension()];
        for (int i = 0; i < cantAutos; i++) {
            System.out.println("Ingrese ubicacion y direccion del auto.");
            System.out.println("Ingrese fila, columna y direccion.");
            System.out.println("La direccion puede ser 0: Arriba, 1: Derecha, 2: Abajo o 3: Izquierda. (Ejemplo: A12 corresponde a fila A, columna 1, direccion 2).");
            String linea = entrada.nextLine();            
            if (linea.length() != 3) {
                System.out.println("Formato incorrecto (Ejemplo: A32). Por favor, ingrese nuevamente.");
                i--; 
            } else {
                int indice = Arrays.asList(filas).indexOf(String.valueOf(Character.toUpperCase(linea.charAt(0))));
                int columna = Character.getNumericValue(linea.charAt(1)) - 1;
                int direccion = Character.getNumericValue(linea.charAt(2));
                if (indice < 0 || indice > tablero.getDimension() || columna < 0 || columna > tablero.getDimension() || direccion < 0 || direccion > 3) {
                    System.out.println("Uno de los datos ingresados esta incorrectamente ingresado.");
                    i--;
                } else {
                    Auto auto1 = new Auto(String.valueOf(Character.toUpperCase(linea.charAt(0))), columna, direccion);
                    boolean booleano = true;
                    for (Auto auto : listaAutito.getListaAutosPropio()) {
                        if (auto.getFila() == auto1.getFila() && auto.getCol() == auto1.getCol() && booleano == true) {
                            i--;
                            System.out.println("No puede ingresar un auto sobre la posicion de otro auto.");
                            booleano = false;
                        }
                    }
                    if (booleano) {
                        System.out.println("Auto agregado correctamente.");
                        listaAutito.agregarAutoPropio(auto1);
                        matrizT[auto1.getFila()][auto1.getCol()] = auto1;
                    }
                }
            }
        }
        tablero.actualizarMatriz(listaAutito, 0);

        boolean esValido = tablero.validarProxJugada(listaAutito, tablero.getPropio(), 0);

        if (!esValido) {
            System.out.println("El tablero propio ingresado no es valido. Por favor, ingrese un tablero valido.");
            crearTableroPropio(cantAutos, entrada, listaAutito, tablero);
        }
    }
    
}
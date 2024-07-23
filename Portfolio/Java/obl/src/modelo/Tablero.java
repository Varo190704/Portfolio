/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Class.java to edit this template
 */
package modelo;

/**
 *
 *  @author Alvaro Redondo Motta
 * 
 */
import java.util.*;

public class Tablero {
    private Random random = new Random();
    private Auto[][] matriz;
    private Auto[][] propio;
    private int dimension = 5;   
    private int dimensionPropio = 5;
    private boolean booleano = false;
    private int cantidadAutos = 3;
    private String[] filas = { "A", "B", "C", "D", "E", "F", "G" };
    private int[] cantidades = { 3, 4, 5, 6, 7, 8, 9, 10, 11, 12};


    public void setDimension(int dimension) {
        this.dimension = dimension;
    }

    public int getDimension() {
        return dimension;
    }

    public void setDimensionPropio(int dimension) {
        this.dimensionPropio = dimension;
    }

    public int getDimensionPropio(){
        return dimensionPropio;
    }

    public void setCantidadAutos(int cantidadAutos) {
        this.cantidadAutos = cantidadAutos;
    }

    public int getCantidadAutos() {
        return cantidadAutos;
    }

    public void setMatriz() {
        this.matriz = new Auto[getDimension()][getDimension()];
    }

    public void setMatriz(Auto[][] matriz) {
        this.matriz = matriz;
    }

    public Auto[][] getMatriz() {
        return this.matriz;
    }    
    
    public void setPropio(Auto[][] matriz) {
        this.propio = matriz;
        setExiste(true);
    }

    public Auto[][] getPropio() {
        return this.propio;
    }
    
    public void setExiste(boolean bool){
        this.booleano = bool;
    }
    
    public boolean getExiste(){
        return this.booleano;
    }
    
    public Tablero(int dimension, int cantidadAutos) {
        if (dimension >= 5 && dimension <= 7) {
            setDimension(dimension);
        } 
        if (cantidadAutos >= 3 && cantidadAutos <= 12) {
            setCantidadAutos(cantidadAutos);
        }
        setMatriz();
    }

    public Tablero() {
        setDimension(5);
        setCantidadAutos(3);
        setMatriz();
    }
    
    public void tableroAzar(Lista listaAutito, int cantAutos, int dimension) {
        Auto[][] matrizT = new Auto[dimension][dimension];
        setDimension(dimension);
        int autosCol = 0;
        while (autosCol < cantAutos) {
            int fil = random.nextInt(dimension);
            int col = random.nextInt(dimension);
            int dir = random.nextInt(4);
            Auto auto1 = new Auto(filas[fil], col, dir);
            if (matrizT[fil][col] == null) {
                boolean autoNoRepetido = true;
                for (Auto auto : listaAutito.getListaAutos()) {
                    if (auto.getFila() == auto1.getFila() && auto.getCol() == auto1.getCol()) {
                        autoNoRepetido = false;
                        break;
                    }
                }
                if (autoNoRepetido) {
                    listaAutito.agregarAuto(auto1);
                    matrizT[fil][col] = auto1;
                    autosCol++;
                }
            }
        }

        setMatriz(matrizT);

        boolean booleano = validarProxJugada(listaAutito, getMatriz(), 1);;

        if (!booleano) {
            tableroAzar(listaAutito, cantAutos, dimension);
        }
    }


    public void tableroPredefinido(Lista listaAutito) {
        Auto auto0 = new Auto("A", 0, 2);
        Auto auto1 = new Auto("A", 3, 2);
        Auto auto2 = new Auto("E", 3, 1);
        Auto auto3 = new Auto("E", 0,0);
        Auto auto4 = new Auto("D", 4, 2);
        Auto auto5 = new Auto("E", 4, 3);
        Auto auto6 = new Auto("A", 1, 3);
        Auto auto7 = new Auto("A", 4, 0);
        listaAutito.agregarAuto(auto5);
        listaAutito.agregarAuto(auto0);
        listaAutito.agregarAuto(auto1);
        listaAutito.agregarAuto(auto2);
        listaAutito.agregarAuto(auto3);
        listaAutito.agregarAuto(auto4);
        listaAutito.agregarAuto(auto6);
        listaAutito.agregarAuto(auto7);
        Auto[][] matrizN = new Auto[5][5];
        for (Auto auto : listaAutito.getListaAutos()) {
            matrizN[auto.getFila()][auto.getCol()]=auto;
        }
        setDimension(5);
        setMatriz(matrizN);
    }
    
    
    
    public void moverAuto(String filaCol, Lista lista, int tipo) {
        char filaChar = Character.toUpperCase(filaCol.charAt(0));
        int col = Integer.parseInt(Character.toString(filaCol.charAt(1)))-1;
        int fila = Arrays.asList(filas).indexOf(String.valueOf(filaChar));
        Auto auto = null;
        ArrayList<Auto> listaABuscar;       

        if(tipo==1){
            listaABuscar = lista.getListaAutos();
        }else{
            listaABuscar = lista.getListaAutosPropio();
        }
        for (Auto auto1 : listaABuscar) {
            if (auto1.getFila() == fila && auto1.getCol() == col) {
                auto = auto1;
                break;
            }
        }

        if (auto == null) {
            System.out.println("No se encontro ningun auto en las coordenadas especificadas.");
        }else{
            boolean encontrado = false;
            for (int i = 0; i < 4; i++) {
                auto.rotacion90();
                if(i<3){
                    Auto lugarACambiar = obtenerNuevaPosicion(auto, tipo);
                    if (lugarACambiar != null) {
                        int nFila = lugarACambiar.getFila();
                        int nCol = lugarACambiar.getCol();
                        encontrado = true;
                        if(tipo==1){
                            lista.eliminarAuto(lugarACambiar);
                        }else{
                            lista.eliminarAutoPropio(lugarACambiar);
                        }                    
                        auto.setFila(filas[lugarACambiar.getFila()]);
                        auto.setCol(lugarACambiar.getCol());
                        actualizarMatriz(lista, tipo);
                        System.out.println("El auto se ha movido exitosamente.");
                        break;
                    }
                }
            }

            if (!encontrado) {
                System.out.println("Este auto no tiene posible choque");
            }
        }
    }

    private Auto obtenerNuevaPosicion(Auto auto, int tipo) {
        int tamanno;
        Auto[][] matrizAux;
        if(tipo==1){
            tamanno = getDimension();
            setDimension(tamanno);
            matrizAux = getMatriz();
        }else{
            tamanno = getDimensionPropio();
            setDimension(tamanno);
            matrizAux = getPropio();
        }
        int fila = auto.getFila();
        int col = auto.getCol();
        int direccion = auto.getDireccion();
        
        Auto autoBorrar = null;
        switch (direccion) {
            case 0: // Arriba
                autoBorrar = autoVeArribaAu(fila, col, matrizAux);
                break;
            case 1: // Derecha
                autoBorrar = autoVeDerechaAu(fila, col, matrizAux);
                break;
            case 2: // Abajo
                autoBorrar = autoVeAbajoAu(fila, col, matrizAux);
                break;
            case 3: // Izquierda
                autoBorrar = autoVeIzquierdaAu(fila, col, matrizAux);
                break;
        }
        ;
        return autoBorrar;
    }

    public void agregarAutosJugables(Lista listaAutito, int tipo) {
        Auto[][] matrizAct;
        ArrayList<Auto> lista;
        int dimension;

        if (tipo == 1) {
            matrizAct = getMatriz();
            lista = listaAutito.getListaAutos();
            dimension = getDimension();
        } else {
            matrizAct = getPropio();
            lista = listaAutito.getListaAutosPropio();
            dimension = getDimensionPropio();
        }
        listaAutito.eliminarListaAutosJugables();

        for (int i = 0; i < dimension; i++) {
            for (int j = 0; j < dimension; j++) {
                if (matrizAct[i][j] != null) {
                    Auto auto = matrizAct[i][j];
                    boolean esJugable = validarAuto(listaAutito, matrizAct, tipo, auto);
                    if(esJugable){
                        listaAutito.agregarAutoJugable(auto);
                    }
                }
            }
        }
    }
    public boolean validarAuto(Lista listaAutito, Auto[][] matrizAct, int tipo, Auto auto){
        boolean booleano = false;
        if(tipo==1){
            booleano = unionDeDirecciones(booleano, listaAutito,auto, matrizAct, tipo);  
        }else{
            booleano = unionDeDirecciones(booleano, listaAutito,auto, matrizAct, tipo);
        }
        
        return booleano;
    }
    
    public Auto autoVeAbajoAu(int fila, int columna, Auto[][] matrizAux) {
        Auto auto = null;
        for (int i = getDimension()-1; i > fila; i--) {
            if (matrizAux[i][columna] != null) {
                auto = matrizAux[i][columna];
            }
        }
        return auto;
    }

    public Auto autoVeArribaAu(int fila, int columna, Auto[][] matrizAux) {
        Auto auto = null;
        for (int i = 0; i < fila; i++) {
            if (matrizAux[i][columna] != null) {
                auto = matrizAux[i][columna];
            }
        }
        return auto;
    }

    public Auto autoVeIzquierdaAu(int fila, int columna, Auto[][] matrizAux) {
        Auto auto = null;
        for (int j = 0; j < columna; j++) {
            if (matrizAux[fila][j] != null) {
                auto = matrizAux[fila][j];
            }
        }
        return auto;
    }

    public Auto autoVeDerechaAu(int fila, int columna, Auto[][] matrizAux) {
        Auto auto = null;
        for (int j = getDimension()-1; j > columna; j--) {
            if (matrizAux[fila][j] != null) {
                auto = matrizAux[fila][j];
            }
        }
        return auto;
    }

    public boolean validarProxJugada(Lista listaAutito, Auto[][] matrizAct, int tipo){
        boolean booleano = false;
        int tamanno;
        ArrayList<Auto> lista;
        if(tipo==1){
            tamanno = getDimension();
            lista = listaAutito.getListaAutos();
        }else{
            tamanno = getDimensionPropio();
            lista = listaAutito.getListaAutosPropio();
        }
        for (Auto auto : lista) {
            if(!booleano){
                booleano = unionDeDirecciones(booleano, listaAutito,auto, matrizAct, tamanno);
            }
        }    
        return booleano;
    }

    public boolean unionDeDirecciones(boolean booleano, Lista lista, Auto auto, Auto[][] matrizAct, int tipo){
        int fila = auto.getFila();
        int columna = auto.getCol();
        int tamanno;
        if(tipo==1){
            tamanno = getDimension();
        }else{
            tamanno = getDimensionPropio();
        }
        boolean booleano2 = false;
        if (auto.getDireccion() == 2) {
            booleano2 = autoVeAbajo(booleano2, fila, columna, matrizAct, tamanno);
        }
        if (auto.getDireccion() == 0) {
            booleano2 = autoVeArriba(booleano2, fila, columna,matrizAct, tamanno);
        }
        if (auto.getDireccion() == 3) {
            booleano2 = autoVeIzquierda(booleano2, fila, columna,matrizAct, tamanno);
        }
        if (auto.getDireccion() == 1) {
            booleano2 = autoVeDerecha(booleano2, fila, columna,matrizAct, tamanno);
        }
        return booleano2;
    }
    
    
    public boolean autoVeAbajo(boolean booleano, int fila, int columna,Auto[][] matrizAct, int tamanno) {
        booleano = fijarEnMismaFila(booleano, columna, fila,matrizAct, tamanno);
        for (int i = 0; i < fila && !booleano; i++) {
            if (matrizAct[i][columna] != null) {
                booleano = true;
            }
        }
        return booleano;
    }

    public boolean autoVeArriba(boolean booleano, int fila, int columna, Auto[][] matrizAct, int tamanno) {
        booleano = fijarEnMismaFila(booleano, columna, fila,matrizAct, tamanno);
        for (int i = tamanno - 1; i > fila && !booleano; i--) {
            if (matrizAct[i][columna] != null) {
                booleano = true;
            }
        }
        return booleano;
    }

    public boolean autoVeIzquierda(boolean booleano, int fila, int columna, Auto[][] matrizAct, int tamanno) {
        booleano = fijarEnMismaColumna(booleano, columna, fila,matrizAct, tamanno);
        for (int j = tamanno - 1; j > columna && !booleano; j--) {
            if (matrizAct[fila][j] != null) {
                booleano = true;
            }
        }
        return booleano;
    }

    public boolean autoVeDerecha(boolean booleano, int fila, int columna, Auto[][] matrizAct, int tamanno) {
        booleano = fijarEnMismaColumna(booleano, columna, fila,matrizAct, tamanno);
        for (int j = 0; j < columna && !booleano; j++) {
            if (matrizAct[fila][j] != null) {
                booleano = true;
            }
        }
        return booleano;
    }

    private boolean fijarEnMismaFila(boolean booleano, int columna, int fila, Auto[][] matrizAct, int tamanno) {
        for (int j = 0; j < tamanno && !booleano; j++) {
            if (matrizAct[fila][j] != null && j != columna) {
                booleano = true;
            }
        }
        return booleano;
    }

    private boolean fijarEnMismaColumna(boolean booleano, int columna, int fila, Auto[][] matrizAct, int tamanno) {
        
        for (int i = 0; i < tamanno && !booleano; i++) {
            if (matrizAct[i][columna] != null && i != fila) {
                booleano = true;
            }
        }
        return booleano;
    }

    public void girarTablero(Lista lista, int tipo){
        Auto[][] matrizSecu = new Auto[getDimension()][getDimension()];
        ArrayList<Auto> lista1;
        Auto[][] matrizTrab;
        if(tipo==1){
            lista1 = lista.getListaAutos();
            matrizTrab = getMatriz();
        }else{
            lista1 = lista.getListaAutosPropio();
            matrizTrab = getPropio();
        }
        
        for(Auto auto : lista1){
            auto.rotacion90();
        }
        int colActual = getDimension()-1;
        for(int j=0; j<getDimension(); j++){
            for(int i=0; i<getDimension();i++){
                if(matrizTrab[i][j]!=null){
                    matrizTrab[i][j].setFila(filas[j]);
                    matrizTrab[i][j].setCol(colActual-i);
                }
                matrizSecu[j][colActual-i] = matrizTrab[i][j];
            }
        }
        if(tipo==1){
            setMatriz(matrizSecu);
        }else{
            setPropio(matrizSecu);
        }
    }

    public void actuaAutosJugables(Lista lista, Auto[][] matrizAct, int tipo){
        ArrayList <Auto> lista1;
        if(tipo==1){
            lista1= lista.getListaAutos();
        }else{
            lista1= lista.getListaAutosPropio();
        }
        for (Auto auto : lista1) {
            boolean booleano = false;
            booleano = unionDeDirecciones(booleano, lista, auto, matrizAct, tipo);
            if(booleano){
                lista.agregarAutoJugable(auto);
            }
        }
    }

    public void actualizarMatriz(Lista lista, int tipo){
        ArrayList<Auto> autosM;
        Auto[][] auxiliar = new Auto[getDimension()][getDimension()];
        if(tipo==1){
            autosM = lista.getListaAutos();
        }else{
            autosM = lista.getListaAutosPropio();
        }
        for(Auto auto: autosM){
            auxiliar[auto.getFila()][auto.getCol()]=auto;
        }
        if(tipo ==1){
            setMatriz(auxiliar);
        }else{
            setPropio(auxiliar);
        }
    }
}
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

public class Auto {

    // https://gist-github-com.translate.goog/fnky/458719343aabd01cfb17a3a4f7296797?_x_tr_sl=en&_x_tr_tl=es&_x_tr_hl=es&_x_tr_pto=rq

    private String[] coloresAutos = { "\033[0;33m", "\033[0;34m", "\033[0;32m",
            "\033[0;31m",  "\033[0;35m" };
    private String[] letras = { "A", "B", "C",
            "D", "E", "F" };        
    Random random = new Random();
    private int fila;
    private int col;
    private int direccion;
    private String color;

    public void setColor() {
        int h = random.nextInt(5);
        this.color = coloresAutos[h];
    }

    public void setFila(String fila) {
        this.fila = Arrays.asList(letras).indexOf(fila);
    }

    public int getFila() {
        return this.fila;
    }

    public void setCol(int col) {
        this.col = col;
    }

    public int getCol() {
        return this.col;
    }

    public void setDireccion(int direccion) {
        this.direccion = direccion;
    }

    public int getDireccion() {
        return this.direccion;
    }

    public void setColor(String color) {
        this.color = color;
    }

    public String getColor() {
        return this.color;
    }

    public Auto(String fila, int col, int direccion) {
        setFila(fila);
        setCol(col);
        setDireccion(direccion);
        setColor();
    }

    public void rotacion90() {
        setDireccion(((getDireccion() + 1) % 4));
    }

    @Override
    public String toString(){
        String string = letras[getFila()] + (getCol()+1);
        return string;
    }

    public String primerLinea() {
        String mostrar = "";
        String color = getColor();
        String resetear = "\033[0m";
        String rojo = "\u001B[31m";
        String fondoCircu = "\u001B[43m";
        String ast = color + "*" + resetear;
        String circ =  fondoCircu + rojo + "o" + resetear;
        if (getDireccion() == 2) {
            mostrar = " " + ast + ast + " ";
        }

        if (getDireccion() == 0) {
            mostrar = " "+ circ + circ + " ";
        }

        if (getDireccion() == 1 || getDireccion() == 3) {
            mostrar = "    ";
        }

        return mostrar;
    }

    public String segundaLinea() {
        String mostrar = "";
        String color = getColor();
        String resetear = "\033[0m";
        String rojo = "\u001B[31m";
        String fondoCircu = "\u001B[43m";
        String ast = color + "*" + resetear;
        String circ =  fondoCircu + rojo + "o" + resetear;
        if (getDireccion() == 2 || getDireccion() == 0) {
            mostrar = " " + ast + ast + " ";
        }
        if (getDireccion() == 1) {
            mostrar = ast + ast + ast + circ;
        }

        if (getDireccion() == 3) {
            mostrar = circ + ast + ast + ast;
        }
        return mostrar;
    }

    public String cuartaLinea() {
        String mostrar = "";
        String color = getColor();
        String resetear = "\033[0m";
        String rojo = "\u001B[31m";
        String fondoCircu = "\u001B[43m";
        String ast = color + "*" + resetear;
        String circ =  rojo + fondoCircu + "o" + resetear;
        if (getDireccion() == 0) {
            mostrar = " " + ast + ast + " ";
        }

        if (getDireccion() == 2) {
            mostrar = " "+ circ + circ + " ";
        }

        if (getDireccion() == 1 || getDireccion() == 3) {
            mostrar = "    ";
        }

        return mostrar;
    }
}


/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Class.java to edit this template
 */
package modelo;

import java.util.*;
/**
 *
 *  @author Alvaro Redondo Motta
 * 
 */
public class Jugador {
    private String nombre;
    private int edad;
    private String alias;
    private int ayudas = 0;
    private int partidas = 0;
    private int ganadas = 0;
    private int perdidas = 0;
    private int abandonadas = 0;
    private int puntaje = 0;

    public String getNombre() {
        return nombre;
    }

    public void setNombre(String nombre, Lista lista, String alias) {
        if (!lista.verificarAlias(alias)) {
            this.nombre = nombre;
        }
    }

    public int getEdad() {
        return edad;
    }

    public void setEdad(int edad, Lista lista, String alias) {
        if (!lista.verificarAlias(alias)) {
            this.edad = edad;
        }
    }

    public String getAlias() {
        return alias;
    }

    public void setAlias(String alias, Lista lista) {
        if (!lista.verificarAlias(alias)) {
            this.alias = alias;
        }
    }

    public void setPartidas(int num){
        this.partidas = num;
    }

    public void setGanadas(int num){
        this.partidas = num;
    }

    public void setPerdidas(int num){
        this.partidas = num;
    }

    public void setAbandonadas(int num){
        this.partidas = num;
    }

    public void setPuntaje(int num){
        this.partidas = num;
    }

    public void setAyudas(int num){
        this.ayudas = num;
    }

    public int getPartidas() {
        return partidas;
    }

    public int getGanadas() {
        return ganadas;
    }

    public int getPerdidas() {
        return perdidas;
    }

    public int getAbandonadas() {
        return abandonadas;
    }

    public int getPuntaje() {
        return puntaje;
    }

    public int getAyuda(){
        return ayudas;
    }
    
    public void aumentarAyudas() {
        this.ayudas++;
    }

    public void aumentarJugadas() {
        this.partidas++;
    }

    public void aumentarGanadas() {
        this.ganadas++;
    }

    public void aumentarPerdidas() {
        this.perdidas++;
    }

    public void aumentarAbandonadas() {
        this.abandonadas++;
    }

    public void actualizarPuntaje() {
        puntaje = getGanadas() * 10 - (getPerdidas() * 2 + getAbandonadas() * 5 + getAyuda());
    }

    public Jugador(String alias, Lista lista, String nombre, int edad) {
        setAlias(alias, lista);
        setEdad(edad, lista, alias);
        setNombre(nombre, lista, alias);
    }

    @Override
    public String toString() {
        return String.format("| %-10s | %-8d | %-8d | %-8d | %-11d | %-7d |",
                getAlias(), getPartidas(), getGanadas(), getPerdidas(), getAbandonadas(), getPuntaje());
    }
    
    public static class ComparadorsPuntajeAlias implements Comparator<Jugador> {
        @Override
        public int compare(Jugador jugador1, Jugador jugador2) {
            int retorna = 0;
            int comparacionPorPuntaje = Integer.compare(jugador2.getPuntaje(), jugador1.getPuntaje());

            if (comparacionPorPuntaje != 0) {
                retorna = comparacionPorPuntaje;
            } else {
                retorna = jugador1.getAlias().compareTo(jugador2.getAlias());
            }
            return retorna;
        }
    }

}


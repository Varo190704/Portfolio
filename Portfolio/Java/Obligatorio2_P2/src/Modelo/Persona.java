/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Class.java to edit this template
 */
package Modelo;

import java.io.Serializable;

/**
 *
 * @author varo1
 */
public class Persona implements Serializable{
    private static final long serialVersionUID = 1L;
    protected String nombre; //no pueden tener el mismo nombre
    protected int ci;
    protected String direction;

    public Persona(){
        setCi(0);
        setNombre("");
        setDirection("");
    }
    
    public Persona(String nombre, String direccion, int ci){
        setCi(ci);
        setNombre(nombre);
        setDirection(direccion);
    }
    
    public String getNombre() {
        return nombre;
    }

    public void setNombre(String nombre) {
        this.nombre = nombre;
    }

    public int getCi() {
        return ci;
    }

    public void setCi(int ci) {
        this.ci = ci;
    }

    public String getDirection() {
        return direction;
    }

    public void setDirection(String direction) {
        this.direction = direction;
    }
    
    @Override
    public String toString(){
        return this.nombre + " (" + this.ci + ")";
    }
}

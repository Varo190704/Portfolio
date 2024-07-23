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
public class Propietario extends Persona implements Serializable{
    private static final long serialVersionUID = 1L;
    private String numCelu;

    public Propietario(){
        super();
        this.numCelu = "";
    }
    
    public Propietario(String name, int ci, String direction, String celu){
        super(name, direction, ci);
        this.numCelu = celu;
    }
    
    public String getNumCelu() {
        return numCelu;
    }

    public void setNumCelu(String numCelu) {
        this.numCelu = numCelu;
    }

    public String datos(){
        return this.nombre + " " +this.ci + " " + this.direction + " " + this.numCelu;
    }
}

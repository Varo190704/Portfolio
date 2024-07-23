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
public class Capataz extends Persona implements Serializable{
    private static final long serialVersionUID = 1L;
    private int anno;
    
    public Capataz(){
        super();
        setAnno(0);
    }
    
    public Capataz(String nombre, int ci, String direccion, int anno){
        super(nombre, direccion, ci);
        setAnno(anno);
    }
    
    public int getAnno() {
        return anno;
    }

    public void setAnno(int anno) {
        this.anno = anno;
    }
     
    public String datos(){
        return this.nombre + " " +this.ci + " " + this.direction + " " + this.anno;
    }
}

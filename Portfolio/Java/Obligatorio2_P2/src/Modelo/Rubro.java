/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Class.java to edit this template
 */
package Modelo;

import java.io.Serializable;
import java.util.Observable;
import java.math.BigDecimal;
import java.util.ArrayList;

/**
 *
 * @author varo1
 */
public class Rubro extends Observable implements Serializable{
    private static final long serialVersionUID = 1L;
    private String nombre;
    private String description;
    private BigDecimal presupuesto;
    private ArrayList<Gasto> listaGastos;
    private ArrayList<Gasto> listaImpagos;

    public ArrayList<Gasto> getListaImpagos() {
        return listaImpagos;
    }

    public void setListaImpagos(ArrayList<Gasto> listaImpagos) {
        this.listaImpagos = listaImpagos;
        setChanged();
        notifyObservers("gasto");
    }

    public Rubro(){
        this.nombre = "";
        this.description = "";
        this.presupuesto = BigDecimal.ZERO;
        this.listaGastos = new ArrayList();
        this.listaImpagos = new ArrayList();
    }
    
    public ArrayList<Gasto> getListaGastos() {
        return listaGastos;
    }

    public void setListaGastos(ArrayList<Gasto> listaGastos) {
        this.listaGastos = listaGastos;
        setChanged();
        notifyObservers("gasto");
    }
    
    public void agregarGastos(Gasto gasto){
        this.listaGastos.add(gasto);
        this.listaImpagos.add(gasto);
        setChanged();
        notifyObservers("gasto");
    }
    
    public BigDecimal getPresupuesto() {
        return presupuesto;
    }

    public void setPresupuesto(BigDecimal presupuesto) {
        this.presupuesto = presupuesto;
        
    }
    
    public Rubro(String nombre1, String description1){
        setNombre(nombre1);
        setDescription(description1);
        setPresupuesto(BigDecimal.ZERO);
        this.listaGastos = new ArrayList();
        this.listaImpagos = new ArrayList();
    }
    
    public String getNombre() {
        return nombre;
    }

    public void setNombre(String nombre) {
        this.nombre = nombre;
    }

    public String getDescription() {
        return description;
    }

    public void setDescription(String description) {
        this.description = description;
        
    }
    
    public String aString(){
        return this.nombre + " -- " + (this.getGastos().subtract(this.presupuesto));
    }
    
    public BigDecimal getGastos() {
        BigDecimal dev = BigDecimal.ZERO;
        for(Gasto g : this.getListaGastos()){
            dev = dev.add(g.getMonto());
        }
        return dev;
    }
    
    public BigDecimal getImpago(){
        BigDecimal dev = BigDecimal.ZERO;
        for(Gasto g : this.getListaImpagos()){
            dev = dev.add(g.getMonto());
        }
        return dev;
    }

    @Override
    public String toString(){
        return this.nombre + " -- " + this.description;
    }
    
    public String boton(){
        return this.nombre + " -- " + this.presupuesto;
    }
    
}

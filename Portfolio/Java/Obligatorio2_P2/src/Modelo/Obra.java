/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Class.java to edit this template
 */
package Modelo;

import java.io.Serializable;
import java.math.BigDecimal;
import java.util.*;

/**
 *
 * @author varo1
 */
public class Obra implements Serializable{
    private static final long serialVersionUID = 1L;
    private Propietario owner;
    private Capataz foreman;
    private ArrayList<Rubro> rubros;
    private int numP;
    private String direction;
    private int mes;
    private int anno;
    private BigDecimal presupuesto;    
    private BigDecimal gastado; 
    private BigDecimal reintegrado; 
    private BigDecimal saldo;    

    public Obra(){
        this.anno = 0;
        this.direction = "";
        this.foreman = new Capataz();
        this.gastado = BigDecimal.ZERO;
        this.mes = 0;
        this.numP=0;
        this.owner = new Propietario();
        this.presupuesto = BigDecimal.ZERO;
        this.reintegrado = BigDecimal.ZERO;
        this.rubros = new ArrayList();
        this.saldo = BigDecimal.ZERO;
    }
    
    public Obra(int m, int a, String d, int n, Capataz c, Propietario ow, BigDecimal presu){
        this.mes = m;
        this.anno = a;
        this.direction = d;
        this.numP= n;
        this.foreman = c;
        this.owner = ow;
        this.presupuesto = presu;
    }
    
    public ArrayList<Rubro> getRubros() {
        return rubros;
    }

    public void setRubros(ArrayList<Rubro> rubros) {
        this.rubros = rubros;
    }
    
    public void agregarRubros(Rubro rubro) {
        getRubros().add(rubro);
    }

    public BigDecimal getGastado() {
        return gastado;
    }

    public void setGastado(BigDecimal gastado) {
        this.gastado = gastado;
    }

    public BigDecimal getReintegrado() {
        return reintegrado;
    }

    public void setReintegrado(BigDecimal reintegrado) {
        this.reintegrado = reintegrado;
    }

    public BigDecimal getSaldo() {
        return saldo;
    }

    public void setSaldo(BigDecimal saldo) {
        this.saldo = saldo;
    }


    public Propietario getOwner() {
        return owner;
    }

    public void setOwner(Propietario owner) {
        this.owner = owner;
    }

    public Capataz getForeman() {
        return foreman;
    }

    public void setForeman(Capataz foreman) {
        this.foreman = foreman;
    }

    public int getNumP() {
        return numP;
    }

    public void setNumP(int numP) {
        this.numP = numP;
    }

    public String getDirection() {
        return direction;
    }

    public void setDirection(String direction) {
        this.direction = direction;
    }

    public int getMes() {
        return mes;
    }

    public void setMes(int mes) {
        this.mes = mes;
    }

    public int getAnno() {
        return anno;
    }

    public void setAnno(int anno) {
        this.anno = anno;
    }

    public BigDecimal getPresupuesto() {
        return presupuesto;
    }

    public void setPresupuesto(BigDecimal presupuesto) {
        this.presupuesto = presupuesto;
    }
    
    @Override
    public String toString(){
        return this.numP + " -- " + this.direction;
    }
    
    public Rubro getRubro(int i){
        return this.getRubros().get(i);
    }
    
    public Rubro getRubro(String s){
        Rubro r = null;
        for(Rubro g : this.getRubros()){
            if(s.equals(g.getNombre()))
                r=g;
        }
        return r;
    }
    
    public BigDecimal getGasto(){
        BigDecimal dev = BigDecimal.ZERO;
        for(Rubro g : this.getRubros()){
            dev = dev.add(g.getGastos());
        }
        return dev;
    }
    
    public BigDecimal getImpago(){
        BigDecimal dev = BigDecimal.ZERO;
        for(Rubro g : this.getRubros()){
            dev = dev.add(g.getImpago());
        }
        return dev;
    }
}

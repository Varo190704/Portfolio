/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Class.java to edit this template
 */
package Modelo;

import java.io.Serializable;
import java.util.Observable;
import java.util.ArrayList;
import java.util.Collections;
import java.util.Comparator;

/**
 *
 * @author varo1.
 */
public class Sistema extends Observable implements Serializable{
    private static final long serialVersionUID = 1L;
    private ArrayList<Rubro> rubros;
    private ArrayList<Propietario> propietario;
    private ArrayList<Capataz> capataz;    
    private ArrayList<Obra> obra;
    /*para que sea mas rapido y mas cortas las sentencias para validar si 
    los objetos estan ya utilizados, voy a crear una lista para cada objeto
    a validar*/
    private ArrayList<Integer> cedulas;
    private ArrayList<Integer> numVali;
    
    public Capataz getCapatazCI(int ci){
        Capataz dev = null;
        for(Capataz e : this.getListaCapataz()){
            if(e.getCi()==ci){
                dev = e;
            }
        }
        return dev;
    }
    
    public boolean verifEsta(String n){
        boolean bool = false;
        for(Rubro e : this.getListaRubros()){
            if(e.getNombre().equals(n)){
                bool = true;
            }
        }   
        return bool;
    }
    
    public Propietario getPropietarioCI(int ci){
        Propietario dev = null;
        for(Propietario e : this.getListaPropietario()){
            if(e.getCi()==ci){
                dev = e;
            }
        }
        return dev;
    }
    
    public void ordenarCiP(ArrayList<Propietario> ow){
        Collections.sort(ow, new Comparator<Propietario>(){
            @Override
            public int compare(Propietario ow1, Propietario ow2){
                return Integer.compare(ow1.getCi(), ow2.getCi());
            }
        });
    }
    
    public void ordenarNbP(ArrayList<Propietario> ow){
        Collections.sort(ow, new Comparator<Propietario>(){
            @Override
            public int compare(Propietario ow1, Propietario ow2){
                return ow1.getNombre().compareTo(ow2.getNombre());
            }
        });
    }
    
    public void ordenarCiC(ArrayList<Capataz> fo){
        Collections.sort(fo, new Comparator<Capataz>(){
            @Override
            public int compare(Capataz fo1, Capataz fo2){
                return Integer.compare(fo1.getCi(), fo2.getCi());
            }
        });
    }
    
    public void ordenarNbC(ArrayList<Capataz> fo){
        Collections.sort(fo, new Comparator<Capataz>(){
            @Override
            public int compare(Capataz fo1, Capataz fo2){
                return fo1.getNombre().compareTo(fo2.getNombre());
            }
        });
    }
    
    public ArrayList<Integer> getListNumVali() {
        return numVali;
    }

    public void setListNumVali(ArrayList<Integer> numVali) {
        this.numVali = numVali;
    }

    public ArrayList<Integer> getListaCedulas() {
        return cedulas;
    }

    public void setListaCedulas(ArrayList<Integer> cedulas) {
        this.cedulas = cedulas;
    }
    
    public Sistema(){
        this.rubros = new ArrayList();
        this.propietario = new ArrayList();
        this.capataz = new ArrayList();
        this.obra = new ArrayList();
        this.cedulas = new ArrayList();
        this.numVali = new ArrayList();    
    }
    
    public ArrayList<Capataz> getListaCapataz() {
        return capataz;
    }

    public void setListaCapataz(ArrayList<Capataz> capataz) {
        this.capataz = capataz;
        setChanged();
        notifyObservers("capataces");
    }
    
    public ArrayList<Rubro> getListaRubros() {
        return rubros;
    }

    public void setListaRubros(ArrayList<Rubro> rubros2) {
        this.rubros = rubros2;
        setChanged();
        notifyObservers("rubros");
    }
    
    public ArrayList<Propietario> getListaPropietario(){
        return propietario;
    }
    
    public void setListaPropietario(ArrayList<Propietario> propietario2){
        this.propietario = propietario2;
        setChanged();
        notifyObservers("propietarios");
    }
          
    public void agregarListaPropietario(Propietario e){
        this.getListaPropietario().add(e);
        setChanged();
        notifyObservers("propietarios");
    }
    
    public Propietario getPropietario(int i){
        return this.getListaPropietario().get(i);
    }
    
    public void presuCambiado(){
        setChanged();
        notifyObservers("presu");
    }
    
    public void agregarListaCapataz(Capataz e){
        this.getListaCapataz().add(e);
        setChanged();
        notifyObservers("capataces");
    }
    
    public Capataz getCapataz(int i){
        return this.getListaCapataz().get(i);
    }
  
    public void agregarListaRubro(Rubro rubros1){
        this.getListaRubros().add(rubros1);
        setChanged();
        notifyObservers("rubros");
    }
    
    public ArrayList<Obra> getListaObra() {
        return obra;
    }

    public void setListaObra(ArrayList<Obra> obra) {
        this.obra = obra;
        setChanged();
        notifyObservers("obras");
    }
    
    public void agregarListaObra(Obra obra1){
        this.getListaObra().add(obra1);
        setChanged();
        notifyObservers("obras");
    }
    
    public void rubrosPred(){
        Rubro Pintura = new Rubro("Pintura", "Renovación de Pintura");
        Rubro Sanitaria = new Rubro("Sanitaria", "Reparación de Sanitaria");
        Rubro Electrica = new Rubro("Eléctrica", "Instalación Eléctrica");
        Rubro Carpinteria = new Rubro("Carpintería", "Trabajo en Madera");
        Rubro Albannileria = new Rubro("Albañilería", "Reparación de Paredes");
        Rubro Pisos = new Rubro("Pisos", "Instalación de pisos");
        Rubro CamVentanas = new Rubro("Cambio de Ventanas", "Reemplazo de Ventanas");
        Rubro Banno = new Rubro("Baño", "Remodelación de Baño");
        Rubro Cocina = new Rubro("Cocina", "Renovación de Cocina");
        Rubro AislaTermi = new Rubro("Aislamiento Térmico", "Mejora Térmica");
        agregarListaRubro(Pintura);       
        agregarListaRubro(Sanitaria);
        agregarListaRubro(Electrica);
        agregarListaRubro(Carpinteria);
        agregarListaRubro(Albannileria);
        agregarListaRubro(Pisos);
        agregarListaRubro(CamVentanas);
        agregarListaRubro(Banno);
        agregarListaRubro(Cocina);
        agregarListaRubro(AislaTermi);
    }
    
    public Obra getObraPorNombre(String nombre) {
        Obra obra= null;
        for (Obra g : this.getListaObra()) {
            if (g.toString().equals(nombre)) {
                obra=g;
            }
        }
        return obra;
    }
    
    public void setRubrosEnObra(Obra obra){
        ArrayList<Rubro> lista1 = new ArrayList();
        for(Rubro r : this.getListaRubros()){
            Rubro rN = new Rubro(r.getNombre(), r.getDescription());
            rN.setPresupuesto(r.getPresupuesto());
            lista1.add(rN);
        }
        obra.setRubros(lista1);
        setChanged();
        notifyObservers("obras");
    }
}

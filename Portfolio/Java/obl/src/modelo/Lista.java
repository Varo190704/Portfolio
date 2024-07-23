package modelo;

import java.util.*;
 /*
 *  @author Alvaro Redondo Motta
 */
public class Lista {
    private ArrayList<Jugador> listaJugadores = new ArrayList<>();
    private ArrayList<Auto> listaAutos = new ArrayList<>();
    private ArrayList<String> listaAlias = new ArrayList<>();
    private ArrayList<Jugador> listaJugadoresActuales = new ArrayList<>();
    private ArrayList<Auto> listaAutosJugables = new ArrayList<>();    
    private ArrayList<Auto> listaAutosPropio = new ArrayList<>();

    
    public boolean verificarAlias(String alias) {
        boolean booleano = false;
        if (listaAlias.contains(alias)) {
            booleano = true;
        }
        return booleano;
    }

    public void agregarJugadorActual(Jugador jugador){
        this.getListaJugadoreActuales().add(jugador);
    }

    public void agregarJugador(Jugador jugador) {
        getListaJugador().add(jugador);
    }

    public void agregarAutoJugable(Auto autito) {
        this.getListaAutosJugables().add(autito);
    }

    public void agregarAuto(Auto autito) {
        this.getListaAutos().add(autito);
    }

    public void agregarAutoPropio(Auto autito) {
        this.getListaAutosPropio().add(autito);
    }
    
    public void agregarAlias(Jugador jugador) {
        this.getListaAlias().add(jugador.getAlias());
    }  
    
    public int tamañoListaAutos() {
        return this.getListaAutos().size();
    }
    
    public int tamañoListaAutosPropio() {
        return this.getListaAutosPropio().size();
    }

    public int tamañoListaAutosJugables() {
        return this.getListaAutosJugables().size();
    }

    public int tamañoListaAlias() {
        return this.getListaAlias().size();
    }

    public int tamañoListaJugadores() {
        return this.getListaJugador().size();
    }

    public int tamañoListaJugadoresActuales(){
        return this.getListaJugadoreActuales().size();
    }

    public void eliminarAuto(Auto autito) {
        this.getListaAutos().remove(autito);
    }
    
    public void eliminarAutoPropio(Auto autito) {
        this.getListaAutosPropio().remove(autito);
    }

    public void eliminarAutoJugable(Auto autito) {
        this.getListaAutosJugables().remove(autito);
    }

    public void eliminarJugadoresActuales(Jugador jugador){
        this.getListaJugadoreActuales().remove(jugador);
    }

    public ArrayList<Auto> getListaAutos() {
        return listaAutos;
    }
    
    public ArrayList<Auto> getListaAutosPropio() {
        return listaAutosPropio;
    }

    public ArrayList<Auto> getListaAutosJugables() {
        return listaAutosJugables;
    }

    public ArrayList<Jugador> getListaJugador() {
        return listaJugadores;
    }

    public ArrayList<Jugador> getListaJugadoreActuales() {
        return listaJugadoresActuales;
    }

    public ArrayList<String> getListaAlias() {
        return listaAlias;
    }

    public String getAliasDeLista(int i) {
        return this.getListaJugador().get(i).getAlias();
    }

    public Auto getAuto(int i) {
        return this.getListaAutos().get(i);
    }
    
    public Auto getAutoPropio(int i) {
        return this.getListaAutosPropio().get(i);
    }


    public Auto getAutoJugable(int i) {
        return this.getListaAutosJugables().get(i);
    }

    public Jugador getJugadorActual(int i) {
        return this.getListaJugadoreActuales().get(i);
    }

    public Jugador getJugador(int i) {
        return this.getListaJugador().get(i);
    }
    
    public void eliminarListaAutos(){
        this.getListaAutos().clear();
    }
    
    public void eliminarListaAutosPropio(){
        this.getListaAutosPropio().clear();
    }
    
    public void eliminarListaAutosJugables(){
        this.getListaAutosJugables().clear();
    }
    
    public void eliminarListaJugadoresActuales(){
        this.getListaJugadoreActuales().clear();
    }
    
    public boolean jugadorEstaJugando(String alias) {
        boolean booleano = false;
        for (Jugador jugador : getListaJugadoreActuales()) {
            if (jugador.getAlias().equals(alias)) {
                booleano = true;
            }
        }
        return booleano;
    }
    
}
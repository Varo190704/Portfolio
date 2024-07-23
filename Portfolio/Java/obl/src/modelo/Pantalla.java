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

public class Pantalla {
    private String tablero = "";
    private String ranking = "";

    public void setTablero(String tablero) {
        this.tablero = tablero;
    }

    public String getTablero() {
        return this.tablero;
    }

    public void actualTablero(Auto[][] tablero) {
        Auto[][] tableroMatri = tablero;
        String tableroMostr = "";
        String[] abecedario = { "A", "B", "C", "D", "E", "F", "G" };
        int tamanno = tableroMatri.length;
        if (tamanno == 5) {
            tableroMostr = "   1    2    3    4    5    " + "\n" +
                    "  +----+----+----+----+----+" + "\n";
        }
        if (tamanno == 6) {
            tableroMostr = "   1    2    3    4    5    6    " + "\n" +
                    "  +----+----+----+----+----+----+" + "\n";
        }
        if (tamanno == 7) {
            tableroMostr = "   1    2    3    4    5    6    7    " + "\n" +
                    "  +----+----+----+----+----+----+----+" + "\n";
        }

        for (int i = 0; i < tamanno; i++) {
            for (int h = 0; h < 4; h++) {
                if(h==0){
                    tableroMostr += abecedario[i] + " |";
                }else{
                    tableroMostr += "  |";
                }
                for (int j = 0; j < tamanno; j++) {
                    Auto auto = tableroMatri[i][j];
                    if(auto!=null){
                        if (h == 0 && auto != null) {
                            tableroMostr += auto.primerLinea();
                        }
                        if (h == 1 || h == 2 && auto != null) {
                            tableroMostr += auto.segundaLinea();
                        }
                        if (h == 3 && auto != null) {
                            tableroMostr += auto.cuartaLinea();
                        }    
                    }else{
                        tableroMostr += "    ";
                    }
                    tableroMostr += "|";
                }
                tableroMostr += "\n";
            }
            if (tamanno == 5) {
                tableroMostr += "  +----+----+----+----+----+" + "\n";
            }
            if (tamanno == 6) {
                tableroMostr += "  +----+----+----+----+----+----+" + "\n";
            }
            if (tamanno == 7) {
                tableroMostr += "  +----+----+----+----+----+----+----+" + "\n";
            }
        }
        setTablero(tableroMostr);
    }

    public void setRanking(String ranking) {
        this.ranking = ranking;
    }

    public String getRanking() {
        return this.ranking;
    }

    public void actualRanking(Tablero ranking, Lista listaJugadores) {
        String rMostra = "----------------------------------------------------------------------" + "\n"
                + "                               RANKING                                " + "\n"
                + "----------------------------------------------------------------------" + "\n"
                + "| ALIAS      | PARTIDAS | GANADAS  | PERDIDAS | ABANDONADAS | PUNTAJE |" + "\n"
                + "----------------------------------------------------------------------" + "\n";

        Collections.sort(listaJugadores.getListaJugador(), new Jugador.ComparadorsPuntajeAlias());

        for (int i = 0; i < listaJugadores.tamañoListaJugadores(); i++) {
            rMostra += listaJugadores.getJugador(i).toString() + "\n";
        }
        rMostra += "----------------------------------------------------------------------";
        
        if(listaJugadores.tamañoListaJugadores()==0){
            rMostra="Se necesita minimamente un jugador";
        }
        
        setRanking(rMostra);
    }


    public String segundoMenu(Scanner entrada) {
        String menu1 = "------------------------------------" + "\n"
                + "              OPCIONES              " + "\n"
                + "------------------------------------" + "\n"
                + "|          1- Tablero Azar          |" + "\n"
                + "------------------------------------" + "\n"
                + "|         2- Tablero propio         |" + "\n"
                + "------------------------------------" + "\n"
                + "|       3- Tablero predefinido      |" + "\n"
                + "------------------------------------" + "\n"
                + "|      4- Salir a primer menu       |" + "\n"
                + "------------------------------------";
        return menu1;
    }

    public String primerMenu(Scanner entrada) {
        String menu1 = "------------------------------------" + "\n"
                + "              OPCIONES              " + "\n"
                + "------------------------------------" + "\n"
                + "|        a) Agregar Jugador        |" + "\n"
                + "------------------------------------" + "\n"
                + "|   b) Configurar tablero propio   |" + "\n"
                + "------------------------------------" + "\n"
                + "|             c) Jugar             |" + "\n"
                + "------------------------------------" + "\n"
                + "|           d)- Ranking            |" + "\n"
                + "------------------------------------" + "\n"
                + "|             e)- Fin              |" + "\n"
                + "------------------------------------";
        return menu1;

    }
    
    public String tercerMenu(Scanner entrada) {
        String menu1 = "-----------------------------------" + "\n"
                + "             OPCIONES              " + "\n"
                + "-----------------------------------" + "\n"
                + "|  1- Seleccionar Auto            |" + "\n"
                + "----------------------------------" + "\n"
                + "| (S)- Mostrar lista de autos     |" + "\n" 
                + "|      con posibles movimientos   |" + "\n"
                + "-----------------------------------" + "\n"
                + "| (X)- Rendirse                   |" + "\n"
                + "-----------------------------------" + "\n"
                + "| (R)- Rotar tablero              |" + "\n"
                + "-----------------------------------";

        return menu1;

    }

    public String listaAyuda(Lista lista){
        String rMostra = "-------------------------------" + "\n"
                    + "|   Lista De Autos Jugables   |" + "\n"
                    + "-------------------------------" + "\n";
        for (int i = 0; i < lista.tamañoListaAutosJugables(); i++) {
            rMostra += "|              " + lista.getAutoJugable(i).toString() + "              |" + "\n";
        }
        rMostra += "-------------------------------";
        return rMostra;
    }
}

/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Main.java to edit this template
 */
package Main;

import modelo.*;
import java.util.*;

public class Main {

    /**
     * @param args the command line arguments
     */
   
    public static void main(String[] args) {
        Lista lista = new Lista();
        Tablero tablero = new Tablero();
        Pantalla pantalla = new Pantalla();
        Obligatorio obligatorio = new Obligatorio();
        Scanner entrada = new Scanner(System.in);
        String opcion = "";
        obligatorio.saleDelMenu(opcion, entrada, tablero, pantalla, lista);
        System.out.println("Salio del programa");
    }
}

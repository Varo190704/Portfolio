/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Main.java to edit this template
 */
package clase;

import clases;

public class Clase {

    public static void main(String[] args) {
        Juego j2 = new Juego("Tetris", 20, 5);
        Fabricante ortLab = new Fabricante("Ort Laboratorio", "UY");
        j2.setFabricante(ortLab)
        System.out.println(j2);
    }
    
}

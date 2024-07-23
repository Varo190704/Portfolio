/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Class.java to edit this template
 */
package Modelo;

import java.io.IOException;
import java.nio.file.Paths;
import java.util.Scanner;



public class ArchivoLectura {
    private Scanner in;
    private String linea;
    
    public ArchivoLectura(String unNombre){
        try{
            in = new Scanner(Paths.get(unNombre));
        }catch (IOException e){
            System.err.println("Error");
            System.exit(1);
        }
    }
    
    public boolean hayMasLineas(){
        boolean hay = false;
        this.linea = null;
        if(this.in.hasNext()){
            this.linea=in.nextLine();
            hay = true;
        }
        return hay;
    }
    
    public String linea(){
        return this.linea;
    }
    
    public void cerrar(){
        this.in.close();
    }
}

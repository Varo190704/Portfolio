/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Class.java to edit this template
 */
package Modelo;

import java.io.FileNotFoundException;
import java.io.FileWriter;
import java.io.IOException;
import java.util.Formatter;

/**
 *
 * @author varo1
 */
public class ArchivoEscritura {
    private Formatter out1;
    
    public ArchivoEscritura(String unNombre, boolean ext) throws IOException{
        /*
        true= extiende
        false= sobreescribe
        */
        try{
            FileWriter f = new FileWriter(unNombre, ext);
            this.out1 = new Formatter(f);
        }catch(FileNotFoundException e){
            System.out.println("No se puede crear/extender");
            System.exit(1);
        }
    }
    
    public void grabarLinea(String linea){
        this.out1.format("%s%n", linea);
    }
    
    public void cerrar(){
        this.out1.close();
    }
}

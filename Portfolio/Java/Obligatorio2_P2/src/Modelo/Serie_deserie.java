/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Class.java to edit this template
 */
package Modelo;

import java.io.FileInputStream;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.ObjectInputStream;
import java.io.ObjectOutputStream;

/**
 *
 * @author varo1
 */
public class Serie_deserie {
    public void serie(Sistema sistema) {
        try {
            FileOutputStream fileOut = new FileOutputStream("Sistema.ser");
            ObjectOutputStream out = new ObjectOutputStream(fileOut);
            out.writeObject(sistema);
            out.close();
            fileOut.close();
        } catch (IOException i) {
            //nada
        }
    }

    // Método para deserializar el objeto Sistema
    public Sistema deserie() {
        Sistema sistema = null;
        try {
            FileInputStream fileIn = new FileInputStream("Sistema.ser");
            ObjectInputStream in = new ObjectInputStream(fileIn);
            sistema = (Sistema) in.readObject();
            in.close();
            fileIn.close();
        } catch (IOException i) {
            //nada
        } catch (ClassNotFoundException ex) {
            //nada
        }
        return sistema;
    }
}

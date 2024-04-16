/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Main.java to edit this template
 */
package gestorcontactos;

import Clases

public class GestorContactos {
    public static void main(String[] args) {
        // Crear ListaContactos
        ListaContactos listaContactos = new ListaContactos();
        // Crear contacto
        Contacto nuevoContacto = new Contacto("Calle Principal", "Juan", 123456789, 30);
        // Agregar a lista de contactos
        listaContactos.agregarContacto(nuevoContacto);
        // Mostrar contactos actuales
        System.out.println("Contactos después de agregar uno nuevo:");
        List<Contacto> contactos = listaContactos.obtenerContactos();
        for (Contacto contacto : contactos) {
           System.out.println("Nombre: " + contacto.getNombr() +
                              ", Número: " + contacto.getNumero() +
                              ", Edad: " + contacto.getEdad() +
                              ", Dirección: " + contacto.getDireccion());
        }
    }
}
package gestorcontactos;

import java.util.*;

public class Contacto {

    private String direccion;
    private String nombr;
    private int num;
    private int edad;

    public void setNombre(String nombre){
       this.nombre = nombre;
    }

    public String getNombr() {
       return nombr;
    }

    public void setNumero(int numero){
        this.numero = numero;
    }

    public int getNumero() {
        return num;
    }

    public void setEdad(int edad){
        this.edad = edad;
    }

    public int getEdad() {
        return edad;
    }

    public void setDireccion(int direccion){
        this.direccion = direccion;
    }

    public String getDireccion() {
        return direccion;
    }

    public Contacto(String direccion, String nombr, int num, int edad){
        this.direccion = direccion;
        this.edad = edad;
        this.numero = num;
        this.nombre = nombr;
    }
}

public class ListaContactos{
    private List<Contacto> contactos;

    public ListaContactos(){
        contactos = new ArrayList<>();
    }

    public void agregarContacto(Contacto contacto){
        contactos.add(contacto);
    }

    public void eliminarContacto(Contacto contacto){
        contactos.remove(contacto)
    }

    public List<Contacto> obtenerContactos() {
        return contactos;
    }

    public Contacto buscarContactoPorNombre(String nombre) {
        for (Contacto contacto : contactos) {
            if (contacto.getNombr().equals(nombre)) {
                return contacto;
            }
        }
        return ("No hay contacto con tal nombre"); // Si no se encuentra el contacto
    }

    public Contacto buscarContactoPorNumero(int numero) {
        for (Contacto contacto : contactos) {
            if (contacto.getNumero().equals(numero)) {
                return contacto;
            }
        }
        return ("No hay contacto con tal numero"); // Si no se encuentra el contacto
    }
}

/*                                 UML:                                 * \
 ***-------------------***          |      ***-------------------***
 ***----------------***             |      ***----------------***
 ***    Contactos                   |      ***      Listas
 ***----------------***             |      ***----------------***
 *** + SetNombre: void              |      *** + agregarContacto: void
 *** + GetNombre: String            |      *** + eliminarContacto: void
 *** + SetNumero: void              |      *** + obtenerContactos: List<Contacto>
 *** + GetNumero: int               |      *** + buscarContactoPorNombre: Contacto
 *** + SetDireccion: void           |      *** + buscarContactoPorNumero: Contacto
 *** + GetDireccion: String         |      ***---------------***
 *** + SetEdad: void                |      *** - contactos: List<Contacto>
 *** + GetEdad: int                 |      ***---------------***
 ***----------------***             |      ***-------------------***
 *** - direccion: String            |
 *** - nombre: String               |
 *** - num: int                     |
 *** - edad: int                    |
 ***----------------***             |
 ***-------------------***          |
/ *                                 |                                  */
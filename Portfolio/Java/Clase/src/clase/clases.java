package clase;

public class Juego {
    private String nombre="N/A";
    private int edadMinima = 8;
    private int edadMaxima = 20;
    private Fabricante fabricante;

    public void setNombre(String nombre){
        this.nombre = nombre
    }

    public String getNombre() {
        return nombre;
    }

    public int getEdadMaxima() {
        return edadMaxima;
    }

    public void setEdadMaxima(int edadMaxima) {
        this.edadMaxima = edadMaxima;
    }

    public int getEdadMinima() {
        return edadMinima;
    }

    public void setEdadMinima(int edadMinima) {
        this.edadMinima = edadMinima;
    }

    public constructor(){
        this.nombre;
        this.edadMaxima;
        this.edadMinima;
    }

    public constructor(String nombre, int edad1, int edad2){
        this.setNombre(nombre);
        if(edad1>edad2){
            this.setEdadMaxima(edad1);
            this.setEdadMinima(edad2);
        }else{
            this.setEdadMaxima(edad2);
            this.setEdadMinima(edad1);
        }
        //Funcionaria la validacion de menor como "int min = Math.mix(edad1, edad2);"  y edad maxima como int max = "Math.max(edad1, edad2)";
    }

    public void setFabricante(clase.Fabricante fabricante) {
        this.fabricante = fabricante;
    }

    public clase.Fabricante getFabricante() {
        return fabricante;
    }

    public String toString(){
        return this.nombre + " ("+ this.edadMinima "-" + this.edadMaxima + ")" + this.getFabricante;
    }

    public boolean esAplicableAEdad(int unaEdad){
        boolean booleano = flase;
        if(this.edadMaxima>= unaEdad && this.edadMinima<= unaEdad){
            booleano = true;
        }
        return booleano;
    }

}

public class Fabricante{
    String nombre;
    String paisOrigen;

    public String getNombre() {
        return nombre;
    }

    public String getPaisOrigen() {
        return paisOrigen;
    }

    public void setNombre(String nombre) {
        this.nombre = nombre;
    }

    public void setPaisOrigen(String paisOrigen) {
        this.paisOrigen = paisOrigen;
    }

    public Fabricante(){
        this.setNombre("N/A");
        this.setPaisOrigen("Uruguay");
    }

    public Fabricante(String nombre, String pais){
        this.setNombre(nombre);
        this.setPaisOrigen(pais);
    }

    public String toString(){
        return "Fabricante"
            + "(Nombre: " + this.getNombre() + "Pais de origen" + this.getPaisOrigen()+")";
    }
}

public  class clases {

    public static void main(String[] args) {
        Juego j2 = new Juego("Tetris", 20, 5);
        Fabricante ortLab = new Fabricante("Ort Laboratorio", "UY");
        j2.setFabricante(ortLab)
        System.out.println(j2);
    }
}

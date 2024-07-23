/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/GUIForms/JFrame.java to edit this template
 */
package Interfaz.ImportarYExportar;

import Interfaz.Errores.NumVali;
import Interfaz.Errores.cedNReg;
import Interfaz.Errores.eFo;
import Modelo.ArchivoLectura;
import Modelo.Capataz;
import Modelo.Obra;
import Modelo.Propietario;
import Modelo.Rubro;
import Modelo.Sistema;
import java.io.File;
import java.math.BigDecimal;
import java.util.ArrayList;
import javax.swing.JFileChooser;

/**
 *
 * @author varo1
 */
public class ImportarArch extends javax.swing.JFrame {
    private Sistema sistema;

    public ImportarArch(Sistema sistema1) {
        this.sistema = sistema1;
        initComponents();
        elegirArchivo();
    }

    public void error(){
        eFo eFo = new eFo();
        eFo.setVisible(true);
        this.dispose();
    }
    private void elegirArchivo(){
        importador.addActionListener(e -> {
            if (JFileChooser.APPROVE_SELECTION.equals(e.getActionCommand())) {
                File arch = importador.getSelectedFile();
                procesSeleccionado(arch);
                this.dispose();
            } else if (JFileChooser.CANCEL_SELECTION.equals(e.getActionCommand())) {
                this.dispose();
            }
        });
    }

    private void procesSeleccionado(File arch) {
        ArrayList<Integer> cedulas = this.sistema.getListaCedulas(); 
        ArrayList<Integer> numP = this.sistema.getListNumVali(); 
        String[] linea1 = leerPrimeraLinea(arch.getPath());
        int cant = leerSegundaLinea(arch.getPath()); 
        ArrayList<Rubro> linea3 = leerTerceraLinea(arch.getPath(), cant);
        if (linea1.length==6 && cant!=0){
            procesarObra(linea1, cant, linea3, cedulas, numP);
            this.dispose();
        } else {
            error();
        }
    }

    public String[] leerPrimeraLinea(String filePath){
        String[] g = new String[6];
        ArchivoLectura archivo = new ArchivoLectura(filePath);
        if (archivo.hayMasLineas()) {
            g = archivo.linea().split("#");
            for (int i = 0; i < 6; i++) {
                if (g[i].equals("")) {
                    error();
                }
            }
        } else {
            error();
        }
        archivo.cerrar();
        return g;
    }

    public int leerSegundaLinea(String filePath){
        int g = 0;
        ArchivoLectura archivo = new ArchivoLectura(filePath);
        if (archivo.hayMasLineas()){ 
            if (archivo.hayMasLineas()) {
                String cant = archivo.linea();
                try {
                    g = Integer.parseInt(cant);
                    if(g<1 || g >12){
                        error();
                    }
                } catch (NumberFormatException e) {
                    error();
                }
            } else {
                error();
            }
        } else {
            error();
        }
        archivo.cerrar();
        return g;
    }

    public ArrayList<Rubro> leerTerceraLinea(String filePath, int cant){
        ArrayList<Rubro> g = new ArrayList();
        ArchivoLectura archivo = new ArchivoLectura(filePath);
        int t = 0;
        if (archivo.hayMasLineas()) { 
            if (archivo.hayMasLineas()) { 
                while (archivo.hayMasLineas()) {
                    String[] s = archivo.linea().split("#");
                    try {
                        BigDecimal h = BigDecimal.valueOf(Integer.parseInt(s[1]));
                        if (this.sistema.verifEsta(s[0])) {
                            for (Rubro e : this.sistema.getListaRubros()) {
                                if (e.getNombre().equals(s[0])) {
                                    Rubro r = e;
                                    r.setPresupuesto(h);
                                    g.add(r);
                                }
                            }
                        } else {
                            Rubro r = new Rubro(s[0], "Descripción a Definir");
                            r.setPresupuesto(h);
                            g.add(r);
                        }
                        t++;
                    } catch (NumberFormatException e) {
                        error();
                    }
                }
                if (t < cant) {
                    error();
                }else if(t> cant){
                    error();
                }
            } else {
                error();
            }
        } else {
            error();
        }
        archivo.cerrar();
        return g;
    }

    public void procesarObra(String[] linea1, int cant, ArrayList<Rubro> linea3, ArrayList<Integer> cedulas, ArrayList<Integer> numP){
        try{
            int ana = Integer.parseInt(linea1[4]);
            int mes = Integer.parseInt(linea1[3]);
            int nP = Integer.parseInt(linea1[5]);
            int cP = Integer.parseInt(linea1[1]);
            int cC = Integer.parseInt(linea1[0]);
            BigDecimal presu = BigDecimal.ZERO;
            for(Rubro e : linea3){
                presu = presu.add(e.getPresupuesto());
            }
            if(cedulas.contains(cC) && cedulas.contains(cP)){
                if(!numP.contains(nP)){
                    Capataz c = this.sistema.getCapatazCI(Integer.parseInt(linea1[0]));
                    if(c==null){
                        cedNReg cedNReg = new cedNReg();
                        cedNReg.setVisible(true);
                        this.dispose();
                    }else{
                        Propietario p = this.sistema.getPropietarioCI(Integer.parseInt(linea1[1]));
                        if(p==null){
                            cedNReg cedNReg = new cedNReg();
                            cedNReg.setVisible(true);
                            this.dispose();
                        }else{
                            Obra o = new Obra(mes, ana, linea1[2], nP, c, p, presu);
                            o.setRubros(linea3);
                            numP.add(nP);
                            this.sistema.agregarListaObra(o);
                        }
                    }
                }else{
                    NumVali fallo = new NumVali();
                    fallo.setVisible(true);
                    this.dispose();
                }
            }else{
                cedNReg cedNReg = new cedNReg();
                cedNReg.setVisible(true);
                this.dispose();
            }
        }catch(NumberFormatException e){
            error();
        }
    }
    
    /**
     * This method is called from within the constructor to initialize the form.
     * WARNING: Do NOT modify this code. The content of this method is always
     * regenerated by the Form Editor.
     */
    @SuppressWarnings("unchecked")
    // <editor-fold defaultstate="collapsed" desc="Generated Code">//GEN-BEGIN:initComponents
    private void initComponents() {

        importador = new javax.swing.JFileChooser();

        setDefaultCloseOperation(javax.swing.WindowConstants.DISPOSE_ON_CLOSE);
        setTitle("Importando Archivos");

        javax.swing.GroupLayout layout = new javax.swing.GroupLayout(getContentPane());
        getContentPane().setLayout(layout);
        layout.setHorizontalGroup(
            layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addComponent(importador, javax.swing.GroupLayout.DEFAULT_SIZE, 676, Short.MAX_VALUE)
        );
        layout.setVerticalGroup(
            layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addComponent(importador, javax.swing.GroupLayout.DEFAULT_SIZE, 530, Short.MAX_VALUE)
        );

        pack();
    }// </editor-fold>//GEN-END:initComponents

    // Variables declaration - do not modify//GEN-BEGIN:variables
    private javax.swing.JFileChooser importador;
    // End of variables declaration//GEN-END:variables
}

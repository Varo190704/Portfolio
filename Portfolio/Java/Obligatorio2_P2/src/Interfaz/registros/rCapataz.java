/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/GUIForms/JFrame.java to edit this template
 */
package Interfaz.registros;

import java.util.Observable;
import java.util.Observer;
import Interfaz.Errores.Cedu;
import Interfaz.Errores.Vacios;
import Interfaz.Errores.ciNoNum;
import Modelo.Sistema;
import Modelo.Capataz;
import java.awt.Color;
import javax.swing.SpinnerNumberModel;
import javax.swing.text.JTextComponent;

/**
 *
 * @author varo1
 */
public class rCapataz extends javax.swing.JFrame implements Observer {
    private Sistema sistema;

    /**
     * Creates new form rCapataz
     */
    public rCapataz(Sistema sistema1) {
        this.sistema = sistema1;
        initComponents();
        placeHolder(this.nombre, "Nombre Apellido");
        placeHolder(this.cedu, "11111111 ");
        placeHolder(this.direc, "Ej: Av Italia 2222");     
    }
    
    public void placeHolder(JTextComponent t, String pH){
        t.setText(pH);
        
        t.addFocusListener(new java.awt.event.FocusAdapter() {
            public void focusGained(java.awt.event.FocusEvent evt) {
                if (t.getText().equals(pH)) {
                    t.setText("");
                    t.setForeground(Color.BLACK);
                }
            }

            public void focusLost(java.awt.event.FocusEvent evt) {
                if (t.getText().isEmpty()) {
                    t.setText(pH);
                    t.setForeground(new Color(153,153,153));
                }
            }
        });
    }
    
    /**
     * This method is called from within the constructor to initialize the form.
     * WARNING: Do NOT modify this code. The content of this method is always
     * regenerated by the Form Editor.
     */
    @SuppressWarnings("unchecked")
    // <editor-fold defaultstate="collapsed" desc="Generated Code">//GEN-BEGIN:initComponents
    private void initComponents() {

        jPanel1 = new javax.swing.JPanel();
        nombre = new javax.swing.JTextField();
        direc = new javax.swing.JTextField();
        cedu = new javax.swing.JTextField();
        jLabel1 = new javax.swing.JLabel();
        jLabel2 = new javax.swing.JLabel();
        jLabel3 = new javax.swing.JLabel();
        jLabel4 = new javax.swing.JLabel();
        add = new javax.swing.JButton();
        SpinnerNumberModel setAnno = new SpinnerNumberModel(2010, 2010, 2024, 1);
        anno = new javax.swing.JSpinner();
        anno.setModel(setAnno);

        setDefaultCloseOperation(javax.swing.WindowConstants.DISPOSE_ON_CLOSE);
        setTitle("Registrando Capataz");

        nombre.setFont(new java.awt.Font("Segoe UI", 1, 12)); // NOI18N
        nombre.setForeground(new java.awt.Color(153, 153, 153));
        nombre.setText("Nombre Apellido");

        direc.setFont(new java.awt.Font("Segoe UI", 1, 12)); // NOI18N
        direc.setForeground(new java.awt.Color(153, 153, 153));
        direc.setText("Ej: Av Italia 2222");

        cedu.setFont(new java.awt.Font("Segoe UI", 1, 12)); // NOI18N
        cedu.setForeground(new java.awt.Color(153, 153, 153));
        cedu.setText("11111111 ");

        jLabel1.setFont(new java.awt.Font("Segoe UI", 1, 12)); // NOI18N
        jLabel1.setText("Nombre");

        jLabel2.setFont(new java.awt.Font("Segoe UI", 1, 12)); // NOI18N
        jLabel2.setText("Cédula");

        jLabel3.setFont(new java.awt.Font("Segoe UI", 1, 12)); // NOI18N
        jLabel3.setText("Dirección");

        jLabel4.setFont(new java.awt.Font("Segoe UI", 1, 12)); // NOI18N
        jLabel4.setText("Año de Ingreso a la empresa");

        add.setFont(new java.awt.Font("Segoe UI", 1, 12)); // NOI18N
        add.setText("Agregar");
        add.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                addActionPerformed(evt);
            }
        });

        anno.setFont(new java.awt.Font("Segoe UI", 1, 12)); // NOI18N

        javax.swing.GroupLayout jPanel1Layout = new javax.swing.GroupLayout(jPanel1);
        jPanel1.setLayout(jPanel1Layout);
        jPanel1Layout.setHorizontalGroup(
            jPanel1Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(jPanel1Layout.createSequentialGroup()
                .addGap(27, 27, 27)
                .addGroup(jPanel1Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING, false)
                    .addComponent(add, javax.swing.GroupLayout.DEFAULT_SIZE, 249, Short.MAX_VALUE)
                    .addComponent(jLabel3)
                    .addComponent(jLabel2)
                    .addComponent(jLabel1)
                    .addComponent(jLabel4)
                    .addComponent(anno)
                    .addComponent(direc)
                    .addComponent(cedu)
                    .addComponent(nombre))
                .addContainerGap(27, Short.MAX_VALUE))
        );
        jPanel1Layout.setVerticalGroup(
            jPanel1Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(jPanel1Layout.createSequentialGroup()
                .addGap(20, 20, 20)
                .addComponent(jLabel1)
                .addGap(18, 18, 18)
                .addComponent(nombre, javax.swing.GroupLayout.PREFERRED_SIZE, 31, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addGap(26, 26, 26)
                .addComponent(jLabel2)
                .addGap(18, 18, 18)
                .addComponent(cedu, javax.swing.GroupLayout.PREFERRED_SIZE, 31, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addGap(26, 26, 26)
                .addComponent(jLabel3)
                .addGap(18, 18, 18)
                .addComponent(direc, javax.swing.GroupLayout.PREFERRED_SIZE, 31, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addGap(26, 26, 26)
                .addComponent(jLabel4)
                .addGap(18, 18, 18)
                .addComponent(anno, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED, 33, Short.MAX_VALUE)
                .addComponent(add, javax.swing.GroupLayout.PREFERRED_SIZE, 31, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addGap(18, 18, 18))
        );

        javax.swing.GroupLayout layout = new javax.swing.GroupLayout(getContentPane());
        getContentPane().setLayout(layout);
        layout.setHorizontalGroup(
            layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(layout.createSequentialGroup()
                .addComponent(jPanel1, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addContainerGap(javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE))
        );
        layout.setVerticalGroup(
            layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addComponent(jPanel1, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
        );

        pack();
    }// </editor-fold>//GEN-END:initComponents

    private void addActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_addActionPerformed
        int ci;
        int ana;
        String name;
        String direction;
        try{
            ci = Integer.parseInt(this.cedu.getText());
            if(this.nombre.getText().isEmpty() || this.direc.getText().isEmpty() ||
                this.nombre.getText().equals("Nombre Apellido") || this.direc.getText().equals("Ej: Av Italia 2222")) {
                Vacios fallo = new Vacios();
                fallo.setVisible(true);
            }else if(this.sistema.getListaCedulas().contains(ci)){
                Cedu fallo = new Cedu();
                fallo.setVisible(true);
            }else{
                ana = (Integer) this.anno.getValue();
                name = this.nombre.getText();
                direction = this.direc.getText();
                this.sistema.getListaCedulas().add(ci);
                Capataz capataz = new Capataz(name, ci, direction, ana);
                this.sistema.agregarListaCapataz(capataz);
                this.dispose();
            }
        }catch(NumberFormatException e){
            ciNoNum fallo = new ciNoNum();
            fallo.setVisible(true);
        }
        
    }//GEN-LAST:event_addActionPerformed

    // Variables declaration - do not modify//GEN-BEGIN:variables
    private javax.swing.JButton add;
    private javax.swing.JSpinner anno;
    private javax.swing.JTextField cedu;
    private javax.swing.JTextField direc;
    private javax.swing.JLabel jLabel1;
    private javax.swing.JLabel jLabel2;
    private javax.swing.JLabel jLabel3;
    private javax.swing.JLabel jLabel4;
    private javax.swing.JPanel jPanel1;
    private javax.swing.JTextField nombre;
    // End of variables declaration//GEN-END:variables

    @Override
    public void update(Observable o, Object arg) {
        //no es abstracto sino
    }
}

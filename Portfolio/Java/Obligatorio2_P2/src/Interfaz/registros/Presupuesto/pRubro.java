/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/GUIForms/JFrame.java to edit this template
 */
package Interfaz.registros.Presupuesto;

import Interfaz.Errores.esZero;
import Interfaz.Errores.noNum;
import Interfaz.registros.rObra;
import Modelo.Rubro;
import Modelo.Sistema;
import java.awt.Color;
import java.math.BigDecimal;
import javax.swing.JButton;

/**
 *
 * @author varo1
 */
public class pRubro extends javax.swing.JFrame {
    private Sistema sistema;
    private JButton str;
    private rObra instancia;
    /**
     * Creates new form pRubro
     */
    public pRubro(Sistema sistema1, JButton strg, rObra obra) {
        this.sistema = sistema1;
        this.str = strg;
        this.instancia = obra;
        initComponents();
        
        this.presu.setText("Ingrese presupuesto. ");
        
        this.presu.addFocusListener(new java.awt.event.FocusAdapter() {
            public void focusGained(java.awt.event.FocusEvent evt) {
                if (presu.getText().equals("Ingrese presupuesto. ")) {
                    presu.setText("");
                    presu.setForeground(Color.BLACK);
                }
            }

            public void focusLost(java.awt.event.FocusEvent evt) {
                if (presu.getText().isEmpty()) {
                    presu.setText("Ingrese presupuesto. ");
                    presu.setForeground(new Color(153,153,153));
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
        jLabel1 = new javax.swing.JLabel();
        presu = new javax.swing.JTextField();
        jButton1 = new javax.swing.JButton();

        setDefaultCloseOperation(javax.swing.WindowConstants.DISPOSE_ON_CLOSE);

        jLabel1.setFont(new java.awt.Font("Segoe UI", 1, 12)); // NOI18N
        jLabel1.setText("Presupuesto");

        presu.setFont(new java.awt.Font("Segoe UI", 1, 12)); // NOI18N
        presu.setForeground(new java.awt.Color(153, 153, 153));
        presu.setText("Ingrese un monto numerico mayor a 0 ");

        jButton1.setFont(new java.awt.Font("Segoe UI", 1, 12)); // NOI18N
        jButton1.setText("Agregar presupuesto");
        jButton1.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                jButton1ActionPerformed(evt);
            }
        });

        javax.swing.GroupLayout jPanel1Layout = new javax.swing.GroupLayout(jPanel1);
        jPanel1.setLayout(jPanel1Layout);
        jPanel1Layout.setHorizontalGroup(
            jPanel1Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(jPanel1Layout.createSequentialGroup()
                .addGroup(jPanel1Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                    .addGroup(jPanel1Layout.createSequentialGroup()
                        .addGap(21, 21, 21)
                        .addGroup(jPanel1Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                            .addComponent(jLabel1)
                            .addComponent(presu, javax.swing.GroupLayout.PREFERRED_SIZE, 235, javax.swing.GroupLayout.PREFERRED_SIZE)))
                    .addGroup(jPanel1Layout.createSequentialGroup()
                        .addGap(67, 67, 67)
                        .addComponent(jButton1)))
                .addGap(21, 21, 21))
        );
        jPanel1Layout.setVerticalGroup(
            jPanel1Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(jPanel1Layout.createSequentialGroup()
                .addGap(19, 19, 19)
                .addComponent(jLabel1)
                .addGap(18, 18, 18)
                .addComponent(presu, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addGap(18, 18, 18)
                .addComponent(jButton1)
                .addGap(19, 19, 19))
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
            .addGroup(layout.createSequentialGroup()
                .addComponent(jPanel1, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addGap(0, 0, Short.MAX_VALUE))
        );

        pack();
    }// </editor-fold>//GEN-END:initComponents

    private void jButton1ActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_jButton1ActionPerformed
        BigDecimal presupuesto;
        try{
            presupuesto = BigDecimal.valueOf(Integer.parseInt(this.presu.getText()));
            //https://stackoverflow.com/questions/25824764/java-compare-integer-and-BigDecimal
            if(presupuesto.compareTo(BigDecimal.ZERO)>0){
                for(Rubro e : this.sistema.getListaRubros()){
                    int indice = str.getText().indexOf(" -- ");
                    if(indice>-1){
                        if(str.getText().substring(0, indice).equals(e.getNombre())){
                            e.setPresupuesto(presupuesto);
                            this.sistema.presuCambiado();
                            this.dispose();
                            instancia.cambiarTxt(str);
                        }
                    }else if(e.getNombre().equals(str.getText())){
                        e.setPresupuesto(presupuesto);
                        this.sistema.presuCambiado();
                        this.dispose();
                        instancia.cambiarTxt(str);
                        
                    }
                }
            }else{
                esZero fallo = new esZero();
                fallo.setVisible(true);
            }
            
        }catch(NumberFormatException e){
            noNum fallo = new noNum();
            fallo.setVisible(true);
        }
    }//GEN-LAST:event_jButton1ActionPerformed

    // Variables declaration - do not modify//GEN-BEGIN:variables
    private javax.swing.JButton jButton1;
    private javax.swing.JLabel jLabel1;
    private javax.swing.JPanel jPanel1;
    private javax.swing.JTextField presu;
    // End of variables declaration//GEN-END:variables
}

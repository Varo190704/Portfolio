package Interfaz.General;

import Interfaz.Errores.NoSisAnt;
import Interfaz.General.PeqOb;
import Modelo.Sistema;
import Modelo.Serie_deserie;
import java.awt.Image;
import javax.swing.ImageIcon;

/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/GUIForms/JFrame.java to edit this template
 */

/**
 *
 * @author varo1
 */
public class ComenzarCon extends javax.swing.JFrame {
    private Serie_deserie des;
    private Sistema sistema;

    /**
     * Creates new form ComenzarCon
     */
    public ComenzarCon() {
        this.des = new Serie_deserie();
        this.sistema = new Sistema();
        initComponents();
        ImageIcon imageIcon = new ImageIcon(getClass().getResource("/Img/icono.png"));
        Image image = imageIcon.getImage();
        Image resizedImage = image.getScaledInstance(group.getWidth(), group.getHeight(), Image.SCALE_SMOOTH);
        group.setIcon(new ImageIcon(resizedImage));
    }

    /**
     * This method is called from within the constructor to initialize the form.
     * WARNING: Do NOT modify this code. The content of this method is always
     * regenerated by the Form Editor.
     */
    @SuppressWarnings("unchecked")
    // <editor-fold defaultstate="collapsed" desc="Generated Code">//GEN-BEGIN:initComponents
    private void initComponents() {

        Prede = new javax.swing.JButton();
        SisAnt = new javax.swing.JButton();
        jLabel1 = new javax.swing.JLabel();
        group = new javax.swing.JLabel();
        jButton1 = new javax.swing.JButton();

        setDefaultCloseOperation(javax.swing.WindowConstants.EXIT_ON_CLOSE);
        setTitle("Comenzar con...");
        setMinimumSize(new java.awt.Dimension(510, 160));

        Prede.setFont(new java.awt.Font("Segoe UI", 1, 12)); // NOI18N
        Prede.setText("Sólo rubros");
        Prede.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                PredeActionPerformed(evt);
            }
        });

        SisAnt.setFont(new java.awt.Font("Segoe UI", 1, 12)); // NOI18N
        SisAnt.setText("Sistema Anterior");
        SisAnt.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                SisAntActionPerformed(evt);
            }
        });

        jLabel1.setFont(new java.awt.Font("Segoe UI", 1, 18)); // NOI18N
        jLabel1.setText("Seleccione una opción:");

        jButton1.setFont(new java.awt.Font("Segoe UI", 1, 12)); // NOI18N
        jButton1.setText("Sistema Vacio");
        jButton1.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                jButton1ActionPerformed(evt);
            }
        });

        javax.swing.GroupLayout layout = new javax.swing.GroupLayout(getContentPane());
        getContentPane().setLayout(layout);
        layout.setHorizontalGroup(
            layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(layout.createSequentialGroup()
                .addGap(16, 16, 16)
                .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                    .addGroup(layout.createSequentialGroup()
                        .addComponent(group, javax.swing.GroupLayout.PREFERRED_SIZE, 43, javax.swing.GroupLayout.PREFERRED_SIZE)
                        .addGap(27, 27, 27)
                        .addComponent(jLabel1, javax.swing.GroupLayout.PREFERRED_SIZE, 373, javax.swing.GroupLayout.PREFERRED_SIZE)
                        .addContainerGap(javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE))
                    .addGroup(layout.createSequentialGroup()
                        .addComponent(jButton1, javax.swing.GroupLayout.PREFERRED_SIZE, 151, javax.swing.GroupLayout.PREFERRED_SIZE)
                        .addGap(18, 18, 18)
                        .addComponent(SisAnt, javax.swing.GroupLayout.PREFERRED_SIZE, 151, javax.swing.GroupLayout.PREFERRED_SIZE)
                        .addGap(18, 18, 18)
                        .addComponent(Prede, javax.swing.GroupLayout.PREFERRED_SIZE, 151, javax.swing.GroupLayout.PREFERRED_SIZE)
                        .addGap(0, 14, Short.MAX_VALUE))))
        );
        layout.setVerticalGroup(
            layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(javax.swing.GroupLayout.Alignment.TRAILING, layout.createSequentialGroup()
                .addContainerGap(14, Short.MAX_VALUE)
                .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                    .addComponent(jLabel1, javax.swing.GroupLayout.Alignment.TRAILING)
                    .addComponent(group, javax.swing.GroupLayout.Alignment.TRAILING, javax.swing.GroupLayout.PREFERRED_SIZE, 38, javax.swing.GroupLayout.PREFERRED_SIZE))
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED, 41, Short.MAX_VALUE)
                .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                    .addComponent(SisAnt, javax.swing.GroupLayout.PREFERRED_SIZE, 50, javax.swing.GroupLayout.PREFERRED_SIZE)
                    .addComponent(Prede, javax.swing.GroupLayout.PREFERRED_SIZE, 50, javax.swing.GroupLayout.PREFERRED_SIZE)
                    .addComponent(jButton1, javax.swing.GroupLayout.PREFERRED_SIZE, 50, javax.swing.GroupLayout.PREFERRED_SIZE))
                .addContainerGap(17, Short.MAX_VALUE))
        );

        pack();
    }// </editor-fold>//GEN-END:initComponents

    private void PredeActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_PredeActionPerformed
        this.sistema.rubrosPred();
        PeqOb PeqOb = new PeqOb(sistema, des);
        this.dispose();
        PeqOb.setVisible(true);
    }//GEN-LAST:event_PredeActionPerformed

    private void SisAntActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_SisAntActionPerformed
        this.sistema = this.des.deserie();
        if(sistema!=null){
            PeqOb PeqOb = new PeqOb(sistema, des);
            PeqOb.setVisible(true);
            this.dispose();
        }else{
            NoSisAnt NoSisAnt = new NoSisAnt();
            NoSisAnt.setVisible(true);
            this.dispose();
        }
    }//GEN-LAST:event_SisAntActionPerformed

    private void jButton1ActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_jButton1ActionPerformed
        PeqOb PeqOb = new PeqOb(this.sistema, des);
        PeqOb.setVisible(true);
        this.dispose();
    }//GEN-LAST:event_jButton1ActionPerformed

    // Variables declaration - do not modify//GEN-BEGIN:variables
    private javax.swing.JButton Prede;
    private javax.swing.JButton SisAnt;
    private javax.swing.JLabel group;
    private javax.swing.JButton jButton1;
    private javax.swing.JLabel jLabel1;
    // End of variables declaration//GEN-END:variables
}

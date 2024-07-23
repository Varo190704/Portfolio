/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/GUIForms/JFrame.java to edit this template
 */
package Interfaz.registros;

import java.util.Observable;
import java.util.Observer;
import Interfaz.Errores.NumVali;
import Interfaz.Errores.Vacios;
import Interfaz.Errores.ciNoNum;
import Interfaz.registros.Presupuesto.pRubro;
import Modelo.Capataz;
import Modelo.Obra;
import Modelo.Propietario;
import Modelo.Rubro;
import Modelo.Sistema;
import java.awt.Color;
import java.awt.Component;
import java.awt.Dimension;
import java.awt.GridLayout;
import java.awt.Insets;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.math.BigDecimal;
import java.util.ArrayList;
import javax.swing.DefaultListModel;
import javax.swing.JButton;
import javax.swing.JPanel;
import javax.swing.JScrollPane;
import javax.swing.JViewport;
import javax.swing.SpinnerNumberModel;
import javax.swing.text.JTextComponent;


/**
 *
 * @author varo1
 */
public class rObra extends javax.swing.JFrame implements Observer {
    private Sistema sistema;
    /**
     * Creates new form rObra
     * @param sistema1
     */
    public rObra(Sistema sistema1) {
        this.sistema = sistema1;
        this.sistema.addObserver(this);
        initComponents();
        placeHolder(this.permiso, "524 ");
        placeHolder(this.dire, "Ej: Av Italia 2222");  
        capataces();
        propietarios();
        botones();
        SpinnerNumberModel setAnno = new SpinnerNumberModel(2010, 2010, 2024, 1);
    }
    public void propietarios(){
        DefaultListModel<String> modelo = new DefaultListModel();
        for (Propietario c : this.sistema.getListaPropietario()) {
            modelo.addElement(c.toString());
        }
        this.propietarios.setModel(modelo);
    }
    
    public void capataces(){
        DefaultListModel<String> modelo = new DefaultListModel();
        for (Capataz c : this.sistema.getListaCapataz()) {
            modelo.addElement(c.toString());
        }
        this.capataces.setModel(modelo);
        
        this.capataces.addListSelectionListener(e -> {
            if (!e.getValueIsAdjusting()) {
                String sele = this.capataces.getSelectedValue();
                if (sele != null) {
                    actualizarSpinner(sele);
                }
            }
        });
    }
    
    public void actualizarSpinner(String sele){
        Capataz c = null;
        for(Capataz e : this.sistema.getListaCapataz()){
            if(e.toString().equals(sele)){
                c=e;
            }
        }
        SpinnerNumberModel setAnno = new SpinnerNumberModel(c.getAnno(), c.getAnno(), 2024, 1);
        anno.setModel(setAnno);
    }
    
    public void botones(){
        JPanel panel = new JPanel(new GridLayout(0, 2, 0, 0));
        int ancho = 312;
        int altura = 108;
        for(Rubro c : this.sistema.getListaRubros()){
            JButton btn = new JButton(c.getNombre());
            btn.setPreferredSize(new Dimension(ancho, altura));
            btn.setMargin(new Insets(-5, -5, -5, -5));
            btn.setBackground(Color.BLACK);
            btn.setForeground(Color.WHITE);
            btn.addActionListener(new accionListener());
            panel.add(btn);
        }
        int filas = (int) Math.ceil(this.sistema.getListaRubros().size() / 2.0);
        panel.setPreferredSize(new Dimension(ancho * 2, altura * filas));
        this.botones = new JScrollPane(panel);
        this.botones.setPreferredSize(new Dimension(624,216));
        this.panelFuera.setPreferredSize(new Dimension (632,216));
        this.panelFuera.setViewportView(this.botones);
    }
    
    private class accionListener implements ActionListener {
        @Override
        public void actionPerformed(ActionEvent e) {
            JButton recurso = (JButton) e.getSource();
            pRubro root = new pRubro(sistema, recurso, rObra.this);
            root.setVisible(true);
        }
    }
    
    public void cambiarTxt(JButton recurso){
        BigDecimal presup = BigDecimal.ZERO;
        for(Rubro g : this.sistema.getListaRubros()){
            int indice = recurso.getText().indexOf(" -- ");
            if(indice>-1){
                if(recurso.getText().substring(0, indice).equals(g.getNombre()) && g.getPresupuesto().compareTo(BigDecimal.ZERO)>0){
                    recurso.setText(g.boton());
                    recurso.setBackground(Color.BLUE);
                }
            }else if(recurso.getText().equals(g.getNombre()) && g.getPresupuesto().compareTo(BigDecimal.ZERO)>0){
                recurso.setText(g.boton());
                recurso.setBackground(Color.BLUE);
            }
            presup = presup.add(g.getPresupuesto());
        }
        this.presupuesto.setText(presup.toString());
    }
     
    public void placeHolder(JTextComponent t, String pH){
        t.setText(pH);
        
        t.addFocusListener(new java.awt.event.FocusAdapter() {
            @Override
            public void focusGained(java.awt.event.FocusEvent evt) {
                if (t.getText().equals(pH)) {
                    t.setText("");
                    t.setForeground(Color.BLACK);
                }
            }

            @Override
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

        jPanel2 = new javax.swing.JPanel();
        jScrollPane1 = new javax.swing.JScrollPane();
        propietarios = new javax.swing.JList<>();
        jScrollPane2 = new javax.swing.JScrollPane();
        capataces = new javax.swing.JList<>();
        jLabel1 = new javax.swing.JLabel();
        jLabel2 = new javax.swing.JLabel();
        jLabel3 = new javax.swing.JLabel();
        jLabel4 = new javax.swing.JLabel();
        jLabel5 = new javax.swing.JLabel();
        dire = new javax.swing.JTextField();
        SpinnerNumberModel setAnno = new SpinnerNumberModel(2010, 2010, 2024, 1);
        anno = new javax.swing.JSpinner();
        anno.setModel(setAnno);
        permiso = new javax.swing.JTextField();
        jLabel6 = new javax.swing.JLabel();
        presupuesto = new javax.swing.JLabel();
        jLabel8 = new javax.swing.JLabel();
        jButton1 = new javax.swing.JButton();
        SpinnerNumberModel setMes = new SpinnerNumberModel(1, 1, 12, 1);
        mes = new javax.swing.JSpinner();
        mes.setModel(setMes);
        panelFuera = new javax.swing.JScrollPane();
        botones = new javax.swing.JScrollPane();

        setDefaultCloseOperation(javax.swing.WindowConstants.DISPOSE_ON_CLOSE);
        addWindowListener(new java.awt.event.WindowAdapter() {
            public void windowClosing(java.awt.event.WindowEvent evt) {
                formWindowClosing(evt);
            }
        });

        propietarios.setFont(new java.awt.Font("Segoe UI", 1, 12)); // NOI18N
        propietarios.setModel(new javax.swing.AbstractListModel<String>() {
            String[] strings = { "Item 1", "Item 2", "Item 3", "Item 4", "Item 5" };
            public int getSize() { return strings.length; }
            public String getElementAt(int i) { return strings[i]; }
        });
        jScrollPane1.setViewportView(propietarios);

        capataces.setFont(new java.awt.Font("Segoe UI", 1, 12)); // NOI18N
        capataces.setModel(new javax.swing.AbstractListModel<String>() {
            String[] strings = { "Item 1", "Item 2", "Item 3", "Item 4", "Item 5" };
            public int getSize() { return strings.length; }
            public String getElementAt(int i) { return strings[i]; }
        });
        jScrollPane2.setViewportView(capataces);

        jLabel1.setFont(new java.awt.Font("Segoe UI", 1, 12)); // NOI18N
        jLabel1.setText("Capataces");

        jLabel2.setFont(new java.awt.Font("Segoe UI", 1, 12)); // NOI18N
        jLabel2.setText("Propietarios");

        jLabel3.setFont(new java.awt.Font("Segoe UI", 1, 12)); // NOI18N
        jLabel3.setText("Permiso Nro:");

        jLabel4.setFont(new java.awt.Font("Segoe UI", 1, 12)); // NOI18N
        jLabel4.setText("Dirección:");

        jLabel5.setFont(new java.awt.Font("Segoe UI", 1, 12)); // NOI18N
        jLabel5.setText("Total Presupuestado");

        dire.setFont(new java.awt.Font("Segoe UI", 1, 12)); // NOI18N
        dire.setForeground(new java.awt.Color(153, 153, 153));
        dire.setText("Ej: Av. Italia 2222");

        anno.setFont(new java.awt.Font("Segoe UI", 1, 12)); // NOI18N

        permiso.setFont(new java.awt.Font("Segoe UI", 1, 12)); // NOI18N
        permiso.setForeground(new java.awt.Color(153, 153, 153));
        permiso.setText("524 ");

        jLabel6.setFont(new java.awt.Font("Segoe UI", 1, 12)); // NOI18N
        jLabel6.setText("Comienzo Mes/Año");

        presupuesto.setFont(new java.awt.Font("Segoe UI", 1, 12)); // NOI18N
        presupuesto.setText("0");

        jLabel8.setFont(new java.awt.Font("Segoe UI", 1, 12)); // NOI18N
        jLabel8.setText("$");

        jButton1.setFont(new java.awt.Font("Segoe UI", 1, 12)); // NOI18N
        jButton1.setText("Agregar");
        jButton1.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                jButton1ActionPerformed(evt);
            }
        });

        mes.setFont(new java.awt.Font("Segoe UI", 1, 12)); // NOI18N

        botones.setFont(new java.awt.Font("Segoe UI", 1, 12)); // NOI18N
        panelFuera.setViewportView(botones);

        javax.swing.GroupLayout jPanel2Layout = new javax.swing.GroupLayout(jPanel2);
        jPanel2.setLayout(jPanel2Layout);
        jPanel2Layout.setHorizontalGroup(
            jPanel2Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(jPanel2Layout.createSequentialGroup()
                .addGap(23, 23, 23)
                .addGroup(jPanel2Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING, false)
                    .addGroup(jPanel2Layout.createSequentialGroup()
                        .addGroup(jPanel2Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                            .addComponent(jScrollPane1, javax.swing.GroupLayout.PREFERRED_SIZE, 298, javax.swing.GroupLayout.PREFERRED_SIZE)
                            .addComponent(jLabel2))
                        .addGap(18, 18, 18)
                        .addGroup(jPanel2Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                            .addComponent(jLabel1)
                            .addComponent(jScrollPane2)))
                    .addComponent(panelFuera, javax.swing.GroupLayout.PREFERRED_SIZE, 633, javax.swing.GroupLayout.PREFERRED_SIZE))
                .addGap(35, 35, 35)
                .addGroup(jPanel2Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                    .addComponent(jButton1, javax.swing.GroupLayout.PREFERRED_SIZE, 175, javax.swing.GroupLayout.PREFERRED_SIZE)
                    .addGroup(jPanel2Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING, false)
                        .addComponent(jLabel3)
                        .addComponent(jLabel4)
                        .addGroup(jPanel2Layout.createSequentialGroup()
                            .addComponent(mes, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                            .addGap(18, 18, 18)
                            .addComponent(anno, javax.swing.GroupLayout.PREFERRED_SIZE, 92, javax.swing.GroupLayout.PREFERRED_SIZE))
                        .addComponent(jLabel6)
                        .addComponent(dire)
                        .addComponent(permiso))
                    .addGroup(jPanel2Layout.createSequentialGroup()
                        .addComponent(jLabel5)
                        .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                        .addComponent(jLabel8, javax.swing.GroupLayout.PREFERRED_SIZE, 7, javax.swing.GroupLayout.PREFERRED_SIZE)
                        .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                        .addComponent(presupuesto, javax.swing.GroupLayout.PREFERRED_SIZE, 84, javax.swing.GroupLayout.PREFERRED_SIZE)))
                .addContainerGap(23, Short.MAX_VALUE))
        );
        jPanel2Layout.setVerticalGroup(
            jPanel2Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(jPanel2Layout.createSequentialGroup()
                .addContainerGap(javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                .addGroup(jPanel2Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                    .addComponent(jLabel3, javax.swing.GroupLayout.Alignment.TRAILING)
                    .addGroup(javax.swing.GroupLayout.Alignment.TRAILING, jPanel2Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                        .addComponent(jLabel2, javax.swing.GroupLayout.Alignment.TRAILING)
                        .addComponent(jLabel1)))
                .addGroup(jPanel2Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                    .addGroup(jPanel2Layout.createSequentialGroup()
                        .addGap(65, 65, 65)
                        .addComponent(jLabel4)
                        .addGap(18, 18, 18)
                        .addComponent(dire, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                        .addGap(72, 72, 72)
                        .addGroup(jPanel2Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                            .addComponent(anno, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                            .addComponent(mes, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)))
                    .addGroup(jPanel2Layout.createSequentialGroup()
                        .addGap(18, 18, 18)
                        .addGroup(jPanel2Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                            .addComponent(jScrollPane1, javax.swing.GroupLayout.Alignment.TRAILING, javax.swing.GroupLayout.PREFERRED_SIZE, 195, javax.swing.GroupLayout.PREFERRED_SIZE)
                            .addGroup(javax.swing.GroupLayout.Alignment.TRAILING, jPanel2Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING, false)
                                .addGroup(jPanel2Layout.createSequentialGroup()
                                    .addComponent(permiso, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                                    .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                                    .addComponent(jLabel6)
                                    .addGap(26, 26, 26))
                                .addComponent(jScrollPane2, javax.swing.GroupLayout.PREFERRED_SIZE, 192, javax.swing.GroupLayout.PREFERRED_SIZE)))))
                .addGroup(jPanel2Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING, false)
                    .addGroup(jPanel2Layout.createSequentialGroup()
                        .addGap(29, 29, 29)
                        .addGroup(jPanel2Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                            .addComponent(jLabel5)
                            .addComponent(presupuesto)
                            .addComponent(jLabel8))
                        .addGap(168, 168, 168)
                        .addComponent(jButton1, javax.swing.GroupLayout.PREFERRED_SIZE, 32, javax.swing.GroupLayout.PREFERRED_SIZE))
                    .addGroup(jPanel2Layout.createSequentialGroup()
                        .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                        .addComponent(panelFuera)))
                .addGap(20, 20, 20))
        );

        javax.swing.GroupLayout layout = new javax.swing.GroupLayout(getContentPane());
        getContentPane().setLayout(layout);
        layout.setHorizontalGroup(
            layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(layout.createSequentialGroup()
                .addComponent(jPanel2, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addGap(0, 0, 0))
        );
        layout.setVerticalGroup(
            layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addComponent(jPanel2, javax.swing.GroupLayout.Alignment.TRAILING, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
        );

        pack();
    }// </editor-fold>//GEN-END:initComponents

    private void jButton1ActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_jButton1ActionPerformed
        int nPe;
        try{
            nPe = Integer.parseInt(permiso.getText());
            if(dire.getText().isEmpty() || capataces.getSelectedValue()==null ||
               propietarios.getSelectedValue()==null || dire.getText().equals("Ej: Av Italia 2222")) {
                Vacios fallo = new Vacios();
                fallo.setVisible(true);
            }else if(sistema.getListNumVali().contains(nPe)){
                NumVali fallo = new NumVali();
                fallo.setVisible(true);
            }else{
                String direc = dire.getText();
                Capataz c = sistema.getCapataz(capataces.getSelectedIndex());
                Propietario ow = sistema.getPropietario(propietarios.getSelectedIndex());
                int m = (Integer) mes.getValue();
                int a = (Integer) anno.getValue();
                BigDecimal presu = BigDecimal.valueOf(Integer.parseInt(presupuesto.getText()));
                sistema.getListNumVali().add(nPe);
                Obra ob = new Obra(m, a, direc, nPe, c, ow, presu);
                sistema.setRubrosEnObra(ob);
                sistema.agregarListaObra(ob);
                for(Rubro g : this.sistema.getListaRubros()){
                    g.setPresupuesto(BigDecimal.ZERO);
                }
                this.dispose();
            }
        }catch(NumberFormatException e){
            ciNoNum fallo = new ciNoNum();
            fallo.setVisible(true);
        }
    }//GEN-LAST:event_jButton1ActionPerformed
    
    public ArrayList<JButton> obtenerBotones(){
        ArrayList<JButton> botonesLista = new ArrayList<>();
        
        JViewport viewport = botones.getViewport();
        Component comp = viewport.getView();
        
        if (comp instanceof JPanel) {
            JPanel panel = (JPanel) comp;
            for (Component c : panel.getComponents()) {
                if (c instanceof JButton) {
                    botonesLista.add((JButton) c);
                }
            }
        }
        return botonesLista;
    }
    
    private void formWindowClosing(java.awt.event.WindowEvent evt) {//GEN-FIRST:event_formWindowClosing
        for(Rubro g : this.sistema.getListaRubros()){
            g.setPresupuesto(BigDecimal.ZERO);
        }
    }//GEN-LAST:event_formWindowClosing

    // Variables declaration - do not modify//GEN-BEGIN:variables
    private javax.swing.JSpinner anno;
    private javax.swing.JScrollPane botones;
    private javax.swing.JList<String> capataces;
    private javax.swing.JTextField dire;
    private javax.swing.JButton jButton1;
    private javax.swing.JLabel jLabel1;
    private javax.swing.JLabel jLabel2;
    private javax.swing.JLabel jLabel3;
    private javax.swing.JLabel jLabel4;
    private javax.swing.JLabel jLabel5;
    private javax.swing.JLabel jLabel6;
    private javax.swing.JLabel jLabel8;
    private javax.swing.JPanel jPanel2;
    private javax.swing.JScrollPane jScrollPane1;
    private javax.swing.JScrollPane jScrollPane2;
    private javax.swing.JSpinner mes;
    private javax.swing.JScrollPane panelFuera;
    private javax.swing.JTextField permiso;
    private javax.swing.JLabel presupuesto;
    private javax.swing.JList<String> propietarios;
    // End of variables declaration//GEN-END:variables
    
    @Override
    public void update(Observable o, Object arg) {
        if(arg.equals("rubros")){
            botones();
        }
        if(arg.equals("propietarios")){
            propietarios();
        }
        if(arg.equals("capataces")){
            capataces();
        }
        if(arg.equals("ci")||arg.equals("num")){
            capataces();
            propietarios();
            botones();
        }
        if(arg.equals("presu")){
            BigDecimal presup = BigDecimal.ZERO;
            for(Rubro e : this.sistema.getListaRubros()){
                for(JButton c : obtenerBotones()){
                    int indice = c.getText().indexOf(" -- ");
                    if(indice>-1){
                        if(c.getText().substring(0, indice).equals(e.getNombre()) && e.getPresupuesto().compareTo(BigDecimal.ZERO)>0){
                            c.setText(e.boton());
                            c.setBackground(Color.BLUE);
                        }
                    }else if(c.getText().equals(e.getNombre()) && e.getPresupuesto().compareTo(BigDecimal.ZERO)>0){
                            c.setText(e.boton());
                            c.setBackground(Color.BLUE);
                    }
                }
                presup = presup.add(e.getPresupuesto());
            }
            this.presupuesto.setText(presup.toString());
        }
    }
}

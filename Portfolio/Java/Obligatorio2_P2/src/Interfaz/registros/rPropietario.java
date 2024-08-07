/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/GUIForms/JFrame.java to edit this template
 */
package Interfaz.registros;

import java.util.Observable;
import java.util.Observer;
import Interfaz.Errores.Cedu;
import Interfaz.Errores.Num;
import Interfaz.Errores.ciNoNum;
import Interfaz.Errores.Vacios;
import Modelo.Propietario;
import Modelo.Sistema;
import java.awt.Color;
import javax.swing.text.JTextComponent;

/*
 *
 * @author varo1
*/

public final class rPropietario extends javax.swing.JFrame implements Observer {
    private Sistema sistema;
    /**
     * Creates new form rPropietario
     * @param sistema1
    */
    
    public rPropietario(Sistema sistema1) {
        this.sistema = sistema1;
        initComponents();
        placeHolder(this.cedu, "11111111 ");
        placeHolder(this.direc, "Ej: Av Italia 2222");
        placeHolder(this.nombre, "Nombre Apellido"); 
        placeHolder(this.num, "91111111 ");
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

        jPanel1 = new javax.swing.JPanel();
        jLabel1 = new javax.swing.JLabel();
        jLabel2 = new javax.swing.JLabel();
        jLabel3 = new javax.swing.JLabel();
        jLabel4 = new javax.swing.JLabel();
        num = new javax.swing.JTextField();
        codigoCount = new javax.swing.JComboBox<>();
        direc = new javax.swing.JTextField();
        cedu = new javax.swing.JTextField();
        nombre = new javax.swing.JTextField();
        add = new javax.swing.JButton();

        setDefaultCloseOperation(javax.swing.WindowConstants.DISPOSE_ON_CLOSE);
        setTitle("Registrando Propietario");

        jLabel1.setFont(new java.awt.Font("Segoe UI", 1, 12)); // NOI18N
        jLabel1.setText("Nombre");

        jLabel2.setFont(new java.awt.Font("Segoe UI", 1, 12)); // NOI18N
        jLabel2.setText("Cédula");

        jLabel3.setFont(new java.awt.Font("Segoe UI", 1, 12)); // NOI18N
        jLabel3.setText("Dirección");

        jLabel4.setFont(new java.awt.Font("Segoe UI", 1, 12)); // NOI18N
        jLabel4.setText("Número de celular");

        num.setFont(new java.awt.Font("Segoe UI", 1, 12)); // NOI18N
        num.setForeground(new java.awt.Color(153, 153, 153));
        num.setText("91111111 ");

        codigoCount.setFont(new java.awt.Font("Segoe UI", 1, 12)); // NOI18N
        codigoCount.setForeground(new java.awt.Color(153, 153, 153));
        codigoCount.setModel(new javax.swing.DefaultComboBoxModel<>(new String[] { "+93", "+355", "+213", "+376", "+244", "+54", "+374", "+61", "+43", "+994", "+1-242", "+973", "+880", "+1-246", "+375", "+32", "+501", "+229", "+975", "+591", "+387", "+267", "+55", "+673", "+359", "+226", "+257", "+238", "+855", "+237", "+1", "+235", "+56", "+86", "+57", "+269", "+243", "+242", "+506", "+385", "+53", "+357", "+420", "+45", "+253", "+1-767", "+1-809", "+1-829", "+1-849", "+593", "+20", "+503", "+240", "+291", "+372", "+268", "+251", "+679", "+358", "+33", "+241", "+220", "+995", "+49", "+233", "+30", "+1-473", "+502", "+224", "+245", "+592", "+509", "+504", "+36", "+354", "+91", "+62", "+98", "+964", "+353", "+972", "+39", "+225", "+1-876", "+81", "+962", "+7", "+254", "+686", "+850", "+82", "+965", "+996", "+856", "+371", "+961", "+266", "+231", "+218", "+423", "+370", "+352", "+261", "+265", "+60", "+960", "+223", "+356", "+692", "+222", "+230", "+52", "+691", "+373", "+377", "+976", "+382", "+212", "+258", "+95", "+264", "+674", "+977", "+31", "+64", "+505", "+227", "+234", "+47", "+968", "+92", "+680", "+507", "+675", "+595", "+51", "+63", "+48", "+351", "+974", "+40", "+7", "+250", "+1-869", "+1-758", "+1-784", "+685", "+378", "+239", "+966", "+221", "+381", "+248", "+232", "+65", "+421", "+386", "+677", "+252", "+27", "+34", "+94", "+249", "+597", "+46", "+41", "+963", "+886", "+992", "+255", "+66", "+228", "+676", "+1-868", "+216", "+90", "+993", "+688", "+256", "+380", "+971", "+44", "+1", "+598", "+998", "+678", "+58", "+84", "+967", "+260", "+263" }));

        direc.setFont(new java.awt.Font("Segoe UI", 1, 12)); // NOI18N
        direc.setForeground(new java.awt.Color(153, 153, 153));
        direc.setText("Ej: Av Italia 2222");

        cedu.setFont(new java.awt.Font("Segoe UI", 1, 12)); // NOI18N
        cedu.setForeground(new java.awt.Color(153, 153, 153));
        cedu.setText("11111111 ");

        nombre.setFont(new java.awt.Font("Segoe UI", 1, 12)); // NOI18N
        nombre.setForeground(new java.awt.Color(153, 153, 153));
        nombre.setText("Nombre Apellido");

        add.setFont(new java.awt.Font("Segoe UI", 1, 12)); // NOI18N
        add.setText("Agregar");
        add.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                addActionPerformed(evt);
            }
        });

        javax.swing.GroupLayout jPanel1Layout = new javax.swing.GroupLayout(jPanel1);
        jPanel1.setLayout(jPanel1Layout);
        jPanel1Layout.setHorizontalGroup(
            jPanel1Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(jPanel1Layout.createSequentialGroup()
                .addGap(27, 27, 27)
                .addGroup(jPanel1Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                    .addComponent(add, javax.swing.GroupLayout.PREFERRED_SIZE, 249, javax.swing.GroupLayout.PREFERRED_SIZE)
                    .addComponent(nombre, javax.swing.GroupLayout.PREFERRED_SIZE, 159, javax.swing.GroupLayout.PREFERRED_SIZE)
                    .addComponent(cedu, javax.swing.GroupLayout.PREFERRED_SIZE, 159, javax.swing.GroupLayout.PREFERRED_SIZE)
                    .addComponent(jLabel4)
                    .addComponent(jLabel3)
                    .addComponent(jLabel2)
                    .addComponent(jLabel1)
                    .addGroup(jPanel1Layout.createSequentialGroup()
                        .addComponent(codigoCount, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                        .addGap(18, 18, 18)
                        .addComponent(num, javax.swing.GroupLayout.PREFERRED_SIZE, 159, javax.swing.GroupLayout.PREFERRED_SIZE))
                    .addComponent(direc, javax.swing.GroupLayout.PREFERRED_SIZE, 159, javax.swing.GroupLayout.PREFERRED_SIZE))
                .addGap(27, 27, 27))
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
                .addGroup(jPanel1Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                    .addComponent(num, javax.swing.GroupLayout.PREFERRED_SIZE, 31, javax.swing.GroupLayout.PREFERRED_SIZE)
                    .addComponent(codigoCount, javax.swing.GroupLayout.PREFERRED_SIZE, 31, javax.swing.GroupLayout.PREFERRED_SIZE))
                .addGap(33, 33, 33)
                .addComponent(add, javax.swing.GroupLayout.PREFERRED_SIZE, 31, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addGap(20, 20, 20))
        );

        javax.swing.GroupLayout layout = new javax.swing.GroupLayout(getContentPane());
        getContentPane().setLayout(layout);
        layout.setHorizontalGroup(
            layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addComponent(jPanel1, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
        );
        layout.setVerticalGroup(
            layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addComponent(jPanel1, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
        );

        pack();
    }// </editor-fold>//GEN-END:initComponents

    private void addActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_addActionPerformed
        int ci;
        int f; 
        String celu;
        //no se especifica que se tenga que validar que sea unico o algo
        String name;
        String direction;
        try{
            ci = Integer.parseInt(this.cedu.getText());
            try{
                f = Integer.parseInt(this.num.getText());
            }catch(NumberFormatException e){
                Num fallo = new Num();
                fallo.setVisible(true);
            }
            celu = " " + this.codigoCount.getSelectedItem() + " " + this.num.getText();
            if(this.nombre.getText().isEmpty() || this.direc.getText().isEmpty() || this.num.getText().equals("91111111 ") ||
               this.nombre.getText().equals("Nombre Apellido") || this.direc.getText().equals("Ej: Av Italia 2222")) {
                Vacios fallo = new Vacios();
                fallo.setVisible(true);
            }else if(this.sistema.getListaCedulas().contains(ci)){
                Cedu fallo = new Cedu();
                fallo.setVisible(true);
            }else{
                name = this.nombre.getText();
                direction = this.direc.getText();
                this.sistema.getListaCedulas().add(ci);
                Propietario propietario = new Propietario(name, ci, direction, celu);
                this.sistema.agregarListaPropietario(propietario);
                this.dispose();
            }    
        }catch(NumberFormatException e){
            ciNoNum fallo = new ciNoNum();
            fallo.setVisible(true);
        }
    }//GEN-LAST:event_addActionPerformed

    // Variables declaration - do not modify//GEN-BEGIN:variables
    private javax.swing.JButton add;
    private javax.swing.JTextField cedu;
    private javax.swing.JComboBox<String> codigoCount;
    private javax.swing.JTextField direc;
    private javax.swing.JLabel jLabel1;
    private javax.swing.JLabel jLabel2;
    private javax.swing.JLabel jLabel3;
    private javax.swing.JLabel jLabel4;
    private javax.swing.JPanel jPanel1;
    private javax.swing.JTextField nombre;
    private javax.swing.JTextField num;
    // End of variables declaration//GEN-END:variables

    @Override
    public void update(Observable o, Object arg) {
        //no es abstracto sino
    }
}

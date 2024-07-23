/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Class.java to edit this template
 */
package Modelo;

import java.io.Serializable;
import java.math.BigDecimal;

/**
 *
 * @author varo1
 */
public class Gasto implements Serializable{
    private static final long serialVersionUID = 1L;
    private BigDecimal monto;
    private int mes;
    private int anno;
    private String description;

    public BigDecimal getMonto() {
        return monto;
    }

    public void setMonto(BigDecimal monto) {
        this.monto = monto;
    }

    public int getMes() {
        return mes;
    }

    public void setMes(int mes) {
        this.mes = mes;
    }

    public int getAnno() {
        return anno;
    }

    public void setAnno(int anno) {
        this.anno = anno;
    }

    public String getDescription() {
        return description;
    }

    public void setDescription(String description) {
        this.description = description;
    }
    
    public Gasto(){
        this.anno=0;
        this.description="";
        this.mes = 0;
        this.monto = BigDecimal.ZERO;
    }
    
    public Gasto(BigDecimal m, int me, String d, int a){
        this.monto=m;
        this.mes=mes;
        this.description=d;
        this.anno=a;
    }
    
    @Override
    public String toString(){
        return ""+this.monto;
    }
    
}

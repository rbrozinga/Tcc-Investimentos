package com.example.pimmobile;


import java.io.Serializable;

public class Conta implements Serializable {

    private String numero;
    private String saldo;
    private String investimento;
    private String saldoFormatado;
    private String investimentoFormatado;


    public String getNumero() {
        return numero;
    }

    public void setNumero(String numero) {
        this.numero = numero;
    }

    public String getSaldo() {
        return saldo;
    }

    public void setSaldo(String saldo) {
        this.saldo = saldo;
    }

    public String getInvestimento() {
        return investimento;
    }

    public void setInvestimento(String investimento) {
        this.investimento = investimento;
    }

    public String getSaldoFormatado() {
        return saldoFormatado;
    }

    public void setSaldoFormatado(String saldoFormatado) {
        this.saldoFormatado = saldoFormatado;
    }

    public String getInvestimentoFormatado() {
        return investimentoFormatado;
    }

    public void setInvestimentoFormatado(String investimentoFormatado) {
        this.investimentoFormatado = investimentoFormatado;
    }



    @Override
    public String toString() {
        return "Conta{" +
                "numero=" + numero +
                ", saldo=" + saldo +
                ", investimento=" + investimento +
                '}';
    }
}

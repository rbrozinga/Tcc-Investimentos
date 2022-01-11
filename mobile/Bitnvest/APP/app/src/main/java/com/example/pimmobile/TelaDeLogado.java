package com.example.pimmobile;

import androidx.appcompat.app.AppCompatActivity;

import android.os.Bundle;
import android.util.Log;
import android.widget.EditText;
import android.widget.TextView;

public class TelaDeLogado extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_tela_de_logado);

        TextView saldoCliente = findViewById(R.id.saldoCliente);
        TextView  saldoInvestido= findViewById(R.id.saldoInvestido);

        if(getIntent().getExtras() != null) {
            Conta conta = (Conta) getIntent().getSerializableExtra("conta");
            saldoCliente.setText(conta.getSaldoFormatado().toString());
            saldoInvestido.setText(conta.getInvestimentoFormatado().toString());
            Log.i("conta", conta.getSaldoFormatado());
        }
    }
}
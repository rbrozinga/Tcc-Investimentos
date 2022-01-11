package com.example.pimmobile;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;

public class TelaCadastro extends AppCompatActivity {

    private EditText NameCadastro;
    private EditText EmailCadastro;
    private EditText PhoneCadastro;
    private EditText CpfCadastro;
    private EditText SenhaCadastro;
    private EditText ConfirmarSenhaCadastro;
    private Button buttonEnviar;


    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_tela_cadastro);

        buttonEnviar = (Button) findViewById(R.id.buttonEnviar);

        buttonEnviar.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Intent intent = new Intent(TelaCadastro.this,TelaDeLogado.class);
                startActivity(intent);
            }
        });
    }
}
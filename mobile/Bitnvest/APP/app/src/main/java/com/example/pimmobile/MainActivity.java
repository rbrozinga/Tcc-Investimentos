package com.example.pimmobile;

import androidx.appcompat.app.AppCompatActivity;

import android.annotation.SuppressLint;
import android.content.Intent;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;

import java.io.IOException;
import java.nio.charset.MalformedInputException;
import java.util.concurrent.ExecutionException;

public class MainActivity extends AppCompatActivity {

    private EditText editTextTextEmailAddress;
    private EditText editTextTextPassword2;
    private Button buttonLogin;
    private Button buttonCadastro;


    @SuppressLint("WrongViewCast")
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        final EditText emailLogin = findViewById(R.id.emailLogin);

        buttonLogin = (Button) findViewById(R.id.buttonLogin);
        buttonCadastro = (Button) findViewById(R.id.buttonCadastro);
        editTextTextEmailAddress = (EditText) findViewById(R.id.emailLogin);
        editTextTextPassword2 = (EditText) findViewById(R.id.editTextTextPassword2);


        buttonLogin.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Intent intent = new Intent(MainActivity.this,TelaDeLogado.class);
                HTTPService service = new HTTPService(emailLogin.getText().toString());
                try {
                    Conta conta = service.execute().get();
                    intent.putExtra("conta", conta);
                    startActivity(intent);
                } catch (InterruptedException e) {
                    e.printStackTrace();
                }catch (ExecutionException e){
                    e.printStackTrace();
                }


            }

        });

        buttonCadastro.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {

                Intent intent = new Intent(MainActivity.this,TelaCadastro.class);
                startActivity(intent);
            }
        });


    }
}
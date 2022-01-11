package com.example.pimmobile;
import android.os.AsyncTask;

import com.google.gson.Gson;

import java.io.IOException;
import java.net.HttpURLConnection;
import java.net.URL;
import java.nio.charset.MalformedInputException;
import java.util.Scanner;


public class HTTPService extends AsyncTask <Void, Void, Conta> {

    private final String email;

    public HTTPService(String email) {
        this.email = email;
    }

    @Override
    protected Conta doInBackground(Void... voids) {
        StringBuilder resposta = new StringBuilder();
       try {
           URL url = new URL("https://1aa849fe89db.ngrok.io/" + this.email);
           HttpURLConnection connection = (HttpURLConnection) url.openConnection();
           connection.setRequestMethod("GET");
           connection.setRequestProperty("Accept", "application/json");
           connection.setConnectTimeout(5000);
           connection.connect();

           Scanner scanner = new Scanner(url.openStream());
           while(scanner.hasNext()) {
               resposta.append(scanner.next());
           }

       } catch (MalformedInputException e) {
           e.printStackTrace();
       }catch (IOException e){
         e.printStackTrace();
       }

       return new Gson().fromJson(resposta.toString(),Conta.class);

    }

}

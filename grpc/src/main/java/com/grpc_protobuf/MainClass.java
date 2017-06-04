package com.grpc_protobuf;
import java.io.*;

public class MainClass {
    public static void main(String[] args) throws IOException {
        BufferedReader br = new BufferedReader(new InputStreamReader(System.in));

        System.out.println("Введите номер хоста: ");
        final String host = br.readLine();
        System.out.println("Введите номер порта: ");
        final  int port = Integer.valueOf(br.readLine());
        System.out.println("Введите номер хоста назначения: ");
        final String host_to_connect = br.readLine();
        System.out.println("Введите номер порта назначения: ");
        final int port_to_connect = Integer.valueOf(br.readLine());
        System.out.println("Введите ваше имя: ");
        final String userName = br.readLine();


        final IMessageEndpoint user = new MessageEndpoint(port, host, port_to_connect, host_to_connect, userName);
        while (true) {
            String s = br.readLine();
            if (s == null) {
                System.out.println("<Chat is closed>");
                return;
            }
            user.sendMessage(s);
        }

    }
}

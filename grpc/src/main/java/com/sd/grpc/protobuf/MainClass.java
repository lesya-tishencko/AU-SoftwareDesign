package com.sd.grpc.protobuf;
import java.io.*;

public class MainClass {
    public static void main(String[] args) throws IOException {
        BufferedReader br = new BufferedReader(new InputStreamReader(System.in));

        System.out.println("Введите IP-адрес: ");
        final String host = br.readLine();
        System.out.println("Введите номер порта: ");
        final  int port = Integer.valueOf(br.readLine());
        System.out.println("Введите IP-адрес назначения: ");
        final String hostToConnect = br.readLine();
        System.out.println("Введите номер порта назначения: ");
        final int portToConnect = Integer.valueOf(br.readLine());
        System.out.println("Введите ваше имя: ");
        final String userName = br.readLine();


        final IMessageEndpoint user = new MessageEndpoint(port, host, portToConnect, hostToConnect, userName);
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

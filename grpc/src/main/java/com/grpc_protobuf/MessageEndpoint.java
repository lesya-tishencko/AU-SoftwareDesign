package com.grpc_protobuf;

import com.com.grpc_protobuf.MessageOuterClass;
import com.com.grpc_protobuf.MessageServerGrpc;
import io.grpc.*;

import java.io.IOException;
import java.text.SimpleDateFormat;
import java.util.Date;
import java.util.logging.Level;
import java.util.logging.Logger;

import static java.lang.System.exit;

interface IMessageEndpoint {
    void onMessageReceived(final MessageOuterClass.Message message);
    void sendMessage(final String messageContent);
}

class MessageEndpoint implements IMessageEndpoint {
    private final Server server;
    private ManagedChannel channel = null;
    private MessageServerGrpc.MessageServerBlockingStub blockingStub = null;

    private static final Logger logger = Logger.getLogger(MessageEndpoint.class.getName());

    private final String host;
    private final String connectToHost;
    private final int connectToPort;
    private final String userName;

    MessageEndpoint(final int port,
                    final String host,
                    final int connectToPort,
                    final String connectToHost,
                    final String userName)
    {
        logger.info("Trying to connect to " + connectToHost + ":" + connectToPort);
        server = ServerBuilder.forPort(port)
                .addService(new MessageServerImpl(this))
                .build();
        try {
            server.start();
        } catch (Exception exc) {
            logger.log(Level.WARNING, exc.toString());
        }

        logger.info("Server started, listening on " + port);
        Runtime.getRuntime().addShutdownHook(new Thread() {
            @Override
            public void run() {
                // Use stderr here since the logger may have been reset by its JVM shutdown hook.
                System.err.println("*** shutting down gRPC server since JVM is shutting down");
                MessageEndpoint.this.stop();
                System.err.println("*** server shut down");
            }
        });

        this.host = host;
        this.connectToHost = connectToHost;
        this.connectToPort = connectToPort;
        this.userName = userName;
    }

    @Override
    public void sendMessage(final String messageContent) {
        logger.info("Trying to sendMessage (" + messageContent + ") to " + connectToHost + ":" + connectToPort);
        final MessageOuterClass.Message message =
                MessageOuterClass.Message.newBuilder()
                        .setName(userName)
                        .setDateTime(new Date().getTime())
                        .setMessageContent(messageContent)
                        .build();

        if (blockingStub == null) {
            channel = ManagedChannelBuilder.forAddress(connectToHost, connectToPort).usePlaintext(true).build();
            blockingStub = MessageServerGrpc.newBlockingStub(channel);
        }

        try {
            blockingStub.sendMessage(message);
        } catch (StatusRuntimeException exc) {
            logger.log(Level.WARNING, exc.toString());
        }
    }

    @Override
    public void onMessageReceived(MessageOuterClass.Message message) {

        final Date date = new Date(message.getDateTime());
        SimpleDateFormat dateFormat = new SimpleDateFormat("dd.MM HH:mm:ss");
        final String formattedDate = dateFormat.format(date);
        System.out.println(message.getName() + "(" + formattedDate + "): " + message.getMessageContent());


    }

    private void stop() {
        if (server != null) {
            server.shutdown();
        }
    }

}

package com.sd.grpc.protobuf;

/**
 * Interface for handle messenges
 */
interface IMessageEndpoint {
    void onMessageReceived(final MessageOuterClass.Message message);
    void sendMessage(final String messageContent);
}
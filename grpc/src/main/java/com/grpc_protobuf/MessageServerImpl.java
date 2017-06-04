package com.grpc_protobuf;


import com.com.grpc_protobuf.MessageOuterClass;
import com.com.grpc_protobuf.MessageServerGrpc;
import io.grpc.stub.StreamObserver;

import java.util.logging.Logger;

class MessageServerImpl extends MessageServerGrpc.MessageServerImplBase {

    private final IMessageEndpoint messageEndpoint;

    MessageServerImpl(IMessageEndpoint messageEndpoint) {
        super();
        this.messageEndpoint = messageEndpoint;
    }

    private static final Logger logger = Logger.getLogger(MessageServerImpl.class.getName());
    @Override
    public void sendMessage(MessageOuterClass.Message request,
                            StreamObserver<MessageOuterClass.Answer> responseObserver)
    {
        messageEndpoint.onMessageReceived(request);
        final MessageOuterClass.Answer answer =
                MessageOuterClass.Answer.newBuilder().setAnswerStatus(1).build();

        responseObserver.onNext(answer);
        responseObserver.onCompleted();
    }
}

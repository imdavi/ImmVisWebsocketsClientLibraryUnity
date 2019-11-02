using System;

public delegate void ClientConnectedAction();
public delegate void ClientDisconnectedAction();
public delegate void MessageReceivedAction(Message message);
public delegate void ClientErrorAction(Exception exception);
public delegate void RawMessageReceivedAction(String message);

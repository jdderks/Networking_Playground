using System;
using Unity.Networking.Transport;
using UnityEngine;


public enum OpCode
{ //Don't go over a full byte size for each opcode (a byte bigger than 255)
    KEEP_ALIVE = 1,
    WELCOME = 2,
    START_GAME = 3,
    MOVE = 4,
    SHOOT = 5,
    GAMEOVER = 6,
    REMATCH = 7
}
public static class NetUtility
{
    public static void OnData(DataStreamReader stream, NetworkConnection cnn, Server server = null)
    {
        NetMessage msg = null;
        var opCode = (OpCode)stream.ReadByte();
        switch (opCode)
        {
            case OpCode.KEEP_ALIVE:
                msg = new NetKeepAlive(stream);
                break;
            //case OpCode.WELCOME:
            //    msg = new NetWelcome(stream);
            //    break;
            //case OpCode.START_GAME:
            //    msg = new NetStartGame(stream);
            //    break;
            //case OpCode.MOVE:
            //    msg = new NetMove(stream);
            //    break;
            //case OpCode.SHOOT:
            //    msg = new NetShoot(stream);
            //    break;
            //case OpCode.GAMEOVER:
            //    msg = new NetGameOver(stream);
            //    break;
            //case OpCode.REMATCH:
            //    msg = new NetRematch(stream);
            //    break;
            default:
                Debug.LogError("Message received had no OpCode.");
                break;
        }

        if (server != null)
        {
            msg.ReceivedOnServer(cnn);
        }
        else
        {
            msg.ReceivedOnClient();
        }
    }


    //Clientside actions
    public static Action<NetMessage> C_KEEP_ALIVE;
    public static Action<NetMessage> C_WELCOME;
    public static Action<NetMessage> C_MOVE;
    public static Action<NetMessage> C_SHOOT;
    public static Action<NetMessage> C_GAMEOVER;
    public static Action<NetMessage> C_REMATCH;

    //Serverside actions with whomever sent it
    public static Action<NetMessage, NetworkConnection> S_KEEP_ALIVE;
    public static Action<NetMessage, NetworkConnection> S_WELCOME;
    public static Action<NetMessage, NetworkConnection> S_MOVE;
    public static Action<NetMessage, NetworkConnection> S_SHOOT;
    public static Action<NetMessage, NetworkConnection> S_GAMEOVER;
    public static Action<NetMessage, NetworkConnection> S_REMATCH;
}

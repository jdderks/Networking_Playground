using System;
using Unity.Collections;
using Unity.Networking.Transport;
using UnityEngine;

public class Server : MonoBehaviour
{
    #region SingleTon
    public static Server Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }
    #endregion

    public NetworkDriver driver;
    private NativeList<NetworkConnection> connections;

    private bool isActive = false;

    //For sending a message every 20 seconds to otherwise disconnect
    private const float keepAliveTickRate = 20.0f;
    private float lastKeepAlive;

    private Action connectionDropped;


    //Methods
    public void Init(ushort port)
    {
        driver = NetworkDriver.Create();
        NetworkEndPoint endpoint = NetworkEndPoint.AnyIpv4;
        endpoint.Port = port;

        if (driver.Bind(endpoint) != 0)
        {
            Debug.Log("Unable to bind on port + " + endpoint.Port);
            return;
        }
        else
        {
            Debug.Log("Currently listening on port " + endpoint.Port);
            driver.Listen();
        }

        //4 for max amount of players
        connections = new NativeList<NetworkConnection>(4, Allocator.Persistent);
        isActive = true;
    }
    public void Shutdown()
    {
        if (isActive)
        {
            driver.Dispose();
            connections.Dispose();
            isActive = false;
        }
    }
    public void OnDestroy()
    {
        Shutdown();
    }

    public void Update()
    {
        if (!isActive)
        {
            return;
        }

        //keepAlive();

        driver.ScheduleUpdate().Complete();

        CleanupConnections(); //Is there anybody still connected but doesn't have a reference
        AcceptNewConnections(); //Is there something knocking on the door to enter server
        UpdateMessagePump(); //Are they sending us a message if so we have to reply
    }
    private void CleanupConnections()
    {
        for (int i = 0; i < connections.Length; i++)
        {
            if (!connections[i].IsCreated) //If connection is dropped
            {
                connections.RemoveAtSwapBack(i);
                --i;
            }
        }
    }
    private void AcceptNewConnections()
    {
        NetworkConnection c;
        while ((c = driver.Accept()) != default(NetworkConnection))
        {
            connections.Add(c);
        }
    }
    private void UpdateMessagePump()
    {
        DataStreamReader streamReader;
        for (int i = 0; i < connections.Length; i++)
        {
            NetworkEvent.Type cmd;
            while ((cmd = driver.PopEventForConnection(connections[i], out streamReader)) != NetworkEvent.Type.Empty)
            {
                if (cmd == NetworkEvent.Type.Data)
                {
                    NetUtility.OnData(streamReader, connections[i], this);
                } 
                else if (cmd == NetworkEvent.Type.Disconnect) //If a player disconnects
                {
                    Debug.Log("Client disconnected from server");
                    connections[i] = default(NetworkConnection);
                    connectionDropped?.Invoke();
                    //Shutdown(); //Not used because server will shut down when one player disconnects
                }
            }
        }
    }



    //Server specific
    public void SendToClient(NetworkConnection connection, NetMessage msg)
    {
        DataStreamWriter streamWriter;
        driver.BeginSend(connection, out streamWriter);
        msg.Serialize(ref streamWriter);
        driver.EndSend(streamWriter);
    }

    private void Broadcast(NetMessage msg)
    {
        for (int i = 0; i < connections.Length; i++)
        {
            if (connections[i].IsCreated)
            {
                Debug.Log($"Sending {msg.Code} to : {connections[i].InternalId}");
                SendToClient(connections[i], msg);
            }
        }
    }
}

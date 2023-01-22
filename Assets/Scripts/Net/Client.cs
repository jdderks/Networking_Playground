using System;
using Unity.Collections;
using Unity.Networking.Transport;
using UnityEditor.MemoryProfiler;
using UnityEngine;

public class Client : MonoBehaviour
{
    #region SingleTon
    public static Client Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }
    #endregion


    public NetworkDriver driver;
    private NetworkConnection connection;

    private bool isActive = false;

    private Action connectionDropped;

    public void Init(string ip, ushort port)
    {
        driver = NetworkDriver.Create();
        NetworkEndPoint endpoint = NetworkEndPoint.Parse(ip, port);
        endpoint.Port = port;

        connection = driver.Connect(endpoint);
        isActive = true;

        RegisterToEvent();
    }
    public void Shutdown()
    {
        if (isActive)
        {
            UnregisterToEvent();
            driver.Dispose();
            connection = default(NetworkConnection);
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

        driver.ScheduleUpdate().Complete();
        CheckAlive();
        UpdateMessagePump(); //Are they sending us a message if so we have to reply
    }

    private void CheckAlive()
    {
        if (!connection.IsCreated && isActive)
        {
            Debug.Log("Something went wrong, lost connection to server.");
            connectionDropped?.Invoke();
            Shutdown();
        }
    }

    private void UpdateMessagePump()
    {
        DataStreamReader streamReader;
        NetworkEvent.Type cmd;

        while ((cmd = connection.PopEvent(driver, out streamReader)) != NetworkEvent.Type.Empty)
        {
            if (cmd == NetworkEvent.Type.Connect)
            {
                //SendToServer(new NetWelcome());
            }
            else if (cmd == NetworkEvent.Type.Data) 
            {
                NetUtility.OnData(streamReader, default(NetworkConnection));
            }
            else if (cmd == NetworkEvent.Type.Disconnect) //If a player disconnects
            {
                Debug.Log("Client got disconnected from server.");
                connection = default(NetworkConnection);
                connectionDropped?.Invoke();
                Shutdown();
            }
        }
    }

    public void SendToServer(NetMessage msg)
    {
        DataStreamWriter streamWriter;
        driver.BeginSend(connection, out streamWriter);
        msg.Serialize(ref streamWriter);
        driver.EndSend(streamWriter);
    }

    //Event parsing
    private void RegisterToEvent()
    {
        NetUtility.C_KEEP_ALIVE += OnKeepAlive;
    }

    private void UnregisterToEvent()
    {
        NetUtility.C_KEEP_ALIVE -= OnKeepAlive;
    }

    private void OnKeepAlive(NetMessage msg)
    {
        SendToServer(msg);
    }
}

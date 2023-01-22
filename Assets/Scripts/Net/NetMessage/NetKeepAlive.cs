using Unity.Networking.Transport;

//For this class I've followed a tutorial from "Epitome" on YouTube
//Link: https://www.youtube.com/watch?v=lPoiTw0qjtc&list=PLmcbjnHce7SeAUFouc3X9zqXxiPbCz8Zp&index=11


public class NetKeepAlive : NetMessage
{
    public NetKeepAlive()
    {
        Code = OpCode.KEEP_ALIVE;
    }

    public NetKeepAlive(DataStreamReader streamReader)
    {
        Code = OpCode.KEEP_ALIVE;
        Deserialize(streamReader);
    }

    public override void Serialize(ref DataStreamWriter streamWriter)
    {
        streamWriter.WriteByte((byte)Code);
    }

    public override void Deserialize(DataStreamReader streamReader)
    {
        //Don't have to deserialize anything because of KeepAlive only has to exist.
    }

    public override void ReceivedOnClient()
    {
        NetUtility.C_KEEP_ALIVE?.Invoke(this);
    }

    public override void ReceivedOnServer(NetworkConnection cnn)
    {
        NetUtility.S_KEEP_ALIVE?.Invoke(this,cnn);
    }
}

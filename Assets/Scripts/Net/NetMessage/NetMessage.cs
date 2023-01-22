using Unity.Networking.Transport;


public class NetMessage
{
    public OpCode Code { set; get; }

    public virtual void Serialize(ref DataStreamWriter streamWriter)
    {
        streamWriter.WriteByte((byte)Code);
    }

    public virtual void Deserialize(DataStreamReader streamReader)
    {
        streamReader.ReadByte();
    }

    public virtual void ReceivedOnClient()
    {

    }

    public virtual void ReceivedOnServer(NetworkConnection cnn)
    {

    }
}

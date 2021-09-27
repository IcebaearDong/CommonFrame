public class MsgCreateRole : MsgBase
{
    public MsgCreateRole() { protoName = "MsgCreateRole"; }
    public string name = "";
    public byte result;
}

public class MsgAddPlayer : MsgBase
{
    public MsgAddPlayer() { protoName = "MsgAddPlayer"; }
    public string name = string.Empty;
    public string id = string.Empty;
}

public class MsgChangeName : MsgBase
{
    public MsgChangeName() { protoName = "MsgChangeName"; }
    public string name = string.Empty;
}

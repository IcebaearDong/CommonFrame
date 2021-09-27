using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net.Sender
{
    class PlayerMsgSender
    {
        public static void CreateRole(string name)
        {
            MsgCreateRole msg = new MsgCreateRole();
            msg.name = name;
            NetManager.Send(msg);
        }

        public static void ChangeName(string name)
        {
            MsgChangeName msg = new MsgChangeName();
            msg.name = name;
            NetManager.Send(msg);
        }
    }
}

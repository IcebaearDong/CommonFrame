using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using PlayerSystem;
using SceneSystem;
using UISysyem;

namespace Net.MsgRecevers
{
    public static class PlayerMsgRecever
    {
        public static void RegisterMsgEvent()
        {
            NetManager.AddMsgListener("MsgLogin", OnMsgLogin);
            NetManager.AddMsgListener("MsgRegister", OnMsgRegister);
            NetManager.AddMsgListener("MsgAddPlayer", OnMsgAddPlayer);
            NetManager.AddMsgListener("MsgChangeName", OnMsgChangeName);
        }

        // 更改名字
        private static void OnMsgChangeName(MsgBase msgBase)
        {
            MsgChangeName msg = (MsgChangeName)msgBase;
            Player player = PlayerManager.Inst.GetMainPlayer();
            player.name = msg.name;
            //EventMgr.Dispatch(EventsType.ChangeName);
        }

        // 创建角色消息
        private static void OnMsgAddPlayer(MsgBase msgBase)
        {
            MsgAddPlayer msg = (MsgAddPlayer)msgBase;
            Player player = PlayerManager.Inst.BuildPlayer(msg);
            PlayerManager.Inst.AddPlayer(player);
            SceneMgr.Inst.ChangeScene(new MainScene());
            BroadcastMgr.Inst.AddBroadcast("创建角色成功");
        }

        // 注册消息
        private static void OnMsgRegister(MsgBase msgBase)
        {
            MsgRegister msg = (MsgRegister)msgBase;
            RegisterData data = new RegisterData(msg.id, msg.pw, msg.result);
            //EventMgr.Dispatch(EventsType.RegisterResult, data);
        }

        // 登录消息
        private static void OnMsgLogin(MsgBase msgBase)
        {
            MsgLogin msg = (MsgLogin)msgBase;
            LoginData data = new LoginData(msg.id, msg.pw, (byte)msg.result);
            //EventMgr.Dispatch(EventsType.LoginResult, data);
        }


    }
}

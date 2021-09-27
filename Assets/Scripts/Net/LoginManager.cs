using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace Net
{
    class LoginManager : Singleton<LoginManager>
    {

        public void Login(string account, string password)
        {
            MsgLogin msg = new MsgLogin();
            msg.id = account;
            msg.pw = password;
            NetManager.Send(msg);
        }
        public void Register()
        {

        }
    }
}

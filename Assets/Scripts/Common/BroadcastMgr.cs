using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class BroadcastMgr : Singleton<BroadcastMgr>
    {
        public void AddBroadcast(string msg)
        {
            EventMgr.Dispatch(EventsType.SendBroadcast, msg);
        }
    }
}

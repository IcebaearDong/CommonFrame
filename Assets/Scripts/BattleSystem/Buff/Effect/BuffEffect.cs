using Common;
using LivedObjectSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleSystem.Buff
{
    public class BuffEffect
    {
        public virtual void Do(BuffData data, List<LivedObject> targets)
        {
        }
    }
}

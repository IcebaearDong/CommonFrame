using LivedObjectSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleSystem.Buff.Selector
{
    public abstract class BuffTargetSelector
    {
        public abstract List<LivedObject> GetTargets(BuffData data);
    }
}

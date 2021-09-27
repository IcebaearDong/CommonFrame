using Common;
using LivedObjectSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleSystem.Buff.Selector
{
    class Self : BuffTargetSelector
    {
        public override List<LivedObject> GetTargets(BuffData data)
        {
            return new List<LivedObject>() { data.owner };
        }
    }
}

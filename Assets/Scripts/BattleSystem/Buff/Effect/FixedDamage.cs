using LivedObjectSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleSystem.Buff.Effect
{
    class FixedDamage : BuffEffect
    {
        public override void Do(BuffData data, List<LivedObject> targets)
        {
            targets.ForEach(t => t.CurHp -= data.cfg.Value);
        }
    }
}

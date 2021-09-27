using LivedObjectSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleSystem.Buff.Selector
{
    class Enemy : BuffTargetSelector
    {
        public override List<LivedObject> GetTargets(BuffData data)
        {
            return BattleMgr.Inst.CurRoomData.GetAllLivedObjs(!(data.owner is HeroData));
        }
    }
}

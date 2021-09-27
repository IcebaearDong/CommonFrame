using BattleSystem;
using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace UISysyem.Components
{
    class MonsterComponent : MonoBehaviour, IDropable
    {
        private MonsterData _data;
        public void Init(MonsterData data)
        {
            _data = data;
        }
        public void OnDrop(GameObject obj)
        {
            if (!BattleMgr.Inst.IsRoomStart())
            {
                BroadcastMgr.Inst.AddBroadcast(Lage.Get(LageType.CantAttack));
                return;
            }

            HeroData heroData = Binder.Get<HeroData>(obj);
            heroData.Attacking(_data);
        }
    }
}

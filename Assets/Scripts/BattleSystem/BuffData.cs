using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Common;
using BattleSystem.Buff;
using LivedObjectSystem;

namespace BattleSystem
{
    public class BuffData
    {
        // 属主
        public LivedObject owner { get; set; }
        public BuffExcelItem cfg { get; set; }
        public int ID { get => cfg.id; }
        public BuffData(int id, LivedObject obj)
        {
            cfg = ExlMgr.Inst.GetBuffCfg(id);
            owner = obj;
        }

        public void Effect(LivedObject enemy)
        {
            BuffEffector effector = new BuffEffector(cfg.Effect);
            effector.Do(this, enemy);
            Debug.Log(cfg.Name);
        }
    }
}

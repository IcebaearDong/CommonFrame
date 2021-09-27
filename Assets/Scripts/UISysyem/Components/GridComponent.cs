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
    public class GridComponent : MonoBehaviour, IDropable
    {
        private GridData _gridData;
        public void Init(GridData data)
        {
            _gridData = data;
        }

        public void OnDrop(GameObject obj)
        {
            HeroData data = Binder.Get<HeroData>(obj);

            if (!data.IsInGrid()) // 如果被鼠标拖动的英雄从等待区
            {
                if (_gridData.Data == null)  // 如果格子没有英雄
                {
                    GameMgr.Inst.GameData.WaitHeros.Remove(data.ID);
                    data.Pos = _gridData.Pos;
                    _gridData.Data = data;
                }
                else // 如果格子有英雄
                {
                    HeroData lastData = (HeroData)_gridData.Data;
                    lastData.Pos = null;
                    GameMgr.Inst.GameData.WaitHeros.Add(lastData.ID);
                    _gridData.Data = data;
                    data.Pos = _gridData.Pos;
                }
            }
            else // 如果被鼠标拖动的英雄在格子内
            {
                if (_gridData.Data == null) // 如果目标格子没有英雄
                {
                    BattleMgr.Inst.CurRoomData.GetGridData((Pos)data.Pos).Data = null;
                    _gridData.Data = data;
                    data.Pos = _gridData.Pos;
                }
                else// 如果目标格子有英雄
                {
                    HeroData lastData = (HeroData)_gridData.Data;
                    Pos lastPos = (Pos)data.Pos;

                    _gridData.Data = data;
                    data.Pos = _gridData.Pos;

                    GridData lastGrid = BattleMgr.Inst.CurRoomData.GetGridData(lastPos);
                    lastData.Pos = lastPos;
                    lastGrid.Data = lastData;
                }
            }

            GameMgr.Inst.SaveGame();
        }
    }
}

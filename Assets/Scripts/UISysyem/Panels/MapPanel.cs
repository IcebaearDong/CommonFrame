using BattleSystem;
using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace UISysyem.Panels
{
    class MapPanel : IPanel
    {
        public override PanelType Type => PanelType.FullScreen;
        private GridLayoutGroup _grid;
        private Button _btnReturn;

        public override void OnAwake()
        {
            _grid = gameObject.FindComponent<GridLayoutGroup>("ScrollView/Viewport/Grid");
            _btnReturn = gameObject.FindComponent<Button>("BtnReturn");

            UIEventListener.Get(_btnReturn.gameObject).onClick = OnReturn;
            EventMgr.AddListener(EventsType.WinRoom, OnWinRoom);
        }
        public override void OnStart(params object[] args)
        {
            ShowPanel();
        }

        public override void OnUpdate()
        {

        }
        public override void OnClose()
        {

        }

        // 显示页面
        private void ShowPanel()
        {
            List<RoomData> list = BattleMgr.Inst.GetMapList();
            for (int i = 0; i < list.Count; i++)
            {
                GameObject item = i < _grid.transform.childCount ? _grid.transform.GetChild(i).gameObject
                    : ResMgr.Inst.LoadUICompoent(UIPrefabType.Items, "MapItem", PanelEnum.MapPanel);

                if (i >= _grid.transform.childCount)
                    item.SetParent(_grid.gameObject, Vector3.zero);

                RoomData data = list[i];
                ShowMapItem(item, data, i);
            }
        }

        // 显示地图物品
        private void ShowMapItem(GameObject uiItem, RoomData data, int idx)
        {
            uiItem.FindComponent<Text>("Name").text = data.type.ToString();
            uiItem.GetComponent<Button>().interactable = BattleMgr.Inst.CanIntoRoom(idx);
            Binder.Bind(uiItem, OnClick, data, idx);
        }

        // 点击
        private void OnClick(GameObject obj)
        {
            RoomData data = Binder.Get<RoomData>(obj, 0);
            int idx = Binder.Get<int>(obj, 1);
            BattleMgr.Inst.IntoRoom(data, idx);
        }

        // 返回主页面
        private void OnReturn(GameObject btn)
        {
            UIMgr.Inst.OpenPanel(PanelEnum.StartGamePanel);
        }

        // 赢得房间
        private void OnWinRoom()
        {
            UIMgr.Inst.OpenPanel(PanelEnum.MapPanel);
        }
    }
}

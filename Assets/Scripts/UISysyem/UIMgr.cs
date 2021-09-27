using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using BattleSystem;
using Common;
using LivedObjectSystem;
using UISysyem.Components;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UISysyem
{
    class UIMgr : MonoSingleton<UIMgr>
    {
        private GameObject _canvas;
        private Dictionary<PanelEnum, PanelData> _panelDic;
        private Stack<PanelData> _panelStack;

        // 顶层面板节点
        private GameObject _topPoint;
        // 普通面板节点
        private GameObject _commonPoint;

        // 当前打开的普通面板
        private PanelData _curTopPanel;

        // 已经选择Buff作用的单位
        public bool HasSelect;

        protected override void Init()
        {
            _panelDic = new Dictionary<PanelEnum, PanelData>();
            _panelStack = new Stack<PanelData>();
            _canvas = FindObjectOfType<Canvas>().gameObject;
            _topPoint = _canvas.FindObject("TopPoint");
            _commonPoint = _canvas.FindObject("CommonPoint");

            EventMgr.AddListener(EventsType.PassGame, PassGame);
        }

        // 通关游戏
        private void PassGame()
        {
            OpenPanel(PanelEnum.StartGamePanel);
        }

        public void OpenPanel(PanelEnum panel, params object[] args)
        {
            PanelData data;
            _panelDic.TryGetValue(panel, out data);

            if (data == null)
            {
                GameObject panelObj = ResMgr.Inst.LoadUICompoent(UIPrefabType.Panels, panel.ToString());

                IPanel panelComp = panelObj.GetComponent<IPanel>();
                if (panelComp != null)
                    panelComp.OnAwake();
                else
                    print("请挂载对应脚本" + panel.ToString());

                GameObject parent = null;
                switch (panelComp.Type)
                {
                    case PanelType.FullScreen:
                    case PanelType.Tips:
                        parent = _commonPoint;
                        break;
                    case PanelType.TopLayer:
                        parent = _topPoint;
                        break;
                }

                panelObj.transform.SetParent(parent.transform, false);
                panelObj.transform.localPosition = Vector3.zero;
                panelObj.transform.localScale = Vector3.one;

                InitNeedCompoent(panelObj, panelComp.Type);

                data = new PanelData(panelComp, panelObj, panelObj.GetComponent<CanvasGroup>());
                _panelDic.Add(panel, data);
            }

            if (_panelStack != null && _panelStack.Count > 0)
            {
                PanelData lastPanel = _panelStack.Peek();
                if (lastPanel != null && lastPanel.scrPanel.Type != PanelType.TopLayer)
                {
                    if (data.scrPanel.Type == PanelType.Tips)
                    {
                        SetCanvasGroup(lastPanel.canvasGroup, true, false);
                    }
                    else
                    {
                        SetCanvasGroup(lastPanel.canvasGroup, false, false);
                        lastPanel.scrPanel.RemoveEvents();
                    }
                }
            }

            CanvasGroup curCanvas = data.canvasGroup;
            if (data.scrPanel.Type != PanelType.TopLayer)
            {
                SetCanvasGroup(curCanvas, true, true);
                data.scrPanel.OnStart(args);
                data.scrPanel.AddEvents();
            }

            _panelStack.Push(data);
            _curTopPanel = data;
        }

        public void ClosePanel()
        {

            PanelData topPanel = _panelStack.Pop(); ;
            SetCanvasGroup(topPanel.canvasGroup, false, false);

            PanelData curPanel = _panelStack.Peek();
            SetCanvasGroup(curPanel.canvasGroup, true, true);

        }

        // 初始化一些必要的组件
        private void InitNeedCompoent(GameObject panel, PanelType type)
        {
            CanvasGroup canvasGroup = panel.GetComponent<CanvasGroup>();
            if (canvasGroup == null)
                canvasGroup = panel.AddComponent<CanvasGroup>();

            switch (type)
            {
                case PanelType.FullScreen:
                    break;
                case PanelType.Tips:
                    break;
                case PanelType.TopLayer:
                    canvasGroup.alpha = 1;
                    canvasGroup.blocksRaycasts = false;
                    break;
            }
        }

        // 设置CanvasGroup
        private void SetCanvasGroup(CanvasGroup canvasGroup, bool isShow, bool isInteract)
        {
            canvasGroup.alpha = isShow ? 1 : 0;
            canvasGroup.blocksRaycasts = isInteract;
            if (isShow)
                canvasGroup.transform.SetAsLastSibling();
        }

        // 进入房间
        public void IntoRoom(RoomData data)
        {
            switch (data.type)
            {
                case RoomType.None:
                    break;
                case RoomType.Monster:
                case RoomType.Elite:
                case RoomType.Boss:
                    OpenPanel(PanelEnum.MonsterPanel, data);
                    break;
                case RoomType.Shop:
                case RoomType.Event:
                    OpenPanel(PanelEnum.NormalPanel, data);
                    break;
                case RoomType.Max:
                    break;
            }
        }

        // 更新
        public void OnUpdate()
        {
            _curTopPanel.scrPanel.OnUpdate();
        }

        // 显示英雄商店物品
        public void ShowShopHeroItem(GameObject uiItem, HeroExcelItem cfg)
        {
            if (uiItem == null || cfg == null) return;

            uiItem.Show(true);
            uiItem.FindComponent<Text>("Name").text = cfg.Name;
            uiItem.FindComponent<Text>("HP").text = cfg.Hp.ToString();
            uiItem.FindComponent<Text>("Attack").text = cfg.Attack.ToString();
            uiItem.FindComponent<Text>("Money").text = cfg.Money.ToString();
            uiItem.FindObject("Action").Show(GameMgr.Inst.GameData.IsHeroBuyed(cfg.id));
        }

        // 显示英雄Item
        public void ShowHeroItem(GameObject uiItem, HeroData data)
        {
            if (uiItem == null || data == null) return;

            uiItem.Show(true);
            Binder.Bind(uiItem, null, data);
            uiItem.GetOrAddCompoent<HeroComponent>();

            uiItem.FindComponent<Text>("Name").text = data.Cfg.Name;
            uiItem.FindComponent<Text>("HP").text = data.CurHp.ToString();
            uiItem.FindComponent<Text>("Attack").text = data.Attack.ToString();
            uiItem.FindComponent<Text>("Money").text = data.Cfg.Money.ToString();

            uiItem.FindObject("Action").Show(data.IsAction);
            // 因为隐藏的物体获取不到Graphic,所以一定要在显隐操作后设置
            uiItem.GetComponentsInChildren<Graphic>().ToList().ForEach(t => t.raycastTarget = !data.IsAction);
        }

        // 显示怪物
        public void ShowMonsterItem(GameObject uiItem, MonsterData data)
        {
            if (uiItem == null || data == null) return;

            uiItem.GetOrAddCompoent<MonsterComponent>().Init(data);
            uiItem.Show(true);
            uiItem.FindComponent<Text>("Name").text = data.Cfg.Name;
            uiItem.FindComponent<Text>("HP").text = data.CurHp.ToString();
            uiItem.FindComponent<Text>("Attack").text = data.Attack.ToString();
            uiItem.FindObject("Action").Show(data.IsAction);
        }

        // 显示LivedObj物品
        public void ShowLivedObjItem(GameObject uiItem, LivedObject data)
        {
            if (uiItem == null || data == null) return;

            bool isHero = data is HeroData;
            GameObject heroItem = uiItem.FindObject("HeroItem");
            if (heroItem != null)
                heroItem.Show(isHero);

            GameObject monsterItem = uiItem.FindObject("MonsterItem");
            if (monsterItem != null)
                monsterItem.Show(!isHero);

            if (isHero)
                ShowHeroItem(uiItem.FindObject("HeroItem"), (HeroData)data);
            else
                ShowMonsterItem(uiItem.FindObject("MonsterItem"), (MonsterData)data);
        }

        // 隐藏额外的物体
        public void HideExtraItem(GameObject parent, int idx)
        {
            while (idx < parent.transform.childCount)
            {
                GameObject item = parent.transform.GetChild(idx++).gameObject;
                item.Show(false);
            }
        }

        // 获取PC鼠标(手机端手指)下所有的UI物体
        public List<GameObject> GetOverObjs()
        {
            PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
            pointerEventData.position = Input.mousePosition;
            GraphicRaycaster gr = _canvas.GetComponent<GraphicRaycaster>();
            List<RaycastResult> results = new List<RaycastResult>();
            gr.Raycast(pointerEventData, results);

            List<GameObject> objs = new List<GameObject>();
            foreach (var result in results)
                objs.Add(result.gameObject);

            return objs;
        }
    }
}

using BattleSystem;
using GameSystem;
using SceneSystem;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UISysyem;
using UnityEngine;

namespace Common
{
    class GameMgr : Singleton<GameMgr>
    {
        public GameData GameData { get; set; }
        public GameMgr()
        {
            string path = $"{Application.persistentDataPath }/{typeof(GameData).Namespace}";
            if (Directory.Exists(path))
                GameData = GameHelper.ReadJsonFromFile<GameData>();
            else
                CreateNewGame();
        }

        public void Init()
        {
            //TODO 初始化操作
        }

        // 新游戏
        public void NewGame()
        {
            Debug.Log("新游戏");
            CreateNewGame();
            SceneMgr.Inst.ChangeScene(new BattleScene());
        }

        // 创建新游戏并保存
        private void CreateNewGame()
        {
            GameData = new GameData();
            SaveGame();
        }

        // 继续游戏
        public void ContinueGame()
        {
            Debug.Log("继续游戏");
            SceneMgr.Inst.ChangeScene(new BattleScene());
        }

        // 设置游戏
        public void SetGame()
        {
            Debug.Log("设置游戏");
            //UIMgr.Inst.OpenPanel(); // 打开设置面板
        }

        // 退出游戏
        public void QuitGame()
        {
            Debug.Log("退出游戏");
            Application.Quit();
        }

        // 开始游戏
        public void StartGame()
        {
            GameData.isStartGame = true;
            GameData.OwnHeros = BattleMgr.Inst.GetIntHero();
            GameData.WaitHeros = GameData.OwnHeros.Keys.ToList();
            GameData.ResetMapIdx();
            GameData.MapList = BattleMgr.Inst.CreateMap();

            SaveGame();
            BattleMgr.Inst.StartBattle(GameData);
        }

        // 房间胜利
        public void WinRoom()
        {
            GameData.WinRoom();
            SaveGame();
        }
        // 游戏失败
        public void LoseGame()
        {
            GameData = new GameData();
            SaveGame();
            UIMgr.Inst.OpenPanel(PanelEnum.StartGamePanel);
        }

        // 保存游戏
        public void SaveGame()
        {
            GameHelper.SaveJsonToFile(GameData);
        }

        // 获取英雄
        public HeroData GetHero(int id)
        {
            HeroData data;
            GameData.OwnHeros.TryGetValue(id, out data);
            return data;
        }
    }
}

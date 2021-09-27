using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using GameSystem;
using Net.Sender;

namespace PlayerSystem
{
    class PlayerManager : MonoSingleton<PlayerManager>
    {
        private GameData _gameData;
        public GameData GameData { get => _gameData; }
        private Dictionary<string, Player> _dicPlayers;
        private string _mainPlayerID;
        public string MainPlayerID { get => _mainPlayerID; }

        protected override void Init()
        {
            _dicPlayers = new Dictionary<string, Player>();
        }

        public Player GetPlayer(string playerID)
        {
            Player player = null;
            _dicPlayers.TryGetValue(playerID, out player);
            return player;
        }

        public void SetMainPlayerID(string id)
        {
            _mainPlayerID = id;
        }

        internal void AddPlayer(Player player)
        {
            if (!_dicPlayers.ContainsKey(player.id))
            {
                _dicPlayers.Add(player.id, player);
                _mainPlayerID = player.id;
            }

        }

        internal Player BuildPlayer(MsgAddPlayer msg)
        {
            Player player = new Player();
            player.id = msg.id;
            player.name = msg.name;

            return player;
        }

        public void ChangeName(string nameToChange)
        {
            PlayerMsgSender.ChangeName(nameToChange);
        }

        public Player GetMainPlayer()
        {
            Player player;
            _dicPlayers.TryGetValue(_mainPlayerID, out player);
            return player;
        }
        
    }
}

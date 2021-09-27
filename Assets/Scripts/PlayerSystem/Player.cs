using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace PlayerSystem
{
    class Player
    {
        public string id;
        public string name;
        private Dictionary<uint, uint> _numericals;
        private List<int> _listLivedHero;
        public List<int> ListLivedHero => _listLivedHero;
        private Dictionary<int, HeroData> _dicHero;
        public Dictionary<int, HeroData> DicHero
        {
            get
            {
                return _dicHero;
            }
        }

        public Player()
        {
            _numericals = new Dictionary<uint, uint>();
            _dicHero = new Dictionary<int, HeroData>();

            InitHeroData();
        }

        // 获取数值升级类等级
        public uint GetNumerical(NumericalType type, ushort subtype = 0)
        {
            uint key = ((uint)type << 16 | subtype);
            if (_numericals.ContainsKey(key))
                return _numericals[key];

            return 0;
        }

        // 设置数值升级类等级
        public void SetNumerical(NumericalType type, ushort subtype, uint value)
        {
            uint key = ((uint)type << 16 | subtype);
            _numericals[key] = value;
        }


        // 初始化英雄数据
        private void InitHeroData()
        {
            //HeroExcelItem[] items = ExlMgr.Inst.GetAllHeroCfg();
            //foreach (var item in items)
            //    _dicHero.Add(item.id, new HeroData(item));
        }

        // 获取英雄数据
        public HeroData GetHeroData(int id)
        {
            HeroData item;
            _dicHero.TryGetValue(id, out item);
            return item;
        }
    }
}

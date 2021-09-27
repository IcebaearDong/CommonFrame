using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace BattleSystem
{
    class TestData
    {
        private int i;
        [JsonIgnore]
        public GameObject o;

        public TestData()
        {
            i = 2;
        }

        public void Int()
        {
            i = 1;
        }
    }
}

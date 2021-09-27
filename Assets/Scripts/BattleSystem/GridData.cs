using Common;
using LivedObjectSystem;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UISysyem;
using UISysyem.Components;
using UnityEngine;

namespace BattleSystem
{
    public class GridData
    {
        public LivedObject Data { get; set; }
        public Pos Pos { get; set; }
    }

    public struct Pos
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Pos(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARKViewer
{
    public class CryoSummary
    {

        //creature,name,lvl,hp,stam,torp,oxy,food,weight,melee,speed,lat,lon,tribe,player

        public string Creature { get; set; }
        public string Name { get; set; }
        public string TribeName { get; set; }
        public string PlayerName { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }
        public int Level { get; set; }
        public float HealthCurrent { get; set; }
        public float HealthMax { get; set; }

        public float StaminaCurrent { get; set; }
        public float StaminaMax { get; set; }

        public float TorpidityCurrent { get; set; }
        public float TorpidityMax { get; set; }

        public float OxygenCurrent { get; set; }
        public float OxygenMax { get; set; }

        public float FoodCurrent { get; set; }
        public float FoodMax { get; set; }

        public float WeightCurrent { get; set; }
        public float WeightMax { get; set; }

        public float MeleePercent { get; set; }

        public float SpeedPercent { get; set; }
        public string ClassName { get; set; }
    }
}

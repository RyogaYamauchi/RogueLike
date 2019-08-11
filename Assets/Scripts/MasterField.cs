using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public class MasterField : MonoBehaviour
    {
        public static Dictionary<int, Color> MasterFieldDictionary;


        public static void Init()
        {
            MasterFieldDictionary = new Dictionary<int, Color>();
            MasterFieldDictionary.Add(MasterFieldData.floor,MasterFieldColor.floor);
            MasterFieldDictionary.Add(MasterFieldData.wall,MasterFieldColor.wall);
            MasterFieldDictionary.Add(MasterFieldData.emptiness,MasterFieldColor.emptiness);
            MasterFieldDictionary.Add(MasterFieldData.wator,MasterFieldColor.wator);
        }

        public static Color GetColor(int id)
        {
            if(id == 1) return new Color(0.1f,0.1f,0.9f);
            if(id == 2) return new Color(0.1f,0.2f,0.8f);
            if(id == 3) return new Color(0.1f,0.3f,0.7f);
            if(id == 4) return new Color(0.1f,0.4f,0.6f);
            if(id == 5) return new Color(0.1f,0.5f,0.5f);
            if(id == 6) return new Color(0.1f,0.6f,0.4f);
            if(id == 7) return new Color(0.1f,0.7f,0.3f);
            if(id == 8) return new Color(0.1f,0.8f,0.2f);
            if(id == 9) return new Color(0.1f,0.9f,0.1f);
            if(id == 10) return new Color(0.1f,0.1f,0.8f);
            if(id ==11) return new Color(0.1f,0.2f,0.7f);
            if(id == 12) return new Color(0.1f,0.3f,0.6f);
            if(id == 13) return new Color(0.1f,0.4f,0.5f);
            

            
            return MasterFieldDictionary[id];
        }
    }
}
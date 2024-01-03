using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace DamageNumbersPro.Demo
{
    public class DNP_PrefabSettings : MonoBehaviour
    {
      
        public int damage = 1;
        public int numberRange = 100;
        public List<string> texts;        
        public bool randomColor;

        public float h;
        public float s;
        public float v;
        public void SetHSV(float _h,float _s, float _v)
        {
            h = _h;
            s = _s;
            v = _v;
        }
     
        public void Apply(DamageNumber target,string _text="")
        {          
            if (texts != null)
            {                
                target.leftText = _text;
                switch(_text)
                {
                    case "Critical":
                        SetHSV(1, 1, 1);
                        target.SetColor(Color.HSVToRGB(h, s, v));
                        break;
                    case "Dodge":
                        SetHSV(0.2f, 1, 1);
                        target.SetColor(Color.HSVToRGB(h, s, v));
                        break;
                    case "LifeSteal":
                        SetHSV(1, 1, 1);
                        target.SetColor(Color.HSVToRGB(h, s, v));
                        break;
                    case "Get Bullet":
                        SetHSV(0.5f, 0.5f, 1);
                        target.SetColor(Color.HSVToRGB(h, s, v));
                        break;
                }
            }

            if (randomColor)
            {
                
            }
        }
    }
    
}

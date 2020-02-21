using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Logics.Map
{
    public class Route
    {
        private int _howHard;
        /// <summary>
        /// HowHard from scale 1-10. 
        /// 1 - Very Easy
        /// 10 - Very Hard
        /// </summary>
        public int HowHard { 
            get
            {
                return _howHard; 
            }
            set
            {
                _ = value > 10 || value < 1 ?
                    throw new ArgumentException() :
                    _howHard = value;
            }
        }

        private int _howLong;
        /// <summary>
        /// HowLong from scale 1-10. 
        /// 1 - Very Short
        /// 10 - Very Long
        /// </summary>
        public int HowLong
        {            
            get
            { return _howLong; }
            set
            {
                _ = value > 10 || value < 1 ?
                    throw new ArgumentException() :
                    _howLong = value;
            }
        }

        public Route()
        {
            HowHard = UnityEngine.Random.Range(1,10);
            HowLong = UnityEngine.Random.Range(1,10);
        }
    }
}
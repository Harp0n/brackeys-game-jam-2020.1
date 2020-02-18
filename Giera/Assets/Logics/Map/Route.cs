using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Logics.Map
{
    public class Route
    {
        /// <summary>
        /// HowHard from scale 1-10. 
        /// 1 - Very Easy
        /// 10 - Very Hard
        /// </summary>
        public int HowHard { get; set;
            /*get
            { return HowHard; }
            set
            {
                _ = HowHard > 10 || HowHard < 1 ?
                    throw new ArgumentException() :
                    HowHard = value;
            }*/
        }
        /// <summary>
        /// HowLong from scale 1-10. 
        /// 1 - Very Short
        /// 10 - Very Long
        /// </summary>
        public int HowLong
        {
            get;set;
            /*
            get
            { return HowLong; }
            set
            {
                _ = HowLong > 10 || HowLong < 1 ?
                    throw new ArgumentException() :
                    HowLong = value;
            }*/
        }

        public Route()
        {
            Random r = new Random();
            HowHard = r.Next(1,10);
            HowLong = r.Next(1,10);
        }
    }
}

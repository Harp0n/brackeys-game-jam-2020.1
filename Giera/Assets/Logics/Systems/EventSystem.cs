using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Logics.Systems
{

    public class EventSystem : ISystem
    {
        public void Update(Boat boat)
        {
            Random r = new Random();

            if (r.Next(0, 100) > 90)
            {
                boat.HolesMast[r.Next(0, boat.HolesMast.Count)].IsPatchedUp = false;
                boat.HolesHull[r.Next(0, boat.HolesHull.Count)].IsPatchedUp = false;
            }
        }
    }
}
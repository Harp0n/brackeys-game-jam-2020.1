using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Logics.Map
{
    public abstract class Place
    {
        public string Name { get; set; }
        public abstract void Interact();

    }
}

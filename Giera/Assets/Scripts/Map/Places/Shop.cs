using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Logics.Map.Places
{
    public class Shop : Place
    {
        public List<string> Items { get; set; }
        public override void Interact()
        {
            throw new NotImplementedException();
        }
    }
}

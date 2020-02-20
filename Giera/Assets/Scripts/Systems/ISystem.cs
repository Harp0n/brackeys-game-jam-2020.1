using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Logics
{
    public interface ISystem
    {
        void Update(GameSystem gameSystem, float deltaTime);
    }
}

using Assets.Logics.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs
{
    /// <summary>
    /// Represents a single vertex inside a graph.
    /// </summary>
    public class Vertex
    {
        private Location _location;
        public Location Location
        {
            get
            {
                return _location;
            }
            set
            {
                _location = Location;
            }
        }

        public Vertex(Location name)
        {
            _location = name;
        }

        #region Overrides
        public override string ToString()
        {
            return _location.ToString();
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Vertex))
                return false;
            return ((Vertex)obj).Location == _location;
        }

        public override int GetHashCode()
        {
            return _location.GetHashCode();
        }
        #endregion
    }
}

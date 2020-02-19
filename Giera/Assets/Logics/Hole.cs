using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Logics.Systems
{
    public abstract class Hole : MonoBehaviour
    {
        protected float size;
        public float Size {
            get => size;
            set {
                OnSizeChange(value);
                size = value;
            }
        }

        private bool isPatchedUp;
        public bool IsPatchedUp
        {
            get => isPatchedUp;
            set
            {
                OnStateChange(value);
                isPatchedUp = value;
            }
        }

        protected virtual void OnStateChange(bool futurePatchUp) { }
        protected virtual void OnSizeChange(float futureSize) { }

        void Start()
        {
            isPatchedUp = true;
        }
    }
}
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
        protected float INITIAL_SIZE = 0.00003f;
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

        protected virtual void OnStateChange(bool futurePatchUp) {
            if (IsPatchedUp && !futurePatchUp) //open a hole
            {
                Size = INITIAL_SIZE;
            }
            else if (!IsPatchedUp && futurePatchUp)//close a hole
            {

            }
            SetDisplay(futurePatchUp);
        }
        protected virtual void OnSizeChange(float futureSize) { }

        void Start()
        {
            Size = INITIAL_SIZE;
            IsPatchedUp = true;
        }

        void SetDisplay(bool isPatchedUp)
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = isPatchedUp;
            foreach (Transform child in gameObject.transform)
            {
                child.gameObject.GetComponent<SpriteRenderer>().enabled = !isPatchedUp;
            }
        }
    }
}
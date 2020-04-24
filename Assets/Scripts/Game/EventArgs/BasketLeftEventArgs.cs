using System;
using UnityEngine;

namespace FlappyDank
{
    public class BasketLeftEventArgs : EventArgs
    {
        public Basket LeftObject { get; }

        public BasketLeftEventArgs(Basket leftObject)
        {
            LeftObject = leftObject;
        }
    }
}
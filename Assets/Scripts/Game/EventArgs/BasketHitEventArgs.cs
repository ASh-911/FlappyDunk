using System;

namespace FlappyDank
{
    public class BasketHitEventArgs : EventArgs
    {
        public Basket HitBasket { get;  }
        public bool HasDirtyTouch { get; }

        public BasketHitEventArgs(Basket hitBasket, bool hasDirtyTouch)
        {
            HitBasket = hitBasket;
            HasDirtyTouch = hasDirtyTouch;
        }
    }
}
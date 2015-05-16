using Box2DX.Dynamics;
using System;
using System.Collections.Generic;

namespace GameProject2D
{
    class AwesomeContactListener : ContactListener
    {
        public override void Add(ContactPoint point)
        {
            IContactCallback s1 = point.Shape1.GetBody().GetUserData() as IContactCallback;
            IContactCallback s2 = point.Shape2.GetBody().GetUserData() as IContactCallback;
            if (s1 != null)
            {
                s1.OnContact(point.Shape2, point);
            }
            if (s2 != null)
            {
                s2.OnContact(point.Shape1, point);
            }
        }

        public override void Remove(ContactPoint point)
        {
            IContactCallback s1 = point.Shape1.GetBody().GetUserData() as IContactCallback;
            IContactCallback s2 = point.Shape2.GetBody().GetUserData() as IContactCallback;
            if (s1 != null)
            {
                s1.OnContactRemove(point.Shape2, point);
            }
            if (s2 != null)
            {
                s2.OnContactRemove(point.Shape1, point);
            }
        }
    }
}

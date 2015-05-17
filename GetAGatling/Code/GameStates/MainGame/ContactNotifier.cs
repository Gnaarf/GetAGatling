using Box2DX.Dynamics;
using System;
using System.Collections.Generic;

namespace GameProject2D
{
    class ContactNotifier : ContactListener
    {
        public override void Add(ContactPoint point)
        {
            IContactNotified s1 = point.Shape1.GetBody().GetUserData() as IContactNotified;
            IContactNotified s2 = point.Shape2.GetBody().GetUserData() as IContactNotified;
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
            IContactNotified s1 = point.Shape1.GetBody().GetUserData() as IContactNotified;
            IContactNotified s2 = point.Shape2.GetBody().GetUserData() as IContactNotified;
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

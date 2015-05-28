using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject2D
{
    class Weapon : GameObject// think will inhert frm gameobj or physicObject
    {
        public override Vector2 midPoint { get; protected set; }
        public float firingSpeed { get; private set; }
        public float firingDelay { get; private set; }
        public float ammunition { get; private set; }
        public float damage { get; private set;}
        

    }
}

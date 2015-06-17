using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;

namespace GameProject2D
{
    [DataContract]
    public class Weapon : GameObject
    {
        [DataMember(Name = "xvlc")]
        public int ammunition;// { get; protected set; }

        [DataMember(Name="uiae")]
        public float firingSpeed;// { get; protected set; }

        [DataMember]
        public float startupShootingDelay;// { get; protected set; }
        
        [DataMember]
        public float damage;// { get; protected set;}

        static Dictionary<WeaponType, Weapon> Weapons = new Dictionary<WeaponType,Weapon>();

        public Weapon()
        {
            initialize();
        }

        public Weapon(WeaponType weaponType)
        {
            if (Weapons.ContainsKey(weaponType))
            {
                initialize(Weapons[weaponType]);
            }
            else
            {
                string fileName = Program.rootPath + "Data/" + weaponType.ToString() + ".gat";
                DataContractSerializer serializer = new DataContractSerializer(typeof(Weapon));
                
                if (File.Exists(fileName))
                {
                    using (XmlReader xr = XmlReader.Create(fileName))
                    {
                        Weapon templateWeapon = (Weapon)serializer.ReadObject(XmlReader.Create(fileName));
                        initialize(templateWeapon);
                    }
                }
                else
                {
                    initialize();
                    using (XmlWriter xw = XmlWriter.Create(fileName))
                    {
                        serializer.WriteObject(xw, this);
                    }
                }
                Weapons[weaponType] = this;
            }
        }

        void initialize()
        {
            this.firingSpeed = 0.1F;
            this.startupShootingDelay = 0.0F;
            this.ammunition = 100;
            this.damage = 1.0F;
        }

        void initialize(Weapon weapon)
        {
            this.firingSpeed = weapon.firingSpeed;
            this.startupShootingDelay = weapon.startupShootingDelay;
            this.ammunition = weapon.ammunition;
            this.damage = weapon.damage;
        }
    }
}

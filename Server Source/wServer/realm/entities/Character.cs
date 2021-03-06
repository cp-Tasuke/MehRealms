﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace wServer.realm.entities
{
    public abstract class Character : Entity
    {
        public wRandom Random { get; private set; }
        protected Character(RealmManager manager, ushort objType, wRandom rand)
            : base(manager, objType)
        {
            this.Random = rand;

            if (ObjectDesc != null)
            {
                if (ObjectDesc.SizeStep != 0)
                {
                    var step = Random.Next(0, (ObjectDesc.MaxSize - ObjectDesc.MinSize) / ObjectDesc.SizeStep + 1) * ObjectDesc.SizeStep;
                    Size = ObjectDesc.MinSize + step;
                }
                else
                    Size = ObjectDesc.MinSize;

                HP = ObjectDesc.MaxHP;
            }
        }

        public int HP { get; set; }
    }
}

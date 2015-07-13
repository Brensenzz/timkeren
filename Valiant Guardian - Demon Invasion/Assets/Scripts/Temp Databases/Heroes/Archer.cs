﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Temp_Databases.Heroes
{
    public class Archer : Heroes
    {
        public Archer(Int32 experience, bool locked)
            : base(experience, locked)
        {
            preparingData();
        }

        private void preparingData()
        {
            mainAttack = new AttackModel(30, new int[] { 1, 1 }, 60);
            skillAttack = new AttackModel(30, new int[] { 1, 1 }, 60);
            ultSkillAttack = new AttackModel(30, new int[] { 1, 1 }, 60);
            attackSpeed = 60;
            price = 100;
        }
    }
}
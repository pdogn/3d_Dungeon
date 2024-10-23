using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monster
{
    public enum Monstate
    {
        idle,
        walk,
        attack,
        dammage,
        death
    }
    public abstract class Monster : MonoBehaviour
    {
        //public string ID;
        public Monstate monstate;
        public abstract void Idle();
        public abstract void Move();
        public abstract void Attack();
        public abstract void Death();
    }
}

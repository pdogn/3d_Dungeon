using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monster
{
    public class Zombie : Monster
    {
        [SerializeField] GameObject target;
        [SerializeField]  float distanToTarget;

        //[SerializeField] private bool findedPlayer;

        [SerializeField] float speed = 2f;
        [SerializeField] float rotationSpeed = 2.0f;

        Animator anim;
        public override void Attack()
        {
            //Attack
        }

        public override void Death()
        {
            //Death
        }

        public override void Idle()
        {
            //Idle
        }

        private void Start()
        {
            monstate = Monstate.idle;
            target = FindObjectOfType<Movement>().gameObject;
            anim = GetComponent<Animator>();
        }
        private void Update()
        {
            CheckDisToPlayer();
            ZombAnim();
        }

        void ZombAnim()
        {
            switch (monstate)
            {
                case Monstate.idle:
                    anim.Play("Zombie@Idle01");
                    break;
                case Monstate.walk:
                    anim.Play("Zombie@Walk01");
                    break;
                case Monstate.attack:
                    anim.Play("Zombie@Attack01");
                    break;
                case Monstate.dammage:
                    anim.Play("Zombie@Damage01");
                    break;
                case Monstate.death:
                    anim.Play("Zombie@Death01_A");
                    break;
            }
        }

        /// <summary>
        /// Kiểm tra khoảng cách với người chơi
        /// </summary>
        void CheckDisToPlayer()
        {
            distanToTarget = Vector3.Distance(this.transform.position, target.transform.position);

            if(distanToTarget >= 1 && distanToTarget <= 7)
            {
                Debug.Log("Duoi theo nguoi choi");
                ZombieRotate();
                Move();
                monstate = Monstate.walk;
            }
            else if(distanToTarget <=1)
            {
                Debug.Log("Attack");
                monstate = Monstate.attack;
            }
            else
            {
                monstate = Monstate.idle;
            }
        }

        public override void Move()
        {
            //Move
            this.transform.position = Vector3.MoveTowards(this.transform.position, target.transform.position, speed * Time.deltaTime);
        }

        void ZombieRotate()
        {
            // Lấy hướng của người chơi
            Vector3 direction = target.transform.position - this.transform.position;
            direction.y = 0; // Chỉ quay theo trục Y

            if (direction != Vector3.zero)
            {
                // Tính góc quay mong muốn
                Quaternion targetRotation = Quaternion.LookRotation(direction);

                // Quay mượt mà về hướng người chơi
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Enemies;

namespace Player
{
    public class playerShoot : MonoBehaviour
    {
        Animator animator;
        float distance = 1000f;
        EnemyAI currentEnemy;
        EnemyAI enemy;
        [SerializeField] float bulletTime;
        [SerializeField] private float _turnSpeed = 360;
        [SerializeField] bullet bullet;
        [SerializeField] Transform bulletPlace;
        [SerializeField] float bulletSpeed;
        [SerializeField] float damage;
        Vector3 dir;
        CutDownTrees cutTrees;
        playerHealth playerHealth;

        private void Start()
        {
            animator = GetComponent<Animator>();
            cutTrees = FindObjectOfType<CutDownTrees>();
            playerHealth = FindObjectOfType<playerHealth>();
        }


        public void Shoot(Vector3 direction)
        {
            if (cutTrees.cut == true) { CancelInvoke(); return; }
            if (playerHealth.dead == true) { CancelInvoke(); return; }
            dir = direction;
            if (direction.magnitude == 0f)
            {
                CheckEnemies();
            }

        }

        private void Look(EnemyAI enemies)
        {
            if (enemies == null) { return; }
            Vector3 relativePos = enemy.transform.position - transform.position;
            Vector3 relativePosXZ = new Vector3(relativePos.x, 0, relativePos.z);
            Quaternion rotation = Quaternion.LookRotation(relativePosXZ, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, _turnSpeed);
            if (animator.GetBool("shoot") == false)
            {
                animator.SetBool("shoot", true);
                Invoke("CreateBullet", bulletTime);
            }


        }

        public void CheckEnemies()
        {
            EnemyAI[] enemies = FindObjectsOfType<EnemyAI>();
            if (enemies.Length <= 0)
            {
                animator.SetBool("shoot", false);
                animator.SetBool("idle", true);
                WinActions();
            }
            else
            {
                enemy = ClosestEnemy(enemies);
                Look(currentEnemy);
            }

        }

        public void WinActions()
        {
            FindObjectOfType<fence>().OpenFence();

        }
        private EnemyAI ClosestEnemy(EnemyAI[] enemies)
        {
            foreach (EnemyAI enemy in enemies)
            {
                float currentDistance = Vector3.Distance(enemy.transform.position, transform.position);
                if (currentDistance < distance)
                {
                    distance = currentDistance;
                    currentEnemy = enemy;
                }
            }
            distance = 1000;
            return currentEnemy;
        }

        public void CreateBullet()
        {
            if (dir.normalized != Vector3.zero) { return; }
            if (enemy == null) { return; }
            Vector3 relativePosM = enemy.transform.position - bulletPlace.transform.position;
            bullet newBullet = Instantiate(bullet, bulletPlace.position, Quaternion.identity);
            newBullet.SetParametrs(enemy.shootCenter, bulletSpeed, damage);
            Invoke("CreateBullet", bulletTime);



        }

    }
}

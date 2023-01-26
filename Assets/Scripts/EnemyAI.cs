using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Player;

namespace Enemies
{
    public class EnemyAI : MonoBehaviour
    {
        playerMovement player;
        [SerializeField] float _turnSpeed;
        [SerializeField] Transform shootPoint;
        [SerializeField] float projectileTime = 1f;
        Animator animator;
        [SerializeField] GameObject bullet;
        [SerializeField] Transform projectilePosition;
        [SerializeField] float projectileSpeed;
        bool gameStarted = false;
        Spawner spawner;
        public Transform shootCenter;
        [SerializeField] float speedBullet;
        [SerializeField] float damage = 5f;
        [SerializeField] float randomX;
        [SerializeField] float randomY;

        private void Awake()
        {
            spawner = FindObjectOfType<Spawner>();
            spawner.gameStarted += GameStarted;

        }
        void Start()
        {
            player = FindObjectOfType<playerMovement>();
            animator = GetComponent<Animator>();
        }

        private void OnDestroy()
        {
            spawner.gameStarted -= GameStarted;

        }
        private void GameStarted()
        {
            gameStarted = true;
            Invoke("LaunchProjectile", 2.0f);
        }

        private void Update()
        {
            if (!gameStarted) { return; }
            Look();
        }

        private void Look() //rotate towards enemy
        {
            if (player == null) { return; }
            if (player.GetComponent<playerHealth>().dead == true) { return; }
            Vector3 relativePos = player.transform.position - transform.position;
            Vector3 relativePosXZ = new Vector3(relativePos.x, 0, relativePos.z);
            Quaternion rotation = Quaternion.LookRotation(relativePosXZ, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, _turnSpeed);


        }

        public void LaunchProjectile()
        {
            if (player.GetComponent<playerHealth>().dead == true) { return; }
            animator.SetTrigger("Attack 02");
            Invoke("LaunchProjectile", projectileTime);

        }

        public void CreateProjectile()
        {
            GameObject projectile = Instantiate(bullet, projectilePosition.position, Quaternion.identity);
            projectile.GetComponent<pr_1>().SetParametrs(speedBullet, damage, randomX, randomY);

        }


    }
}

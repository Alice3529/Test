using System;
using UnityEngine;
using Enemies;

namespace Player
{
    public class playerMovement : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rb;
        [SerializeField] private float _speed = 5;
        [SerializeField] private float _turnSpeed = 360;
        private Vector3 direction;
        Joystick joystick;
        Animator animator;
        float angle;
        [SerializeField] EnemyAI enemy;
        [SerializeField] float force;
        public Transform center;
        playerHealth playerHealth;
        bool gameStarted = false;
        CharacterController characterController;
        Spawner spawner;
        playerShoot playerShoot;



        private void OnDestroy()
        {
            spawner.gameStarted -= GameStarted;

        }

        private void Start()
        {
            characterController = GetComponent<CharacterController>();
            playerShoot = FindObjectOfType<playerShoot>();
            playerHealth = GetComponent<playerHealth>();
        }
        public void GameStarted()
        {
            gameStarted = true;
        }
        private void Awake()
        {
            spawner = FindObjectOfType<Spawner>();
            spawner.gameStarted += GameStarted;
            joystick = FindObjectOfType<Joystick>();
            animator = GetComponent<Animator>();
            playerHealth = GetComponent<playerHealth>();

        }

        private void Update()
        {
            if (!gameStarted) { return; }
            if (playerHealth.dead) { return; }
            GatherInput();
        }

        private void FixedUpdate()
        {
            if (!gameStarted) { return; }
            if (playerHealth.dead) { direction = Vector3.zero; return; }
            Rotate();
            Move();
            playerShoot.Shoot(direction);

        }

        private void GatherInput()
        {
            direction = joystick.GetDirection();
        }

        private void Rotate()
        {
            if (direction.magnitude != 0)
            {
                angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, angle, 0), _turnSpeed);
            }
        }

        private void Move()
        {
            if (direction.magnitude != 0)
            {
                CancelInvoke();
                characterController.SimpleMove(transform.forward * _speed);
                animator.SetBool("shoot", false);
                animator.SetFloat("state", 1);
            }
            else
            {
                animator.SetFloat("state", 0);

            }
        }

    }
}

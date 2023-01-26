using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    public class playerHealth : MonoBehaviour
    {
        float currentHealth;
        [SerializeField] float healthAmount;
        [SerializeField] Image healthBar;
        Animator animator;
        [SerializeField] GameObject bar;
        public bool dead = false;
        float timeDeadCanvas = 6f;


        private void Start()
        {
            currentHealth = healthAmount;
            healthBar.fillAmount = currentHealth / healthAmount;
            animator = GetComponent<Animator>();

        }

        public void MinusHealth(float amount)
        {
            if (dead == true) { return; }
            currentHealth -= amount;
            healthBar.fillAmount = Mathf.Clamp01(currentHealth / healthAmount);
            if (currentHealth <= 0)
            {
                DeadActions();

            }
        }

        private void DeadActions()
        {
            bar.SetActive(false);
            animator.SetBool("Die", true);
            dead = true;
            FindObjectOfType<Joystick>().DisactiveJoystick();
            Invoke("DeadCanvas", timeDeadCanvas);
        }

        public void PlayerUp()
        {
            transform.position += new Vector3(0, 0.4f, 0); //lifts a little off the ground to fix the animation
        }

        public void DeadCanvas()
        {
            FindObjectOfType<UIManager>().Lose();
        }
    }
}

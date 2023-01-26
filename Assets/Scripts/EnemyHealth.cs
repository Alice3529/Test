using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Enemies
{
    public class EnemyHealth : MonoBehaviour
    {
        float currentHealth;
        [SerializeField] float healthAmount;
        [SerializeField] Image healthBar;
        Animator animator;
        [SerializeField] GameObject bar;
        CoinManager coinManager;

        private void Start()
        {
            currentHealth = healthAmount;
            healthBar.fillAmount = currentHealth / healthAmount;
            animator = GetComponent<Animator>();
            coinManager = FindObjectOfType<CoinManager>();

        }

        public void MinusHealth(float amount)
        {
            currentHealth -= amount;
            healthBar.fillAmount = Mathf.Clamp01(currentHealth / healthAmount);
            if (currentHealth <= 0)
            {
                bar.SetActive(false);
                animator.SetBool("Die", true);
                Destroy(GetComponent<EnemyAI>());
                coinManager.AddCoin();

            }
        }

        public void DestroyEnemy()
        {
            Destroy(this.gameObject);
        }
    }
}

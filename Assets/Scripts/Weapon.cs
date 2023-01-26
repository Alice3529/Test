using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

namespace Player
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] GameObject weapon;
        [SerializeField] GameObject gun;
        [SerializeField] GameObject ax;
        [SerializeField] GameObject pos2;
        UIManager uiManager;

        private void Start()
        {
            uiManager = FindObjectOfType<UIManager>();
        }

        public void ChangeAx()
        {
            gun.SetActive(false);
            ax.SetActive(true);
        }
        public void SwitchGun()
        {
            ax.SetActive(false);
            gun.SetActive(true);
        }
        private void Update()
        {
            weapon.transform.position = pos2.transform.position;

        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "Enemy")
            {
                playerHealth playerHealth = GetComponent<playerHealth>();
                playerHealth.MinusHealth(20);
            }

        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "WinCollider")
            {
                FindObjectOfType<Joystick>().DisactiveJoystick();
                uiManager.Win();

            }
        }
    }
}

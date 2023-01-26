using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] GameObject weapon;
        [SerializeField] GameObject gun;
        [SerializeField] GameObject ax;
        [SerializeField] GameObject pos2;

     
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

      
    }
}

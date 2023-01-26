using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

namespace Player
{
    public class CutDownTrees : MonoBehaviour
    {
        Animator animator;
        [SerializeField] LayerMask treeLayer;
        int woodLayer = 1 << 11;
        [SerializeField] float distance;
        Weapon weaponHolder;
        Transform currentTree;
        int currentWood;
        public bool cut = true;
        [SerializeField] float collectRadius;



        void Start()
        {
            animator = GetComponent<Animator>();
            weaponHolder = FindObjectOfType<Weapon>();

        }

        private void Update()
        {
            if (Physics.Raycast(transform.position, transform.forward, out var hit, distance, treeLayer))
            {
                CutTree(hit);
            }
            else
            {
                cut = false;
                weaponHolder.SwitchGun();
                EndCut();
            }

        }

        private void CutTree(RaycastHit hit)
        {
            cut = true;
            currentTree = hit.collider.gameObject.transform;
            var obj = hit.collider.gameObject;
            transform.LookAt(hit.transform);
            weaponHolder.ChangeAx();
            StartCut();
        }

        public void StartCut()
        {
            animator.SetBool("wood", true);
            animator.SetBool("shoot", false);

        }
        public void EndCut()
        {
            animator.SetBool("wood", false);
        }

        public void AddForce()
        {
            if (currentTree != null)
            {
                currentTree.GetComponent<Tree>().Peaces();
            }
        }

        public void AddWood()
        {
            currentWood++;
        }

    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fence : MonoBehaviour
{
    public bool open = false;
    [SerializeField] float angle;
    [SerializeField] float speed;
    [SerializeField] Collider top;
    public void OpenFence()
    {
        open = true;
        top.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (open == true) 
        { 
            transform.rotation=Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, angle, 0), Time.deltaTime * speed);
        }
    }
}

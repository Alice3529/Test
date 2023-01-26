using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;
public class pr_1 : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float speed;
    Vector3 st;
    [SerializeField] float damage = 5f;
    [SerializeField] float randomX;
    [SerializeField] float randomY;
    void Start()
    {
        target=FindObjectOfType<playerMovement>().center;
        float value=Random.Range(randomX, randomY);
        st = new Vector3(target.transform.position.x+value, target.position.y, target.position.z);

    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject player=collision.gameObject;
        if (collision.gameObject.tag == "Player")
        {
            player.GetComponent<playerHealth>().MinusHealth(damage);
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "Obstacle")
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, st, speed*Time.deltaTime);
        if (transform.position == st)
        {
            Destroy(gameObject,0.01f);    
        }
    }
}

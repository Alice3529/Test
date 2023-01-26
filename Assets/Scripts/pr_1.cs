using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;
public class pr_1 : MonoBehaviour
{
    [SerializeField] Transform target;
    Vector3 st;
    float speed;
    float damage = 5f;
    float randomX;
    float randomY;

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

    public void SetParametrs(float speed1, float damage1, float randomX1, float randomY1)
    {
        speed = speed1;
        damage = damage1;
        randomX = randomX1;
        randomY = randomY1;
    }
}

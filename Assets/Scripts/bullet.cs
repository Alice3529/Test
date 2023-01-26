using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enemies;
public class bullet : MonoBehaviour
{
    Transform target;
    Vector3 endPosition;
    float speed;
    float damage;

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "Enemy")
        {
            EnemyHealth enemyHealth=collision.gameObject.GetComponent<EnemyHealth>();
            enemyHealth.MinusHealth(damage);
            Destroy(this.gameObject);
        }
        if (collision.gameObject.tag=="Obstacle")
        {
            Destroy(this.gameObject);
            
        }
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, endPosition, speed * Time.deltaTime);
        if (transform.position == endPosition)
        {
            Destroy(gameObject, 0.01f);
        }
    }

    public void SetParametrs(Transform newTarget, float bulletSpeed, float bulletDamage)
    {
        damage = bulletDamage;
        speed = bulletSpeed;
        target = newTarget;
        endPosition = target.position;
    }


}

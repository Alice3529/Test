using Player;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BatEnemy : MonoBehaviour
{
    [SerializeField] int steps;
    [SerializeField] float timeToWait;
    Spawner spawner;
    Vector3 topRightCorner;
    Vector3 topLeftCorner;
    Vector3 bottomRightCorner;
    Vector3 bottomLeftCorner;
    float width;
    float height;
    [SerializeField] float speed;
    Vector3 endPos;
    bool move = false;
    playerMovement player;
    NavMeshAgent agent;
    int playerLayer = 1 << 8;
    bool catch1 = false;
    float step;

    private void Start()
    {
        spawner = FindObjectOfType<Spawner>();
        player = FindObjectOfType<playerMovement>();
        SetFieldDimensions();
        CalculateNewPos();
        agent = GetComponent<NavMeshAgent>();

    }

    public void SetFieldDimensions()
    {
        topRightCorner = spawner.topRight;
        topLeftCorner = spawner.topLeft;
        bottomRightCorner = spawner.rigtBottom;
        bottomLeftCorner = spawner.leftBottom;
        width = spawner.planeWidth;
        height = spawner.planeHeight;
    }

    public void CalculateNewPos()
    {
        float step = height / steps;
        if (Mathf.Abs(player.transform.position.z - transform.position.z) <= step)
        {
            endPos = player.transform.position;
        }
        else
        {
            float randomValue = UnityEngine.Random.Range(bottomLeftCorner.x, bottomRightCorner.x);

            if (transform.position.z > player.transform.position.z)
            {
                endPos = new Vector3(randomValue, transform.position.y, transform.position.z - step);
            }
            else
            {
                endPos = new Vector3(randomValue, transform.position.y, transform.position.z + step);

            }

        }
        StartCoroutine(WaitMovement());
    }

    IEnumerator WaitMovement()
    {
        yield return new WaitForSeconds(timeToWait);
        agent.SetDestination(endPos);
        move = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log(567);
        }
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.gameObject.tag == "Player")
        {
            Debug.Log(780);
        }
    }

    private void Update()
    {
       Collider[] col= Physics.OverlapSphere(transform.position,1f, playerLayer);
        if (col.Length > 0 && catch1==false)
        {
            catch1 = true;
            FindObjectOfType<playerHealth>().MinusHealth(20);
            //CalculateEndPos1();    
         }
        else
        {
            catch1 = false;
        }

        if (Vector3.Distance(agent.destination, endPos)<3f)
        {
            CalculateNewPos();

        }
    }

    public void CalculateEndPos()
    {
        float randomValue = UnityEngine.Random.Range(bottomLeftCorner.x, bottomRightCorner.x);

        if (transform.position.z > player.transform.position.z)
        {
            endPos = new Vector3(randomValue, transform.position.y, transform.position.z - step);
        }
        else
        {
            endPos = new Vector3(randomValue, transform.position.y, transform.position.z + step);

        }
    }
}


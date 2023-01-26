using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.UIElements;

public class Spawner : MonoBehaviour
{
    [SerializeField] MeshFilter gameField;
    [SerializeField] List<GameObject> enemies;
    [SerializeField] Transform player;
    List<Vector3> vertexes;
    public float planeHeight;
    float playerPosZ;
    Vector3 playerPos;
    public Vector3 rigtBottom;
    public Vector3 leftBottom;
    public Vector3 topRight;
    public Vector3 topLeft ;
    public float planeWidth;
    [SerializeField] LayerMask obstacleLayer;
    int spawnedEnemies = 0;
    public Action gameStarted;
    [SerializeField] BoxCollider colliderTop;
    [SerializeField] BoxCollider colliderLeft;
    [SerializeField] BoxCollider colliderRight;
    [SerializeField] BoxCollider colliderBottom;



    void Awake()
    {
        CalculateFieldBoards();
        planeHeight = Mathf.Abs((rigtBottom - topRight).z);
        planeWidth = Mathf.Abs((topRight-topLeft).x);
        float enemyBottom = rigtBottom.z + planeHeight / 3; //bottom of enemy field
        CalculatePlayerPos();
        Instantiate(player, playerPos, Quaternion.identity);
        CheckObstacles(enemyBottom);
        SetWalls();

    }

    private void CalculatePlayerPos()
    {
        playerPosZ = (float)(rigtBottom.z + planeHeight / 6); // center of player field (Z-axis)
        playerPos = new Vector3(gameField.transform.position.x, gameField.transform.position.y, playerPosZ);
    }

    public void SetWalls()
    {
        ColliderDimensions();
        ColliderPosition();

    }

    private void ColliderDimensions()
    {
        colliderLeft.size = new Vector3(1, 1, planeHeight);
        colliderRight.size = new Vector3(1, 1, planeHeight);
        colliderTop.size = new Vector3(planeWidth, 1, 1);
        colliderBottom.size = new Vector3(planeWidth, 1, 1);
    }

    private void ColliderPosition()
    {
        float xColTop = topRight.z - (colliderTop.size.z / 2);
        colliderTop.transform.position = new Vector3(gameField.transform.position.x, gameField.transform.position.y, xColTop);
        float xColLeft = topLeft.x - (colliderLeft.size.x / 2); //offset
        colliderLeft.transform.position = new Vector3(xColLeft, gameField.transform.position.y, gameField.transform.position.z);
        float xColRight = topRight.x + (colliderLeft.size.x / 2);
        colliderRight.transform.position = new Vector3(xColRight, gameField.transform.position.y, gameField.transform.position.z);
        float xColBottom = rigtBottom.z - (colliderBottom.size.z / 2);
        colliderBottom.transform.position = new Vector3(gameField.transform.position.x, gameField.transform.position.y, xColBottom);
    }

    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(3f);
        Debug.Log("started");
        gameStarted.Invoke();
    }

    private void CalculateFieldBoards()
    {
        vertexes = gameField.sharedMesh.vertices.ToList();
        topLeft = gameField.transform.TransformPoint(vertexes[0]);
        leftBottom = gameField.transform.TransformPoint(vertexes[10]);
        topRight = gameField.transform.TransformPoint(vertexes[110]);
        rigtBottom = gameField.transform.TransformPoint(vertexes[120]);
    }

    void CheckObstacles(float enemyBottom)
    {
        if (spawnedEnemies >= enemies.Count)
        {
            StartCoroutine(StartGame());
            return;
        }

        float xValue = Random.Range(topLeft.x, topRight.x); 
        float zValue = Random.Range(enemyBottom, topRight.z);
        Vector3 spawnPoint = new Vector3(xValue, 0, zValue);
        Vector3 enemyColliderSize = enemies[spawnedEnemies].GetComponent<BoxCollider>().size;
        Collider[] colliders = Physics.OverlapBox(spawnPoint, (enemyColliderSize * 1.2f)/2, Quaternion.identity, obstacleLayer);

        if (colliders.Length == 0)
        {
            Instantiate(enemies[spawnedEnemies], spawnPoint, Quaternion.Euler(0, -180f, 0f));
            spawnedEnemies += 1;
        }
        
        CheckObstacles(enemyBottom);
        
    }


}

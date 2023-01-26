using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

public class Tree : MonoBehaviour
{
    [SerializeField] float cutRadius;
    [SerializeField] LayerMask playerLayer;
    public float minForce;
    public float maxForce;
    [SerializeField] float radius;
    [SerializeField] List<PieceGroup> pieceGroup;
    int amount = 0;
    int treeLive;
    [SerializeField] float timeToDestroyPeace;
    BoxCollider collider;

    [Header("wood")]
    [SerializeField] int woodAmount;
    [SerializeField] GameObject wood;
    [SerializeField] Transform woodCenter;


    [System.Serializable]
    public struct PieceGroup
    {
        public List<Transform> pieces;
    }
 
    private void Start()
    {
        treeLive = pieceGroup.Count;
        collider=GetComponent<BoxCollider>();
    }

 
    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, cutRadius);
    }

    public void Peaces()
    {
        if (amount == treeLive) { collider.enabled = false;  return; }
        List<Transform> peaces=pieceGroup[amount].pieces;
        foreach (Transform child in peaces)
        {
            Rigidbody rb = child.GetComponent<Rigidbody>();
            rb.isKinematic = false;
            float force = Random.Range(minForce, maxForce);
            rb.AddExplosionForce(force, transform.position, radius);
            Destroy(child.gameObject, timeToDestroyPeace);
        }
        CreateWood();
        amount++; 


    }

    public void CreateWood()
    {
        for (int i = 0; i < woodAmount; i++)
        {
            Instantiate(wood, woodCenter.position, Quaternion.identity);
        }
    }
}

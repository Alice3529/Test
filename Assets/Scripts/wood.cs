using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;
public class wood : MonoBehaviour
{
    Rigidbody rigidbody;
    [SerializeField] Vector3 force;
    Vector3 direction;
    void Start() //Create wood
    {
        float xValue = Random.Range(-1f, 1f);
        float zValue = Random.Range(-1f, 1f);
        direction = new Vector3(xValue, 1, zValue);
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.AddForce(new Vector3(force.x*direction.x, force.y*direction.y, force.z*direction.z));
    }

    private void OnTriggerStay(Collider collision)
    {
        collision.gameObject.TryGetComponent(out CutDownTrees cutDownTrees);
        if (cutDownTrees != null)
        {
            cutDownTrees.AddWood();
            Destroy(this.gameObject);
        }
    }


}

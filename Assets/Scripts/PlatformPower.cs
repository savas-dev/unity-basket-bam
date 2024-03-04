using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformPower : MonoBehaviour
{
    
    [SerializeField] private float degree;
    [SerializeField] private float power;
    
    private void OnCollisionEnter(Collision collision){
        collision.gameObject.GetComponent<Rigidbody>().AddForce(
            new Vector3(degree, 90, 0) * power, ForceMode.Force
        );
    }
}

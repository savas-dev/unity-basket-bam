using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketController : MonoBehaviour
{
    public GameObject ball;
    private void Start()
    {
        ball = GameObject.Find("Ball");


        
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            this.gameObject.GetComponent<Cloth>().sphereColliders[0].first = ball.GetComponent<SphereCollider>();
            this.gameObject.GetComponent<Cloth>().sphereColliders[0].second = ball.GetComponent<SphereCollider>();
        }
    }
}

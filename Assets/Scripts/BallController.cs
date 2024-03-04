using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public GameObject col;
    public AudioSource ballSound;

    [SerializeField] private GameManager gameManager;
    void OnTriggerEnter(Collider other){

        //ballSound.Play();

        if(other.gameObject.CompareTag("Basket")){
            gameManager.Basket();
        }

        if(other.gameObject.CompareTag("ColliderCloser")){
            col = other.gameObject.transform.parent.GetChild(1).gameObject;
            other.gameObject.transform.parent.GetChild(1).gameObject.SetActive(false);

            Invoke("OpenCollider", 2f);
        }

        if(other.gameObject.CompareTag("GameOver")){
            gameManager.GameOver();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        ballSound.Play();
    }

    public void OpenCollider()
    {
        col.SetActive(true);
    }
}

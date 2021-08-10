using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public float speed;
    public float damage = 10;
    public float damegeSpeed = 5;
    public GameObject blood;
    Rigidbody rb;


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            if (rb.velocity.magnitude >= damegeSpeed)
            {
                collision.transform.GetComponent<player>().hp -= damage;
                collision.transform.GetComponent<player>().updatePlayerInfo();
                ContactPoint contact = collision.contacts[0];               
                Instantiate(blood,contact.point,Quaternion.FromToRotation(Vector3.forward,contact.normal));
            }
            Destroy(gameObject);            
        }
    }

    public void autoDestory()
    {
        Destroy(gameObject);
    }

    private void Start()
    {
        rb = transform.GetComponent<Rigidbody>();
        Invoke("autoDestory",20);
    }

}

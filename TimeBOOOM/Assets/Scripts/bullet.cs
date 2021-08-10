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
    GameObject light;


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            if (rb.velocity.magnitude >= damegeSpeed)
            {
                collision.transform.GetComponent<player>().hp -= damage;
                collision.transform.GetComponent<player>().updatePlayerHPInfo();
                ContactPoint contact = collision.contacts[0];               
                Instantiate(blood,contact.point,Quaternion.FromToRotation(Vector3.forward,contact.normal));
                Destroy(gameObject);
            }                   
        }
        if (collision.transform.tag == "Enemy")
        {
            UnityEngine.Debug.Log("»÷ÖÐµÐÈË");
        }
    }

    public void autoDestory()
    {
        Destroy(gameObject);
    }

    public void lightDestory()
    {
        Destroy(light);
    }

    private void Start()
    {
        rb = transform.GetComponent<Rigidbody>();
        light = transform.GetChild(1).gameObject;
        Invoke("autoDestory",20);
        Invoke("lightDestory", 0.1f);

    }

}

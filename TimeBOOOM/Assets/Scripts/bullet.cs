using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public float speed;
    public float damage = 10;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            collision.transform.GetComponent<player>().hp -= damage;
            collision.transform.GetComponent<player>().updatePlayerInfo();
            Destroy(gameObject);            
        }
    }

    public void autoDestory()
    {
        Destroy(gameObject);
    }

    private void Start()
    {
        Invoke("autoDestory",20);
    }

}

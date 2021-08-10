using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    public GameObject bullet;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("shoot",3,0.2f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(new Vector3(player.transform.position.x,1, player.transform.position.z));
    }

    private void shoot()
    {
        GameObject bulletcClone;
        bulletcClone = Instantiate(bullet,transform.position+ transform.TransformDirection(Vector3.forward), transform.rotation);
        bulletcClone.GetComponent<Rigidbody>().velocity = 
            transform.TransformDirection(Vector3.forward * bullet.GetComponent<bullet>().speed);
    }
}

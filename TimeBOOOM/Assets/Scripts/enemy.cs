using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    public GameObject bullet;
    public GameObject player;
    public int enemyHP = 3;
    public GameObject enemyHPUI;
    GameObject heart1, heart2, heart3;

    private void Awake()
    {
        heart1 = enemyHPUI.transform.GetChild(1).gameObject;
        heart2 = enemyHPUI.transform.GetChild(2).gameObject;
        heart3 = enemyHPUI.transform.GetChild(3).gameObject;
    }
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("shoot",3,0.2f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(new Vector3(player.transform.position.x,
                                    1, 
                                    player.transform.position.z));
    }

    private void shoot()
    {
        GameObject bulletcClone;
        bulletcClone = Instantiate(bullet,
            transform.position + 
            transform.TransformDirection(Vector3.forward), 
            transform.rotation);
        bulletcClone.GetComponent<Rigidbody>().velocity = 
            transform.TransformDirection(Vector3.forward * 
            bullet.GetComponent<bullet>().speed);
    }

    public void hurt()
    {
        enemyHP -= 1;
        if (enemyHP == 2)
            heart3.SetActive(false);
        if (enemyHP == 1)
            heart2.SetActive(false);
        if (enemyHP == 0)
        {
            heart1.SetActive(false);
            Destroy(gameObject);
        }
            

    }
}

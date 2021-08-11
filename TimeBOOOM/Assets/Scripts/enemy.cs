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
    bool Immune = false;
    GameObject ps1, ps2; //Á£×ÓÏµÍ³
    public GameObject exp;
    AudioSource audioSource;
    public AudioClip danamgeSFX;
    public AudioClip shootSFX;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        heart1 = enemyHPUI.transform.GetChild(1).gameObject;
        heart2 = enemyHPUI.transform.GetChild(2).gameObject;
        heart3 = enemyHPUI.transform.GetChild(3).gameObject;
    }
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("shoot",Random.Range(3f,4f),0.2f);
        ps1 = transform.GetChild(0).gameObject;
        ps2 = transform.GetChild(1).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (!Immune)
        {
            transform.LookAt(new Vector3(player.transform.position.x,
                            1,
                            player.transform.position.z));
        }
    }

    private void shoot()
    {
        if (!Immune)
        {
            GameObject bulletcClone;
            audioSource.clip = shootSFX;
            audioSource.Play();
            bulletcClone = Instantiate(bullet,
                transform.position +
                transform.TransformDirection(Vector3.forward),
                transform.rotation);
            bulletcClone.GetComponent<Rigidbody>().velocity =
                transform.TransformDirection(Vector3.forward *
                bullet.GetComponent<bullet>().speed);
        }
    }

    public void hurt()
    {
        if (!Immune)
        {
            audioSource.clip = danamgeSFX;
            audioSource.Play();
            Immune = true;
            Invoke("Evocation", 3);
            ps1.GetComponent<ParticleSystem>().Play();
            ps2.GetComponent<ParticleSystem>().Play();
            enemyHP -= 1;
            if (enemyHP == 2)
                heart3.SetActive(false);
            if (enemyHP == 1)
                heart2.SetActive(false);
            if (enemyHP == 0)
            {
                heart1.SetActive(false);
                var expPS = Instantiate(exp, transform.position, transform.rotation);
                expPS.GetComponent<ParticleSystem>().collision.AddPlane(GameObject.Find("Plane").transform);
                enemyHPUI.SetActive(false);
                Destroy(gameObject);
            }
        }
    }
    public void Evocation()
    {
        ps1.GetComponent<ParticleSystem>().Stop();
        ps2.GetComponent<ParticleSystem>().Stop();
        Immune = false;
    }
}

using System.Reflection.Emit;
using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class player : MonoBehaviour
{
    Rigidbody rb;
    public float speed;
    public GameObject enemy;
    public GameObject boom;
    public GameObject HPUI;
    public GameObject BOOOMUI;
    public float maxhp = 100;
    public float hp;
    public float maxbooomValue = 300;
    public float booomValue;
    public float booomValueRecovery = 1;

    bool alive = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = transform.GetComponent<Rigidbody>();     
        hp = maxhp;
        booomValue = maxbooomValue;
    }

    // Update is called once per frame
    void Update()
    {

        //transform.LookAt(new Vector3(enemy.transform.position.x, 1, enemy.transform.position.z));//朝向敌人

        //朝向指针
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray,out hit);
        transform.LookAt(new Vector3(hit.point.x, 1, hit.point.z));
        

        if(alive)
        {
            rb.velocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * speed;
        }

        if (Input.GetMouseButton(0))
        {
            Debug.DrawLine(transform.position, hit.point);
        }

        if (Input.GetMouseButtonUp(0) && booomValue >= 100)
        {
            GameObject boomClone;
            boomClone = Instantiate(boom, transform.position, transform.rotation);
            boomClone.GetComponent<Rigidbody>().velocity =
                transform.TransformDirection(Vector3.forward * 8);
            booomValue -= 100;
        }
        if(hp<=0)
        {
            alive = false;
            updatePlayerHPInfo();
        }

        updatePlayerBOOOMInfo();
    }

    public void updatePlayerHPInfo()
    {   
        if(alive)
        {
            HPUI.transform.GetChild(0).GetComponent<Image>().fillAmount = 
                hp/maxhp;
            HPUI.transform.GetChild(1).GetComponent<Text>().text = 
                hp.ToString();
        }   
        else
        {
            //rb.AddForce(Vector3.up * 20);
            transform.GetComponent<BoxCollider>().enabled =false;
        }
    }
    public void updatePlayerBOOOMInfo()
    {
        BOOOMUI.transform.GetChild(0).GetComponent<Image>().fillAmount =
            booomValue / maxbooomValue;
        BOOOMUI.transform.GetChild(6).GetComponent<Text>().text =
            ((int)(booomValue/100)).ToString();
        if(booomValue<=maxbooomValue)booomValue += booomValueRecovery;
    }
}

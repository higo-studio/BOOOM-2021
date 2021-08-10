using System;
using System.Threading;
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
    float maxbooomValue = 300;
    float booomValue;
    public float booomValueRecovery = 1;
    GameObject booom1, booom2, booom3;
    public GameObject deathUI;

    bool alive = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = transform.GetComponent<Rigidbody>();     
        hp = maxhp;
        booomValue = maxbooomValue;
        booom1 = BOOOMUI.transform.GetChild(1).gameObject;
        booom2 = BOOOMUI.transform.GetChild(2).gameObject;
        booom3 = BOOOMUI.transform.GetChild(3).gameObject;

    }

    // Update is called once per frame
    void Update()
    {
        //角色朝向敌人
        //transform.LookAt(new Vector3(
        //enemy.transform.position.x,
        //1,
        //enemy.transform.position.z));

        //角色朝向光标
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray,out hit);
        transform.LookAt(new Vector3(hit.point.x, 1, hit.point.z));
        

        if(alive)
        {
            rb.velocity = new Vector3(
                Input.GetAxis("Horizontal"), 
                0, 
                Input.GetAxis("Vertical")) * speed;
        }

        if (Input.GetMouseButton(0))
        {
            Debug.DrawLine(transform.position, hit.point);
        }

        if (Input.GetMouseButtonUp(0) && booomValue >= 100)
        {
            GameObject boomClone;
            boomClone = Instantiate(boom, transform.position, 
                transform.rotation);
            boomClone.GetComponent<Rigidbody>().velocity =
                transform.TransformDirection(Vector3.forward * 8);
            booomValue -= 100;
        }
        if(hp<=0)
        {
            alive = false;
            updatePlayerHPInfo();
            Time.timeScale = 0.3f;
            deathUI.SetActive(true);
        }

        updatePlayerBOOOMInfo();
    }

    public void updatePlayerHPInfo()
    {   
        if(alive)
        {
            HPUI.transform.GetChild(0).GetComponent<Image>().fillAmount = 
                hp/maxhp;
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

        if (booomValue >= maxbooomValue)
            booom3.SetActive(true);
        else
            booom3.SetActive(false);

        if (booomValue >= maxbooomValue / 3 * 2)
            booom2.SetActive(true);
        else
            booom2.SetActive(false);

        if (booomValue >= maxbooomValue / 3)
            booom1.SetActive(true);
        else
            booom1.SetActive(false);

        if (booomValue<=maxbooomValue)
            booomValue += booomValueRecovery * Time.deltaTime;
    }
}

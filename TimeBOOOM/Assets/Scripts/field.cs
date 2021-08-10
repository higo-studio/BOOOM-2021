using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class field : MonoBehaviour
{
    public float rate = 10;

    private void Start()
    {
        CameraShake.Instance.shake(8f, 0.3f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "bullet")
        {
            other.GetComponent<Rigidbody>().velocity /= rate; 
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "bullet")
        {
            other.GetComponent<Rigidbody>().velocity *= rate;
        }
    }


    public void autoDestory()
    {
        Destroy(gameObject);
    }
}

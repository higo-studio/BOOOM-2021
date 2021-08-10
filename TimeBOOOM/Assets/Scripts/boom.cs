using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boom : MonoBehaviour
{
    public GameObject field;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("explosion",3);
    }

    void explosion()
    {
        Instantiate(field,transform.position,transform.rotation);
        Destroy(gameObject);
    }
}

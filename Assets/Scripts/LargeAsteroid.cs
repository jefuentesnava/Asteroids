using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LargeAsteroid : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.transform.eulerAngles = new Vector3(0f, 0f, Random.Range(0, 360));
        GetComponent<Rigidbody2D>().AddForce(transform.up * 50);
    }

    // Update is called once per frame
    void Update()
    {
        //TODO
    }
}

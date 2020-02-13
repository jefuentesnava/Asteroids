using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public int hp = 10;

    // Start is called before the first frame update
    void Start()
    {
        this.transform.eulerAngles = new Vector3(0f, 0f, Random.Range(0, 360));
        GetComponent<Rigidbody2D>().AddForce(transform.up * 100);
    }

    // Update is called once per frame
    void Update()
    {
        if(hp <= 0) Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Bullet"){
            hp--;
            Debug.Log(hp);
        }
        if(other.tag == "Player" || other.tag == "Asteroid"){
            Debug.Log(this.transform.eulerAngles);
            this.transform.eulerAngles = new Vector3(0f, 0f, this.transform.eulerAngles.z - 180);
            //neutralize current force
            GetComponent<Rigidbody2D>().AddForce(transform.up * 100);
            //adds new force
            GetComponent<Rigidbody2D>().AddForce(transform.up * 100);
            Debug.Log(this.transform.eulerAngles);
        }
    }
}

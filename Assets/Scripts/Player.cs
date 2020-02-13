using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 5f;
    public GameObject bullet;
    public static float bulletCD = .25f; 
    private float targetTime = bulletCD;
    // Start is called before the first frame update
    void Start()
    {
        
    } 

     void Update() {
         targetTime -= Time.deltaTime;
         if(Input.GetKey(KeyCode.W)) {
             GetComponent<Rigidbody2D>().AddForce(transform.up * speed);
         }
         if(Input.GetKey(KeyCode.A)) {
             transform.Rotate (Vector3.forward * 1);
         }
         if(Input.GetKey(KeyCode.S)) {
             GetComponent<Rigidbody2D>().AddForce(-transform.up * speed);
         }
         if(Input.GetKey(KeyCode.D)) {
             transform.Rotate (Vector3.forward * -1);
         }
         if(Input.GetKey(KeyCode.Space) && targetTime <= 0) {
             targetTime = bulletCD;
             GameObject bulletInstance = (GameObject)Instantiate(bullet);
              bulletInstance.transform.position = this.transform.position;
              bulletInstance.transform.eulerAngles = this.transform.eulerAngles;
              bulletInstance.GetComponent<Rigidbody2D>().AddForce(transform.up * 500);
         }
         if(Input.GetKey(KeyCode.KeypadEnter)) {
             Debug.Log(transform.eulerAngles);
         }
     }

    
}

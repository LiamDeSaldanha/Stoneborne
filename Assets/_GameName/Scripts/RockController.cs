using System;
using UnityEngine;

public class RockController : MonoBehaviour
{
    public GameObject parent;
    public float spinSpeed = 40.0f;
    public Boolean fired = false;
    public bool combine = false;
    public bool expand = false;
    public bool contract= false;
    public float speedRelease = 5000f;
    
    Rigidbody rb ;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //player = GameObject.FindWithTag("Player");
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {


        if (fired)
        {
            fired = false;
            transform.parent = null;
            GetComponent<Collider>().enabled = true;
            rb.useGravity = true;
            rb.AddForce(parent.transform.forward * speedRelease, ForceMode.Impulse);
            


        }
        else if (combine)
        {
            //transform.parent = null;
            //transform.position = player.transform.position + new Vector3(0,3,0);
            Vector3 target = parent.transform.position + new Vector3(0, 3, 0);
            transform.position = Vector3.MoveTowards(transform.position, target, 0.5f * Time.deltaTime);
            


        }
        else if (expand) {
            
            transform.Translate(Vector3.forward * 4f * Time.deltaTime);

        } else if (contract) {

            transform.Translate(Vector3.back * 4f * Time.deltaTime);
            
        }

        transform.Rotate(Vector3.forward, Time.deltaTime * spinSpeed);
        
    }
}

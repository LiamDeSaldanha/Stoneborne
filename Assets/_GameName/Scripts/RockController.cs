using System;
using UnityEngine;

public class RockController : MonoBehaviour
{
    public GameObject parent;
    public int index;
    public float spinSpeed = 40.0f;
    public Boolean fired = false;
    public bool combine = false;
    public bool expand = false;
    public bool contract= false;
    public float speedRelease = 5000f;
    public float expansionSpeed = 4f;
    public bool isBigRock = false;
    public bool isLongRock ;
    public AudioClip swishSFX;
    private AudioSource audioSource;
    
    Rigidbody rb ;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //player = GameObject.FindWithTag("Player");
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
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
            GetComponent<Outline>().enabled = false;
            audioSource.Stop();
            audioSource.clip = swishSFX;
            audioSource.Play();

        }
        else if (combine)
        {
            
            Vector3 target = parent.transform.position + new Vector3(0, 6, 0);
            transform.position = Vector3.MoveTowards(transform.position, target, 0.5f * Time.deltaTime);
            


        }
        else if (expand) {
            
            transform.Translate(Vector3.forward * expansionSpeed * Time.deltaTime);

        } else if (contract) {

            transform.Translate(Vector3.back * expansionSpeed * Time.deltaTime);

        }
        
            transform.Rotate(Vector3.forward, Time.deltaTime * spinSpeed);


        

    }
}

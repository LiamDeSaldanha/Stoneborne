using System;
using Unity.VisualScripting;
using UnityEditor.Search;
using UnityEngine;
using System.Collections.Generic;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.InputSystem.HID;

public class CollisionDection : MonoBehaviour
{
    GameObject player;
    RockCircleController rockCircle;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.Find("Player");
        
         rockCircle = transform.parent.GetComponent<RockCircleController>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy") && !rockCircle.parent.CompareTag("Enemy"))
        {

            if (CompareTag("Rock"))
            {
                Debug.Log("collesion detection");
                Transform child = other.gameObject.transform.Find("Health Slider");
                if (child != null)
                {



                    rockCircle.updateNextRock = true;





                    GameObject childObj = child.gameObject;


                    childObj.GetComponent<HealthSliderManager>().resolveCollision(1);






                    Destroy(gameObject);
                    // rockCircle.GetComponent<RockCircleController>().nextRock = new Queue<int>(newQueue);
                }


            }
            else if (CompareTag("Big_Rock"))
            {
                Transform child = other.gameObject.transform.Find("Health Slider");

                if (child != null)
                {
                    GameObject childObj = child.gameObject;


                    childObj.GetComponent<HealthSliderManager>().resolveCollision(50);
                }

            }

        }

        if (other.CompareTag("Player") && !rockCircle.parent.CompareTag("Player")) {

            rockCircle.updateNextRock = true;
            GameObject child = GameObject.Find("Player Health Slider");




            GameObject childObj = child.gameObject;


            childObj.GetComponent<HealthSliderManager>().resolveCollision(1);






            Destroy(gameObject);

        }
    
    
    
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        
    }





}

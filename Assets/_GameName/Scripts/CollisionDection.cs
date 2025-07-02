using System;
using Unity.VisualScripting;
using UnityEditor.Search;
using UnityEngine;
using System.Collections.Generic;
using static UnityEngine.GraphicsBuffer;

public class CollisionDection : MonoBehaviour
{
    GameObject player;
    GameObject rockCircle;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.Find("Player");
        rockCircle = (player.transform.Find("RockCircle")).gameObject;
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") )
        {

            if (CompareTag("Rock") )
            {
                Transform child = other.transform.Find("Health Slider");
                if (child != null)
                {
                    
                    
                   
               rockCircle.GetComponent<RockCircleController>().updateNextRock = true;
                



                
                    GameObject childObj = child.gameObject;
                  
                    
                    childObj.GetComponent<HealthSliderManager>().resolveCollision(1);

                    




                    Destroy(gameObject);
                   // rockCircle.GetComponent<RockCircleController>().nextRock = new Queue<int>(newQueue);
                }
                

            }
            else if (CompareTag("Big_Rock")) {
                Transform child = other.transform.Find("Health Slider");

                if (child != null)
                {
                    GameObject childObj = child.gameObject;


                    childObj.GetComponent<HealthSliderManager>().resolveCollision(10);
                }

            }

            }
    }

    




}

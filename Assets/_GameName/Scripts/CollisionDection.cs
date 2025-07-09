using System;
using Unity.VisualScripting;
using UnityEditor.Search;
using UnityEngine;
using System.Collections.Generic;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.InputSystem.HID;
using Unity.Cinemachine;
using System.Collections;

public class CollisionDection : MonoBehaviour
{
    GameObject player;
    RockCircleController rockCircle;
    public ParticleSystem damageParticlePrefab;
    ParticleSystem damageParticleInstance;
    public GameObject floatingTextPF;
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

     void CreateFloatingText(Collider other)
    {
        GameObject prefab = Instantiate(floatingTextPF, other.transform.position, Quaternion.identity);
        prefab.GetComponentInChildren<TextMesh>().text = "5";
        Debug.Log("Destroying prefab");
        Destroy(prefab,2);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy") && !rockCircle.parent.CompareTag("Enemy"))
        {

            if (CompareTag("Rock"))
            {


                Debug.Log("collesion detection");
                CreateFloatingText(other);
                
                Transform child = other.gameObject.transform.Find("Health Slider");
                if (child != null)
                {

                    SpawnDamageParticle(other);
                    rockCircle.updateNextRock = true;





                    GameObject childObj = child.gameObject;


                    childObj.GetComponent<HealthSliderManager>().resolveCollision(5);






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

        if (other.CompareTag("Player") && !rockCircle.parent.CompareTag("Player"))
        {

            rockCircle.updateNextRock = true;
            GameObject child = GameObject.Find("Player Health Slider");




            GameObject childObj = child.gameObject;


            childObj.GetComponent<HealthSliderManager>().resolveCollision(1);






            Destroy(gameObject);

        }



    }

    

    void SpawnDamageParticle(Collider other) {
        Vector3 offset = new Vector3(0, 1, 0);
        damageParticleInstance = Instantiate(damageParticlePrefab,other.transform.position + offset,Quaternion.identity);
    
    
    }





}

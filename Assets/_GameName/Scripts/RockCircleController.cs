using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;
using System.Collections.Generic;

public class RockCircleController : MonoBehaviour
{
    public GameObject parent;
    public float spinSpeed = 40.0f;
    private Vector3 offset = new Vector3(-0.64f, 0, 0.63f);
    public float yOffset;
    public GameObject[] rocks;
    public GameObject bigRock;
    public GameObject genericRocks;
    private Vector3[] originalRockPos = {new Vector3(0,0,2),
    new Vector3(1.4142f,0,1.4142f),
    new Vector3(2,0,0),
    new Vector3(1.4142f,0,-1.4142f),
    new Vector3(0,0,-2),
    new Vector3(-1.4142f,0,-1.4142f),
    new Vector3(-2,0,0),
    new Vector3(-1.4142f,0,1.4142f)};
    public bool timer_running = false;
    bool timer = false;
    public Slider reloadSlider;
    public Slider bigRockSlider;
    public Slider expansionSlider;


    public Queue<int> rockQueue = new Queue<int>();

    int rockInterval;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {


        if (parent.CompareTag("Player"))
        {


            if (Input.GetKeyDown(KeyCode.F))
            {

                if (RockController.isBigRock)
                {
                    RockController.isBigRock = false;
                    rocks[0].GetComponent<RockController>().fired = true;
                    rocks[0] = null;
                }
                else
                {


                    if (rockInterval > rocks.Length - 1)
                    {

                        rockInterval = 0;
                    }
                    if (rocks[rockInterval] != null)
                    {
                        rocks[rockInterval].GetComponent<RockController>().fired = true;

                        rockQueue.Enqueue(rockInterval);

                        rocks[rockInterval] = null;
                        rockInterval++;
                    }
                }



            }


            if (Input.GetKeyDown(KeyCode.G) && rockCircleFull())
            {
                RockController.isBigRock = true;
                for (int i = 0; i < rocks.Length; i++)
                {
                    if (rocks[i] != null)
                    {
                        rocks[i].GetComponent<RockController>().combine = true;

                    }

                }
                StartCoroutine(GenerateBigRock());


            }

            transform.Rotate(0, Time.deltaTime * spinSpeed, 0);


            if (Input.GetKeyDown(KeyCode.R))
            {

                for (int i = 0; i < rocks.Length; i++)
                {
                    if (rocks[i] != null)
                    {
                        rocks[i].GetComponent<RockController>().expand = true;
                    }

                }

                StartCoroutine(GenerateRockExpansion());



            }




        }



        if (!timer && !rockCircleFull() && !RockController.isBigRock)
        {
            timer = true;
            Debug.Log(parent.name + " : Timer detected");
            StartCoroutine(rockTimer());
        }


    }


    IEnumerator rockTimer()
    {




        float waitTime = 5f;
        float timeLeft = waitTime;

        while (timeLeft > 0)
        {
            if (parent.CompareTag("Player"))
            {
                reloadSlider.value = waitTime - timeLeft;

            }
            yield return null;
            timeLeft -= Time.deltaTime;
        }
        if (parent.CompareTag("Player"))
        {

            reloadSlider.value = 0;

        }
        Debug.Log(parent.name + "timer finished");
        timer = false;

        spawnGenericRock();


    }

    IEnumerator GenerateBigRock()
    {

        float waitTime = 6f;
        float timeLeft = waitTime;


        while (timeLeft > 0)
        {
            if (parent.CompareTag("Player"))
            {
                bigRockSlider.value = waitTime - timeLeft;

            }
            yield return null;
            timeLeft -= Time.deltaTime;
        }
        if (parent.CompareTag("Player"))
        {

            bigRockSlider.value = 0;

        }


        for (int i = 0; i < rocks.Length; i++)
        {
            if (rocks[i] != null)
            {
                Destroy(rocks[i]);
                rockQueue.Enqueue(i);
            }

        }
        rocks[0] = Instantiate(bigRock, parent.transform.position + new Vector3(0, 6, 0), bigRock.transform.rotation, transform);
        rocks[0].GetComponent<RockController>().parent = parent;

    }


    IEnumerator GenerateRockExpansion()
    {
        float waitTime = 4f;
        float timeLeft = waitTime;

        while ( timeLeft> (waitTime / 2) )
        {
            if (parent.CompareTag("Player"))
            {
                expansionSlider.value = waitTime - timeLeft;

            }
            yield return null;
            timeLeft -= Time.deltaTime;
        }


        for (int i = 0; i < rocks.Length; i++)
        {
            if (rocks[i] != null)
            {

                rocks[i].GetComponent<RockController>().expand = false;

                rocks[i].GetComponent<RockController>().contract = true;


            }

        }



        while (timeLeft >0)
        {
            if (parent.CompareTag("Player"))
            {
                expansionSlider.value = waitTime - timeLeft;

            }
            yield return null;
            timeLeft -= Time.deltaTime;
        }
        if (parent.CompareTag("Player"))
        {

            expansionSlider.value = 0;

        }

        for (int i = 0; i < rocks.Length; i++)
        {
            if (rocks[i] != null)
            {



                rocks[i].GetComponent<RockController>().contract = false;


            }

        }

    }





    public bool rockCircleFull()
    {

        for (int i = 0; i < rocks.Length; i++)
        {
            if (rocks[i] == null)
            {
                return false;

            }

        }
        return true;

    }

    public void spawnGenericRock()
    {

        if (rockQueue.Count != 0)
        {
            int temp = rockQueue.Dequeue();
            rocks[temp] = Instantiate(genericRocks, transform);
            rocks[temp].GetComponent<RockController>().parent = parent;


            rocks[temp].transform.localPosition = originalRockPos[temp];
            rocks[temp].transform.localRotation = Quaternion.Euler(0, temp * 45, 0);

            return;
        }



    }


}

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
    public TextMeshProUGUI reloadText;
    private float waitTime = 5f;
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


            if (Input.GetKeyDown(KeyCode.G))
            {

                for (int i = 0; i < rocks.Length; i++)
                {
                    if (rocks[i] != null)
                    {
                        rocks[i].GetComponent<RockController>().combine = true;//
                                                                               // Destroy(rocks[i].gameObject);
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



        if (!timer && !rockCircleFull())
        {
            timer = true;
            Debug.Log(parent.name + " : Timer detected");
            StartCoroutine(rockTimer());
        }


    }


    IEnumerator rockTimer()
    {





        float timeLeft = waitTime;

        while (timeLeft > 0)
        {
            if (parent.CompareTag("Player"))
            {
                reloadText.text = timeLeft.ToString("F1");
            }
            yield return null;
            timeLeft -= Time.deltaTime;
        }
        if (parent.CompareTag("Player"))
        {
            
            reloadText.text = "0s";
        }
        Debug.Log(parent.name +"timer finished");
        timer = false;
        
        spawnGenericRock();


    }

    IEnumerator GenerateBigRock()
    {

        yield return new WaitForSeconds(6.5f);
        for (int i = 0; i < rocks.Length; i++)
        {
            if (rocks[i] != null)
            {
                Destroy(rocks[i].gameObject);
            }

        }
        rocks[rockInterval] = Instantiate(bigRock, parent.transform.position + new Vector3(0, 4, 0), bigRock.transform.rotation, transform);
        rocks[rockInterval].GetComponent<RockController>().parent = parent;
        // rocks[rockInterval].GetComponent<RockController>().combine = true;
    }


    IEnumerator GenerateRockExpansion()
    {

        yield return new WaitForSeconds(2f);
        for (int i = 0; i < rocks.Length; i++)
        {
            if (rocks[i] != null)
            {

                rocks[i].GetComponent<RockController>().expand = false;

                rocks[i].GetComponent<RockController>().contract = true;


            }

        }
        StartCoroutine(GenerateRockContraction());

    }

    IEnumerator GenerateRockContraction()
    {

        yield return new WaitForSeconds(2f);
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

        if (rockQueue.Count != 0) { 
            int temp =rockQueue.Dequeue();
            rocks[temp] = Instantiate(genericRocks, transform);
            rocks[temp].GetComponent<RockController>().parent = parent;
            

            rocks[temp].transform.localPosition = originalRockPos[temp];
            rocks[temp].transform.localRotation = Quaternion.Euler(0, temp * 45, 0);

            return;
        }



       /* for (int i = 0; i < rocks.Length; i++)
        {
            if (rocks[i] == null)
            {



                
                rocks[i] = Instantiate(genericRocks, transform);
                rocks[i].GetComponent<RockController>().parent = parent;
                
                rocks[i].transform.localPosition = originalRockPos[i]; // Set local space
                return;


            }

        }*/
    }


}

using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;
using System.Collections.Generic;
using System;

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
    public Slider fireSlider;
    public bool hasBigRock;
    public float rockReloadTime = 4f;
    public float expansionSpeed = 4f;

    public Queue<int> rockQueue = new Queue<int>();
    public Queue<int> nextRock = new Queue<int>(new int[] { 0, 1, 2, 3, 4, 5, 6, 7 });


    public bool updateNextRock = false;
    private bool spell3Running = false;
    private bool spell2Running = false;

    public string Enemy_name;
    public bool GolemAttack;
    private bool golemAttackTimer;

    public bool bossTimer = false;
    public bool BossAttack1;
    public bool BossAttack2;

    public bool BigRockReady;



    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void LateUpdate()
    {


        if (parent.CompareTag("Player"))
        {
            if ((nextRock.Count != 0) && rocks[nextRock.Peek()] != null)
            {
                if (!rocks[nextRock.Peek()].GetComponent<Outline>().enabled)
                {
                    rocks[nextRock.Peek()].GetComponent<Outline>().enabled = true;

                }
            }

            if (updateNextRock)
            {


                updateNextRock = false;
            }

            if (Input.GetKeyDown(KeyCode.F) && !spell3Running && !spell2Running)
            {

                if (hasBigRock)
                {
                    hasBigRock = false;
                    rocks[0].GetComponent<RockController>().fired = true;
                    StartCoroutine(FireRock());
                    rocks[0] = null;

                }
                else
                {


                    Debug.Log("nextRock count: " + nextRock.Count);
                    if (nextRock.Count <= 8 && nextRock.Count > 0)
                    {
                        int index = nextRock.Dequeue();

                        // rockQueue.Enqueue(index);
                        rocks[index].GetComponent<RockController>().fired = true;
                        rocks[index] = null;
                        StartCoroutine(FireRock());

                        //  rockQueue.Enqueue(rockInterval);



                    }
                }



            }


            if (Input.GetKeyDown(KeyCode.G) && rockCircleFull() && parent.GetComponent<PlayerController>().bigRockUnlockd)
            {
                spell2Running = true;
                hasBigRock = true;
                for (int i = 0; i < rocks.Length; i++)
                {
                    if (rocks[i] != null)
                    {
                        rocks[i].GetComponent<RockController>().combine = true;

                    }

                }
                StartCoroutine(GenerateBigRock());
                nextRock.Clear();


            }




            if (Input.GetKeyDown(KeyCode.R) && rockCircleFull() && parent.GetComponent<PlayerController>().ExpansionUnlockd)
            {
                spell3Running = true;
                spinSpeed = 500;

                for (int i = 0; i < rocks.Length; i++)
                {
                    if (rocks[i] != null)
                    {
                        rocks[i].GetComponent<RockController>().expand = true;
                        rocks[i].GetComponent<Collider>().enabled = true;

                    }

                }

                StartCoroutine(GenerateRockExpansion());



            }




        }

        transform.Rotate(0, Time.deltaTime * spinSpeed, 0);




        if (parent.CompareTag("Enemy") && Enemy_name == "Golem" && GolemAttack &&
            !golemAttackTimer && rockCircleFull())
        {



            golemAttackTimer = true;
            StartCoroutine(golemTimer());
            spell3Running = true;
            spinSpeed = 150* expansionSpeed;

            for (int i = 0; i < rocks.Length; i++)
            {
                if (rocks[i] != null)
                {
                    rocks[i].GetComponent<RockController>().expansionSpeed = expansionSpeed;

                    rocks[i].GetComponent<RockController>().expand = true;
                    rocks[i].GetComponent<Collider>().enabled = true;

                }

            }

            StartCoroutine(GenerateRockExpansion());




        }





        if (parent.CompareTag("Enemy") && Enemy_name == "Boss" &&
             !bossTimer && BossAttack1 && rockCircleFull() && !spell2Running && !spell3Running)
        {
            bossTimer = true;
            StartCoroutine(BossTimer());
            spell2Running = true;
            hasBigRock = true;
            for (int i = 0; i < rocks.Length; i++)
            {
                if (rocks[i] != null)
                {
                    rocks[i].GetComponent<RockController>().combine = true;
                }

            }
            StartCoroutine(GenerateBigRock());
            nextRock.Clear();


        }

        if (parent.CompareTag("Enemy") && Enemy_name == "Boss" &&
             !bossTimer && BossAttack2 && rockCircleFull() && !spell2Running && !spell3Running)
        {
            bossTimer = true;
            StartCoroutine(BossTimer());
            spell3Running = true;
            spinSpeed = 500;

            for (int i = 0; i < rocks.Length; i++)
            {
                if (rocks[i] != null)
                {
                    rocks[i].GetComponent<RockController>().expand = true;
                    rocks[i].GetComponent<Collider>().enabled = true;

                }

            }

            StartCoroutine(GenerateRockExpansion());


        }


        if (!timer && !rockCircleFull() && !hasBigRock && !spell3Running && !spell2Running)
        {
            timer = true;
            Debug.Log(parent.name + " : Timer detected");
            StartCoroutine(rockTimer());
        }




    }


    IEnumerator golemTimer()
    {


        yield return new WaitForSeconds(7f);
        golemAttackTimer = false;
    }

    IEnumerator BossTimer()
    {


        yield return new WaitForSeconds(11f);
        bossTimer = false;
    }




    IEnumerator rockTimer()
    {




        float waitTime = rockReloadTime;
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
        spell2Running = false;
        if (BossAttack1)
        {
            StartCoroutine(ThrowBigRock());

            BossAttack1 = false;

        }



    }


    IEnumerator ThrowBigRock()
    {

        yield return new WaitForSeconds(2f);
        rocks[0].GetComponent<RockController>().fired = true;
        rocks[0] = null;
        hasBigRock = false;
    }

    IEnumerator GenerateRockExpansion()
    {
        float waitTime = 4f;
        float timeLeft = waitTime;

        while (timeLeft > (waitTime / 2))
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



        while (timeLeft > 0)
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
                rocks[i].GetComponent<Collider>().enabled = false;



            }

        }
        spinSpeed = 40;
        updateNextRockQueue();
        spell3Running = false;
        if (GolemAttack)
        {
            GolemAttack = false;
        }

        if (BossAttack2)
        {
            BossAttack2 = false;
        }
    }

    IEnumerator FireRock()
    {

        float waitTime = 0.2f;
        float timeLeft = waitTime;


        while (timeLeft > 0)
        {
            if (parent.CompareTag("Player"))
            {
                fireSlider.value = waitTime - timeLeft;

            }
            yield return null;
            timeLeft -= Time.deltaTime;
        }
        if (parent.CompareTag("Player"))
        {

            fireSlider.value = 0;

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
        // Debug.Log("rockQueue"+rockQueue.Count);
        for (int i = 0; i < rocks.Length; i++)
            if (rocks[i] == null)
            {
                //int temp = rockQueue.Dequeue();
                //Debug.Log("Tried to spawn/add to nextRock" + temp);
                rocks[i] = Instantiate(genericRocks, transform);
                rocks[i].GetComponent<RockController>().index = i;
                rocks[i].GetComponent<RockController>().parent = parent;
                if (parent.CompareTag("Enemy"))
                {
                    rocks[i].tag = "Enemy_Rock";
                }

                if (Enemy_name == "Golem")
                {
                    rocks[i].transform.localPosition = originalRockPos[i*2];
                }
                else
                {
                    rocks[i].transform.localPosition = originalRockPos[i];
                }
                rocks[i].transform.localRotation = Quaternion.Euler(0, i * 45, 0);

                nextRock.Enqueue(i);


                return;
            }



    }

    bool inQueue(int target)
    {
        Queue<int> temp = new Queue<int>(nextRock);
        while (temp.Count > 0)
        {

            int value = temp.Dequeue();
            if (value == target)
            {

                return true;
            }

        }
        return false;

    }


    void updateNextRockQueue()
    {

        Queue<int> temp = new Queue<int>();
        for (int i = 0; i < rocks.Length; i++)
        {

            if (rocks[i] != null && inQueue(i))
            {

                temp.Enqueue(i);
            }



        }
        nextRock.Clear();
        nextRock = new Queue<int>(temp);// this isnt instaniating

    }




}

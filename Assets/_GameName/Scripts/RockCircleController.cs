using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

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

    int rockInterval;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void LateUpdate()
    {


        if (parent.CompareTag("Player"))
        {
            // transform.Rotate(0, 90 * Time.deltaTime, 0, Space.Self);
            if (Input.GetKeyDown(KeyCode.F) && rockInterval < rocks.Length)
            {

                if (rocks[rockInterval] != null)
                {
                    rocks[rockInterval].GetComponent<RockController>().fired = true;
                    rocks[rockInterval] = null;
                }
                rockInterval++;
                if (rockInterval > rocks.Length)
                {

                    rockInterval = 0;
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
        rocks[rockInterval].GetComponent<RockController>().combine = true;
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
        for (int i = 0; i < rocks.Length; i++)
        {
            if (rocks[i] == null )
            {



                //rocks[rockInterval] = Instantiate(genericRocks, transform.position + originalRockPos[rockInterval], genericRocks.transform.rotation, transform);
                
                rocks[rockInterval] = Instantiate(genericRocks);
                rocks[rockInterval].GetComponent<RockController>().parent = gameObject; //
                rocks[rockInterval].transform.SetParent(transform, false); // Keep local position
                rocks[rockInterval].transform.localPosition = originalRockPos[rockInterval]; // Set local space

            }

        }
    }


}

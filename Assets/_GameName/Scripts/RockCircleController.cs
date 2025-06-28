using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class RockCircleController : MonoBehaviour
{
    private GameObject player;
    public float spinSpeed = 40.0f;
    private Vector3 offset = new Vector3(-0.64f, 0, 0.63f);
    public float yOffset;
    public GameObject[] rocks;
    public GameObject bigRock;

    int rockInterval;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void LateUpdate()
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
            if (rockInterval > rocks.Length) {
                
                rockInterval = 0;
            }
        }


        if (Input.GetKeyDown(KeyCode.G))
        {

            for (int i = 0; i < rocks.Length; i++)
            {
                if (rocks[i] != null)
                {
                    rocks[i].GetComponent<RockController>().combine= true;//
                   // Destroy(rocks[i].gameObject);
                }
                
            }
            StartCoroutine(GenerateBigRock());
            

        }
        else
        {
            transform.Rotate(0, Time.deltaTime * spinSpeed, 0);
        }

    }

    IEnumerator GenerateBigRock() {

        yield return new WaitForSeconds(6.5f);
        for (int i = 0; i < rocks.Length; i++)
        {
            if (rocks[i] != null)
            {
                 Destroy(rocks[i].gameObject);
            }

        }
        rocks[rockInterval] = Instantiate(bigRock, player.transform.position + new Vector3(0, 4, 0), bigRock.transform.rotation,transform);
        rocks[rockInterval].GetComponent<RockController>().combine = true;
    }
}

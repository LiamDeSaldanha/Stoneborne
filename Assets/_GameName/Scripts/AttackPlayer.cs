using System.Collections;
using UnityEngine;

public class AttackPlayer : MonoBehaviour
{
    public GameObject rockCircle;
    private RockCircleController rockCircleController;
    bool timer = false;
    
    private int rockInterval;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

       rockCircleController = rockCircle.gameObject.GetComponent<RockCircleController>();

    }

    // Update is called once per frame
    void Update()
    {
        if (!timer && !rockCircleController.rockCircleFull()) { 
        timer = true;
        rockCircleController.spawnGenericRock();
            StartCoroutine(rockTimer());
        }
    }

    IEnumerator rockTimer() {
        yield return new WaitForSeconds(5f);
        Debug.Log("timer finished");
        timer = false;
    }

    private void OnTriggerStay(Collider other)
    {

        if (other.CompareTag("Player")) {
            if (rockInterval > rockCircleController.rocks.Length -1)
            {

                rockInterval = 0;
                
                
            }


            if (rockCircleController.rocks[rockInterval] != null)
            {
                rockCircleController.rocks[rockInterval].GetComponent<RockController>().fired = true;
                rockCircleController.rocks[rockInterval] = null;
            }
            rockInterval++;
            


        }


    }
}

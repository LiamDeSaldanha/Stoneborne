using System.Collections;
using UnityEngine;

public class AttackPlayer : MonoBehaviour
{

    public GameObject rockCircle;
    private RockCircleController rockCircleController;


    private int rockInterval;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        rockCircleController = rockCircle.GetComponent<RockCircleController>();

    }

    // Update is called once per frame
    void Update()
    {

    }



    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && rockCircleController.Enemy_name == "Golem")
        {
            Debug.Log("golem");
            rockCircleController.GolemAttack = true;

        }
        else if ( other.CompareTag("Player") && rockCircleController.Enemy_name == "Boss" && !rockCircleController.hasBigRock) {


            
            rockCircleController.BossAttack = true;
            
        }
        else
        {
            if ((other.CompareTag("Player") && !(rockCircleController.Enemy_name == "Boss")
                && !(rockCircleController.Enemy_name == "Golem")) )
            {
                if (rockInterval > rockCircleController.rocks.Length - 1)
                {

                    rockInterval = 0;


                }


                if (rockCircleController.rocks[rockInterval] != null)
                {
                    rockCircleController.rocks[rockInterval].GetComponent<RockController>().fired = true;
                    rockCircleController.rockQueue.Enqueue(rockInterval);
                    rockCircleController.rocks[rockInterval] = null;
                    rockInterval++;
                }
                rockCircleController.BossAttack = false;



            }
        }


    }
}

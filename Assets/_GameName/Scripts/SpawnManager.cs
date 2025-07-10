using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public int numberOfEnemies;
    public float spawnInterval;
    public GameObject[] enemies;
    private int numberOfSpawnedEnemies;
    public float sizeOfSpawnArea;
    private bool timerExpired = false;
    public TextMesh text;
    GameObject player;
    public string spawnName;
    public bool playerEnetered;
    public bool playerLeft;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemies = new GameObject[numberOfEnemies];
        text = GetComponentInChildren<TextMesh>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {




        if (numberOfSpawnedEnemies == numberOfEnemies)
        {
            text.text = "Done Spawning Enemies!";
            if (AllEnemiesSlain())
            {
                if (!playerLeft)
                {
                    player.GetComponent<PlayerController>().inCombat = false;
                    playerLeft = true;
                }
                text.color = Color.yellow;
                text.text = "All Enemies Slain!";
                if (spawnName == "Rock Camp")
                {

                    player.GetComponent<PlayerController>().bigRockUnlockd = true;
                }
                else if (spawnName == "Camp 2")
                {
                    player.GetComponent<PlayerController>().ExpansionUnlockd = true;
                }
                else if (spawnName == "Boss Camp")
                {

                }

            }

        }
        else
        {

            text.text = "Enemies Left: " + (numberOfEnemies - numberOfSpawnedEnemies);
        }

    }

    private void OnTriggerStay(Collider other)
    {

        while (numberOfSpawnedEnemies < numberOfEnemies && !timerExpired && other.CompareTag("Player"))
        {
            timerExpired = true;
            spawn();
            StartCoroutine(timer());


        }


    }


    private void spawn()
    {
        if (numberOfSpawnedEnemies < numberOfEnemies)
        {




            Vector3 randomPos = new Vector3(Random.Range(-sizeOfSpawnArea, sizeOfSpawnArea), 0f, Random.Range(-sizeOfSpawnArea, sizeOfSpawnArea));
            enemies[numberOfSpawnedEnemies] = Instantiate(enemyPrefab, Vector3.zero, Quaternion.identity, transform);
            enemies[numberOfSpawnedEnemies].transform.localPosition = randomPos;
            //enemies[numberOfSpawnedEnemies].transform.localScale = Vector3.one;

            numberOfSpawnedEnemies++;
        }

    }

    public bool AllEnemiesSlain()
    {
        if (numberOfSpawnedEnemies == numberOfEnemies)
        {
            for (int i = 0; i < numberOfEnemies; i++)
            {

                if (enemies[i] != null)
                {

                    return false;
                }
            }
            return true;
        }
        else { 
        
        return false;
        }
        



    }

    IEnumerator timer()
    {
        yield return new WaitForSeconds(spawnInterval);
        timerExpired = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") &&!playerEnetered) {
        
        other.GetComponent<PlayerController>().inCombat = true;
        playerEnetered = true;
        
        }
    }


}

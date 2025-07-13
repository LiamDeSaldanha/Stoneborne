using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameController : MonoBehaviour
{
    public GameObject player;
    public  GameObject PlayerUI;
    public  GameObject GameOverUI;
    public  GameObject WinUI;
    public  GameObject loreUI;
    public  GameObject helpUI;
    public  GameObject startUI;
    public  GameObject exitUI;
    public  GameObject Boss;
    public  GameObject BossSpawner;
    public  AudioClip bossMusic;
    public AudioClip enemySpawner;
    public AudioClip exploring;
    public AudioClip winMusic;
    public bool combatCooldown = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Time.timeScale = 0f;
    }

    public void CloseExitUI() {

        exitUI.SetActive(false);
   
    }

    public void OpenExitUI() {

        exitUI.SetActive(true);
    }

    public void Exit() { 
    Application.Quit();
    
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
       // Time.timeScale = 1f;
    }

    public void CloseHelp() { 
    
    helpUI .SetActive(false);
        HelpUI.helpIsOpen = false;
    }

    public void CloseStart() { 
    
    startUI.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void CloseLore()
    {
        loreUI.SetActive(false);
        LoreUI.loreIsOpen = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OpenExitUI();
        }


        if (Boss == null && BossSpawner.GetComponent<SpawnManager>().AllEnemiesSlain()) { 
        
      //  Time.timeScale = 0f;
            PlayerUI.SetActive(false);
            WinUI.SetActive(true);
            player.GetComponent<CharacterController>().enabled = false;
        
        
        }

        if (player.GetComponent<PlayerController>().inCombat  ) {





            


            if ( player.GetComponent<AudioSource>().clip != enemySpawner) {

                player.GetComponent<AudioSource>().Stop();

                player.GetComponent<AudioSource>().clip = enemySpawner;
                player.GetComponent<AudioSource>().Play(); // If you want it to start playing
            }

        } else {
            if (player.GetComponent<AudioSource>().clip != exploring)
            {
                player.GetComponent<AudioSource>().Stop();

                player.GetComponent<AudioSource>().clip = exploring;
                player.GetComponent<AudioSource>().Play();
            }
        }



    }

   
}

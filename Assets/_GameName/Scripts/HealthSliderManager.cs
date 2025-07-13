using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSliderManager : MonoBehaviour
{
    public GameObject parent;
    public Slider healthSlider;
    public int maxHealthValue;
    private int currentHealthValue = 0;
    private PlayerController playerController;
    public GameObject PlayerUI;
    public GameObject GameOverUI;
    public GameObject WinUI;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        healthSlider.maxValue = maxHealthValue;
        healthSlider.value = maxHealthValue;
        currentHealthValue = maxHealthValue;
        healthSlider.fillRect.gameObject.SetActive(true);
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {


        
    }


    public void resolveCollision(int amount)
    {
        currentHealthValue -= amount;
        healthSlider.value = currentHealthValue;
        if (currentHealthValue <=0)
        {
            //  playerController.AddScore(amountToBeFed);

            if (parent.CompareTag("Player")) {
              //  Time.timeScale = 0f;
                parent.GetComponent<CharacterController>().enabled = false;
                PlayerUI.gameObject.SetActive(false);
                GameOverUI.gameObject.SetActive(true);
            }
            else {



                Destroy(gameObject, 0.1f);
                Destroy(parent); }
        }
    }



}

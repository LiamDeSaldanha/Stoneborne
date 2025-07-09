using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float throwSpeed = 20.0f;
    
    public bool bigRockUnlockd;
    public bool ExpansionUnlockd;
    public GameObject bigRockPanel;
    public GameObject ExpansionPanel;
    public GameObject NewSpellText;
    private bool flag1;
    private bool flag2;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bigRockPanel.SetActive(false);
        ExpansionPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {


        if (bigRockUnlockd) {
            if (!flag1)
            {
                StartCoroutine(NewSpellUnlocked());
                flag1 = true;
            }
            bigRockPanel.SetActive(true);
        }
        if (ExpansionUnlockd)
        {
            if (!flag2)
            {
                StartCoroutine(NewSpellUnlocked());
                flag2 = true;
            }

            ExpansionPanel.SetActive(true);

        }




  

    }
    IEnumerator NewSpellUnlocked()
    {

        NewSpellText.SetActive(true);

        yield return new WaitForSeconds(3f);
        NewSpellText.SetActive(false);
    }

}

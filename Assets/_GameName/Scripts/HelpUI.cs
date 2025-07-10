using UnityEngine;

public class HelpUI : MonoBehaviour
{

    public GameObject helpUI;
    public static bool helpIsOpen = false;


    void OnMouseDown()
    {
        if (!helpIsOpen)
        {
            helpUI.SetActive(true);
            helpIsOpen = true;
        }
    }
}

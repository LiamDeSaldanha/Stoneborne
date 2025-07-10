using UnityEngine;

public class LoreUI : MonoBehaviour
{
    public GameObject loreUI;
    public static bool loreIsOpen = false;

    void OnMouseDown()
    {
        if (!loreIsOpen)
        {
            loreUI.SetActive(true);
            loreIsOpen = true;
        }
    }
}

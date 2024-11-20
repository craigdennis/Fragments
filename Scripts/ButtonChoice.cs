using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;

public class ButtonChoice : MonoBehaviour
{

    public TextMeshProUGUI buttonOptionOne;
    public TextMeshProUGUI buttonOptionTwo;
    public TextMeshProUGUI buttonOptionThree;

    private RoomNavigation roomNavigation;

    void Awake()
    {
        roomNavigation = GetComponent<RoomNavigation>();
    }

    void DisplayButtonOptions() 
    {
        var descriptions = roomNavigation.GetInteractionDescriptions();
        if (descriptions.Count > 0) buttonOptionOne.text = descriptions[0];
        if (descriptions.Count > 1) buttonOptionTwo.text = descriptions[1];
        if (descriptions.Count > 2) buttonOptionThree.text = descriptions[2];
    }

    void HideAllButtons() 
    {
        buttonOptionOne.gameObject.SetActive(false);
        buttonOptionTwo.gameObject.SetActive(false);
        buttonOptionThree.gameObject.SetActive(false);
    }

    void ClearButtonText() 
    {
        buttonOptionOne.text = "";
        buttonOptionTwo.text = "";
        buttonOptionThree.text = "";
    }
    
}

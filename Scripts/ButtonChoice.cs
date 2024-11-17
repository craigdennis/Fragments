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

    private GameController controller;

    void Awake()
    {
        controller = GetComponent<GameController>();
    }

    void DisplayButtonOptions() 
    {
        buttonOptionOne.text = controller.interactionDescriptionsInRoom[0];
        buttonOptionTwo.text = controller.interactionDescriptionsInRoom[1];
        buttonOptionThree.text = controller.interactionDescriptionsInRoom[2];
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

using UnityEngine;
using UnityEngine.UI;

public class PowerMeter : MonoBehaviour
{
    public Slider powerMeter;
    public Button buildPowerButton;
    public Button swingHammerButton;
    public Text messageText;

    public int totalRounds = 5;
    private int currentRound = 1;

    private bool isBuildingPower = false;
    private float power = 0f;
    private float maxPower = 100f;
    private float powerBuildRate = 50f;

    private TimingMechanic timingMechanic;
    private int score = 0;

    void Start()
    {
        swingHammerButton.interactable = false;
        buildPowerButton.onClick.AddListener(OnBuildPowerButtonPressed);

        timingMechanic = GetComponent<TimingMechanic>();
        StartRound();
    }

    void Update()
    {
        if (isBuildingPower)
        {
            BuildPower();
        }
    }

    void StartRound()
    {
        messageText.text = $"Round {currentRound}/{totalRounds}\nPress 'Build Power' to start.";
        ResetPower();
    }

    void OnBuildPowerButtonPressed()
    {
        isBuildingPower = true;
        swingHammerButton.interactable = true;
        buildPowerButton.interactable = false;

        swingHammerButton.onClick.AddListener(OnSwingHammerButtonPressed);
    }

    void BuildPower()
    {
        if (power < maxPower)
        {
            power += powerBuildRate * Time.deltaTime;
            powerMeter.value = power;
        }
        else
        {
            power = maxPower;
            powerMeter.value = power;
            isBuildingPower = false;
            messageText.text = "Power Maxed!";
        }
    }

    void OnSwingHammerButtonPressed()
    {
        isBuildingPower = false;
        swingHammerButton.interactable = false;

        if (timingMechanic != null)
        {
            timingMechanic.StartTiming(currentRound);
        }

        buildPowerButton.interactable = false;
    }

    public void EvaluateSwing(bool timingSuccess)
    {
        string resultMessage = "";

        if (power > 70f && timingSuccess)
        {
            resultMessage = "You swung with great power and perfect timing! The bell rings loudly!";
            score += 10;
        }
        else if (power > 70f)
        {
            resultMessage = "Great power but your timing was off. The bell doesn't ring.";
            score += 5;
        }
        else if (power > 30f && timingSuccess)
        {
            resultMessage = "Moderate power but perfect timing. The bell rings faintly.";
            score += 5;
        }
        else
        {
            resultMessage = "Your swing lacked power and timing. Nothing happens.";
        }

        messageText.text = resultMessage;

        currentRound++;
        if (currentRound <= totalRounds)
        {
            Invoke("StartRound", 2f); // Wait for 2 seconds before starting the next round
        }
        else
        {
            messageText.text += $"\nYou've completed all rounds!\nYour total score is: {score}";
            buildPowerButton.interactable = false;
        }

        ResetPower();
    }

    void ResetPower()
    {
        power = 0f;
        powerMeter.value = power;
        swingHammerButton.onClick.RemoveListener(OnSwingHammerButtonPressed);
        buildPowerButton.interactable = true;
    }
}
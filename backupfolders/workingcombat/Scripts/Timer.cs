using UnityEngine;

public class Timer
{
    private float currentTime;
    private float duration;

    public bool IsReady => currentTime <= 0;
    public float TimeRemaining => currentTime;

    public void Update(float deltaTime)
    {
        if (currentTime > 0)
        {
            currentTime -= deltaTime;
        }
    }

    public void Start(float duration)
    {
        this.duration = duration;
        currentTime = duration;
    }

    public void Reset()
    {
        currentTime = duration;
    }
}
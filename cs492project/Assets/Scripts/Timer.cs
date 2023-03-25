using UnityEngine;

public abstract class Timer : MonoBehaviour
{
    public bool isTimerOn = false;
    public float duration;

    public void TurnTimerOn()
    {
        isTimerOn = true;
    }

    public void TurnTimerOff()
    {
        isTimerOn = false;
    }
}

using System.Collections;
using UnityEngine;

public class TimeWaiter : MonoBehaviour
{
    public static TimeWaiter Instance { get; private set; }
    public static IEnumerator WaitFor(float seconds)
    {
        yield return new WaitForSecondsRealtime(seconds);
    }
}

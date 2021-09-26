using System.Collections;
using System.Threading;
using Events;
using UnityEngine;

public class FramerateLimiter : MonoBehaviour
{
    public static float Limit = 120.0f;
    [SerializeField] private FloatEvent setFramerateLimitEvent;
    private float currentFrameTime;

    public void OnEnable()
    {
        setFramerateLimitEvent.OnRaised += SetFramerateLimit;
    }

    public void OnDisable()
    {
        setFramerateLimitEvent.OnRaised -= SetFramerateLimit;
    }

    private static void SetFramerateLimit(float limit)
    {
        Limit = limit;
    }

    public void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 9999;
        currentFrameTime = Time.realtimeSinceStartup;
        StartCoroutine(WaitForNextFrame());
    }

    private IEnumerator WaitForNextFrame()
    {
        // Reference: https://blog.unity.com/technology/precise-framerates-in-unity
        while (true)
        {
            yield return new WaitForEndOfFrame();
    
            if (Limit <= 0)
            {
                continue;
            }
    
            currentFrameTime += 1.0f / Limit;
            
            var t = Time.realtimeSinceStartup;
            var sleepTime = currentFrameTime - t - 0.01f;
    
            if (sleepTime > 0)
            {
                Thread.Sleep((int)(sleepTime * 1000));
            }
    
            while (t < currentFrameTime)
            {
                t = Time.realtimeSinceStartup;
            }
        }
    }
}
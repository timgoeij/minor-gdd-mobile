using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InternetTest : MonoBehaviour
{
    private bool internetAvailable = false;

    private Ping ping;

    private const string pingAddress = "8.8.8.8"; // Google Public DNS server
    private const float waitingTime = 5.0f;
    private float pingStartTime;

    public bool InternetAvailable
    {
        get { return internetAvailable; }
    }

    void Start()
    {
        CheckInternetConnection();
    }

    public void CheckInternetConnection()
    {
        Invoke("CheckInternetConnection", 10);

        Debug.Log("Check Internet connection");

        switch (Application.internetReachability)
        {
            case NetworkReachability.ReachableViaLocalAreaNetwork:
                internetAvailable = true;
                break;
            case NetworkReachability.ReachableViaCarrierDataNetwork:
                internetAvailable = true;
                break;
            default:
                internetAvailable = false;
                break;
        }

        ping = new Ping(pingAddress);
        pingStartTime = Time.time;
    }

    public void Update()
    {
        if (ping != null)
        {
            bool stopChecking = true;

            if (ping.isDone)
            {
                internetAvailable = true;
            }
            else if (Time.time - pingStartTime < waitingTime)
            {
                stopChecking = false;
            }
            else
            {
                internetAvailable = false;
                stopChecking = true;
            }

            if (stopChecking)
                ping = null;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InternetTest : MonoBehaviour
{
    private bool internetAvailable = false;

    public bool InternetAvailable
    {
        get { return internetAvailable; }
    }

    void Start()
    {
        StartCoroutine(CheckInternetConnection());
    }

    IEnumerator CheckInternetConnection()
    { 
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

        yield return new WaitForSeconds(20);

        yield return CheckInternetConnection();
    }
}
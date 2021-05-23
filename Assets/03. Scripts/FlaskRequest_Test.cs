using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class FlaskRequest_Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
       
    }

    void Update()
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(("http://127.0.0.1:5000")))
        {
           
            if (webRequest.isNetworkError)
            {
                Debug.Log("Error:" + webRequest.error);

            }
            else
            {
                Debug.Log("From Python Flake :"+ webRequest.downloadHandler.text);
            }
        }
    }
}

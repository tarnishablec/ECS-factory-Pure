using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotsDataWebSocketBootstrap : MonoBehaviour
{
    public string uri;

    private WebSocketClient socketClient;
    // Start is called before the first frame update
    void Awake()
    {
        socketClient = new WebSocketClient(uri);
        socketClient.Connect();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

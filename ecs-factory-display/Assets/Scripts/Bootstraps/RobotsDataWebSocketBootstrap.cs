using System.Collections;
using System.Collections.Generic;
using BestHTTP.WebSocket;
using UnityEngine;

public class RobotsDataWebSocketBootstrap : Bootstrap
{
    public string uri;

    private WebSocketClient socketClient;
    // Start is called before the first frame update
    void Start()
    {
        socketClient = new RobotDataWebSocketClient(uri);
        socketClient.Connect();
    }

    // Update is called once per frame
    void Update()
    {

    }
}

public class RobotDataWebSocketClient : WebSocketClient
{
    public RobotDataWebSocketClient(string uri) : base(uri)
    {
    }

    public override void OnMessageReceived(WebSocket ws, string message)
    {
        RobotPool.UpdateRobotsData(message);
        //Debug.Log("updated "+RobotPool.robotsData.Count);
    }
}

using BestHTTP.WebSocket;
using System;
using UnityEngine;

public class WebSocketClient
{
    protected string uri;

    private WebSocket webSocket;

    public WebSocketClient(string uri)
    {
        this.uri = uri;
        Init();
    }

    private void Init()
    {
        webSocket = new WebSocket(new Uri(uri));
        webSocket.OnOpen += OnOpen;
        webSocket.OnMessage += OnMessageReceived;
        webSocket.OnError += OnError;
        webSocket.OnClosed += OnClosed;
    }

    private void AntiInit()
    {
        webSocket.OnOpen = null;
        webSocket.OnMessage = null;
        webSocket.OnError = null;
        webSocket.OnClosed = null;
        webSocket = null;
    }

    void OnOpen(WebSocket ws)
    {
        Debug.Log("connected");
    }

    public virtual void OnMessageReceived(WebSocket ws, string message)
    {
        Debug.Log(message);
    }

    void OnClosed(WebSocket ws, UInt16 code, string message)
    {
        AntiInit();
        Init();
    }

    private void OnDestroy()
    {
        if (webSocket != null && webSocket.IsOpen)
        {
            webSocket.Close();
            AntiInit();
        }
    }

    void OnError(WebSocket ws, Exception ex)
    {
        string errorMsg = string.Empty;
        if (ws.InternalRequest.Response != null)
            errorMsg = string.Format("Status Code from Server: {0} and Message: {1}", ws.InternalRequest.Response.StatusCode, ws.InternalRequest.Response.Message);
        Debug.Log(errorMsg);
        AntiInit();
        Init();
    }

    public void Close()
    {
        webSocket.Close();
    }

    public void Connect()
    {
        webSocket.Open();
    }

    public void Send(string str)
    {
        webSocket.Send(str);
    }

}

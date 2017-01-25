using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;

public class WebsocketManager : MonoBehaviour {

    WebSocket ws;

	// Use this for initialization
	void Start () {
      ws = new WebSocket("wss://seed.gomix.me");
      // var ws = new WebSocket("ws://localhost:3000");

        ws.OnOpen += OnOpenHandler;
        ws.OnMessage += OnMessageHandler;
      //  ws.OnClose += OnCloseHandler;
      //  ws.OnError += OnErrorHandler;

        ws.ConnectAsync();

	}

    void Awake()
    {
        DontDestroyOnLoad(this);
    }

    void OnDestroy()
    {
        ws.CloseAsync();
    }
	
    private void OnOpenHandler(object sender, System.EventArgs e) {
        Debug.Log("Websocket connected!");
    }

    private void OnMessageHandler(object sender, MessageEventArgs e) {
        Debug.Log("Websocket said: " + e.Data);
    }

	// Update is called once per frame
	void Update () {
		
	}
}

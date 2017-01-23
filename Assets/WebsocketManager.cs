using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;

public class WebsocketManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
      var ws = new WebSocket("ws://seed.gomix.me");
      // var ws = new WebSocket("ws://localhost:8080");

        ws.OnOpen += OnOpenHandler;
        ws.OnMessage += OnMessageHandler;
      //  ws.OnClose += OnCloseHandler;
      //  ws.OnError += OnErrorHandler;

        ws.ConnectAsync();

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

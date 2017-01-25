using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;

public class WebsocketManager : MonoBehaviour {

    [Tooltip("example: seed.gomix.me")]
    public string gomixServerUrl; // =  "ws://localhost:3000"; // for testing locally deployed seed

    WebSocket ws;

	// Use this for initialization
	void Start () {
        ws = new WebSocket("ws://" + gomixServerUrl);

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

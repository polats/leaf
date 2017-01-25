using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;

public class WebsocketManager : MonoBehaviour {

    // object is singleton
    private static WebsocketManager instance;
    public  static WebsocketManager Instance     { get { return instance; } }

    public void Awake()                 
    { 
        DontDestroyOnLoad(this);
        if (instance == null) instance = this; 
    }

    public void OnDestroy()             
    { 
        ws.CloseAsync();
        if (instance == this) instance = null; 
    }

    // Inspector variables
    [Tooltip("example: seed.gomix.me")]
    public string gomixServerUrl; // =  "ws://localhost:3000"; // for testing locally deployed seed
    public GameObject otherHead;

    private WebSocket ws;

	// Use this for initialization
	void Start () {

        ws = new WebSocket("ws://" + gomixServerUrl);

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
        // Debug.Log("Websocket said: " + e.Data);

    }

	// Update is called once per frame
	void Update () {
		
	}
}



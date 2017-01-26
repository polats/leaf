using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;
using SimpleJSON;

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
        Debug.Log("Disconnecting from : " + ws.Url);

        ws.CloseAsync();
        if (instance == this) instance = null; 
    }

    // Inspector variables
    [Tooltip("example: seed.gomix.me")]
    public string gomixServerUrl; 
    public GameObject otherHead;

    bool gameConnected = false;
    bool isHost = true;
    private Quaternion newRotVector;
    private bool pendingRotation = false;

    // change used websocket
    public WebSocket ws = null;



	// Use this for initialization
	void Start () {

        // ws = new WebSocket("ws://" + gomixServerUrl);

       ws = new WebSocket("ws://localhost:3000"); // for testing locally deployed seed

        ws.OnOpen += OnOpenHandler;
        ws.OnMessage += OnMessageHandler;
      //  ws.OnClose += OnCloseHandler;
      //  ws.OnError += OnErrorHandler;

        Debug.Log("Connecting to : " + ws.Url);

        // start to send
        Application.ExternalCall("UnityLoaded");

        ws.ConnectAsync();

	}
        	
    private void OnOpenHandler(object sender, System.EventArgs e) {
        Debug.Log("Websocket connected!");
    }

    public void ProcessMessage(string res) {

        Debug.Log(res);
        if (gameConnected)
        {
            // convert jsonString to list of users as Dictionaries
            JSONNode root = SimpleJSON.JSON.Parse(res);
            List<Dictionary<string, string>> rotationVectors = new List<Dictionary<string, string>>();
            if (!isHost)
                newRotVector = new Quaternion(root[0]["x"].AsFloat,
                    root[0]["y"].AsFloat,
                    root[0]["z"].AsFloat,
                    root[0]["w"].AsFloat);
            else
                newRotVector = new Quaternion(root[1]["x"].AsFloat,
                    root[1]["y"].AsFloat,
                    root[1]["z"].AsFloat,
                    root[1]["w"].AsFloat);                

            pendingRotation = true;
        }
        else
        {
            if (res == "h")
            {
                gameConnected = true;
                isHost = true;
            }
            else if (res == "c")
            {
                gameConnected = true;
                isHost = false;
            }
        }        
    }

    private void OnMessageHandler(object sender, MessageEventArgs e) {
        ProcessMessage(e.Data);
    }

	// Update is called once per frame
	void Update () {
        if (pendingRotation)
            otherHead.transform.rotation = newRotVector;

        pendingRotation = false;
	}
}



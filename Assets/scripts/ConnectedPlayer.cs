using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

using WebSocketSharp;


public class ConnectedPlayer : MonoBehaviour {

    public GameObject PlayerCamera;

    // Use this for initialization
	void Start () {

        PlayerCamera.transform.ObserveEveryValueChanged(x => x.rotation).Subscribe(
            rotationVector =>
            {
                if (WebsocketManager.Instance.ws != null)
                {
                    WebSocket ws = WebsocketManager.Instance.ws;
                    if (ws.IsConnected)
                    ws.SendAsync(JsonUtility.ToJson(rotationVector),
                            OnSendComplete);
                }

                Application.ExternalCall ("UpdateRotation", JsonUtility.ToJson (rotationVector));
            });
	}
	
    void OnSendComplete(bool result)
    {
    }

	// Update is called once per frame
	void Update () {
		
	}
}

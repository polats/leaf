using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class ConnectedPlayer : MonoBehaviour {

    public GameObject PlayerCamera;

    // Use this for initialization
	void Start () {

        PlayerCamera.transform.ObserveEveryValueChanged(x => x.rotation).Subscribe(
            rotationVector =>
            {
                Debug.Log(JsonUtility.ToJson(rotationVector));
            });
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

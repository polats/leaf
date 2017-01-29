using UnityEngine;
using System.Collections;
using System;

// Sample Data Classes that could be stringified by JSONUtility
public class User
{
    public string uid;
    public string displayname;

    public User(string u,string d)
    {
        uid = u;
        displayname = d;
    }
}

public class MatchJoinResponse
{
    public bool result;
}

public class NetworkManager : MonoBehaviour
{
    void Start()
    {
        ConnectToServer(new User("polats", "polatski"));
    }

    public void ConnectToServer(User user)
    { 
        Debug.Log("Connecting...");

        Application.ExternalCall ("Logon", JsonUtility.ToJson (user));
    }

    public void OnMatchJoined(string jsonresponse)
    {
        MatchJoinResponse mjr = JsonUtility.FromJson<MatchJoinResponse> (jsonresponse);
        if(mjr.result)
        {
            Debug.Log("Logon successful");
        }
        else
        {
            Debug.Log("Logon failed");
        }
    }

    public void BroadcastQuit()
    {
        Debug.Log("Quitting!");

        Application.ExternalCall ("QuitMatch");
    }
}
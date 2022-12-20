using System;
using System.Collections.Generic;
using SocketIOClient;
using SocketIOClient.Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json.Linq;

//using Debug = System.Diagnostics.Debug;


public class SocketManager : MonoBehaviour
{
    public SocketIOUnity socket;

    public InputField EventNameTxt;
    public InputField DataTxt;
    public Text ReceivedText;  

    public GameObject objectToSpin;

    // Start is called before the first frame update
    void Start()
    {
        //TODO: check the Uri if Valid.
        var uri = new Uri("http://localhost:11100");
        socket = new SocketIOUnity(uri, new SocketIOOptions
        {
            Query = new Dictionary<string, string>
                {
                    {"token", "UNITY" }
                }
            ,
            EIO = 4
            ,
            Transport = SocketIOClient.Transport.TransportProtocol.WebSocket
        });
        socket.JsonSerializer = new NewtonsoftJsonSerializer();

        ///// reserved socketio events
        socket.OnConnected += (sender, e) =>
        {
            Debug.Log("socket.OnConnected");
        };
        socket.OnPing += (sender, e) =>
        {
            Debug.Log("Ping");
        };
        socket.OnPong += (sender, e) =>
        {
            Debug.Log("Pong: " + e.TotalMilliseconds);
        };
        socket.OnDisconnected += (sender, e) =>
        {
            Debug.Log("disconnect: " + e);
        };
        socket.OnReconnectAttempt += (sender, e) =>
        {
            Debug.Log($"{DateTime.Now} Reconnecting: attempt = {e}");
        };
        ////

        Debug.Log("Connecting...");
        socket.Connect();

        socket.OnUnityThread("spin", (data) =>
        {
            Debug.Log("spin");
            rotateAngle = 0;
        });
        
        socket.OnUnityThread("broadcast", (data) =>
        {
            Debug.Log(data);
            //rotateAngle = 0;
        });
        socket.OnUnityThread("spin", (data) =>
        {
            Debug.Log(data);
            rotateAngle = 0;
        });

        //ReceivedText.text = "";
        socket.OnAnyInUnityThread((name, response) =>
        {
            Debug.Log("OnAnyInUnityThread "+name);
            if(name=="spin"){
                rotateAngle = 0;
            }
            //ReceivedText.text += "Received On " + name + " : " + response.GetValue().GetRawText() + "\n";
        });
    }

    public void EmitTest()
    {
        string eventName = EventNameTxt.text.Trim().Length < 1 ? "hello" : EventNameTxt.text;
        string txt = DataTxt.text;
        if (!IsJSON(txt))
        {
            socket.Emit(eventName, txt);
        }
        else
        {
            socket.EmitStringAsJSON(eventName, txt);
        }
    }

    public static bool IsJSON(string str)
    {
        if (string.IsNullOrWhiteSpace(str)) { return false; }
        str = str.Trim();
        if ((str.StartsWith("{") && str.EndsWith("}")) || //For object
            (str.StartsWith("[") && str.EndsWith("]"))) //For array
        {
            try
            {
                var obj = JToken.Parse(str);
                return true;
            }catch (Exception ex) //some other exception
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    public void EmitSpin()
    {
        socket.Emit("spin");
    }


 


    float rotateAngle = 45;
    readonly float MaxRotateAngle = 45;
    void Update()
    {
        if(rotateAngle < MaxRotateAngle)
        {
            rotateAngle++;
            objectToSpin.transform.Rotate(0, 1, 0);
        }

        socket.OnUnityThread("spin", (res) =>
        {
         // Debug.Log("csi")
        });
    }
}
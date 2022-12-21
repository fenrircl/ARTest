using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] SocketManager socketManager=null;
    // Start is called before the first frame update
    void Start()
    {
        socketManager = gameObject.GetComponent<SocketManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public  void EnviarRespuesta(object obj)
    {
        //var sm = new SocketManager();
        //sm.EmitEvaluados(obj);
        //var sm = this.GetComponent<SocketManager>();
        //Debug.Log(obj);
        //socketManager.EmitEvaluados(obj);
        //Debug.Log(socketManager);
        //if(socketManager)
        //socketManager.EmitSpin();
    }
}

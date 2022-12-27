using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] SocketManager socketManager=null;

    [SerializeField] TextMeshProUGUI labelEvaluados;
    [SerializeField] private int totalEvaluados=0;
    [SerializeField] private int totalAEvaluar=0;
    [SerializeField] private string respuestaCorrecta="";

    // Start is called before the first frame update
    void Start()
    {
        socketManager = gameObject.GetComponent<SocketManager>();
    }

    // Update is called once per frame
    void Update()
    {
        labelEvaluados.text = totalEvaluados+"/"+totalAEvaluar;
    }
    
    public void setTotalEvaluados()
    {
        totalEvaluados++;
    }

    public void resetEvaluacion()
    {
        totalEvaluados=0;
    }
    
    public void setRespuestaCorrecta(string resp)
    {
        respuestaCorrecta=resp;
    }
    public string getRespuestaCorrecta()
    {
        return "respuestaCorrecta";
    }
    public void getTotalEvaluar(int ev)
    {
        
        totalAEvaluar = ev;
    }
    public  void EnviarRespuesta(string obj)
    {
        //var sm = new SocketManager();
        //sm.EmitEvaluados(obj);
        var sm = this.GetComponent<SocketManager>();
        //totalEvaluados++;
        Debug.Log(obj);
        Debug.Log(totalEvaluados);
        //socketManager.EmitEvaluados(obj);
        //Debug.Log(socketManager);
        if(socketManager)
        socketManager.EmitEvaluados(obj);
    }
}

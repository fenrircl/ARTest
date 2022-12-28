using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class ImageRotateDetection : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI m_Object;
    // [SerializeField] GameObject target= null;
     [SerializeField] string id=null;
     [SerializeField] string respuesta;
    private bool contoRespuesta=false;
    //GameManager GM;
    [SerializeField] GameManager GM=null;
    public AudioSource audioSource;
    public AudioClip clip;

    //[SerializeField] private gameObject target=null;

    // Start is called before the first frame update
    void Start()
    {
        //GM = new GameManager();
        //GM = gameObject.GetComponent<GameManager>();

        //GM.EnviarRespuesta("coso");
       // target.GetComponent<Image>().sprite = soundOn;


    }

    // Update is called once per frame
    void Update()
    {
    
        //   var angle2 = Vector3.Angle(target.transform.up, Vector3.up);
            var angle = gameObject.transform.rotation.eulerAngles;
            //m_Object.text = angle.ToString();//angle2.ToString();
            var temp = "";
            var resp = "";
            if(angle.y >= 110 && angle.y <= 255)
            {
                temp = "arriba";
                resp = "A";
            }
            else if(angle.y > 105 && angle.y < 120)
            {
                temp = "derecha";
                resp = "B"; 
            }
                else if(angle.y > 280 && angle.y < 360 || angle.y > 0 && angle.y < 120)
            {
                temp = "abajo";
                resp = "C";

            }
                    else if(angle.y > 270 && angle.y < 280)
            {
                temp = "izquierda";
                resp = "D";

            }
            if(respuesta!=resp){
                respuesta = resp;
                print(resp+" /"+respuesta);
                var respuesta_correcta = GM.getRespuestaCorrecta();
                //Debug.Log(temp+ " "+id);
                if(!contoRespuesta) {
                    GM.setTotalEvaluados();
                    contoRespuesta = true;
                    if(respuesta_correcta==resp){
                        print("acierta");
                    }
                    else{
                        print("no acierta");
                    }
                }
                GM.EnviarRespuesta(id+ ","+resp);
                audioSource.PlayOneShot(clip);
            }
            m_Object.text="Estudiante "+id+" evaluado";
            //     else if(angle.y > 320 && angle.y < 30)
            // {
            //     m_Object.text = "derecha";

            // }
        
    }

    public void showRotation()
    {
        // //Debug.Log(gameObject.transform.rotation.eulerAngles);
        // var angle = gameObject.transform.rotation.eulerAngles;
        // var angle2 = Vector3.Angle(transform.up, Vector3.up);

        // m_Object.text = angle2.ToString();

    }
    public void sendEvaluacion()
    {
        //GM.EnviarRespuesta();
    }
}

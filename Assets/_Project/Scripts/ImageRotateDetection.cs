using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ImageRotateDetection : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI m_Object;
     [SerializeField] GameObject target= null;
     [SerializeField] string id=null;
     [SerializeField] bool respuesta=false;

    //GameManager GM;
    [SerializeField] GameManager GM=null;


    // Start is called before the first frame update
    void Start()
    {
        //GM = new GameManager();
        //GM = gameObject.GetComponent<GameManager>();

        GM.EnviarRespuesta("coso");
      
    }

    // Update is called once per frame
    void Update()
    {
        if(!respuesta)
        {
        //   var angle2 = Vector3.Angle(target.transform.up, Vector3.up);
            var angle = gameObject.transform.rotation.eulerAngles;
            //m_Object.text = angle.ToString();//angle2.ToString();
            var temp = "";
            if(angle.y >= 100 && angle.y <= 255)
            {
                temp = "arriba";
            }
            else if(angle.y > 80 && angle.y < 110)
            {
                temp = "derecha";

            }
                else if(angle.y > 280 && angle.y < 360 || angle.y > 0 && angle.y < 100)
            {
                temp = "abajo";

            }
                    else if(angle.y > 255 && angle.y < 280)
            {
                temp = "izquierda";

            }
            if(temp!=""){
            respuesta = true;
            Debug.Log(temp+ " "+id);
            GM.EnviarRespuesta(id);
            }
            m_Object.text=temp+" "+angle.y;
            //     else if(angle.y > 320 && angle.y < 30)
            // {
            //     m_Object.text = "derecha";

            // }
        }
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

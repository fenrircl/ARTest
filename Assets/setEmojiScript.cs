using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class setEmojiScript : MonoBehaviour
{
            [SerializeField] public  Image myIMGcomponent;

    public Sprite[] images;
     public Image m_Image;
     public Sprite m_Sprite;
    // Start is called before the first frame update
    void Start()
    {
        var random = Random.Range(0, 10); 
       // myIMGcomponent.sprite = images[0];
        m_Image = GetComponent<Image>();
         m_Image.sprite = images[random];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
public class testRotation : MonoBehaviour
{
    // Start is called before the first frame update
    private ImageTargetBehaviour itb_stones;
    public GameObject playerObj;

    [SerializeField] private string estudianteTag="";
    void Start()
    {
        //rb = GetComponent<Rigidbody>();
		//itb_stones = coso;
        playerObj = GameObject.FindGameObjectWithTag(estudianteTag);
        itb_stones = (ImageTargetBehaviour)playerObj.GetComponent(typeof(ImageTargetBehaviour));

    }

    // Update is called once per frame
    void Update()
    {
        //rb.constraints = ~RigidbodyConstraints.FreezeRotationX;
        if (itb_stones != null) {
            print("entra");
			Vector3 v3 = new Vector3(itb_stones.transform.position.x, itb_stones.transform.position.y, itb_stones.transform.position.z);
			transform.position = v3;
		}
        
        
    }
}

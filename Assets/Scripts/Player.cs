using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public GameObject sara0, sara1, sara2, sara3;

    public AudioClip seChange;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        //移動 x:0 -90 90
        if( Input.GetButtonDown("Right") && transform.position.x < 80 )
        {
            transform.position = new Vector3(transform.position.x+90,-290,0);
        } else if ( Input.GetButtonDown("Left") && transform.position.x > -80 )
        {
            transform.position = new Vector3(transform.position.x-90, -290, 0);
        }

        //皿回転 スペースボタン
        if (Input.GetButtonDown("Jump"))
        {
            //sound
            SoundManager.instance.SeSound(seChange, 0.3f);
            GameObject tmp;
            if (transform.position.x < -80)
            {
                tmp = sara0;
                sara1.transform.position = tmp.transform.position;
                sara0.transform.position = new Vector3(sara1.transform.position.x + 90, -250, 0);
                sara0 = sara1;
                sara1 = tmp;
            }
            else if (transform.position.x > 80)
            {
                tmp = sara2;
                sara3.transform.position = tmp.transform.position;
                sara2.transform.position = new Vector3(sara3.transform.position.x + 90, -250, 0);
                sara2 = sara3;
                sara3 = tmp;
            }
            else
            {
                tmp = sara1;
                sara2.transform.position = tmp.transform.position;
                sara1.transform.position = new Vector3(sara2.transform.position.x + 90, -250, 0);
                sara1 = sara2;
                sara2 = tmp;
            }
        }
        
	}

}

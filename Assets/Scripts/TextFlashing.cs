using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextFlashing : MonoBehaviour {

    private CanvasRenderer rend; 
    private float nextTime;
    public float interval = 0.8f; //点滅周期

    // Use this for initialization
    void Start () {
        rend = GetComponent<CanvasRenderer>();
        nextTime = Time.time;
    }
	
	// Update is called once per frame
	void Update () {
        //一定時間ごとに点滅
        if (Time.time > nextTime)
        {
            float alpha = rend.GetAlpha();
            if (alpha == 1.0f)
                rend.SetAlpha(0.0f);
            else
                rend.SetAlpha(1.0f);

            nextTime += interval;
        }
    }
}

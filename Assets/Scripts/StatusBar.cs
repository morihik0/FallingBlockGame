using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StatusBar : MonoBehaviour {

    public GameObject nabeCountObj , levelObj , gameOverobj , readyObj;
    private Text nabeCountText , levelText;

    private int nabeCount;

	// Use this for initialization
	void Start () {
        levelText = levelObj.GetComponent<Text>();
        nabeCountText = nabeCountObj.GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        nabeCountText.text = "x"+nabeCount+"個";
        levelText.text = "LEVEL " + GameManager.instance.LevelNow();

        //スタートしたらGETREADYは消す
        if (GameManager.instance.IsGameStartNow()) Destroy(readyObj.gameObject);

        //ゲームオーバー時の絵を出すよ
        if (GameManager.instance.IsGameOverNow() && gameOverobj.transform.position.y > 15)
        {
            gameOverobj.transform.position = new Vector3(0, gameOverobj.transform.position.y - 5, 0);
        }
	}

    public void NabeCountUp()
    {
        nabeCount++;
        //5個消すごとにレベルアップ
        if(nabeCount%5 ==0)GameManager.instance.LevelUp();
    }
}

using UnityEngine;
using System.Collections;

public class ItemManager : MonoBehaviour {

    //X軸:-135 -45 45 135 y:235

    public GameObject[] item;
    public GameObject special;

    private GameObject waitItem, fallingItem, compItem;
    private bool firstItem;

	// Use this for initialization
	void Start () {
        //最初の1個
        waitItem = (GameObject)Instantiate(item[1], new Vector3(-135 + Random.Range(0, 4) * 90, 235, 0), Quaternion.identity);
        waitItem.GetComponent<Rigidbody2D>().gravityScale = 10 + GameManager.instance.LevelNow() * 5;
    }

    // Update is called once per frame
    void Update() {
        //ゲームスタートするタイミング 最初のアイテムを落とす処理(1回だけ
        if (GameManager.instance.IsGameStartNow() && !firstItem)
        {
            firstItem = true;
            waitItem.layer = LayerMask.NameToLayer("FallingItem");
        }

        if (GameManager.instance.IsGameStartNow()) {
            //ゲームオーバーかどうか見る
            if (!GameManager.instance.IsGameOverNow())
            {
                //待機アイテムが落下始めてる場合はネクスト用意　落ち始めてる奴はfallに
                if (waitItem.layer == LayerMask.NameToLayer("FallingItem"))
                {
                    fallingItem = waitItem;
                    //アイテムを生成する　スペシャルアイテム（破壊アイテム）と通常がある
                    if (Random.Range(0, 100) > 10) waitItem = (GameObject)Instantiate(item[Random.Range(0, 3)], new Vector3(-135 + Random.Range(0, 4) * 90, 235, 0), Quaternion.identity);
                    else {
                        waitItem = (GameObject)Instantiate(special, new Vector3(-135 + Random.Range(0, 4) * 90, 235, 0), Quaternion.identity);
                        waitItem.transform.name = "Special";
                    }
                    //落下速度を決めましょう
                    waitItem.GetComponent<Rigidbody2D>().gravityScale = 10 + GameManager.instance.LevelNow() * 10;
                }

                //接地時タグが変わるのでそれを取る
                if (fallingItem.transform.tag == "Item")
                {
                    //落下を始めたらオブジェクトを開放
                    waitItem.layer = LayerMask.NameToLayer("FallingItem");
                    waitItem.GetComponent<Renderer>().sortingOrder = 1;
                    //スペシャルアイテムは後から追加した仕様なので何かおかしいがココで名前でとって削除すえう
                    if (fallingItem.transform.name == "Special")
                    {
                        Destroy(fallingItem.gameObject);
                    }
                }
            }
        }
    }
}

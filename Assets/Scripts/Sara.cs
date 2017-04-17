using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class Sara : MonoBehaviour {

    List<GameObject> allChildren = new List<GameObject>();
    public GameObject nabe , statusBar;
    private StatusBar statusBarScript;

    public AudioClip seDestroy;

    // Use this for initialization
    void Start () {
        statusBarScript = statusBar.GetComponent<StatusBar>();
    }
	
	// Update is called once per frame
	void Update () {
        //子要素を全て取得　（追加していく書式なので必ず毎回初期化する）
        allChildren.Clear();
        GetChildren(this.gameObject, ref allChildren);
        //高さ順に並び替え
        var itemList =  allChildren.OrderBy(node => node.transform.position.y);
        //配列に変換
        GameObject[] itemArray = itemList.ToArray();
        //7段目に達していてもそれが消える場合は除外する為のbool
        bool isGameOver = true;
        for( int i=0; i < itemArray.Length; i++)
        {
            //前後に要素が存在する範囲をとる
            if( i > 0 && i < itemArray.Length-1)
            {
                //3個の要素の名前が全て違う場合
                if(itemArray[i].transform.name != itemArray[i-1].transform.name && itemArray[i-1].transform.name != itemArray[i+1].transform.name && itemArray[i].transform.name != itemArray[i+1].transform.name)
                {
                    //名前がspecialのものが紛れてたらダメ
                    if (itemArray[i].transform.name != "Special" && itemArray[i-1].transform.name != "Special" && itemArray[i+1].transform.name != "Special")
                    {
                        StartCoroutine(OdenDestroy(itemArray[i - 1].gameObject, itemArray[i].gameObject, itemArray[i + 1].gameObject));
                        isGameOver = false;
                    }
                }
            }
        }

        //7段目に達したらゲームオーバー
        if (itemArray.Length > 6 && isGameOver) GameManager.instance.GameOver();
    }

    private IEnumerator OdenDestroy(GameObject a , GameObject b , GameObject c)
    {
        //sound
        SoundManager.instance.SeSound(seDestroy, 0.3f);

        statusBarScript.NabeCountUp();
        GameObject tmp =  (GameObject)Instantiate(nabe, b.transform.position,Quaternion.identity);
        Destroy(a.gameObject);
        Destroy(b.gameObject);
        Destroy(c.gameObject);
        yield return new WaitForSeconds(0.2f);
        Destroy(tmp.gameObject);
    }

    //全ての子要素を取得する
    private void GetChildren(GameObject obj, ref List<GameObject> allChildren)
    {
        Transform children = obj.GetComponentInChildren<Transform>();
        //子要素がいなければ終了
        if (children.childCount == 0)
        {
            return;
        }
        foreach (Transform ob in children)
        {
            allChildren.Add(ob.gameObject);
            GetChildren(ob.gameObject, ref allChildren);
        }
    }

}

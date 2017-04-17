using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {

    public AudioClip seSet , seSpecial;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter2D(Collision2D col)
    {
        //皿に触れた場合
        if(col.transform.tag == "Sara")
        {
            //sound
            SoundManager.instance.SeSound(seSet,0.3f);

            //位置を綺麗に補正して物理挙動を止める
            this.transform.position = new Vector3(this.transform.position.x, -207, 0);
            this.GetComponent<Rigidbody2D>().isKinematic = true;
            
            //接触した皿の子に登録　タグとレイヤーの切り替え
            this.transform.parent = col.transform;
            this.gameObject.layer = LayerMask.NameToLayer("Item");
            this.transform.tag = "Item";
        }

        //itemに触れた場合
        if(col.transform.tag == "Item" && this.transform.tag == "FallingItem")
        {
            //sound
            SoundManager.instance.SeSound(seSet, 0.3f);

            //位置を綺麗に補正して物理挙動を止める
            this.transform.position = new Vector3(this.transform.position.x, -207 + 66* col.transform.parent.transform.childCount, 0);
            this.GetComponent<Rigidbody2D>().isKinematic = true;

            //接触したアイテムと同一の親にして　タグとレイヤーの切り替え
            this.transform.tag = "Item";
            this.gameObject.layer = LayerMask.NameToLayer("Item");
            this.transform.parent = col.transform.parent;
        }

        //specialアイテムが他のアイテムに触れた場合
        if (col.transform.tag == "Item" && this.transform.tag == "Special")
        {
            ///位置を綺麗に補正して親を登録
            if(this.transform.parent != col.transform.parent) this.transform.position = new Vector3(this.transform.position.x, -207 + 66 * col.transform.parent.transform.childCount, 0);
            this.transform.parent = col.transform.parent;
            //sound
            SoundManager.instance.SeSound(seSpecial, 0.3f);
            Destroy(col.gameObject);
        }
    }
}

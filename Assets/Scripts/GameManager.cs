using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;

    private bool isGameOver , isGameStart , isGameOverSe;
    private int level = 1;

    public AudioClip seGameOver;
    
    void Awake()
    {
        //シングルトン処理
        if (instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);
        //DontDestroyOnLoad(gameObject);
    }

    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        //ゲームスタート
        if (Input.GetKeyDown("z") && !isGameStart)
        {
            //SoundManager.instance.PlaySingle(gameBGM);
            isGameStart = true;
        }

        //こんてにゅー
        if (Input.GetKeyDown("z") && isGameOver) SceneManager.LoadScene("Main");

        if (isGameOver && !isGameOverSe)
        {
            SoundManager.instance.StopBgm();
            SoundManager.instance.SeSound(seGameOver, 0.3f);
            isGameOverSe = true;
        }

    }

    public void GameOver()
    {
        isGameOver = true;
    }

    public bool IsGameOverNow()
    {
        return isGameOver;
    }

    public void StartGame()
    {
        isGameStart = true;
    }

    public bool IsGameStartNow()
    {
        return isGameStart;
    }

    public void LevelUp()
    {
        level++;
    }

    public int LevelNow()
    {
        return level;
    }

}

using UnityEngine;
using System.Collections;

/*===============================================================*/
/**
* EndSceneの管理
* 2014年11月26日 Buravo
*/ 
public class EndSceneController : MonoBehaviour 
{
    #region メンバ変数
    /*===============================================================*/
    /**
    * @brief シーンの遷移を制御するコンポーネント
    */
    private Transition m_trans;
    /**
    * @brief GUIの表示
    */
    private bool m_is_gui = false;
    /**
    * @brief GUISkin
    */
    private GUISkin m_skin;
    /**
    * @brief 時計
    */
    private Clock m_clock;
    /*===============================================================*/
    #endregion 
    
    /*===============================================================*/
    /**
    * @brief 開始時に一度呼ばれるメソッド.
    */
    void Awake () 
    {
        m_skin = Resources.Load("GUISkin/button_skin") as GUISkin;
        // BGMの設定.
        AudioManager.Instance.AddBGM("end_bgm", "BGM/bgm_maoudamashii_acoustic08", true);
        AudioManager.Instance.PlayBGM("end_bgm");
        // カーソルの変更.
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        // 遷移状態を管理するコンポーネントの取得.
        m_trans = GameObject.Find("Transition").GetComponent<Transition>();
        // フェードイン開始.
        m_trans.PlayFade( new Color(0.0f, 0.0f, 0.0f, 1.0f), new Color(0.0f, 0.0f, 0.0f, 0.0f), 0.0f, 90.0f);
        // Clockの設定.
        m_clock = new Clock();
        m_clock.TimeLimit = 3.0f;
    }
    /*===============================================================*/

    /*===============================================================*/
    /**
    * @brief 開始時に一度呼ばれるメソッド.
    */
    void Start () 
    {
        if (ScoreManager.Instance.Score > PlayerPrefs.GetInt ("HighScore", 0))
        {
            // ハイスコアのセーブ.
            PlayerPrefs.SetInt("HighScore", ScoreManager.Instance.HighScore);
        }
        // スコアの表示.
        GameObject.Find("ScoreText").guiText.text = "Score:"+ScoreManager.Instance.Score;
        // ハイスコアの表示.
        GameObject.Find("HighScoreText").guiText.text = "HighScore:"+PlayerPrefs.GetInt ("HighScore", 0);
    }
    /*===============================================================*/
    
    /*===============================================================*/
    /**
    * @brief 更新処理
    */
    void Update () 
    {
        if (m_clock != null)
        {
            m_clock.Execution();
            if (m_clock.CheckTimeLimit())
            {
                m_is_gui = true;
            }
        }
    }
    /*===============================================================*/

    /*===============================================================*/
    /**
    * @brief GUI表示処理
    */
    void OnGUI () 
    {
        if (m_is_gui)
        {
            GUI.skin = m_skin;
            // ボタンの位置と大きさ.
            Rect rect = new Rect(Screen.width/2 - 200/2, Screen.height - 100 - 50 , 200, 100);
            // ボタンが押されたかどうかの判定.
            bool isClicked = GUI.Button(rect, "Back to Title");
            // ボタンが押されたら処理.
            if (isClicked)
            {
                // スコアの初期化.
                ScoreManager.Instance.Reset();
                // BGM停止.
                AudioManager.Instance.StopBGM("end_bgm");
                // シーンの遷移.
                m_trans.PlayFadeAfterLoadScene( new Color(0.0f, 0.0f, 0.0f, 0.0f), new Color(0.0f, 0.0f, 0.0f, 1.0f), 0.0f, 90.0f, "TitleScene" );
                m_is_gui = false;
                m_clock = null;
            }
        }
    }
    /*===============================================================*/
}
/*===============================================================*/

using UnityEngine;
using System.Collections;

/*===============================================================*/
/**
* ゲームの管理
* 2014年11月26日 Buravo
*/ 
public class TitleSceneController : MonoBehaviour 
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
    * @brief 初期化
    */
    void Awake () 
    {
        m_skin = Resources.Load("GUISkin/button_skin") as GUISkin;
        // BGMの設定.
        AudioManager.Instance.AddBGM("title_bgm", "BGM/bgm_maoudamashii_acoustic10", true);
        AudioManager.Instance.PlayBGM("title_bgm");
        // カーソルの変更.
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        m_trans = GameObject.Find("Transition").GetComponent<Transition>();
        m_trans.PlayFade( new Color(0.0f, 0.0f, 0.0f, 1.0f), new Color(0.0f, 0.0f, 0.0f, 0.0f), 0.0f, 90.0f);
    }
    /*===============================================================*/

    /*===============================================================*/
    /**
    * @brief 開始時に一度呼ばれるメソッド.
    */
    void Start () 
    {
        // タイトルロゴの作成.
        LogoMaker logo = new LogoMaker();
        logo.SetLogoText("Text/LogoText");
        logo.SetParent("Main Camera");
        Vector3 fromPos = new Vector3(-21.0f, -10.0f, 30.0f);
        Vector3 toPos = new Vector3(-21.0f, 10.0f, 30.0f);
        logo.SetLogoPosition(fromPos, toPos);
        logo.CreateLogoObjects("OtherAssets/Party Pack/Prefabs/LogoBalloon");
        logo.StartAnimation();

        // Clockの設定.
        m_clock = new Clock();
        m_clock.TimeLimit = 3.0f;
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
            Rect rect = new Rect(Screen.width/2 - 200/2, Screen.height - 100 - 50 , 200, 100);
            bool isClicked = GUI.Button(rect, "START");
            if (isClicked)
            {
                AudioManager.Instance.StopBGM("title_bgm");
                m_trans.PlayFadeAfterLoadScene( new Color(0.0f, 0.0f, 0.0f, 0.0f), new Color(0.0f, 0.0f, 0.0f, 1.0f), 0.0f, 90.0f, "PlayScene" );
                m_is_gui = false;
                m_clock = null;
            }
        }
    }
    /*===============================================================*/
}
/*===============================================================*/

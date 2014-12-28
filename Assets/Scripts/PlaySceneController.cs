using UnityEngine;
using System.Collections;
using System;

/*===============================================================*/
/**
* ゲームの管理
* 2014年11月21日 Buravo
*/ 
public class PlaySceneController : MonoBehaviour 
{
    #region 列挙型変数
    /*===============================================================*/
    /**
    * @brief Tag名
    */
    private enum Tags 
    {
        Balloon, //!<< タグ名が"Balloon"の場合の値
        None     //!<< 一致したタグ名がない場合の値
    }
    /*===============================================================*/
    #endregion

    #region メンバ変数
    /*===============================================================*/
    /**
    * @brief ゲームクリアしたかどうかの判定
    */
    private bool m_is_finished = false;
    /**
    * @brief 生成するバルーンの個数
    */
    private int m_create_num = 25;
    /**
    * @brief 生成するバルーンのファイルパス
    */
    private string m_balloon_path = "OtherAssets/Party Pack/Prefabs/RandomColorBalloon";
    /**
    * @brief 生成するパーティクルのファイルパス
    */
    private string m_particle_path = "OtherAssets/Small particle pack/Confetti";
    /**
    * @brief シーンの遷移を制御するコンポーネント
    */
    private Transition m_trans;
    /**
    * @brief 時計
    */
    private Clock m_clock;
    /**
    * @brief 3DText
    */
    private Text m_text;
    /*===============================================================*/
    #endregion

    /*===============================================================*/
    /**
    * @brief 開始時に一度呼ばれるメソッド.
    */
    void Awake () 
    {
        // Clockの設定.
        Vector3 clockPos = new Vector3(0.0f, 0.95f, 0.0f);
        m_clock = new Clock();
        m_clock.CreateGameObject("Time : ", "OtherAssets/1960s/Heavy heap/heavy heap", 40, Color.black, clockPos);
        m_clock.TimeLimit = 30;
        m_clock.IsCountDown = true;
        // Textの設定.
        m_text = new Text();
        // BGM, SEの設定.
        AudioManager.Instance.AddBGM("play_bgm", "BGM/bgm_maoudamashii_acoustic07", true);
        AudioManager.Instance.PlayBGM("play_bgm");
        AudioManager.Instance.AddSE("balloon_break", "SE/se_maoudamashii_battle16", false);
        // カーソルに使用するテクスチャの設定.
        Texture2D cursorTexture = Resources.Load("Textures/marker") as Texture2D;
        // ターゲット地点として使用するテクスチャの左上からのオフセット距離.
        Vector2 hotSpot = new Vector2(cursorTexture.width/2, cursorTexture.height/2);
        // カーソルの変更.
        Cursor.SetCursor(cursorTexture, hotSpot, CursorMode.Auto);
        // 暗転処理の設定.
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
        // 指定した個数分生成.
        for (int i = 0; i < m_create_num; i++)
        {
            // オブジェクト生成.
            GameObject instance = Instantiate(Resources.Load(m_balloon_path)) as GameObject;
            instance.name = "Balloon"+i;
            instance.tag = "Balloon";
            // オブジェクトプールに追加.
            ObjectPoolManager.Instance.AddPoolObject("Balloon", instance);
        }
        // InvokeRepeating("関数名",初回呼出までの遅延秒数,次回呼出までの遅延秒数).
        InvokeRepeating("CreateBalloon", 0.5f, 0.5f); 
    }
    /*===============================================================*/
    
    /*===============================================================*/
    /**
    * @brief 更新処理
    */
    void Update () 
    {
        if (!m_is_finished)
        {
            this.InputRaycast();
            this.CheckFinish();
            m_clock.Execution();
        }
    }
    /*===============================================================*/

    /*===============================================================*/
    /**
    * @brief バルーンのアクティブ化
    */
    void CreateBalloon ()
    {
        ObjectActiveManager.Instance.PerformActivity("Balloon");
    }
    /*===============================================================*/

    /*===============================================================*/
    /**
    * @brief 終了条件の判定
    */
    void CheckFinish () 
    {
        // 制限時間を超えたら処理.
        if (m_clock.CheckTimeLimit())
        {
            // InvokeRepeatingをキャンセル.
            CancelInvoke();
            // 時間を止める.
            m_clock.IsTimeOver = true;
            m_is_finished = true;
            // 指定した個数分探す.
            for (int i = 0; i < m_create_num; i++)
            {
                // アクティブなオブジェクトを探す.
                GameObject instance = GameObject.Find("Balloon"+i);
                // アクティブなオブジェクトがあれば処理.
                if (instance != null)
                {
                    m_is_finished = false;
                }
            }
            // クリアしたら遷移.
            if (m_is_finished)
            {
                // BGM停止.
                AudioManager.Instance.StopBGM("play_bgm");
                // フェード処理後に遷移.
                m_trans.PlayFadeAfterLoadScene( new Color(0.0f, 0.0f, 0.0f, 0.0f), new Color(0.0f, 0.0f, 0.0f, 1.0f), 0.0f, 90.0f, "EndScene" );
                // オブジェクトプールの解放.
                ObjectPoolManager.Instance.Release();
            }
        }
    }
    /*===============================================================*/

    /*===============================================================*/
    /**
    * @brief Raycastの入力
    */
    void InputRaycast () 
    {
        if (Input.GetMouseButtonDown(0))
        {
            // マウスのクリックしたスクリーン座標をrayに変換.
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            // Rayに当たったオブジェクトの情報を格納するためのクラスを作成.
            RaycastHit hit = new RaycastHit();
            // オブジェクトにあたった場合の挙動を処理.
            if (Physics.Raycast(ray, out hit))
            {
                // Raycastの当たり判定.
                CheckRaycast(hit);
            }
        }
    }
    /*===============================================================*/

    /*===============================================================*/
    /**
    * @brief Raycastの当たり判定
    * @param RaycastHit 衝突したオブジェクトの情報
    */
    void CheckRaycast (RaycastHit t_hit) 
    {
        // 取得した文字列とenum名が一致すればenum値を取得.
        Tags hitObjectTag = this.GetTag(t_hit.collider.gameObject.tag);
        // enum値で判定.
        switch (hitObjectTag)
        {
            // タグがBalloonの場合.
            case Tags.Balloon:
                // スコアの加算.
                ScoreManager.Instance.AddScore(100);
                // エフェクト生成,
                GameObject particle = Instantiate(Resources.Load(m_particle_path), t_hit.collider.gameObject.transform.position, Quaternion.identity) as GameObject;
                // Scoreの生成.
                m_text.CreateGameObject("Prefabs/3DText", "Score", t_hit.collider.gameObject.transform.position, "100Up");
                // Balloon破裂音.
                AudioManager.Instance.PlayOneShotSE("balloon_break");
                // Balloonの消去.
                t_hit.collider.gameObject.SetActive(false);
                // エフェクトを1秒後に消去.
                Destroy(particle, 1.0f);
                break;
            default:
                Debug.Log("not find : EnumName .");
                break;
        }
    }
    /*===============================================================*/

    /*===============================================================*/
    /**
    * @brief タグの取得
    * @param string タグ名
    * @return Tags タグ名と一致したEnumの値
    */
    Tags GetTag (string label) 
    {
        switch (label) 
        {
            case "Balloon": 
            return Tags.Balloon;

            default: 
            return Tags.None;
        }
    }
    /*===============================================================*/
}
/*===============================================================*/

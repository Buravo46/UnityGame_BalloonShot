using UnityEngine;
using System.Collections;

/*===============================================================*/
/**
* 時間クラス
* 2014年12月16日 Buravo
*/ 
public class Clock
{
    #region メンバ変数
    /*===============================================================*/
    /**
    * @brief 時計のゲームオブジェクト
    */
    private UnityEngine.GameObject m_clock;
    /**
    * @brief GUITextコンポーネント
    */
    private GUIText m_gui_text;
    /**
    * @brief 標準で表示するテキスト
    */
    private string m_text;
    /**
    * @brief 時間
    */
    private float m_time;
    /**
    * @brief 制限時間
    */
    private float m_time_limit;
    /**
    * @brief 制限時間の判定
    */
    private bool m_is_time_over;
    /**
    * @brief カウントアップ
    */
    private bool m_count_up;
    /**
    * @brief カウントダウン
    */
    private bool m_count_down;
    /*===============================================================*/
    #endregion

    #region アクセサ
    /*===============================================================*/
    /**
    * @brief タイマーオブジェクト
    */
    public GameObject ClockObject
    {
        get
        {
            return m_clock;
        }
    }
    /**
    * @brief 時間
    */
    public float Time
    {
        get
        {
            return m_time;
        }
    }
    /**
    * @brief 制限時間
    */
    public float TimeLimit
    {
        set
        {
            m_time_limit = value;
        }
        get
        {
            return m_time_limit;
        }
    }
    /**
    * @brief 制限時間
    */
    public bool IsTimeOver
    {
        set
        {
            m_is_time_over = value;
        }
        get
        {
            return m_is_time_over;
        }
    }
    /**
    * @brief カウントアップ
    */
    public bool IsCountUp
    {
        set
        {
            m_count_up = value;
        }
        get
        {
            return m_count_up;
        }
    }
    /**
    * @brief カウントダウン
    */
    public bool IsCountDown
    {
        set
        {
            m_count_down = value;
        }
        get
        {
            return m_count_down;
        }
    }
    /*===============================================================*/
    #endregion

    /*===============================================================*/
    /**
    * @brief コンストラクタ
    */
    public Clock ()
    {
        this.Initialize();
    }
    /*===============================================================*/

    /*===============================================================*/
    /**
    * @brief 初期化
    */
    public void Initialize ()
    {
        m_clock = null;
        m_gui_text = null;
        m_text = "";
        m_time = 0.0f;
        m_time_limit = 0.0f;
        m_is_time_over = false;
        m_count_up = false;
        m_count_down = false;
    }
    /*===============================================================*/

    /*===============================================================*/
    /**
    * @brief 実行処理
    */
    public void Execution ()
    {
        if (!m_is_time_over)
        {
            // カウントアップ.
            if (m_count_up)
            {
                m_time += UnityEngine.Time.deltaTime;
            }
            // カウントダウン.
            else if (m_count_down)
            {
                m_time_limit -= UnityEngine.Time.deltaTime;
                m_time = m_time_limit;
            }
            // デフォルト.
            else if (!m_count_up && !m_count_down)
            {
                m_time += UnityEngine.Time.deltaTime;
            }
            if (m_gui_text != null)
            {
                m_gui_text.text = m_text + (int)m_time;
            }
        }
    }
    /*===============================================================*/

    /*===============================================================*/
    /**
    * @brief GUITextで文字を表示するゲームオブジェクトの生成
    * @param string 表示したいテキスト
    * @param string 読み込むFontのPath
    * @param int フォントのサイズ
    * @param Color Fontの色
    * @param Vector3 スクリーン座標
    */
    public void CreateGameObject (string t_text, string t_font_path, int t_font_size, Color t_font_color, Vector3 t_position)
    {
        // オブジェクト生成.
        m_clock = new UnityEngine.GameObject();
        m_clock.name = "Clock";
        m_clock.transform.position = t_position;
        // GUITextの設定.
        m_gui_text = m_clock.AddComponent<GUIText>();
        m_text = t_text;
        m_gui_text.text = m_text;
        if(t_font_path != null) m_gui_text.font = Resources.Load(t_font_path) as Font;
        m_gui_text.fontSize = t_font_size;
        m_gui_text.color = t_font_color;
    }
    /*===============================================================*/

    /*===============================================================*/
    /**
    * @brief 制限時間のチェック
    * @return bool 制限時間を超えた場合の真偽値
    */
    public bool CheckTimeLimit ()
    {
        // カウントアップ.
        if (m_count_up && m_time > m_time_limit)
        {
            return true;
        }
        // カウントダウン.
        else if (m_count_down && 0.0f > m_time_limit)
        {
            return true;
        }
        // デフォルト.
        else if (!m_count_up && !m_count_down && m_time > m_time_limit)
        {
            return true;
        }
        return false;
    }
    /*===============================================================*/
}
/*===============================================================*/
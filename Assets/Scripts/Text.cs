using UnityEngine;
using System.Collections;

/*===============================================================*/
/**
* テキストクラス
* 2014年12月19日 Buravo
*/
public class Text
{
    #region メンバ変数
    /*===============================================================*/
    /**
    * @brief ゲームオブジェクト
    */
    private UnityEngine.GameObject m_text_object;
    /**
    * @brief TextMeshコンポーネント
    */
    private UnityEngine.TextMesh m_text_mesh;
    /**
    * @brief 標準で表示するテキスト
    */
    private string m_text;
    /*===============================================================*/
    #endregion

    #region アクセサ
    /*===============================================================*/
    /**
    * @brief ゲームオブジェクト
    */
    public GameObject TextObject
    {
        get
        {
            return m_text_object;
        }
    }
    /*===============================================================*/
    #endregion

    /*===============================================================*/
    /**
    * @brief コンストラクタ
    */
    public Text ()
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
    }
    /*===============================================================*/

    /*===============================================================*/
    /**
    * @brief 実行処理
    */
    public void Execution ()
    {
    }
    /*===============================================================*/

    /*===============================================================*/
    /**
    * @brief 3DTextで文字を表示するゲームオブジェクトの生成
    */
    public void CreateGameObject (string t_text_path, string t_object_name, Vector3 t_position, string t_text)
    {
        // オブジェクト生成.
        m_text_object = UnityEngine.GameObject.Instantiate(Resources.Load(t_text_path)) as GameObject;
        m_text_object.name = t_object_name;
        m_text_object.transform.position = t_position;
        // TextMeshの設定.
        m_text_mesh = m_text_object.GetComponent<UnityEngine.TextMesh>();
        m_text = t_text;
        m_text_mesh.text = m_text;
        Object.Destroy(m_text_object, 2.0f);
    }
    /*===============================================================*/
}
/*===============================================================*/

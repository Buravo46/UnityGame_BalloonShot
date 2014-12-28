using UnityEngine;
using System.Collections;

/*===============================================================*/
/**
* バルーンを制御するスクリプト
* 2014年11月21日 Buravo
*/ 
public class Balloon : MonoBehaviour 
{
    #region メンバ変数
    /*===============================================================*/
    /**
    * @brief トランスフォーム
    */
    private Transform m_trans;
    /**
    * @brief 移動速度
    */
    private float m_speed;
    /**
    * @brief 最小速度
    */
    private float m_min_speed = 2.0f;
    /**
    * @brief 最大速度
    */
    private float m_max_speed = 5.0f;
    /**
    * @brief 最小座標
    */
    private Vector3 m_min_position = new Vector3(1.0f, -15.0f, 0.0f);
    /**
    * @brief 最大座標
    */
    private Vector3 m_max_position = new Vector3(9.0f, -15.0f, 10.0f);
    /*===============================================================*/
    #endregion
    
    /*===============================================================*/
    /**
    * @brief 開始時に一度呼ばれるメソッド.
    */
    void Awake () 
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
        // トランスフォームの格納.
        m_trans = gameObject.transform;
        // 座標のランダム.
        m_trans.position = new Vector3(Random.Range(m_min_position.x, m_max_position.x),
                                         Random.Range(m_min_position.y, m_max_position.y), 
                                         Random.Range(m_min_position.z, m_max_position.z) );
        // 初期速度.
        m_speed = Random.Range(m_min_speed, m_max_speed);
        // 色をランダム.
        renderer.material.color = new Color(Random.value, Random.value, Random.value, 1.0f);
    }
    /*===============================================================*/

    /*===============================================================*/
    /**
    * @brief 更新処理
    */
    void Update () 
    {
        // 速度.
        Vector3 speed = new Vector3(0.0f, m_speed*Time.deltaTime, 0.0f);
        // 速度の加算.
        m_trans.position += speed;
        // 画面外で消去.
        Vector3 viewPos = Camera.main.WorldToViewportPoint(m_trans.position);
        // 画面上部へ消えたら消去.
        if (viewPos.y > 1.5f)
        {
            this.gameObject.SetActive(false);;
        }
    }
    /*===============================================================*/

    /*===============================================================*/
    /**
    * @brief オブジェクトが有効化されたときに呼び出される.
    */
    void OnEnable ()
    {
        this.Initialize();
    }
    /*===============================================================*/
}
/*===============================================================*/
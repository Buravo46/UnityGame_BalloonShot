﻿using UnityEngine;
using System.Collections;

/*===============================================================*/
/**
* スコアの管理をするクラス
* 2014年12月6日 Buravo
*/ 
public class ScoreManager : Singleton<ScoreManager> 
{

    #region メンバ変数
    /*===============================================================*/
    /**
    * @brief スコア
    */
    private int m_score;
    /**
    * @brief ハイスコア
    */
    private int m_high_score;
    /*===============================================================*/
    #endregion

    #region アクセサ
    /*===============================================================*/
    /**
    * @brief スコア
    */
    public int Score 
    {
        set
        {
            m_score = value;
        }
        get
        {
            return m_score;
        }
    }
    /**
    * @brief ハイスコア
    */
    public int HighScore 
    {
        get
        {
            return m_high_score;
        }
    }
    /*===============================================================*/
    #endregion

    /*===============================================================*/
    /**
    * @brief パラメーターなしのパブリックなコンストラクタ
    */
    public ScoreManager () {}
    /*===============================================================*/

    /*===============================================================*/
    /**
    * @brief スコアの加算処理
    * @param 加算するスコアの値
    */
    public void AddScore (int t_add_score) 
    {
        m_score += t_add_score;
        if (m_score > m_high_score)
        {
            m_high_score = m_score;
        }
    }
    /*===============================================================*/

    /*===============================================================*/
    /**
    * @brief スコアの初期化
    */
    public void Reset () 
    {
        m_score = 0;
    }
    /*===============================================================*/
}
/*===============================================================*/

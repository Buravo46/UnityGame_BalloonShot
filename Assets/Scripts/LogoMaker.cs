using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
/*===============================================================*/
/**
* ロゴの作成
* 2014年11月23日 Buravo
*/ 
public class LogoMaker 
{
    #region メンバ変数
    /*===============================================================*/
    /**
    * @brief ロゴのテキストデータ
    */
    private string m_logo_text;
    /**
    * @brief 開始座標リスト
    */
    private List<Vector3> m_from_position_list = new List<Vector3>();
    /**
    * @brief 終了座標リスト
    */
    private List<Vector3> m_to_position_list = new List<Vector3>();
    /**
    * @brief オブジェクトリスト
    */
    private List<GameObject> m_obj_list = new List<GameObject>();
    /**
    * @brief 親オブジェクトが存在するかどうかを判断するためのフラグ
    */
    private bool m_is_parent = false;
    /**
    * @brief 親オブジェクト
    */
    private GameObject m_parent;
    /*===============================================================*/
    #endregion 
    
    /*===============================================================*/
    /**
    * @brief コンストラクタ
    */
    public LogoMaker ()
    {
        this.Initialize();
    }
    /*===============================================================*/
    
    /*===============================================================*/
    /**
    * @brief 初期化処理関数
    */
    public void Initialize () 
    {
    }
    /*===============================================================*/

    /*===============================================================*/
    /**
    * @brief ロゴの生成を開始する関数
    * @param ロゴのプレハブのパス
    */
    public void CreateLogoObjects (string t_prefab_path)
    {
        int logoPosX = 0;
        int logoPosY = 0;
        int objectPosIndex = 0;
        // テキストを改行ごとに分割し, 複数の行として取得.
        string[] logoLines = TextAssetCommon.GetTextLines(m_logo_text);
        // 複数の行から全ての行が取り出されるまで、１行を取り出し処理する.
        foreach (string logoLine in logoLines)
        {
            // １行から全ての文字が取り出されるまで、１文字を取り出し処理する.
            foreach (char pattern in logoLine)
            {
                if(pattern == '+')
                {
                    // オブジェクト生成.
                    GameObject obj = UnityEngine.GameObject.Instantiate( Resources.Load(t_prefab_path) ) as GameObject;
                    if (m_is_parent)
                    {
                        obj.transform.parent = m_parent.transform;
                        obj.transform.localPosition = m_from_position_list[objectPosIndex];
                    } 
                    else 
                    {
                        obj.transform.position = m_from_position_list[objectPosIndex];
                    }
                    m_obj_list.Add(obj);
                    objectPosIndex++;
                }
                logoPosX++;
            }
            logoPosY++;
            logoPosX = 0;
        }
    }
    /*===============================================================*/

    /*===============================================================*/
    /**
    * @brief ロゴの座標をセットする関数
    * @param Vector3 開始位置
    * @param Vector3 終了位置
    */
    public void SetLogoPosition (Vector3 t_from_position, Vector3 t_to_position)
    {
        int logoPosX = 0;
        int logoPosY = 0;
        // テキストを改行ごとに分割し, 複数の行として取得.
        string[] logoLines = TextAssetCommon.GetTextLines(m_logo_text);
        // 複数の行から全ての行が取り出されるまで、１行を取り出し処理する.
        foreach (string logoLine in logoLines)
        {
            // １行から全ての文字が取り出されるまで、１文字を取り出し処理する.
            foreach (char pattern in logoLine)
            {
                if(pattern == '+')
                {
                    // 開始位置の生成.
                    Vector3 fromPos = new Vector3(t_from_position.x + logoPosX, t_from_position.y - logoPosY, t_from_position.z);
                    // 開始位置の格納.
                    m_from_position_list.Add(fromPos);
                    // 終了位置の生成.
                    Vector3 toPos = new Vector3(t_to_position.x + logoPosX, t_to_position.y - logoPosY, t_to_position.z);
                    // 終了位置の格納.
                    m_to_position_list.Add(toPos);
                }
                logoPosX++;
            }
            logoPosY++;
            logoPosX = 0;
        }
    }
    /*===============================================================*/

    /*===============================================================*/
    /**
    * @brief ロゴのアニメーションを開始する関数
    */
    public void StartAnimation ()
    {
        int objectIndex = 0;
        foreach (GameObject obj in m_obj_list)
        {
            AnimationClip clip = AnimationClipCommon.Move("Move", "EaseInOut", m_from_position_list[objectIndex], m_to_position_list[objectIndex], 0.0f, 180.0f);
            obj.animation.AddClip(clip, clip.name);
            obj.animation.PlayQueued(clip.name, QueueMode.CompleteOthers);
            objectIndex++;
        }
    }
    /*===============================================================*/

    /*===============================================================*/
    /**
    * @brief 親オブジェクトの設定を行う関数
    * @param string 親オブジェクトの名前
    */
    public void SetParent (string t_parent_name)
    {
        if (t_parent_name != null)
        {
            m_parent = GameObject.Find(t_parent_name);
            m_is_parent = true;
        } 
        else 
        {
            m_parent = null;
            m_is_parent = false;
        }
    }
    /*===============================================================*/

    /*===============================================================*/
    /**
    * @brief テキストデータのセットを行う関数
    * @param string ロゴが描かれているテキストデータのファイルパス
    */
    public void SetLogoText (string t_file_path)
    {
        if (t_file_path != null)
        {
            m_logo_text = TextAssetCommon.ReadText(t_file_path);
        }
    }
    /*===============================================================*/
}
/*===============================================================*/

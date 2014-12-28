using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*===============================================================*/
/**
* オブジェクトプール管理するクラス
* 2014年12月16日 Buravo
*/ 
public class ObjectPoolManager : Singleton<ObjectPoolManager>
{
    
    #region メンバ変数
    /*===============================================================*/
    /**
    * @brief オブジェクトを格納するリストのハッシュテーブル
    */
    private Dictionary<string, List<GameObject>> m_pooled_dictionary = new Dictionary<string,List<GameObject>>();
    /*===============================================================*/
    #endregion

    /*===============================================================*/
    /**
    * @brief パラメーターなしのパブリックなコンストラクタ
    */
    public ObjectPoolManager ()
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
        m_pooled_dictionary = new Dictionary<string,List<GameObject>>();
    }
    /*===============================================================*/

    /*===============================================================*/
    /**
    * @brief オブジェクトの追加
    * @param string 追加するオブジェクトのキー
    * @param GameObject 追加するオブジェクト
    */
    public void AddPoolObject (string t_pooled_name, GameObject t_add_object)
    {
        // キーの存在がなければ.
        if (!m_pooled_dictionary.ContainsKey(t_pooled_name))
        {
            // ジェネリックリストの生成.
            List<GameObject> pooledObjectList = new List<GameObject>();
            // オブジェクトをリストに追加.
            pooledObjectList.Add(t_add_object);
            // リストをディクショナリに追加.
            m_pooled_dictionary.Add(t_pooled_name, pooledObjectList);
        } 
        // キーの存在があれば.
        else if(m_pooled_dictionary.ContainsKey(t_pooled_name))
        {
            // ジェネリックリストの取得.
            List<GameObject> pooledObjectList = m_pooled_dictionary[t_pooled_name];
            // オブジェクトをリストに追加.
            pooledObjectList.Add(t_add_object);
            // リストをディクショナリに追加.
            m_pooled_dictionary[t_pooled_name] = pooledObjectList;
        }
    }
    /*===============================================================*/

    /*===============================================================*/
    /**
    * @brief 無効なオブジェクトの取得
    * @param string 取得したいオブジェクトのキー
    * @return GameObject 取得できるオブジェクト
    */
    public GameObject GetPooledObject (string t_pooled_name)
    {
        // キーの存在チェック.
        if (m_pooled_dictionary.ContainsKey(t_pooled_name))
        {
            // リストの取得.
            List<GameObject> pooledObjectList = m_pooled_dictionary[t_pooled_name];
            // 全てのオブジェクトに対して.
            for (int i = 0; i < pooledObjectList.Count; i++)
            {
                // もしもゲームオブジェクトがシーンで有効でなければ.
                if(!pooledObjectList[i].activeInHierarchy)
                {
                    // 使い回わせるobjectを渡す.
                    return pooledObjectList[i];
                }
            }
        }
        return null;
    }
    /*===============================================================*/

    /*===============================================================*/
    /**
    * @brief 解放
    */
    public void Release ()
    {
        m_pooled_dictionary = null;
        this.Initialize();
    }
    /*===============================================================*/
}
/*===============================================================*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*===============================================================*/
/**
* オブジェクトの有効化/無効化を管理するクラス
* 2014年12月16日 Buravo
*/ 
public class ObjectActiveManager : Singleton<ObjectActiveManager> 
{

    /*===============================================================*/
    /**
    * @brief パラメーターなしのパブリックなコンストラクタ
    */
    public ObjectActiveManager ()
    {
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
    * @brief オブジェクトをアクティブにする
    * @param string 利用するオブジェクトのキー
    */
    public void PerformActivity (string t_pooled_name)
    {
        // 使い回せるobjectを代入.
        GameObject activeObject = ObjectPoolManager.Instance.GetPooledObject(t_pooled_name);
        // 使い回せないならreturn.
        if(activeObject == null) return;
        // 有効化.
        activeObject.SetActive(true);
    }
    /*===============================================================*/

    /*===============================================================*/
    /**
    * @brief オブジェクトをアクティブにして渡す
    * @param string 利用するオブジェクトのキー
    * @return GameObject アクティブにしたオブジェクト
    */
    public GameObject GetActiveObject (string t_pooled_name)
    {
        // 使い回せるobjectを代入.
        GameObject activeObject = ObjectPoolManager.Instance.GetPooledObject(t_pooled_name);
        // 使い回せないならreturn.
        if(activeObject == null) return null;
        // 有効化.
        activeObject.SetActive(true);
        return activeObject;
    }
    /*===============================================================*/
}
/*===============================================================*/
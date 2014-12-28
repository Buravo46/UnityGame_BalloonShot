using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

/*===============================================================*/
/**
* ジェネリックなシングルトンパターン
* 参考URL : http://msdn.microsoft.com/ja-jp/library/ms998558.aspx
* このジェネリッククラスはclassとnewの型パラメーターの制約をしている
* 参考URL : http://msdn.microsoft.com/ja-jp/library/d5x73970.aspx
* 2014年12月6日 Buravo
*/
public class Singleton<T> where T : class, new()
{
    #region メンバ変数
    /*===============================================================*/
    /**
    * @brief インスタンス変数への代入が完了するまで、アクセスできなくなるジェネリックなインスタンス
    */
    private static volatile T m_instance;
    /**
    * @brief ジェネリックなインスタンス
    */
    private static object m_sync_obj = new object (); 
    /*===============================================================*/
    #endregion

    #region アクセサ変数
    /*===============================================================*/
    /**
    * @brief ジェネリックなインスタンス
    */
    public static T Instance
    {
        get 
        {
            // ダブルチェック ロッキング アプローチ.
            if (m_instance == null)
            {
                // m_sync_objインスタンスをロックし、この型そのものをロックしないことで、デッドロックの発生を回避
                lock (m_sync_obj)
                { 
                    if (m_instance == null)
                    {
                        m_instance = new T ();
                    }
                }
            }
            return m_instance;
        }
    }
    /*===============================================================*/
    #endregion

    /*===============================================================*/
    /**
    * @brief コンストラクタ
    */
    protected Singleton () {}
    /*===============================================================*/
}
/*===============================================================*/

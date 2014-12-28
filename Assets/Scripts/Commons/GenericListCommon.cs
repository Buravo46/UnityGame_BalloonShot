using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

/*===============================================================*/
/**
* ジェネリックリストのユーティリティクラス
* 2014年11月30日 Buravo
*/ 
public sealed class GenericListCommon 
{

    /*===============================================================*/
    /**
    * @brief コンストラクタ
    */
    private GenericListCommon () {}
    /*===============================================================*/

    /*===============================================================*/
    /**
    * @brief ジェネリックリストのシャッフルをする関数
    * @param List<T> シャッフル前のジェネリックリスト
    * @return List<T> シャッフル後のジェネリックリスト
    */
    public static List<T> Shuffle<T> (List<T> t_list)
    {
        if (t_list != null) 
        {
            System.Random random = new System.Random(unchecked((int)DateTime.Now.Ticks));
            for (int i = 0; i < t_list.Count; i++)
            {
                int value = random.Next(t_list.Count);
                T temp = t_list[value];
                t_list[value] = t_list[0];
                t_list[0] = temp;
            }
            return t_list;
        }
        else
        {
            Debug.Log("GenericListCommon.Shuffle : list is null .");
            return null;
        }
    }
    /*===============================================================*/
}
/*===============================================================*/


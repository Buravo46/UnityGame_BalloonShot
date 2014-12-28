using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

/*===============================================================*/
/**
* アニメーションイベントのユーティリティクラス
* 2014年12月6日 Buravo
*/ 
public sealed class AnimationEventCommon 
{
    /*===============================================================*/
    /**
    * @brief コンストラクタ
    */
    private AnimationEventCommon () {}
    /*===============================================================*/

    /*===============================================================*/
    /**
    * @brief アニメーションクリップにアニメーションイベントを追加する関数
    * @param AnimationClip 参照渡しするAnimationClip名
    * @param string 発火する関数名
    * @param string 引数の型名
    * @param System.Object 引数の値
    * @param float イベントが発火する時間
    */
    public static void AddEvent (ref AnimationClip t_clip, string t_function_name, string t_param_type, System.Object t_param, float t_time)
    {
        // アニメーションイベントの作成.
        AnimationEvent animationEvent = new AnimationEvent();
        // 関数名をセット.
        animationEvent.functionName = t_function_name;
        // 引数のタイプに応じた引き渡し.
        switch (t_param_type)
        {
            case "int":
            // int型のパラメーターを格納.
            animationEvent.intParameter = (int)t_param;
            break;

            case "float":
            // float型のパラメーターを格納.
            animationEvent.floatParameter = (float)t_param;
            break;

            case "string":
            // string型のパラメーターを格納.
            animationEvent.stringParameter = t_param as string;
            break;

            case "object":
            // object型のパラメーターを格納.
            animationEvent.objectReferenceParameter = t_param as UnityEngine.Object;
            break;

            default:
            break;
        }
        // 設定した時間にイベントを送信.
        animationEvent.time = t_time;
        // アニメーションイベントの追加.
        t_clip.AddEvent(animationEvent);
    }
    /*===============================================================*/
}
/*===============================================================*/



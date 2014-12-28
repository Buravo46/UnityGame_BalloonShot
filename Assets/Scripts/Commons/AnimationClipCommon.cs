using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

/*===============================================================*/
/**
* アニメーションクリップのユーティリティクラス
* 2014年12月1日 Buravo
*/ 
public sealed class AnimationClipCommon 
{
    /*===============================================================*/
    /**
    * @brief コンストラクタ
    */
    private AnimationClipCommon () {}
    /*===============================================================*/

    /*===============================================================*/
    /**
    * @brief 移動アニメーションクリップを生成し渡す関数
    * @param string AnimationClip名
    * @param string Type名
    * @param Vector3 開始座標
    * @param Vector3 終了座標
    * @param float 開始時間
    * @param float 終了座標
    * @return AnimationClip 生成したアニメーションクリップ
    */
    public static AnimationClip Move (string t_clip_name, string t_curve_type, Vector3 t_from_pos, Vector3 t_to_pos, float t_start_time, float t_end_time)
    {
        AnimationCurve curveX = new AnimationCurve();
        AnimationCurve curveY = new AnimationCurve();
        AnimationCurve curveZ = new AnimationCurve();
        // タイプに応じたAnimationCurveの作成.
        switch (t_curve_type)
        {
            case "Keyframe":
            curveX.AddKey(new Keyframe(t_start_time/60, t_from_pos.x));
            curveX.AddKey(new Keyframe(t_end_time/60, t_to_pos.x));
            curveY.AddKey(new Keyframe(t_start_time/60, t_from_pos.y));
            curveY.AddKey(new Keyframe(t_end_time/60, t_to_pos.y));
            curveZ.AddKey(new Keyframe(t_start_time/60, t_from_pos.z));
            curveZ.AddKey(new Keyframe(t_end_time/60, t_to_pos.z));
            break;

            case "Linear":
            curveX = AnimationCurve.Linear((t_start_time/60), t_from_pos.x, (t_end_time/60), t_to_pos.x);
            curveY = AnimationCurve.Linear((t_start_time/60), t_from_pos.y, (t_end_time/60), t_to_pos.y);
            curveZ = AnimationCurve.Linear((t_start_time/60), t_from_pos.z, (t_end_time/60), t_to_pos.z);
            break;

            case "EaseInOut":
            curveX = AnimationCurve.EaseInOut((t_start_time/60), t_from_pos.x, (t_end_time/60), t_to_pos.x);
            curveY = AnimationCurve.EaseInOut((t_start_time/60), t_from_pos.y, (t_end_time/60), t_to_pos.y);
            curveZ = AnimationCurve.EaseInOut((t_start_time/60), t_from_pos.z, (t_end_time/60), t_to_pos.z);
            break;

            default:
            curveX.AddKey(new Keyframe(t_start_time/60, t_from_pos.x));
            curveX.AddKey(new Keyframe(t_end_time/60, t_to_pos.x));
            curveY.AddKey(new Keyframe(t_start_time/60, t_from_pos.y));
            curveY.AddKey(new Keyframe(t_end_time/60, t_to_pos.y));
            curveZ.AddKey(new Keyframe(t_start_time/60, t_from_pos.z));
            curveZ.AddKey(new Keyframe(t_end_time/60, t_to_pos.z));
            break;
        }
            
        AnimationClip clip = new AnimationClip();
        clip.name = t_clip_name;

        clip.SetCurve("", typeof(Transform), "localPosition.x", curveX);
        clip.SetCurve("", typeof(Transform), "localPosition.y", curveY);
        clip.SetCurve("", typeof(Transform), "localPosition.z", curveZ);

        return clip;
    }
    /*===============================================================*/

    /*===============================================================*/
    /**
    * @brief 回転アニメーションクリップを生成し渡す関数
    * @param string AnimationClip名
    * @param string Type名
    * @param Vector3 開始角度
    * @param Vector3 終了角度
    * @param float 開始時間
    * @param float 終了座標
    * @return AnimationClip 生成したアニメーションクリップ
    */
    public static AnimationClip Rotate (string t_clip_name, string t_curve_type, Vector3 t_from_rotation, Vector3 t_to_rotation, float t_start_time, float t_end_time)
    {
        // Vector3である角度をQuaternionに変換してCurveに渡す.
        Quaternion startAngle = Quaternion.Euler(t_from_rotation);
        Quaternion endAngle = Quaternion.Euler(t_to_rotation);
        // アニメーションカーブの生成.
        AnimationCurve curveX = new AnimationCurve();
        AnimationCurve curveY = new AnimationCurve();
        AnimationCurve curveZ = new AnimationCurve();
        AnimationCurve curveW = new AnimationCurve();
        // タイプに応じたAnimationCurveの作成.
        switch (t_curve_type)
        {
            case "Keyframe":
            curveX.AddKey(new Keyframe(t_start_time/60, startAngle.x));
            curveX.AddKey(new Keyframe(t_end_time/60, endAngle.x));
            curveY.AddKey(new Keyframe(t_start_time/60, startAngle.y));
            curveY.AddKey(new Keyframe(t_end_time/60, endAngle.y));
            curveZ.AddKey(new Keyframe(t_start_time/60, startAngle.z));
            curveZ.AddKey(new Keyframe(t_end_time/60, endAngle.z));
            curveW.AddKey(new Keyframe(t_start_time/60, startAngle.w));
            curveW.AddKey(new Keyframe(t_end_time/60, endAngle.w));
            break;
            
            case "Linear":
            curveX = AnimationCurve.Linear((t_start_time/60), startAngle.x, (t_end_time/60), endAngle.x);
            curveY = AnimationCurve.Linear((t_start_time/60), startAngle.y, (t_end_time/60), endAngle.y);
            curveZ = AnimationCurve.Linear((t_start_time/60), startAngle.z, (t_end_time/60), endAngle.z);
            curveW = AnimationCurve.Linear((t_start_time/60), startAngle.w, (t_end_time/60), endAngle.w);
            break;

            case "EaseInOut":
            curveX = AnimationCurve.EaseInOut((t_start_time/60), startAngle.x, (t_end_time/60), endAngle.x);
            curveY = AnimationCurve.EaseInOut((t_start_time/60), startAngle.y, (t_end_time/60), endAngle.y);
            curveZ = AnimationCurve.EaseInOut((t_start_time/60), startAngle.z, (t_end_time/60), endAngle.z);
            curveW = AnimationCurve.EaseInOut((t_start_time/60), startAngle.w, (t_end_time/60), endAngle.w);
            break;

            default:
            curveX.AddKey(new Keyframe(t_start_time/60, startAngle.x));
            curveX.AddKey(new Keyframe(t_end_time/60, endAngle.x));
            curveY.AddKey(new Keyframe(t_start_time/60, startAngle.y));
            curveY.AddKey(new Keyframe(t_end_time/60, endAngle.y));
            curveZ.AddKey(new Keyframe(t_start_time/60, startAngle.z));
            curveZ.AddKey(new Keyframe(t_end_time/60, endAngle.z));
            curveW.AddKey(new Keyframe(t_start_time/60, startAngle.w));
            curveW.AddKey(new Keyframe(t_end_time/60, endAngle.w));
            break;
        }
        
        AnimationClip clip = new AnimationClip();
        clip.name = t_clip_name;

        clip.SetCurve("", typeof(Transform), "localRotation.x", curveX);
        clip.SetCurve("", typeof(Transform), "localRotation.y", curveY);
        clip.SetCurve("", typeof(Transform), "localRotation.z", curveZ);
        clip.SetCurve("", typeof(Transform), "localRotation.w", curveW);

        return clip;
    }
    /*===============================================================*/

    /*===============================================================*/
    /**
    * @brief AudioSouceのボリュームを制御するアニメーションクリップを生成し渡す関数
    * @param string AnimationClip名
    * @param string Type名
    * @param float 開始ボリューム値
    * @param float 終了ボリューム値
    * @param float 開始時間
    * @param float 終了座標
    * @return AnimationClip 生成したアニメーションクリップ
    */
    public static AnimationClip Volume (string t_clip_name, string t_curve_type, float t_from_volume, float t_to_volume, float t_start_time, float t_end_time)
    {
        AnimationCurve curve = new AnimationCurve();
        // タイプに応じたAnimationCurveの作成.
        switch (t_curve_type)
        {
            case "Keyframe":
            curve.AddKey(new Keyframe(t_start_time/60, t_from_volume));
            curve.AddKey(new Keyframe(t_end_time/60, t_to_volume));
            break;

            case "Linear":
            curve = AnimationCurve.Linear((t_start_time/60), t_from_volume, (t_end_time/60), t_to_volume);
            break;

            case "EaseInOut":
            curve = AnimationCurve.EaseInOut((t_start_time/60), t_from_volume, (t_end_time/60), t_to_volume);
            break;

            default:
            curve.AddKey(new Keyframe(t_start_time/60, t_from_volume));
            curve.AddKey(new Keyframe(t_end_time/60, t_to_volume));
            break;
        }
        // アニメーションクリップの生成.
        AnimationClip clip = new AnimationClip();
        clip.name = t_clip_name;

        clip.SetCurve("", typeof(AudioSource), "m_Volume", curve);

        return clip;
    }
    /*===============================================================*/

    /*===============================================================*/
    /**
    * @brief GUITextureを利用したフェードアニメーションクリップを生成し渡す関数
    * @param string AnimationClip名
    * @param string Type名
    * @param Color 開始色
    * @param Color 終了色
    * @param float 開始時間
    * @param float 終了座標
    * @return AnimationClip 生成したアニメーションクリップ
    */
    public static AnimationClip FadeGUITexture (string t_clip_name, string t_curve_type, Color t_from_color, Color t_to_color, float t_start_time, float t_end_time)
    {
        // アニメーションカーブの生成.
        AnimationCurve curveR = new AnimationCurve();
        AnimationCurve curveG = new AnimationCurve();
        AnimationCurve curveB = new AnimationCurve();
        AnimationCurve curveA = new AnimationCurve();
        // タイプに応じたAnimationCurveの作成.
        switch (t_curve_type)
        {
            case "Keyframe":
            curveR.AddKey(new Keyframe(t_start_time/60, t_from_color.r));
            curveR.AddKey(new Keyframe(t_end_time/60, t_to_color.r));
            curveG.AddKey(new Keyframe(t_start_time/60, t_from_color.g));
            curveG.AddKey(new Keyframe(t_end_time/60, t_to_color.g));
            curveB.AddKey(new Keyframe(t_start_time/60, t_from_color.b));
            curveB.AddKey(new Keyframe(t_end_time/60, t_to_color.b));
            curveA.AddKey(new Keyframe(t_start_time/60, t_from_color.a));
            curveA.AddKey(new Keyframe(t_end_time/60, t_to_color.a));
            break;
            
            case "Linear":
            curveR = AnimationCurve.Linear((t_start_time/60), t_from_color.r, (t_end_time/60), t_to_color.r);
            curveG = AnimationCurve.Linear((t_start_time/60), t_from_color.g, (t_end_time/60), t_to_color.g);
            curveB = AnimationCurve.Linear((t_start_time/60), t_from_color.b, (t_end_time/60), t_to_color.b);
            curveA = AnimationCurve.Linear((t_start_time/60), t_from_color.a, (t_end_time/60), t_to_color.a);
            break;

            case "EaseInOut":
            curveR = AnimationCurve.EaseInOut((t_start_time/60), t_from_color.r, (t_end_time/60), t_to_color.r);
            curveG = AnimationCurve.EaseInOut((t_start_time/60), t_from_color.g, (t_end_time/60), t_to_color.g);
            curveB = AnimationCurve.EaseInOut((t_start_time/60), t_from_color.b, (t_end_time/60), t_to_color.b);
            curveA = AnimationCurve.EaseInOut((t_start_time/60), t_from_color.a, (t_end_time/60), t_to_color.a);
            break;

            default:
            curveR.AddKey(new Keyframe(t_start_time/60, t_from_color.r));
            curveR.AddKey(new Keyframe(t_end_time/60, t_to_color.r));
            curveG.AddKey(new Keyframe(t_start_time/60, t_from_color.g));
            curveG.AddKey(new Keyframe(t_end_time/60, t_to_color.g));
            curveB.AddKey(new Keyframe(t_start_time/60, t_from_color.b));
            curveB.AddKey(new Keyframe(t_end_time/60, t_to_color.b));
            curveA.AddKey(new Keyframe(t_start_time/60, t_from_color.a));
            curveA.AddKey(new Keyframe(t_end_time/60, t_to_color.a));
            break;
        }
        
        AnimationClip clip = new AnimationClip();
        clip.name = t_clip_name;

        clip.SetCurve("", typeof(GUITexture), "m_Color.r", curveR);
        clip.SetCurve("", typeof(GUITexture), "m_Color.g", curveG);
        clip.SetCurve("", typeof(GUITexture), "m_Color.b", curveB);
        clip.SetCurve("", typeof(GUITexture), "m_Color.a", curveA);

        return clip;
    }
    /*===============================================================*/
}
/*===============================================================*/


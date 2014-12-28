using UnityEngine;
using System.Collections;

// コンポーネントの追加.
[RequireComponent(typeof(GUITexture))]
[RequireComponent(typeof(Animation))]

/*===============================================================*/
/**
* 遷移状態を制御するクラス
* 2014年9月1日 Shuhei Hirokawa
*/
public class Transition : MonoBehaviour 
{

    #region アクセサ変数
    /*===============================================================*/
    /**
    * @brief 再生中かどうか
    */
    public bool IsPlaying
    {
        get
        { 
            return animation.isPlaying;
        }
    }
    /*===============================================================*/
    #endregion

    /*===============================================================*/
    /**
    * @brief 初期化
    */
    void Awake () 
    {
        // テクスチャ生成.
        Texture2D texture = new Texture2D( 1, 1, TextureFormat.ARGB32, false );
        texture.SetPixel(0,0, Color.white );
        texture.Apply();
        // 代入.
        guiTexture.texture = texture;
        guiTexture.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
        // フルスクリーン化.
        guiTexture.pixelInset = new Rect(Screen.width/2, Screen.height/2, Screen.width, Screen.height);
    }
    /*===============================================================*/

    /*===============================================================*/
    /**
    * @brief 初期化
    */
    void Start () 
    {
    }
    /*===============================================================*/

    /*===============================================================*/
    /**
    * @brief 更新処理
    */
    void Update () 
    {
    }
    /*===============================================================*/

    /*===============================================================*/
    /**
    * @brief フェードアニメーション開始
    * @param Color 開始値.
    * @param Color 終了値.
    * @param float 開始時間.
    * @param float 終了時間.
    */
    public void PlayFade (Color t_from_color, Color t_to_color, float t_start_time, float t_end_time) 
    {
        // 生成.
        AnimationClip clip = AnimationClipCommon.FadeGUITexture("Fade", "EaseInOut", t_from_color, t_to_color, t_start_time, t_end_time);
        // アニメーションの追加.
        animation.AddClip(clip, clip.name);
        // アニメーションの再生.
        animation.PlayQueued(clip.name, QueueMode.CompleteOthers);
    }
    /*===============================================================*/

    /*===============================================================*/
    /**
    * @brief フェードアニメーションしてシーン遷移を行う
    * @param Color 開始値.
    * @param Color 終了値.
    * @param float 開始時間.
    * @param float 終了時間.
    * @param string シーン遷移名
    */
    public void PlayFadeAfterLoadScene (Color t_from_color, Color t_to_color, float t_start_time, float t_end_time, string t_scene_name) 
    {
        // 生成.
        AnimationClip clip = AnimationClipCommon.FadeGUITexture("Fade", "EaseInOut", t_from_color, t_to_color, t_start_time, t_end_time);
        // 終了時に発火するイベント追加.
        AnimationEventCommon.AddEvent(ref clip, "LoadSceneEvent", "string", t_scene_name, clip.length);
        // アニメーションの追加.
        animation.AddClip(clip, clip.name);
        // アニメーションの再生.
        animation.PlayQueued(clip.name, QueueMode.CompleteOthers);
    }
    /*===============================================================*/

    /*===============================================================*/
    /**
    * @brief アニメーションが終了すれば発火する遷移するシーンを読み込むアニメーションイベント
    * @param string 遷移するシーン名
    */
    public void LoadSceneEvent (string t_secne_name) 
    {
        Application.LoadLevel(t_secne_name);
    }
    /*===============================================================*/
}
/*===============================================================*/
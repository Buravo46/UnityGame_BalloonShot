using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

/*===============================================================*/
/**
* 音の管理をするクラス
* 2014年12月6日 Buravo
*/ 
public class AudioManager : Singleton<AudioManager> 
{
    #region メンバ変数
    /*===============================================================*/
    /**
    * @brief ゲームオブジェクト
    */
    private GameObject m_audio_manager;
    /**
    * @brief SEのハッシュテーブル
    */
    private Dictionary<string, AudioSource> m_se_dictionary = new Dictionary<string,AudioSource>();
    /**
    * @brief BGMのハッシュテーブル
    */
    private Dictionary<string, AudioSource> m_bgm_dictionary = new Dictionary<string,AudioSource>();
    /*===============================================================*/
    #endregion

    /*===============================================================*/
    /**
    * @brief パラメーターなしのパブリックなコンストラクタ
    */
    public AudioManager ()
    {
        if(!GameObject.Find("AudioManager"))
        {
            // オブジェクト生成.
            m_audio_manager = new GameObject();
            m_audio_manager.name = "AudioManager";
            // シーンを切り替えても指定のオブジェクトを破棄せずに残す属性を付与.
            UnityEngine.Object.DontDestroyOnLoad(m_audio_manager);
        }
    }
    /*===============================================================*/

    /*===============================================================*/
    /**
    * @brief SEの追加
    * @param string SE名
    * @param string SEのファイルパス
    * @param bool Loopするかどうか
    */
    public void AddSE (string t_se_name, string t_file_path, bool t_loop)
    {
        // キーの存在チェック.
        if (!m_se_dictionary.ContainsKey(t_se_name))
        {
            AudioSource audioComponent = m_audio_manager.AddComponent<AudioSource>();
            AudioClip clip = Resources.Load(t_file_path) as AudioClip;
            audioComponent.clip = clip;
            audioComponent.loop = t_loop;
            // 要素の追加.
            m_se_dictionary.Add(t_se_name, audioComponent);
        }
    }
    /*===============================================================*/

    /*===============================================================*/
    /**
    * @brief BGMの追加
    * @param string BGM名
    * @param string BGMのファイルパス
    * @param bool Loopするかどうか
    */
    public void AddBGM (string t_bgm_name, string t_file_path, bool t_loop)
    {
        // キーの存在チェック.
        if (!m_bgm_dictionary.ContainsKey(t_bgm_name))
        {
            AudioSource audioComponent = m_audio_manager.AddComponent<AudioSource>();
            AudioClip clip = Resources.Load(t_file_path) as AudioClip;
            audioComponent.clip = clip;
            audioComponent.loop = t_loop;
            m_bgm_dictionary.Add(t_bgm_name, audioComponent);
        }
    }
    /*===============================================================*/

    /*===============================================================*/
    /**
    * @brief SEの再生
    * @param string SE名
    */
    public void PlayOneShotSE (string t_se_name)
    {
        // キーの存在チェック.
        if (m_se_dictionary.ContainsKey(t_se_name))
        {
            AudioSource audioComponent = m_se_dictionary[t_se_name];
            audioComponent.PlayOneShot(audioComponent.clip);
        }
    }
    /*===============================================================*/

    /*===============================================================*/
    /**
    * @brief SEの再生
    * @param string SE名
    */
    public void PlaySE (string t_se_name)
    {
        // キーの存在チェック.
        if (m_se_dictionary.ContainsKey(t_se_name))
        {
            AudioSource audioComponent = m_se_dictionary[t_se_name];
            audioComponent.Play();
        }
    }
    /*===============================================================*/

    /*===============================================================*/
    /**
    * @brief BGMの再生
    * @param string BGM名
    */
    public void PlayBGM (string t_bgm_name)
    {
        // キーの存在チェック.
        if (m_bgm_dictionary.ContainsKey(t_bgm_name))
        {
            AudioSource audioComponent = m_bgm_dictionary[t_bgm_name];
            audioComponent.Play();
        }
    }
    /*===============================================================*/

    /*===============================================================*/
    /**
    * @brief SEの停止
    * @param string SE名
    */
    public void StopSE (string t_se_name)
    {
        // キーの存在チェック.
        if (m_se_dictionary.ContainsKey(t_se_name))
        {
            AudioSource audioComponent = m_se_dictionary[t_se_name];
            audioComponent.Stop();
        }
    }
    /*===============================================================*/

    /*===============================================================*/
    /**
    * @brief BGMの停止
    * @param string BGM名
    */
    public void StopBGM (string t_bgm_name)
    {
        // キーの存在チェック.
        if (m_bgm_dictionary.ContainsKey(t_bgm_name))
        {
            AudioSource audioComponent = m_bgm_dictionary[t_bgm_name];
            audioComponent.Stop();
        }
    }
    /*===============================================================*/
}
/*===============================================================*/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;

/*===============================================================*/
/**
* テキストアセットのユーティリティクラス
* 2014年11月30日 Buravo
*/ 
public sealed class TextAssetCommon 
{
    
    /*===============================================================*/
    /**
    * @brief コンストラクタ
    */
    private TextAssetCommon () {}
    /*===============================================================*/

    /*===============================================================*/
    /**
    * @brief テキストデータの読み込みを行う関数
    * @param string テキストデータのファイルパス
    * @return string テキストデータ
    */
    public static string ReadText (string t_file_path)
    {
        if (t_file_path != null)
        {
            TextAsset textAsset = Resources.Load(t_file_path) as TextAsset;
            string text = textAsset.text;
            return text;
        }
        else
        {
            Debug.Log("TextAssetCommon.ReadText : filePath is null .");
            return null;
        }
    }
    /*===============================================================*/

    /*===============================================================*/
    /**
    * @brief テキストデータの幅を取得する関数
    * @param string テキストデータ
    * @return Vector3 テキストデータの幅を格納したデータ
    */
    public static Vector3 GetTextSize (string t_text)
    {
        if (t_text != null)
        {
            // OS環境ごとに適切な改行コードをCR(=キャリッジリターン)に置換.
            string text = t_text.Replace(Environment.NewLine, "\r");
            // テキストデータの前後からCRを取り除く.
            text = text.Trim('\r');
            // CRを区切り文字として分割して配列に変換.
            string[] textLines = text.Split('\r');
            // 改行コード分を加えた横幅の取得.
            int width = textLines[0].Length + 1;
            // 縦幅の取得.
            int height = textLines.Length;
            return new Vector3(width, height, 0);
        } 
        else
        {
            Debug.Log("TextAssetCommon.GetTextSize : text is null .");
            return new Vector3(0, 0, 0);
        }
    }
    /*===============================================================*/

    /*===============================================================*/
    /**
    * @brief 海峡を区切り文字として分割したテキストデータを取得する関数
    * @param string テキストデータ
    * @return string[] 分割したテキストデータ
    */
    public static string[] GetTextLines (string t_text)
    {
        if (t_text != null)
        {
            // OS環境ごとに適切な改行コードをCR(=キャリッジリターン)に置換.
            string text = t_text.Replace(Environment.NewLine, "\r");
            // テキストデータの前後からCRを取り除く.
            text = text.Trim('\r');
            // CRを区切り文字として分割して配列に変換.
            string[] textLines = text.Split('\r');
            return textLines;
        } 
        else
        {
            Debug.Log("TextAssetCommon.GetTextLines : text is null .");
            return null;
        }
    }
    /*===============================================================*/
}
/*===============================================================*/


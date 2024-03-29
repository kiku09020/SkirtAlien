using System.IO;
using UnityEngine;

/* ★データの管理に関するスクリプトです */
public class DataManager : MonoBehaviour
{
    /* 値 */
    public GameData data;
    string filePath;
    string fileName = "GameData.json";

    /* コンポーネント取得用 */


//-------------------------------------------------------------------
    void Awake()
    {
        // パス指定
        filePath = Application.dataPath + "/" + fileName;

        if (!File.Exists(filePath)) {
            Save(data);
        }

        data = Load();
    }

//-------------------------------------------------------------------
    // セーブ
    public void Save(GameData data)
	{
        string json = JsonUtility.ToJson(data);                     // json形式に変換
        StreamWriter wr = new StreamWriter(filePath, false);        // ファイル開く
        wr.WriteLine(json);                                         // 書き込み
        wr.Close();                                                 // ファイル閉じる
	}
    
    // ロード
    public GameData Load()
	{
        StreamReader rd = new StreamReader(filePath);       // ファイル開く
        string json = rd.ReadToEnd();                   // ファイル内容読み込み
        rd.Close();                                     // ファイル閉じる

        return JsonUtility.FromJson<GameData>(json);    // jsonファイルをGameData型にして戻す
	}

	void OnDestroy()
	{
        // セーブ
        Save(data);
	}
}

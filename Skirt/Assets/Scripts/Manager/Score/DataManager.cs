using System.IO;
using UnityEngine;

/* ★データの管理に関するスクリプトです */
public class DataManager : MonoBehaviour
{
    /* 値 */
    public GameData data = new GameData();
    string filePath;
    string fileName = "GameData.json";

    /* コンポーネント取得用 */


//-------------------------------------------------------------------
    void Awake()
    {
        // パス指定
        filePath = Application.dataPath + "/" + fileName;

        Save(data);

        // ロード
        Load(filePath);
    }

//-------------------------------------------------------------------
    // セーブ
    public void Save(GameData data)
	{
        string json = JsonUtility.ToJson(data);                     // json形式に変換
        StreamWriter wr = new StreamWriter(filePath, false);        // ファイル開く
        wr.WriteLine(json);                                         // 書き込み
        wr.Flush();                                                 // バッファ削除
        wr.Close();                                                 // ファイル閉じる
	}
    
    // ロード
    public GameData Load(string path)
	{
        StreamReader rd = new StreamReader(path);       // ファイル開く
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

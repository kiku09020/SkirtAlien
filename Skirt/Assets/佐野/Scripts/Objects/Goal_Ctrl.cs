using UnityEngine;

/* ★ゴール関連のスクリプトです */
public class Goal_Ctrl : MonoBehaviour
{
    /* コンポーネント取得用 */
    GameManager     gm;
    AudioManager    aud;
    CanvasGenelator cvsGen;
    Pl_Particle     part;
    ScoreManager    score;

    //-------------------------------------------------------------------
    void Start()
    {
        GameObject gm_obj  = GameObject.Find("GameManager");
        GameObject um_obj  = gm_obj.transform.Find("UIManager").gameObject;
        GameObject prt_obj = gm_obj.transform.Find("ParticleManager").gameObject;
        GameObject aud_obj = gm_obj.transform.Find("AudioManager").gameObject;

        /* コンポーネント取得 */
        gm      = gm_obj.GetComponent<GameManager>();
        score   = gm_obj.GetComponent<ScoreManager>();
        aud     = aud_obj.GetComponent<AudioManager>();
        part    = prt_obj.GetComponent<Pl_Particle>();
        cvsGen  = um_obj.GetComponent<CanvasGenelator>();
    }

    //-------------------------------------------------------------------
	// ゴール時
	private void OnTriggerEnter2D(Collider2D col)
	{
        if (col.tag == "Player") {
            gm.isGoaled = true;             // フラグ

            // UICanvas生成,削除
            if (!gm.isStarting) {
                cvsGen.Inst_Goal();
            }

            Vector3 hitpos = col.ClosestPoint(transform.position);      // プレイヤーが触れた位置をパーティクルの生成位置にする
            part.InstPart(Pl_Particle.PartNames.goal, hitpos);          // パーティクル生成
            aud.PauseAudio(true);                                       // 音声停止

            score.SaveScore();
        }
	}
}

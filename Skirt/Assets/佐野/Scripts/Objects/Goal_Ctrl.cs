using UnityEngine;

/* ★ゴール関連のスクリプトです */
public class Goal_Ctrl : MonoBehaviour
{
    /* コンポーネント取得用 */
    GameManager gm;
    CanvasGenelator cvsGen;
    Pl_Particle part;
    //-------------------------------------------------------------------
    void Start()
    {
        GameObject gm_obj  = GameObject.Find("GameManager");
        GameObject um_obj  = gm_obj.transform.Find("UIManager").gameObject;
        GameObject prt_obj = gm_obj.transform.Find("ParticleManager").gameObject;

        /* コンポーネント取得 */
        gm     = gm_obj.GetComponent<GameManager>();
        part   = prt_obj.GetComponent<Pl_Particle>();
        cvsGen = um_obj.GetComponent<CanvasGenelator>();
    }

    //-------------------------------------------------------------------
	// ゴール時
	private void OnTriggerEnter2D(Collider2D col)
	{
        if (col.tag == "Player") {
            gm.isGoaled = true;                // ゴール

            // ゴール生成
            if (!gm.isStarting) {
                cvsGen.Inst_Goal();
            }

            Vector3 hitpos = col.ClosestPoint(transform.position);
            part.InstPart(Pl_Particle.PartNames.goal, hitpos);
        }
	}
}

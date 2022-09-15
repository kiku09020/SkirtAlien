using UnityEngine;

/* ★カメラに関するスクリプトです */
public class PlayerCamera : MonoBehaviour
{
    /* 値 */
    public float scrnWidthWld;
    public float scrnHeightWld;
    public float scrnYWld;

    public float camSize;

    [Header("カメラ")]
    [SerializeField] float camVal;
    [SerializeField] float camMinSize;
    [SerializeField] float camMaxSize;

    /* オブジェクト */
    GameObject pl_obj;

    /* コンポーネント取得用 */
    Camera cam;
    GameManager gm;
    StageManager stg;

//-------------------------------------------------------------------
    void Start()
    {
        /* コンポーネント取得 */
        pl_obj = GameObject.Find("Player");
        GameObject gm_obj = GameObject.Find("GameManager");

        gm    = gm_obj.GetComponent<GameManager>();
        stg   = gm_obj.GetComponent<StageManager>();
        cam   = GetComponent<Camera>();

        /* 初期化 */
        transform.position = new Vector3(0, stg.stg_length, -10);
        cam.orthographicSize = camMinSize;
    }

//-------------------------------------------------------------------
    void FixedUpdate()
	{
		if(gm.isStarting) {
            StartingCamera();
		}

        if (!gm.isGameOver && !gm.isGoaled) {
            // y座標のみ追従
            transform.position = new Vector3(transform.position.x, pl_obj.transform.position.y, transform.position.z);

            // カメラのワールド座標を取得
            scrnWidthWld    = cam.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x;
            scrnHeightWld   = cam.ScreenToWorldPoint(new Vector2(0, Screen.height)).y;
            scrnYWld        = cam.ViewportToWorldPoint(new Vector2(0, cam.rect.y)).y;

            camSize = cam.orthographicSize;
        }
    }

    // 開始時のカメラ制御
    void StartingCamera()
	{
        // カメラズームアウト
        if(cam.orthographicSize < camMaxSize) {
            cam.orthographicSize += camVal;
        }
    }

//-------------------------------------------------------------------
}

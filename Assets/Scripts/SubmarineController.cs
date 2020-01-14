using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[RequireComponent(typeof(Rigidbody))]
public class SubmarineController : MonoBehaviour
{


    //=====================移動処理関連======================
    private Rigidbody rb;
    public Joystick joystick;
    private const float z = 0.7f; //移動時に向くZ軸（不可変）
	private float x,y; //X軸とy軸
    private const float moveToZ = 0f; //移動時に実際に移動するZ軸の座標
    private Vector3 direction = new Vector3(); //ジョイスティックの方向を見る
    private float movePower = 50.0f; //速度加速度
    private float dushSpeed = 120.0f; //ブーストダッシュ速度
    private float maxSpeed = 100f; //速度上限(Sliderで可変)
    private float minSpeed = 20f; //最低速度
    private float coefficient = 0.98f; //動きを減速させる係数
    public Text text;
    public Slider speedSlider;


    //=====================衝突処理関連======================
    public GameObject Shield;
    private SphereCollider sphereCol;
    public bool isCharging = false; //突進中かどうか

    // Use this for initialization
    void Start()
    {
        //移動処理系
        rb = GetComponent<Rigidbody>();
        speedSlider.maxValue = maxSpeed;
        speedSlider.minValue = minSpeed;
        x = joystick.Horizontal; //X軸を取得
        y = joystick.Vertical; //y軸を取得
        direction = new Vector3(x, y, moveToZ);

        //衝突処理系
        Shield.SetActive(false);
        sphereCol = GetComponent<SphereCollider>();
        sphereCol.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "速度：" + rb.velocity.magnitude;

        //Xとy軸を常に更新
        x = joystick.Horizontal;
        y = joystick.Vertical;

        //Sliderの位置で速度を変える
        maxSpeed = speedSlider.value;


        //常にジョイスティックの方向を見る
        direction = new Vector3(x, y, z);
        transform.rotation = Quaternion.LookRotation (direction);

    }

    void FixedUpdate()
    {
        //移動処理＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝

        //移動

		if(rb.velocity.magnitude < maxSpeed)
		{
			rb.AddForce(direction * movePower);
		}else
		{
			rb.velocity *= coefficient; //最大値まで減速させる
        }

        //操作していない時に徐々に横移動を減速する
        if (x == 0 && y == 0)
        {
            rb.velocity = new Vector3(rb.velocity.x * coefficient, rb.velocity.y * coefficient, rb.velocity.z);
        }

        //ブーストダッシュ
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(direction * dushSpeed, ForceMode.Impulse);
            this.transform.DOLocalRotate(new Vector3(0, 0, -720f), 1.5f, RotateMode.FastBeyond360).SetRelative();
        }
    }

    public void DoBoostDash()
    {
        if (!isCharging) { //突進中であれば再度突進しない
            sphereCol.enabled = true; //あたり判定有効化
            isCharging = true; //突進中
            Shield.SetActive(true); //アニメーション開始
            rb.AddForce(direction * dushSpeed, ForceMode.Impulse);
            this.transform.DOLocalRotate(new Vector3(0, 0, -720f), 1.5f, RotateMode.FastBeyond360).SetRelative().OnComplete(() =>
            {
                Shield.SetActive(false); //アニメーションオブジェクト無効化;
                    isCharging = false; //突進完了
                    sphereCol.enabled = false;//あたり判定無効化
                });
        }
    }
}

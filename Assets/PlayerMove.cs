using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.InputSystem;


[RequireComponent(typeof(Rigidbody))]
public class PlayerMove : MonoBehaviour
{
  [SerializeField]
  private float moveSpeed_ = 1f;
  private Rigidbody rb_;
  private Vector2 moveInput_;
  // カーソル
  [SerializeField]
  private Cursor cursor;

    // 略
    [SerializeField]
    // 腕の情報。Unityエディタ上で直接設定する
    private Arm arm_;
    // InputSystemのFireが押下されているか否か
    private bool isPushFire_;

    void Start()
    {
        // RigidBodyの取得
        rb_ = GetComponent<Rigidbody>();
        // 取得確認
        bool isGet =
          Camera.main.TryGetComponent<Cursor>(out cursor);
        // 取得できていなければ処理を停止
        Assert.IsTrue(isGet, "componentの取得失敗");

        // 略
        isPushFire_ = false;
    }

    // 前ページから
    private void UpdateGunTrigger()
    {
        // armが銃を持っていなければ早期リターン
        if (!arm_.IsGrabGun()) { return; }
        // Fireを押下しているか否かで呼び出す処理を変える
        if (isPushFire_)
        {
            arm_.OnTrigger();
        }
        else
        {
            arm_.OffTrigger();
        }
    }
    // 次ページへ

    public void OnMove(InputValue value)
    {
        moveInput_ = value.Get<Vector2>();
    }


    // 前ページから
    private void TryGetGun(Collider item)
    {
        GunBase gun;
        // GunBaseコンポーネントを持っていなければ
        if (!item.TryGetComponent(out gun)) { return; }
        // 銃の親が居たら早期リターン
        if (!gun.GetIsAlone()) { return; }
        // 銃を取得
        arm_.Grab(gun);
    }

    // 落ちてる銃に触れたら取得しようとする。
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Item")) { return; }
        TryGetGun(other);
    }
    // 次ページへ

    // 前ページから
    public void OnFire(InputValue inputValue)
    {
        isPushFire_ = inputValue.isPressed;
    }



    // Update is called once per frame
    // 前ページから
    private void Update()
    {
        // 早期リターン前に銃に入力状況を伝える
        UpdateGunTrigger();

        // もしCursorのレイがヒットしてなければ早期リターン
        if (!cursor.GetIsHit()) { return; }
        // レイの衝突情報を取得
        RaycastHit raycasthit = cursor.GetRaycastHit();
        // pointが衝突の座標
        Vector3 lookAt = raycasthit.point;
        // 衝突位置は床なので、Playerと同じ目線の高さまで補正
        lookAt.y = transform.position.y;
        // LookAtメソッドは、引数で指定した座標へ向くメソッドだ
        transform.LookAt(lookAt);
    }



    // 前ページから
    void FixedUpdate()
    {
        Vector3 input;
        input = new Vector3(
          // 水平方向の入力
          moveInput_.x,
          0,
          // 垂直方向の入力
          moveInput_.y);
        // 入力が無かったら早期リターン
        if (input.sqrMagnitude == 0) { return; }
        // Rigidbodyの移動機能を用いて移動
        rb_.MovePosition(
          transform.position +
          input * moveSpeed_ * Time.deltaTime
        );
    }
}


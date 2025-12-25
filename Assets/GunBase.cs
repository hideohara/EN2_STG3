using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class GunBase : MonoBehaviour
{
    [SerializeField]
    // 発砲する弾のプレハブ
    protected RayBullet bulletPrefab_;
    [SerializeField]
    // 銃口のトランスフォーム。発射座標
    protected Transform muzzleTransform_;
    [SerializeField]
    // 連射間隔
    protected float fireRate_ = 0;
    // 発射タイマー
    protected float shotTimer_ = 0;
    [SerializeField]
    // 威力
    protected float power_ = 1;
    [SerializeField]
    // 取得前に回転している速度
    private float itemRotateSpeedDeg_ = 90f;

    // 次ページへ

    // Start is called before the first frame update
    void Start()
    {
        
    }


    // 前ページから

    // 発射入力があったら実行。
    public abstract void OnTrigger();
    // 発射入力がなかったら実行。
    public abstract void OffTrigger();


    private void ItemRotate()
    {
        // 地面に落ちている間の回転処理
        transform.RotateAround(
          transform.position,
          Vector3.up,
          itemRotateSpeedDeg_ * Time.deltaTime
        );
    }
    // 次ページへ


    // 前ページから
    public bool GetIsAlone()
    {
        // 親がいなかったら誰にも所持されていない
        return transform.parent == null;
    }
    public virtual void Update()
    {
        // 所持されていなかったら回転する
        if (GetIsAlone()) { ItemRotate(); }
        // タイマーの更新
        if (shotTimer_ <= 0) { return; }
        shotTimer_ -= Time.deltaTime;
    }
}




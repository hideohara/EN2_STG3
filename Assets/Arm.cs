using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Arm : MonoBehaviour
{
    // Š‚µ‚Ä‚¢‚ée
    private GunBase gun_;
    // e‚Ìæ“¾
    public void Grab(GunBase gun)
    {
        // ‚à‚µŠù‚Ée‚ğ‚Á‚Ä‚¢‚½‚ç”jŠü‚·‚é
        if (gun_ != null) { Destroy(gun_.gameObject); }
        // V‚µ‚­e‚ğ’u‚«Š·‚¦‚é
        gun_ = gun;
        // ‘ÎÛ‚Ìe‚Ìe‚ğ©•ª‚Æ‚·‚é
        gun.transform.SetParent(transform);
        // ©•ª‚ÌˆÊ’u‚Éd‚ËA‰ñ“]‚ğ‰Šú‰»‚·‚éB
        gun.transform.localPosition = Vector3.zero;
        gun.transform.localRotation =
          Quaternion.identity;
    }

    // ‘Oƒy[ƒW‚©‚ç
    // e‚ğ‚Á‚Ä‚¢‚é‚©”Û‚©
    public bool IsGrabGun()
    {
        return gun_ != null;
    }

    // ƒgƒŠƒK[‚ğˆø‚¢‚Ä‚¢‚é‚±‚Æ‚ğe‚É“`‚¦‚é
    public void OnTrigger()
    {
        if (!IsGrabGun()) { return; }
        gun_.OnTrigger();
    }
    // ƒgƒŠƒK[‚ğ—£‚µ‚Ä‚¢‚é‚±‚Æ‚ğe‚É“`‚¦‚é
    public void OffTrigger()
    {
        if (!IsGrabGun()) { return; }
        gun_.OffTrigger();
    }
}






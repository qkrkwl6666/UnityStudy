using System.Collections;
using UnityEngine;
using Photon.Pun;

// 총을 구현한다
public class Gun : MonoBehaviourPun, IPunObservable
{
    // 총의 상태를 표현하는데 사용할 타입을 선언한다
    public enum State
    {
        Ready, // 발사 준비됨
        Empty, // 탄창이 빔
        Reloading // 재장전 중
    }

    public State state { get; private set; } // 현재 총의 상태

    public Transform fireTransform; // 총알이 발사될 위치

    public ParticleSystem muzzleFlashEffect; // 총구 화염 효과
    public ParticleSystem shellEjectEffect; // 탄피 배출 효과

    private LineRenderer bulletLineRenderer; // 총알 궤적을 그리기 위한 렌더러

    private AudioSource gunAudioPlayer; // 총 소리 재생기

    private float fireDistance = 50f; // 사정거리

    public int ammoRemain; // 남은 전체 탄약
    public int magAmmo; // 현재 탄창에 남아있는 탄약

    private float lastFireTime; // 총을 마지막으로 발사한 시점

    public GunData gunData;

    private void Awake()
    {
        // 사용할 컴포넌트 들의참조를 가져오기 
        bulletLineRenderer = GetComponent<LineRenderer>();
        gunAudioPlayer = GetComponent<AudioSource>();

        bulletLineRenderer.enabled = false;
        bulletLineRenderer.positionCount = 2;
    }

    private void OnEnable()
    {
        // 총 상태 초기화

        magAmmo = gunData.magCapacity;
        ammoRemain = gunData.startAmmoRemain;
        lastFireTime = 0f;

        state = State.Ready;

        UIManager.instance.UpdateAmmoText(magAmmo, ammoRemain);

    }

    // 발사 시도
    public void Fire()
    {
        if (state == State.Ready && Time.time > lastFireTime + gunData.timeBetFire && magAmmo > 0)
        {
            lastFireTime = Time.time;
            Shot();
        }
    }

    [PunRPC]
    private void ShotOnServer()
    {
        var hitPoint = Vector3.zero;

        Ray ray = new Ray(fireTransform.position, fireTransform.forward);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, fireDistance))
        {
            hitPoint = hitInfo.point;

            //Damage
            var damageable = hitInfo.collider.GetComponent<IDamageable>();

            if (damageable != null)
            {
                damageable.OnDamage(gunData.damage, hitPoint, hitInfo.normal);
            }

        }
        else
        {
            hitPoint = fireTransform.position + fireTransform.forward * fireDistance;
        }

        photonView.RPC("ShotEffectOnClients", RpcTarget.All, hitPoint);
    }

    [PunRPC]
    private void ShotEffectOnClients(Vector3 hitPoint)
    {
        StartCoroutine(ShotEffect(hitPoint));
    }

    // 실제 발사 처리
    private void Shot()
    {
        photonView.RPC("ShotOnServer", RpcTarget.MasterClient);

        --magAmmo;
        UIManager.instance.UpdateAmmoText(magAmmo, ammoRemain);

        if (magAmmo == 0)
        {
            state = State.Empty;
        }
    }

    // 발사 이펙트와 소리를 재생하고 총알 궤적을 그린다
    private IEnumerator ShotEffect(Vector3 hitPosition)
    {
        // 라인 렌더러를 활성화하여 총알 궤적을 그린다
        bulletLineRenderer.enabled = true;

        bulletLineRenderer.SetPosition(0, fireTransform.position);
        bulletLineRenderer.SetPosition(1, hitPosition);

        muzzleFlashEffect.Play();
        shellEjectEffect.Play();

        gunAudioPlayer.PlayOneShot(gunData.shotClip);

        // 0.03초 동안 잠시 처리를 대기
        yield return new WaitForSeconds(0.03f);

        // 라인 렌더러를 비활성화하여 총알 궤적을 지운다
        bulletLineRenderer.enabled = false;
    }

    // 재장전 시도
    //    public int ammoRemain; // 남은 전체 탄약
    //public int magAmmo; // 현재 탄창에 남아있는 탄약
    //
    //
    public bool Reload()
    {
        if (ammoRemain != 0 && state == State.Ready || magAmmo != gunData.startAmmoRemain)
        {
            StartCoroutine(ReloadRoutine());
            return true;
        }
        else
        {
            return false;
        }   
    }

    // 실제 재장전 처리를 진행
    private IEnumerator ReloadRoutine() 
    {
        // 현재 상태를 재장전 중 상태로 전환
        state = State.Reloading;

        gunAudioPlayer.PlayOneShot(gunData.reloadClip);
        // 재장전 소요 시간 만큼 처리를 쉬기
        yield return new WaitForSeconds(gunData.reloadTime);

        ammoRemain += magAmmo;

        if (ammoRemain - gunData.magCapacity > 0)
        {
            ammoRemain -= gunData.magCapacity;
            magAmmo = gunData.magCapacity;
        }
        else
        {
            magAmmo = ammoRemain;
            ammoRemain = 0;
        }

        UIManager.instance.UpdateAmmoText(magAmmo, ammoRemain);

        // 총의 현재 상태를 발사 준비된 상태로 변경
        state = State.Ready;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(ammoRemain);
            stream.SendNext(magAmmo);
            stream.SendNext(state);
        }
        else
        {
            ammoRemain = (int)stream.ReceiveNext();
            magAmmo = (int)stream.ReceiveNext();
            state = (State)stream.ReceiveNext();
        }
    }
}
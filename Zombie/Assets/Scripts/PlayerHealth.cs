using Photon.Pun;
using Unity.Services.Analytics;
using UnityEngine;
using UnityEngine.UI; // UI 관련 코드

// 플레이어 캐릭터의 생명체로서의 동작을 담당
public class PlayerHealth : LivingEntity {
    public Slider healthSlider; // 체력을 표시할 UI 슬라이더

    public AudioClip deathClip; // 사망 소리
    public AudioClip hitClip; // 피격 소리
    public AudioClip itemPickupClip; // 아이템 습득 소리

    private AudioSource playerAudioPlayer; // 플레이어 소리 재생기
    private Animator playerAnimator; // 플레이어의 애니메이터

    private PlayerMovement playerMovement; // 플레이어 움직임 컴포넌트
    private PlayerShooter playerShooter; // 플레이어 슈터 컴포넌트

    public event System.Action OnRespawn;

    private void Awake() 
    {
        // 사용할 컴포넌트를 가져오기
        healthSlider = GetComponentInChildren<Slider>();
        playerAudioPlayer = GetComponent<AudioSource>();
        playerAnimator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        playerShooter = GetComponent<PlayerShooter>();
    }

    protected override void OnEnable() 
    {
        // LivingEntity의 OnEnable() 실행 (상태 초기화)
        base.OnEnable();
        healthSlider.gameObject.SetActive(true);
        healthSlider.minValue = 0;
        healthSlider.maxValue = startingHealth;
        healthSlider.value = health;

        playerMovement.enabled = true;
        playerShooter.enabled = true;
    }

    [PunRPC]
    // 체력 회복
    public override void RestoreHealth(float newHealth) 
    {
        // LivingEntity의 RestoreHealth() 실행 (체력 증가)
        base.RestoreHealth(newHealth);

        healthSlider.value = health;
    }

    [PunRPC]
    // 데미지 처리
    public override void OnDamage(float damage, Vector3 hitPoint, Vector3 hitDirection) 
    {
        // LivingEntity의 OnDamage() 실행(데미지 적용)

        if (dead) return;

        Debug.Log("OnDamage");

        base.OnDamage(damage, hitPoint, hitDirection);
        healthSlider.value = health;

        playerAudioPlayer.PlayOneShot(hitClip);
    }


    // 사망 처리
    public override void Die() 
    {
        // LivingEntity의 Die() 실행(사망 적용)
        //Debug.Log("플레이어 죽음");
        base.Die();

        healthSlider.gameObject.SetActive(false);
        playerAudioPlayer.PlayOneShot(deathClip);

        playerAnimator.SetTrigger("Die");

        playerMovement.enabled = false;
        playerShooter.enabled = false;

        //Invoke("Respawn", 5f);
    }

    private void OnTriggerEnter(Collider other) 
    {
        // 아이템과 충돌한 경우 해당 아이템을 사용하는 처리

        if (other.CompareTag("Item"))
        {
            if (!photonView.IsMine) return;

            var item = other.GetComponent<IItem>();

            if (item != null)
            {
                photonView.RPC("ServerOnItem", RpcTarget.MasterClient, photonView.ViewID, other.GetComponent<PhotonView>().ViewID);
                playerAudioPlayer.PlayOneShot(itemPickupClip);
            }
        }
       
    }

    [PunRPC]
    private void ServerOnItem(int playerViewId , int itemViewId)
    {
        if (!PhotonNetwork.IsMasterClient) return;

        var player = PhotonView.Find(playerViewId).gameObject;
        var item = PhotonView.Find(itemViewId).gameObject.GetComponent<IItem>();

        if (player != null && item != null)
        {
            photonView.RPC("UseItem", RpcTarget.All, playerViewId, itemViewId);
        }
    }

    [PunRPC]
    private void UseItem(int playerViewId , int itemViewId)
    {
        var player = PhotonView.Find(playerViewId).gameObject;
        var item = PhotonView.Find(itemViewId).gameObject.GetComponent<IItem>();

        item.Use(player);
        
        if(PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.Destroy(PhotonView.Find(itemViewId).gameObject);
        }

    }

    public void Respawn()
    {
        if(OnRespawn != null)
        {
            OnRespawn();
        }

        if (photonView.IsMine)
        {
            // 위치 수정
            photonView.RPC("OnRespawn", RpcTarget.MasterClient, PhotonNetwork.LocalPlayer);
        }

        gameObject.SetActive(false);
        gameObject.SetActive(true);
    }
}
using Cinemachine;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

// 점수와 게임 오버 여부를 관리하는 게임 매니저
public class GameManager : MonoBehaviourPunCallbacks, IPunObservable
{
    public static Transform playerTransform;

    public CinemachineVirtualCamera virtualCamera;

    // 싱글톤 접근용 프로퍼티
    public static GameManager instance
    {
        get
        {
            // 만약 싱글톤 변수에 아직 오브젝트가 할당되지 않았다면
            if (m_instance == null)
            {
                // 씬에서 GameManager 오브젝트를 찾아 할당
                m_instance = FindObjectOfType<GameManager>();
            }

            // 싱글톤 오브젝트를 반환
            return m_instance;
        }
    }

    private static GameManager m_instance; // 싱글톤이 할당될 static 변수

    private int score = 0; // 현재 게임 점수
    public bool isGameover { get; private set; } // 게임 오버 상태

    private List<Player> diePlayers = new List<Player>();

    [PunRPC]
    private void OnDie(Player player)
    {
        if (diePlayers.Contains(player)) return;

        diePlayers.Add(player);

        if(diePlayers.Count >= PhotonNetwork.PlayerList.Length)
        {
            photonView.RPC("EndGame", RpcTarget.All);
        }
    }

    [PunRPC]
    private void OnRespawn(Player player)
    {
        diePlayers.Remove(player);

        photonView.RPC("OnDie", RpcTarget.MasterClient, PhotonNetwork.LocalPlayer);
    }

    private void Awake() 
    {
        // 씬에 싱글톤 오브젝트가 된 다른 GameManager 오브젝트가 있다면
        if (instance != this)
        {
            // 자신을 파괴
            Destroy(gameObject);
        }
    }

    private void Start() 
    {
        // 플레이어 캐릭터의 사망 이벤트 발생시 게임 오버
        //FindObjectOfType<PlayerHealth>().onDeath += EndGame;

        Vector3 randomPos = transform.position + Random.insideUnitSphere * 10f;

        NavMeshHit hit;

        if(NavMesh.SamplePosition(randomPos, out hit, 100f, NavMesh.AllAreas))
        {
            Debug.Log(hit.position);
        }
        else
        {
            Debug.Log("Nav Mesh Not Found!");
        }

        var go = PhotonNetwork.Instantiate("Woman", hit.position, Quaternion.identity);

        playerTransform = go.transform;

        virtualCamera.Follow = playerTransform;
        virtualCamera.LookAt = playerTransform;

        //FindObjectOfType<PlayerHealth>().onDeath += EndGame;

        var playerHealth = go.GetComponent<PlayerHealth>();
        playerHealth.onDeath += OnPlayerDeath;
        playerHealth.OnRespawn += () => photonView.RPC("OnRespawn", RpcTarget.MasterClient, PhotonNetwork.LocalPlayer); ;
        //playerHealth.onDeath += () => photonView.RPC("OnDie", RpcTarget.MasterClient, PhotonNetwork.LocalPlayer); ;

    }

    private void OnPlayerDeath()
    {
        // 이 메소드 내에서 RPC 호출을 수행
        photonView.RPC("OnDie", RpcTarget.MasterClient, PhotonNetwork.LocalPlayer);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PhotonNetwork.LeaveRoom();
        }
    }

    // 점수를 추가하고 UI 갱신
    public void AddScore(int newScore) 
    {
        // 게임 오버가 아닌 상태에서만 점수 증가 가능
        if (!isGameover)
        {
            // 점수 추가
            score += newScore;
            // 점수 UI 텍스트 갱신
            UIManager.instance.UpdateScoreText(score);
        }
    }

    [PunRPC]
    // 게임 오버 처리
    public void EndGame() 
    {
        // 게임 오버 상태를 참으로 변경
        isGameover = true;
        // 게임 오버 UI를 활성화
        UIManager.instance.SetActiveGameoverUI(true);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(score);
        }   
        else
        {
            score = (int)stream.ReceiveNext();
            UIManager.instance.UpdateScoreText(score);
        }
    }

    // 방에서 나갔을 때
    public override void OnLeftRoom()
    {
        SceneManager.LoadScene("Lobby");
    }
}
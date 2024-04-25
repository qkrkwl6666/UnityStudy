using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine.UI;

public class PaneelLobby : MonoBehaviourPunCallbacks
{
    private string gameVersion = "1.0";

    public TMPro.TextMeshProUGUI message;
    public Button button;

    // Start is called before the first frame update
    private void Start()
    {
        if(!PhotonNetwork.IsConnected)
        {
            button.interactable = false;
            PhotonNetwork.GameVersion = gameVersion;
            PhotonNetwork.ConnectUsingSettings();
            message.text = "Connecting to Master Server";
        }
        else
        {
            button.interactable = false;
        }

    }

    // 연결 시 호출
    public override void OnConnectedToMaster()
    {
        message.text = "Online : Connected to Master Server";
        button.interactable = true;
    }

    // 연결 끊기면 호출
    public override void OnDisconnected(DisconnectCause cause)
    {
        message.text = "Offline : Disconnected from Master Server";
        button.interactable = false;
    }

    public void OnJoin()
    {
        Debug.Log("OnJoin");

        if(PhotonNetwork.IsConnected)
        {
            PhotonNetwork.JoinRandomRoom();
            message.text = "Joining Room...";
            Debug.Log(message.text);

            button.interactable = false;
        }
        else
        {
            PhotonNetwork.ConnectUsingSettings();
            Debug.Log(message.text);
        }
    }

    // 방 입장 성공
    public override void OnJoinedRoom()
    {
        Debug.Log("OnJoinedRoom");
        message.text = "Joined Room";
        PhotonNetwork.LoadLevel("Main");
    }

    public override void OnJoinRoomFailed(short returnCode, string error)
    {
        Debug.Log(error);

        PhotonNetwork.CreateRoom(null, new RoomOptions() { MaxPlayers = 4 });
        this.message.text = error;
    }

    // 방 접속 실패
    public override void OnJoinRandomFailed(short returnCode, string error)
    {
        //base.OnJoinRandomFailed(returnCode, message);

        Debug.Log(error);
        PhotonNetwork.CreateRoom(null, new RoomOptions() { MaxPlayers = 4 });
        this.message.text = error;
    }

    public override void OnCreateRoomFailed(short returnCode, string error)
    {
        Debug.Log(error);
    }
}

using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private Text TextName;
    private void Start() {
        TextName.text = PlayerPrefs.GetString("name");
        PhotonNetwork.NickName = TextName.text;
        Debug.Log("Welcome");
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.GameVersion = "1";
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected");
    }

    public void CreateRoom(){
        PhotonNetwork.CreateRoom(null, new Photon.Realtime.RoomOptions{MaxPlayers = 2});
        SaveName();  
    }

    public void JoinRoom(){
        PhotonNetwork.JoinRandomRoom();
        SaveName();  
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Main");
    }

    private void SaveName(){
        PlayerPrefs.SetString("name", TextName.text);
    }

}

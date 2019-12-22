using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

public class GameFacade : MonoBehaviour
{

    private static GameFacade _instance;
    public static GameFacade Instance { get { return _instance; } }

    private UIManager uiMng;
    private AudioManager audioMng;
    private PlayerManager playerMng;
    private CameraManager cameraMng;
    private RequestManager requestMng;
    private ClientManager clientMng;
    private bool isEnterPlaying = false;

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        _instance = this;
    }
    void Start()
    {
        InitManager();
    }
    private void Update()
    {
        UpdateManager();
        if (isEnterPlaying)
        {
            EnterPlaying();
            isEnterPlaying = false;
        }
    }
    private void OnDestroy()
    {
        DestroyManager();
    }
    private void InitManager()
    {
        uiMng = new UIManager(this);
        audioMng = new AudioManager(this);
        playerMng = new PlayerManager(this);
        cameraMng = new CameraManager(this);
        requestMng = new RequestManager(this);
        clientMng = new ClientManager(this);

        uiMng.OnInit();
        audioMng.OnInit();
        playerMng.OnInit();
        cameraMng.OnInit();
        requestMng.OnInit();
        clientMng.OnInit();
    }
    private void UpdateManager()
    {
        uiMng.Update();
        audioMng.Update();
        playerMng.Update();
        cameraMng.Update();
        requestMng.Update();
        clientMng.Update();
    }
    private void DestroyManager()
    {
        uiMng.OnDestroy();
        audioMng.OnDestroy();
        playerMng.OnDestroy();
        cameraMng.OnDestroy();
        requestMng.OnDestroy();
        clientMng.OnDestroy();
    }

    public void AddRequest(ActionCode actionCode, BaseRequest request)
    {
        requestMng.AddRequest(actionCode, request);
    }
    public void RemoveRequest(ActionCode actionCode)
    {
        requestMng.RemoveRequest(actionCode);
    }
    public void HandleReponse(ActionCode actionCode,string date)
    {
        requestMng.HandlerReponse(actionCode, date);
    }
    public void ShowMessage(string msg)
    {
        uiMng.ShowMessage(msg);
    }
    public void SendRequest(RequestCode requestCode, ActionCode actionCode, string data)
    {
        clientMng.SendRequest(requestCode, actionCode, data);
       
    }
    public void PlayBgSound(string soundName)
    {
        audioMng.PlayBgSound(soundName);
    }
    public void PlayNormalSound(string soundName)
    {
        audioMng.PlayNormalSound(soundName);
    }
    public void SetUserData(UserData ud)
    {
        playerMng.UserData = ud;
    }
    public UserData GetUserData()
    {
        return playerMng.UserData;
    }
    public void SetCurrentRoleType(RoleType rt)
    {
        playerMng.SetCurrentRoleType(rt);
    }
    public GameObject GetCurrentRoleGameObject()
    {
        return playerMng.GetCurrentRoleGameObject();
    }
    public  void EnterPlayingSync()
    {
        isEnterPlaying = true;
    }
    private void EnterPlaying()
    {
        playerMng.SpawnRoles();
        cameraMng.FollowTarget();
    }
    public void StartPlaying()
    {
        playerMng.AddControlScript();
        playerMng.CreateSyncRequest();
    }
    public void SendAttack(int damage)
    {
        playerMng.SendAttack(damage);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Common;

public class LoginPanel : BasePanel {

    private Button closeButton;
    private InputField usernameIF;
    private InputField passwordIF;
    private LoginRequest loginRequest;
    //private Button loginButton;
    //private Button registerButton;
    private void Start()
    {
        loginRequest = GetComponent<LoginRequest>();
        usernameIF = transform.Find("UsernameLabel/UsernameInput").GetComponent<InputField>();
        passwordIF = transform.Find("PasswordLabel/PasswordInput").GetComponent<InputField>();
        closeButton = transform.Find("CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(OnCloseClick);
        transform.Find("LoginButton").GetComponent<Button>().onClick.AddListener(OnLoginClick);
        transform.Find("ReginsterButton").GetComponent<Button>().onClick.AddListener(OnReginsterClick);
    }
    public override void OnEnter()
    {
        base.OnEnter();
        EnterAnim();
    }
    private void OnCloseClick()
    {
        PlayClickSound();
        uiMng.PopPanel();
    }
    public override void OnPause()
    {
        HideAnim();
    }
    public override void OnResume()
    {
        EnterAnim();
    }
    private void OnLoginClick()
    {
        PlayClickSound();
        string msg = "";
        if (string.IsNullOrEmpty(usernameIF.text))
        {
            msg += "用户名不能为空";
        }
        if (string.IsNullOrEmpty(passwordIF.text))
        {
            msg += "密码不能为空";
        }
        if (msg != "")
        {
            uiMng.ShowMessage(msg);return;
        }
        loginRequest.SendRequest(usernameIF.text, passwordIF.text);

    }
    private void OnReginsterClick()
    {
        PlayClickSound();
        uiMng.PushPanel(UIPanelType.Register);
    }
    public void OnLoginResponse(ReturnCode returnCode)
    {
        if (returnCode == ReturnCode.Success)
        {
            uiMng.PushPanelSync(UIPanelType.RoomList);
        }
        else
        {
            uiMng.ShowMessageSync("用户名或密码错误，请重新输入！");
        }
    }
    public override void OnExit()
    {
        base.OnExit();
        HideAnim();
    }
    private void EnterAnim()
    {
        gameObject.SetActive(true);
        transform.localScale = Vector3.zero;
        transform.DOScale(1, 0.4f);
        transform.localPosition = new Vector3(800, 0, 0);
        transform.DOLocalMove(Vector3.zero, 0.4f);
    }
    private void HideAnim()
    {
        transform.DOScale(0, 0.4f);
        Tweener tweener = transform.DOLocalMove(new Vector3(800, 0, 0), 0.4f);
        tweener.OnComplete(() => gameObject.SetActive(false));
    }
    
}

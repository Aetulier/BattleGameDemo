using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Common;

public class GamePanel : BasePanel {

    private Text timer;
    private int time = -1;

    private Button successBtn;
    private Button failBtn;
    private void Start()
    {
        timer = transform.Find("Timer").GetComponent<Text>();
        timer.gameObject.SetActive(false);
        successBtn = transform.Find("SuccessButton").GetComponent<Button>();
        successBtn.gameObject.SetActive(false);
        failBtn = transform.Find("FailButton").GetComponent<Button>();
        failBtn.gameObject.SetActive(false);
    }
    private void Update()
    {
        if (time > -1)
        {
            ShowTime(time);
            time = -1;
        }
    }
    public void ShowTimeSync(int time)
    {
        this.time = time;
    }
    public void ShowTime(int time)
    {
        timer.gameObject.SetActive(true);
        timer.text = time.ToString();
        timer.transform.localScale = Vector3.one;
        Color tempColor = timer.color;
        tempColor.a = 1;
        timer.color = tempColor;
        timer.transform.DOScale(2, 0.3f).SetDelay(0.3f);
        timer.DOFade(0, 0.3f).SetDelay(0.3f).OnComplete(()=>timer.gameObject.SetActive(false));
        facade.PlayNormalSound(AudioManager.Sound_Alert);
    }
    public  void OnGameOverResponse(ReturnCode returnCode)
    {
        Button tempBtn=null;
        switch (returnCode)
        {
            case ReturnCode.Success:
                tempBtn = successBtn;
                break;
            case ReturnCode.Fail:
                tempBtn = failBtn;
                break;
        }
        tempBtn.gameObject.SetActive(true);
        tempBtn.transform.localScale = Vector3.zero;
        tempBtn.transform.DOScale(1, 0.5f);
    }
}

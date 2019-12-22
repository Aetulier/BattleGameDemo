using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraManager : BaseManager {

    private GameObject cameraGO;
    private Animator cameraAnim;
    private FollowTarget followTarget;
    private Vector3 originalPosition;
    private Vector3 originalRotation;
    public CameraManager(GameFacade facade) : base(facade) { }

    public override void OnInit()
    {
        cameraGO = Camera.main.gameObject;
        cameraAnim = cameraGO.GetComponent<Animator>();
        followTarget = cameraGO.GetComponent<FollowTarget>();
    }

    //public override void Update()
    //{
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        FollowTarget((GameObject.Find("Hunter_BLUEText")as GameObject).transform);
    //    }
    //    if (Input.GetMouseButtonDown(1))
    //    {
    //        WalkthroughScene();
    //    }
    //}
    public void FollowTarget()
    {
        followTarget.target = facade.GetCurrentRoleGameObject().transform;
        cameraAnim.enabled = false;
        originalPosition = cameraGO.transform.position;
        originalRotation = cameraGO.transform.eulerAngles;
        Quaternion targetQuaternion = Quaternion.LookRotation(followTarget.target.position - cameraGO.transform.position);
        cameraGO.transform.DORotateQuaternion(targetQuaternion, 1f).OnComplete(delegate ()
        {
            followTarget.enabled = true;
        });
            
    }
    public  void WalkthroughScene()
    {
        followTarget.enabled = false;
        cameraGO.transform.DOMove(originalPosition, 1f);
        cameraGO.transform.DORotate(originalRotation, 1f).OnComplete(delegate()
        {
            cameraAnim.enabled = true;            
        });
    }
}



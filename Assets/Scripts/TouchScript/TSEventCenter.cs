using System;
using System.Collections;
using System.Collections.Generic;
using Common;
using TouchScript.Gestures;
using UnityEngine;
using QFramework;
using EventType = Common.EventType;

public class TSEventCenter : MonoBehaviour
{
    private MetaGesture mMetaGesture;

    void  Awake()
    {
        mMetaGesture = GetComponent<MetaGesture>();
    }

    void Start()
    {
        mMetaGesture.PointerPressed += OnPointerPressed;
        mMetaGesture.PointerUpdated += OnPointerUpdated;
        mMetaGesture.PointerReleased += OnPointerReleased;
    }

    void OnDestroy()
    {
        mMetaGesture.PointerPressed -= OnPointerPressed;
        mMetaGesture.PointerUpdated -= OnPointerUpdated;
        mMetaGesture.PointerReleased -= OnPointerReleased;
    }

    private void OnPointerPressed(object sender, MetaGestureEventArgs e)
    {
        var pointer = e.Pointer;
      
        //映射的画布大小不一致，所以要做下处理
        var offsetPos = new Vector2(pointer.Position.x * (2800f / 3840f), pointer.Position.y);
        CameraUtils.Instance.IsTouchUIBtn(offsetPos);
        //Debug.Log(string.Format("PointerID:{0} PressedPoint{1}: ", pointer.Id, pointer.Position));
        EventCenter.Broadcast(EventType.PointerPressed, offsetPos);
    }

    private void OnPointerUpdated(object sender, MetaGestureEventArgs e)
    {

    }

    private void OnPointerReleased(object sender, MetaGestureEventArgs e)
    {

    }

}

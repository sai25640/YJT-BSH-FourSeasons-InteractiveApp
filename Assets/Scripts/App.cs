﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;
using QFramework.Example;
using UnityEngine.UI;

namespace FourSeasons
{

    public class App : MonoBehaviour
    {
        private AppConfig mConfig;
        private VideoCanvas mVideoCanvas;
        void Awake()
        {
            ResMgr.Init();
        }

        void Start()
        {
            mConfig = DataManager.Instance.Init();

            //是否开启无边框模式
            if (mConfig.IsNoBorderMode)
            {
                Screen.SetResolution(mConfig.ScreenWidth, mConfig.ScreenHight, FullScreenMode.Windowed);
                StartCoroutine(WindowsUtil.Setposition(0, 0, mConfig.ScreenWidth, mConfig.ScreenHight));
            }
            else
            {
                Screen.SetResolution(mConfig.ScreenWidth, mConfig.ScreenHight, mConfig.ScreenMode);
            }

            UIMgr.SetResolution(mConfig.ScreenWidth, mConfig.ScreenHight, 0);

            InitVideoCanvas();

            //InitKinect();

            GeometricRectificationMode();

            UIMgr.OpenPanel<UIMainPanel>();

#if !UNITY_EDITOR
              Cursor.visible = mConfig.IsCursorVisible;
#endif
        }

        void InitVideoCanvas()
        {
            var go = GameObject.Find("VideoCanvas");
            if (go.IsNotNull())
            {
                go.DestroySelfGracefully();
            }

            mVideoCanvas = ResLoader.Allocate().LoadSync<GameObject>("VideoCanvas").Instantiate()
                                         .GetComponent<VideoCanvas>();

            CameraUtils.Instance.SetCustomGraphicRaycaster(mVideoCanvas.GetComponent<GraphicRaycaster>());
        }

        void InitKinect()
        {
            if (mConfig.IsKinectOn)
            {
                ResLoader.Allocate().LoadSync<GameObject>("KinectController").Instantiate();
            }
        }

        /// <summary>
        /// 几何校正模式
        /// </summary>
        void GeometricRectificationMode()
        {
            if (!mConfig.IsGeometricRectificationMode) return;

            var canvasScaler = ResLoader.Allocate().LoadSync<GameObject>("Canvas").Instantiate()
                .GetComponent<CanvasScaler>();

            var canvas = canvasScaler.GetComponent<Canvas>();

           mVideoCanvas.Camera.targetTexture =
                new RenderTexture(2800, 800, 99, RenderTextureFormat.ARGB32);

            canvas.transform.Find("RawImage").GetComponent<UISkewImage>().material.mainTexture =
                mVideoCanvas.Camera.targetTexture;

            UIMgr.Camera.targetTexture =
                new RenderTexture(mConfig.ScreenWidth, mConfig.ScreenHight, 99, RenderTextureFormat.ARGB32);

            canvas.transform.Find("RawImage/UIRawImage").GetComponent<RawImage>().material.mainTexture =
                UIMgr.Camera.targetTexture;
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                //断开局域网通讯
                UdpManager.Instance.Disconnect();

                Application.Quit();
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                var msg = new UdpMessage("ID1", "HelloWorld");
                UdpManager.Instance.SendMessage(msg.ToJson());
            }
        }
    }
}

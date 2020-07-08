using System;
using System.Collections;
using System.Collections.Generic;
using QFramework;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using Random = UnityEngine.Random;

namespace FourSeasons
{

    public class SummerEffect : MonoBehaviour
    {
        private Button mButton;
        private RawImage mRawImage1;
        private VideoPlayer mVideoPlayer1;
        private RawImage mRawImage2;
        private VideoPlayer mVideoPlayer2;
        void Awake()
        {
            mButton = GetComponent<Button>();
            mRawImage1 = transform.GetChild(0).GetComponent<RawImage>();
            mVideoPlayer1 = transform.GetChild(0).GetComponent<VideoPlayer>();
            mRawImage2 = transform.GetChild(1).GetComponent<RawImage>();
            mVideoPlayer2 = transform.GetChild(1).GetComponent<VideoPlayer>();
        }

        void Start()
        {
            mButton.onClick.AddListener(OnBtnClick);
            mVideoPlayer1.targetTexture = RenderTexture.GetTemporary(300, 600, 99);
            mRawImage1.texture = mVideoPlayer1.targetTexture;
            mVideoPlayer2.targetTexture = RenderTexture.GetTemporary(800, 3000, 99);
            mRawImage2.texture = mVideoPlayer2.targetTexture;
            mVideoPlayer2.loopPointReached += OnLoopPointReached;
            mRawImage2.enabled = false;
        }

        void OnDestroy()
        {
            mVideoPlayer2.loopPointReached -= OnLoopPointReached;
            mButton.onClick.RemoveListener(OnBtnClick);
            RenderTexture.ReleaseTemporary(mVideoPlayer1.targetTexture);
            RenderTexture.ReleaseTemporary(mVideoPlayer2.targetTexture);
        }

        public void Init(Transform parent)
        {    
            transform.parent = parent;
            transform.localPosition = new Vector3(Random.Range(-1300, 1300), Random.Range(-350, 350), 0);
            transform.localEulerAngles = Vector3.zero;
            transform.localScale = new Vector3(1, 1, -1);

            this.Delay(Random.Range(0,2f),PlayEffect1);    
        }

        private void OnBtnClick()
        {
            Debug.Log("OnBtnClick");

            PlayEffect2();
        }

        void PlayEffect1()
        {
            if (mVideoPlayer1.targetTexture != null && mRawImage1.texture != null)
            {
                mRawImage1.enabled = true;
                mVideoPlayer1.isLooping = true;
                mVideoPlayer1.Play();
            }
        }

        void PlayEffect2()
        {
            if (mVideoPlayer2.targetTexture != null && mRawImage2.texture != null)
            {
                mRawImage1.enabled = false;
                mVideoPlayer1.Stop();

                mRawImage2.enabled = true;
                mVideoPlayer2.isLooping = false;
                mVideoPlayer2.Play();
            }
        }
        private void OnLoopPointReached(VideoPlayer source)
        {
            //通知投影端播放动画
            var msg = new UdpMessage(MessageDefine.PlaySummerEffect);
            UdpManager.Instance.SendMessage(msg.ToJson());

            Destroy(this.gameObject);
        }
    }
}

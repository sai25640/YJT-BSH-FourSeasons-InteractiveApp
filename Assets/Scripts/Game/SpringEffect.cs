using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using Random = UnityEngine.Random;

namespace FourSeasons
{
    public class SpringEffect : MonoBehaviour
    {
        private Button mButton;
        private Image mImage;
        private RawImage mRawImage;
        private VideoPlayer mVideoPlayer;

        void Awake()
        {
            mButton = GetComponent<Button>();
            mImage = GetComponent<Image>();
            mRawImage = GetComponentInChildren<RawImage>();
            mVideoPlayer = GetComponentInChildren<VideoPlayer>();
        }

        void Start()
        {
            mButton.onClick.AddListener(OnBtnClick);
            mVideoPlayer.targetTexture = RenderTexture.GetTemporary(800, 1600, 99);
            mRawImage.texture = mVideoPlayer.targetTexture;
            mVideoPlayer.loopPointReached += OnLoopPointReached;
            mRawImage.enabled = false;
        }

        void OnDestroy()
        {
            mVideoPlayer.loopPointReached -= OnLoopPointReached;
            mButton.onClick.RemoveListener(OnBtnClick);
            RenderTexture.ReleaseTemporary(mVideoPlayer.targetTexture);
        }

        public void Init(Transform parent,string name)
        {
            //this.Awake();
            //this.Start();
            gameObject.name = name;
            transform.parent = parent;
            transform.localPosition = new Vector3(Random.Range(-1300, 1300), Random.Range(-350, 350), 0);
            transform.localEulerAngles = Vector3.zero;
            transform.localScale = new Vector3(1, 1, -1);

        }

        private void OnBtnClick()
        {
            Debug.Log("OnBtnClick");

            mImage.enabled = false;
            mButton.interactable = false;

            PlayEffect();

            //通知投影端播放动画
            var msg = new UdpMessage(MessageDefine.PlaySpringEffect, name);
            UdpManager.Instance.SendMessage(msg.ToJson());
        }


        void PlayEffect()
        {
            if (mVideoPlayer.targetTexture != null && mRawImage.texture != null)
            {
                mRawImage.enabled = true;
                mVideoPlayer.isLooping = false;
                mVideoPlayer.Play();
            }
        }

        private void OnLoopPointReached(VideoPlayer source)
        {
            ////通知投影端播放动画
            //var msg = new UdpMessage(MessageDefine.PlaySpringEffect, name);
            //UdpManager.Instance.SendMessage(msg.ToJson());

            Destroy(this.gameObject);
        }
    }
}

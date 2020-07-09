using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace FourSeasons
{
    public class FallEffect : MonoBehaviour
    {
        private Button mButton;
        private RawImage mRawImage;
        private VideoPlayer mVideoPlayer;

        void Awake()
        {
            mButton = GetComponent<Button>();
            mRawImage = transform.GetChild(0).GetComponent<RawImage>();
            mVideoPlayer = transform.GetChild(0).GetComponent<VideoPlayer>();
        }

        void Start()
        {
            mButton.onClick.AddListener(OnBtnClick);
            mVideoPlayer.targetTexture = RenderTexture.GetTemporary(1500, 3000, 99);
            mRawImage.texture = mVideoPlayer.targetTexture;
            mVideoPlayer.loopPointReached += OnLoopPointReached;

            //Init(transform.parent, "FallEffect");
        }

        void OnDestroy()
        {
            mVideoPlayer.loopPointReached -= OnLoopPointReached;
            mButton.onClick.RemoveListener(OnBtnClick);
            RenderTexture.ReleaseTemporary(mVideoPlayer.targetTexture);

        }

        public void Init(Transform parent, string name)
        {
            gameObject.name = name;
            transform.parent = parent;
            transform.localPosition = new Vector3(Random.Range(-1300, 1300), Random.Range(-300, 300), 0);
            transform.localEulerAngles = Vector3.zero;
            transform.localScale = new Vector3(1, 1, -1);
            //transform.GetChild(0).localScale = Vector3.one * 0.5f;

            mVideoPlayer.Play();
            mVideoPlayer.frame = 1;
            //mVideoPlayer.isLooping = true;
            this.Delay(1f, (() => mVideoPlayer.Pause()));
        }

        private void OnBtnClick()
        {
            Debug.Log("OnBtnClick");

            mButton.interactable = false;

            PlayEffect();

            //通知投影端播放动画
            var msg = new UdpMessage(MessageDefine.PlayFallEffect, name);
            UdpManager.Instance.SendMessage(msg.ToJson());

            AudioManager.PlaySound("叶子");
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
            Destroy(this.gameObject);
        }
    }
}

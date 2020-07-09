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
        private Image mImage;
        private RawImage mRawImage;
        private VideoPlayer mVideoPlayer;

        void Awake()
        {
            mButton = GetComponent<Button>();
            mImage = transform.GetChild(0).GetComponent<Image>();
            mRawImage = transform.GetChild(1).GetComponent<RawImage>();
            mVideoPlayer = transform.GetChild(1).GetComponent<VideoPlayer>();
        }

        void Start()
        {
            mButton.onClick.AddListener(OnBtnClick);
            mVideoPlayer.targetTexture = RenderTexture.GetTemporary(1500, 3000, 99);
            mRawImage.texture = mVideoPlayer.targetTexture;
            mVideoPlayer.loopPointReached += OnLoopPointReached;
            mRawImage.enabled = false;

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
            transform.localPosition = new Vector3(Random.Range(-1300, 1300), Random.Range(-250, 250), 0);
            transform.localEulerAngles = Vector3.zero;
            transform.localScale = new Vector3(1, 1, -1);
        }

        private void OnBtnClick()
        {
            Debug.Log("OnBtnClick");

            this.Delay(0.5f,()=>mImage.enabled = false);
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

using UnityEngine;
using QFramework;
using UnityEngine.Video;
using System;

namespace FourSeasons
{
	public partial class TableVideo : ViewController
	{
	    private VideoPlayer mVideoPlayer; 
	    private VideoCanvas mVideoCanvas;
        public bool IsPlaying { get; set; }

        void Awake()
	    {
	        mVideoPlayer = GetComponent<VideoPlayer>();
	        mVideoCanvas = transform.parent.GetComponent<VideoCanvas>();
        }

		void Start()
		{
            // Code Here
		    mVideoPlayer.loopPointReached += OnLoopPointReached;
		    //Debug.Log(mVideoPlayer.frameCount);
		    Reset();
		}

        private void OnLoopPointReached(VideoPlayer source)
        {
            Debug.Log(string.Format("Video:{0} PlayEnd", source.name));      
        }

	    public void Play()
	    {
	        mVideoPlayer.SetDirectAudioMute(0, false);
            mVideoPlayer.Play();
	        IsPlaying = true;
	    }

	    public void Reset()
	    {
	        mVideoPlayer.Play();
            mVideoPlayer.SetDirectAudioMute(0,true);
            mVideoPlayer.frame = 1;
            this.Delay(0.3f, (() => mVideoPlayer.Pause()));
	        IsPlaying = false;
        }

	    void Update()
	    {
	        if (mVideoPlayer.frame >= 280 && IsPlaying && !mVideoCanvas.IsProjectorVideoPlay )
	        {
                //通知投影端开始播放视频
                var msg = new UdpMessage(MessageDefine.TableVideoEnd);
                UdpManager.Instance.SendMessage(msg.ToJson());
                mVideoCanvas.IsProjectorVideoPlay = true;
	        }
	    }
        
    }
}

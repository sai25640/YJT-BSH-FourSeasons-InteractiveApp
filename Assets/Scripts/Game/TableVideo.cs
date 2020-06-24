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
        public bool IsPlaying
        {
            get => mVideoPlayer.isPlaying;
        }
        void Awake()
	    {
	        mVideoPlayer = GetComponent<VideoPlayer>();
	        mVideoCanvas = transform.parent.GetComponent<VideoCanvas>();
        }

		void Start()
		{
            // Code Here
		    mVideoPlayer.loopPointReached += OnLoopPointReached;
            mVideoPlayer.frame = 1;
		}

        private void OnLoopPointReached(VideoPlayer source)
        {
            Debug.Log(string.Format("Video:{0} PlayEnd", source.name));
            //֪ͨͶӰ�˿�ʼ������Ƶ
            var msg = new UdpMessage(MessageDefine.TableVideoEnd);
            UdpManager.Instance.SendMessage(msg.ToJson());
            mVideoCanvas.IsProjectorVideoPlay = true;
        }

	    public void Play()
	    {
            mVideoPlayer.Play();
	    }
        
    }
}

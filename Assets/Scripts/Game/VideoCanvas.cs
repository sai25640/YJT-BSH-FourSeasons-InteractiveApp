using UnityEngine;
using QFramework;
using TouchScript.Gestures;
using System;
using Common;
using UniRx;
using EventType = Common.EventType;

namespace FourSeasons
{
	public partial class VideoCanvas : ViewController
	{
	    public bool IsProjectorVideoPlay { get; set; }
	    private int mStopTime = 32; //投影程序刚好播放完龙视频
	    private IDisposable mStopTimeStream;

        void Start()
		{
            // Code Here
		    IsProjectorVideoPlay = false;
            EventCenter.AddListener<Vector2>(EventType.PointerPressed, OnPointerPressed);
        }

	    void OnDestroy()
	    {
	        EventCenter.RemoveListener<Vector2>(EventType.PointerPressed, OnPointerPressed);
        }

        private void OnPointerPressed(Vector2 pos)
        {
            Debug.Log(string.Format("PointerPosition:{0} Pressed", pos));

            //销毁上一次计时，使重新开始
            if (mStopTimeStream.IsNotNull())
            {
                //Debug.Log("销毁上一次计时，使重新开始");
                mStopTimeStream.Dispose();
            }
            //在指定时间内没有操作投影程序停止播放视频
            mStopTimeStream = Observable.Timer(TimeSpan.FromSeconds(mStopTime)).Subscribe(_ =>
            {
                var msg = new UdpMessage(MessageDefine.StopPlayProjectVideo);
                UdpManager.Instance.SendMessage(msg.ToJson());
                IsProjectorVideoPlay = false;
                mStopTimeStream.Dispose();
            });

            if (!TableVideo.IsPlaying && !IsProjectorVideoPlay)//&&投影程序没有在播放视频
            {
                TableVideo.Play();           
            }          
        }
    }
}

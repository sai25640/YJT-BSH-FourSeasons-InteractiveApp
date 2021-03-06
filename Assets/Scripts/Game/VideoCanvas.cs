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
	    private int mStopTime = 68; //投影程序播放四季视频开始倒计时
	    private IDisposable mStopTimeStream;

        void Start()
		{
            // Code Here
		    IsProjectorVideoPlay = false;
		    StopPlayFourSeasons();

            EventCenter.AddListener<Vector2>(EventType.PointerPressed, OnPointerPressed);
		    EventCenter.AddListener(EventType.WholeVideoEnd, OnWholeVideoEnd);
        }

        void OnDestroy()
        {
            EventCenter.RemoveListener<Vector2>(EventType.PointerPressed, OnPointerPressed);
            EventCenter.RemoveListener(EventType.WholeVideoEnd, OnWholeVideoEnd);
        }

        private void OnPointerPressed(Vector2 pos)
        {
            Debug.Log(string.Format("PointerPosition:{0} Pressed", pos));

            //销毁上一次计时，使重新开始
            if (mStopTimeStream.IsNotNull())
            {
                //Debug.Log("销毁上一次计时，使重新开始");
                mStopTimeStream.Dispose();
                RefreshTimer();
            }
   
            if (!TableVideo.IsPlaying && !IsProjectorVideoPlay)//&&投影程序没有在播放视频
            {
                TableVideo.Play();           
            }          
        }

        private void OnWholeVideoEnd()
        {
            //同步播放四季动画
            PlayFourSeasons();

            RefreshTimer();
        }

	    void RefreshTimer()
	    {
	        if (mStopTimeStream.IsNotNull())
	        {
	            mStopTimeStream.Dispose();
            }

            //Debug.Log("开始倒计时");
            //在指定时间内没有操作投影程序停止播放视频
            mStopTimeStream = Observable.Timer(TimeSpan.FromSeconds(mStopTime)).Subscribe(_ =>
            {
                Debug.Log("倒计时结束");
                var msg = new UdpMessage(MessageDefine.StopPlayProjectVideo);
                UdpManager.Instance.SendMessage(msg.ToJson());
                IsProjectorVideoPlay = false;
                mStopTimeStream.Dispose();
                mStopTimeStream = null;

                StopPlayFourSeasons();
                TableVideo.Reset();
            });
        }

	    void StopPlayFourSeasons()
	    {
            FourSeasonAnim.enabled = false;
	        FourSeasonAnim.Hide();
	        EffectBuilder.DestroyAllChild();
	    }
	   
        void PlayFourSeasons()
	    {
	        FourSeasonAnim.Show();
	        FourSeasonAnim.enabled = true;
	        FourSeasonAnim.SetTrigger("FourSeasons");
        }

    }
}

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
	    private int mStopTime = 32; //ͶӰ����պò���������Ƶ
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

            //������һ�μ�ʱ��ʹ���¿�ʼ
            if (mStopTimeStream.IsNotNull())
            {
                //Debug.Log("������һ�μ�ʱ��ʹ���¿�ʼ");
                mStopTimeStream.Dispose();
            }
            //��ָ��ʱ����û�в���ͶӰ����ֹͣ������Ƶ
            mStopTimeStream = Observable.Timer(TimeSpan.FromSeconds(mStopTime)).Subscribe(_ =>
            {
                var msg = new UdpMessage(MessageDefine.StopPlayProjectVideo);
                UdpManager.Instance.SendMessage(msg.ToJson());
                IsProjectorVideoPlay = false;
                mStopTimeStream.Dispose();
            });

            if (!TableVideo.IsPlaying && !IsProjectorVideoPlay)//&&ͶӰ����û���ڲ�����Ƶ
            {
                TableVideo.Play();           
            }          
        }
    }
}

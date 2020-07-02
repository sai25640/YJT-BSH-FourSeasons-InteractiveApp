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
	    private int mStopTime = 30; //ͶӰ���򲥷��ļ���Ƶ��ʼ����ʱ
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

            //������һ�μ�ʱ��ʹ���¿�ʼ
            if (mStopTimeStream.IsNotNull())
            {
                Debug.Log("������һ�μ�ʱ��ʹ���¿�ʼ");
                mStopTimeStream.Dispose();
                OnWholeVideoEnd();
            }
   
            if (!TableVideo.IsPlaying && !IsProjectorVideoPlay)//&&ͶӰ����û���ڲ�����Ƶ
            {
                TableVideo.Play();           
            }          
        }

        private void OnWholeVideoEnd()
        {
            //ͬ�������ļ�����
            PlayFourSeasons();

            Debug.Log("��ʼ����ʱ");
            //��ָ��ʱ����û�в���ͶӰ����ֹͣ������Ƶ
            mStopTimeStream = Observable.Timer(TimeSpan.FromSeconds(mStopTime)).Subscribe(_ =>
            {
                Debug.Log("����ʱ����");
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
	        FourSeasons.Hide();
	        FourSeasons.enabled = false;
        }


        void PlayFourSeasons()
	    {
	        FourSeasons.Show();
	        FourSeasons.enabled = true;
        }

    }
}

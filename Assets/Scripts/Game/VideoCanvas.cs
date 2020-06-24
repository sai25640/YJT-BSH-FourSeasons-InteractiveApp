using UnityEngine;
using QFramework;
using TouchScript.Gestures;
using System;
using Common;
using EventType = Common.EventType;

namespace FourSeasons
{
	public partial class VideoCanvas : ViewController
	{
        void Start()
		{
            // Code Here
            EventCenter.AddListener<Vector2>(EventType.PointerPressed, OnPointerPressed);
        }

	    void OnDestroy()
	    {
	        EventCenter.RemoveListener<Vector2>(EventType.PointerPressed, OnPointerPressed);
        }

        private void OnPointerPressed(Vector2 pos)
        {
            Debug.Log(string.Format("PointerPosition:{0} Pressed", pos));

            if (!TableVideo.IsPlaying)//&&投影程序没有在播放视频
            {
                TableVideo.Play();
            }          
        }
    }
}

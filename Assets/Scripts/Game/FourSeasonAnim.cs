using Common;
using UnityEngine;
using QFramework;
using EventType = Common.EventType;

namespace FourSeasons
{
	public partial class FourSeasonAnim : ViewController
	{
		void Start()
		{
			// Code Here
		}

	    public void OnSpringBegin()
	    {
            Debug.Log("OnSpringBegin");
            EventCenter.Broadcast(EventType.SpringBegin);
	    }

	    public void OnSpringEnd()
	    {
	        Debug.Log("OnSpringEnd");
	        EventCenter.Broadcast(EventType.SpringEnd);
        }

	    public void OnSummerBegin()
	    {
	        Debug.Log("OnSummerBegin");
	        EventCenter.Broadcast(EventType.SummerBegin);
        }

	    public void OnSummerEnd()
	    {
	        Debug.Log("OnSummerEnd");
	        EventCenter.Broadcast(EventType.SummerEnd);
        }

	    public void OnFallBegin()
	    {
	        Debug.Log("OnFallBegin");
	        EventCenter.Broadcast(EventType.FallBegin);
        }

	    public void OnFallEnd()
	    {
	        Debug.Log("OnFallEnd");
	        EventCenter.Broadcast(EventType.FallEnd);
        }

	    public void OnWinterBegin()
	    {
	        Debug.Log("OnWinterBegin");
	        EventCenter.Broadcast(EventType.WinterBegin);
        }

	    public void OnWinterEnd()
	    {
	        Debug.Log("OnWinterEnd");
	        EventCenter.Broadcast(EventType.WinterEnd);
        }



	}
}

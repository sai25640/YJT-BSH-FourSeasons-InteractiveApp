using UnityEngine;
using QFramework;

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
	    }

	    public void OnSpringEnd()
	    {
	        Debug.Log("OnSpringEnd");
        }

	    public void OnSummerBegin()
	    {
	        Debug.Log("OnSummerBegin");
        }

	    public void OnSummerEnd()
	    {
	        Debug.Log("OnSummerEnd");
        }

	    public void OnFallBegin()
	    {
	        Debug.Log("OnFallBegin");
        }

	    public void OnFallEnd()
	    {
	        Debug.Log("OnFallEnd");
        }

	    public void OnWinterBegin()
	    {
	        Debug.Log("OnWinterBegin");
        }

	    public void OnWinterEnd()
	    {
	        Debug.Log("OnWinterEnd");
        }



	}
}

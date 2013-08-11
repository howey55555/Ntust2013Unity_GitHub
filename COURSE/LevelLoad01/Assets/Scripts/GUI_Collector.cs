/*
@file GUI_Collector.cs
@author NDark
@date 20130805 . file created.
*/
using UnityEngine;

public class GUI_Collector : MonoBehaviour 
{
	static GameObject gGUICollector = null ;

	// Use this for initialization
	void Start () 
	{
		if( null == gGUICollector )
		{
			gGUICollector = GameObject.Find( "GUICollector" ) ;
		}
		
		if( null != gGUICollector )
		{
			this.gameObject.transform.parent = gGUICollector.transform ;
		}	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

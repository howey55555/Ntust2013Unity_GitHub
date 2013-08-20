/**
@file AlientDetectAndReset01.cs
@author NDark
@date 20130820 . file started.
*/
using UnityEngine;

public class AlientDetectAndReset01 : MonoBehaviour 
{

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		Vector3 posNow = this.gameObject.transform.position ;
		if( posNow.z < -6 ) 
		{
			this.gameObject.transform.position = 
				new Vector3( posNow.x , posNow.y , 6 ) ;
			
						
		}
	
	}
}

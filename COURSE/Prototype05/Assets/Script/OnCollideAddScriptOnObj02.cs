/**
@file OnCollideAddScriptOnObj02.cs
@author NDark
@date 20130805 file started.
*/
using UnityEngine;

public class OnCollideAddScriptOnObj02 : MonoBehaviour 
{
	
	// Use this for initialization
	void Start () 
	{
	}
	
	// Update is called once per frame
	void Update () 
	{	
	}
	
	
	void OnTriggerEnter( Collider other )
	{
		// Debug.Log( "other.gameObject.name=" + other.gameObject.name ) ;
		if( -1 != other.gameObject.name.IndexOf( "Missle" ) )
		{
			GameObject.Destroy( other.gameObject ) ;
			GameObject.Destroy( this.gameObject ) ;
		}
	}
	
}

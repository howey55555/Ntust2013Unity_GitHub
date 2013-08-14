/*
@file ClickOnGUI_SetSelectCharacter.cs
@author NDark
@date 20130814 file started.
*/
using UnityEngine;

public class ClickOnGUI_SetSelectCharacter : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
	
	void OnMouseDown()
	{
		// Debug.Log( "ClickOnGUI_SetSelectCharacter OnMouseDown" ) ;
		GameObject obj = GameObject.Find( "StaticGameSystemData" ) ;
		if( null != obj )
		{
			GameSystemData script = obj.GetComponent<GameSystemData>() ;
			if( null != script )
			{
				script.m_PickUpCharacterName = this.gameObject.name ;
			}
		}
	}		
}

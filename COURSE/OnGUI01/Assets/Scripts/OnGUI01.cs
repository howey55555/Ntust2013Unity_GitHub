/*
@file OnGUI01.cs
@author NDark
@date 20130721 . file started.

*/
using UnityEngine;
using System.Collections;

public class OnGUI01 : MonoBehaviour 
{
	public Texture m_Texture1 = null ;
	public GUIStyle m_Style1 = new GUIStyle() ;
	public GUISkin m_Skin1 = null ;
	 
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI()
	{
		
		{
			Rect box1Rect = new Rect( 0 , 0 , 100 , 50 ) ;
			GUI.Box( box1Rect , "BDox1" ) ;
		}
		
		{
			Rect box2Rect = new Rect( 0 , 70 , 100 , 50 ) ;
			GUI.Box( box2Rect , "BDox2" ) ;
		}

		{
			Rect box3Rect = new Rect( 0 , 140 , 100 , 50 ) ;
			GUI.Box( box3Rect , "" ) ;
		}

		{
			Rect box4Rect = new Rect( 0 , 210 , 100 , 50 ) ;
			GUI.Box( box4Rect , "box4box4box4box4" ) ;
		}
		
		if( null != m_Texture1 )
		{
			Rect box5Rect = new Rect( 0 , 280 , 100 , 50 ) ;
			GUI.Box( box5Rect , m_Texture1 ) ;
		}
		
		if( null != m_Texture1 )
		{
			Rect box6Rect = new Rect( 0 , 350 , 100 , 50 ) ;
			GUI.Box( box6Rect , new GUIContent ( m_Texture1 ) ) ;
		}
		
		if( null != m_Texture1 )
		{
			Rect box7Rect = new Rect( 0 , 420 , 100 , 50 ) ;
			GUI.Box( box7Rect , new GUIContent ( "box7" , m_Texture1 ) ) ;
		}
		
		{
			Rect box8Rect = new Rect( 0 , 490 , 100 , 50 ) ;
			GUI.Box( box8Rect , new GUIContent ( "box8" , "box8 tooltip" ) ) ;
			
			Rect tooltipRect = new Rect( Input.mousePosition.x , 
									     Screen.height - Input.mousePosition.y , 
										 100 , 50 ) ;
			GUI.Label( tooltipRect , GUI.tooltip ) ;
		}
		
		{
			Rect box9Rect = new Rect( 0 , 560 , 100 , 50 ) ;
			GUI.Box( box9Rect , "box9box9box9" , m_Style1 ) ;	
		}
		
		if( null != m_Texture1 )
		{
			Rect box10Rect = new Rect( 0 , 630 , 100 , 50 ) ;
			GUI.Box( box10Rect , m_Texture1 , m_Style1 ) ;	
		}
		
		
		GUI.skin = m_Skin1 ;
		{
			Rect box11Rect = new Rect( 0 , 700 , 100 , 25 ) ;
			GUI.Box( box11Rect , "box11" ) ;	
		}		
		GUI.skin = null ;
		{
			Rect box12Rect = new Rect( 0 , 725 , 100 , 25 ) ;
			GUI.Box( box12Rect , "box12" ) ;	
		}				
	}
}

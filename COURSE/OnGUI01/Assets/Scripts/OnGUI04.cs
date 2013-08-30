/*
@file OnGUI04.cs
@author NDark
@date 20130830 file started.
*/
using UnityEngine;

public class OnGUI04 : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	
	public Rect m_WindowRect = new Rect( 100 , 100 , 200 , 400 ) ;
	public Texture m_Texture = null ;
	void OnGUI()
	{
		if( null != m_Texture )
		{
			GUI.DrawTexture( m_WindowRect , m_Texture ) ;
			GUI.Window( 0 , m_WindowRect , windowFunc , "This is window" ) ;
		}
	}
	
	private void windowFunc( int _ID )
	{
		
	}
}

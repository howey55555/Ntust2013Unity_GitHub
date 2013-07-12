/*
 * @file RotateByAngular01.cs
 * @author NDark
 * @date 20130712 . file started.
 */
using UnityEngine;
using System.Collections;

public class RotateByAngular01 : MonoBehaviour 
{
	
	public float x = 0 ;
	public float y = 0 ;
	public float z = 0 ;
	
	public enum RotateByAngular01State
	{
		UnActive = 0 ,
		X,
		Y,
		End ,
	}
	
	public RotateByAngular01State m_State = RotateByAngular01State.UnActive ;
	Quaternion m_PreviousRotation = Quaternion.identity ;
	// Use this for initialization
	void Start () 
	{
	}
	
	// Update is called once per frame
	void Update () 
	{
		switch( m_State )
		{
		case RotateByAngular01State.UnActive :
			m_State = RotateByAngular01State.X ;
			break ; 
		case RotateByAngular01State.X  :
			x += 0.1f ;
			this.transform.rotation = Quaternion.identity ;
			this.transform.Rotate( x , 0 , 0 , Space.Self ) ;
			if( x > 90 )
			{
				m_PreviousRotation = this.transform.rotation ;
				m_State = RotateByAngular01State.Y ;
			}
			break ;
		case RotateByAngular01State.Y  :
			y += 0.1f ;
			this.transform.rotation = m_PreviousRotation ;
			this.transform.Rotate( 0 , y , 0 , Space.Self  ) ;
			if( y > 90 ) 
				m_State = RotateByAngular01State.End ;			
			break ;
		case RotateByAngular01State.End :
			break ;
		}
		
	
	}
}

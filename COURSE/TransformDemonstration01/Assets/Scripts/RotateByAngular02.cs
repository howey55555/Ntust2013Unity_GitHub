/*
 * @file RotateByAngular02.cs
 * @author NDark
 * @date 20130712 . file started.
 */
using UnityEngine;
using System.Collections;

public class RotateByAngular02 : MonoBehaviour 
{
	
	public float x = 0 ;
	public float y = 0 ;
	public float z = 0 ;
	
	public enum RotateByAngular02State
	{
		UnActive = 0 ,
		Y,
		Z,
		End ,
	}
	
	public RotateByAngular02State m_State = RotateByAngular02State.UnActive ;
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
		case RotateByAngular02State.UnActive :
			m_State = RotateByAngular02State.Y ;
			break ; 
		case RotateByAngular02State.Y  :
			y += 0.1f ;
			this.transform.rotation = Quaternion.identity ;
			this.transform.Rotate( 0 , y , 0 , Space.Self ) ;
			if( y > 90 )
			{
				m_PreviousRotation = this.transform.rotation ;
				m_State = RotateByAngular02State.Z ;
			}
			break ;
		case RotateByAngular02State.Z  :
			z += 0.1f ;
			this.transform.rotation = m_PreviousRotation ;
			this.transform.Rotate( 0 , 0 , z , Space.Self  ) ;
			if( z > 90 ) 
				m_State = RotateByAngular02State.End ;			
			break ;
		case RotateByAngular02State.End :
			break ;
		}
		
	
	}
}

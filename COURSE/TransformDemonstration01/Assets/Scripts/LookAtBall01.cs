/**
 * @file LookAtBall01.cs
 * @author NDark
 * 
 * Demonstration of using LookAt().
 * 
 * @date 20130712 . file started.
 */
using UnityEngine;
using System.Collections;

public class LookAtBall01 : MonoBehaviour 
{
	GameObject m_Ball = null ;
	
	// Use this for initialization
	void Start () 
	{
		if( null == m_Ball )
		{
			InitializeBall() ;
		}
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if( null == m_Ball )
			return ;
	
		this.gameObject.transform.LookAt( m_Ball.transform ) ;
	}
	
	private void InitializeBall()
	{
		m_Ball = GameObject.Find( "Ball" ) ;
		if( null == m_Ball )
		{
			Debug.LogError( "LookAtBall01::InitializeBall() null == m_Ball" ) ;
		}
		else
		{
			Debug.Log( "LookAtBall01::InitializeBall() end." ) ;
		}		
	}
}

/*
 * @file ChangeScript01.cs
 * @author NDark
 * @date 20130601 . file started.
 */
using UnityEngine;
using System.Collections;

public class ChangeScript01 : MonoBehaviour 
{
	public float m_DistanceThreashold = 4.0f ;
	public GameObject m_TargetObject = null ;
	public GameObject m_TestObject = null ;
	public bool m_Valid = true ;

	// Use this for initialization
	void Start () 
	{
		// 沒設定才要初始化
		if( null == m_TestObject )
			InitializeCameraPtr() ;			
		
		if( null == m_TargetObject )
			InitializeMainCharacter() ;			
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if( true == m_Valid )
		{
			CheckAndChangeScript() ;
		}
	
	}
	
	private void CheckAndChangeScript()
	{
		if( null == m_TargetObject || null == m_TestObject )
			return ;
		Vector3 distanceVec = m_TargetObject.transform.position - m_TestObject.transform.position ;
		if( distanceVec.magnitude < m_DistanceThreashold )
		{
			CameraController_CameraRoutes02 scriptPtr = m_TargetObject.GetComponent<CameraController_CameraRoutes02>() ;
			Component.Destroy( scriptPtr ) ;
			m_TargetObject.AddComponent<CameraController_RotateAroundTarget01>() ;
			m_Valid = false ;
		}
	}
	
	
	private void InitializeCameraPtr()
	{
		m_TestObject = (GameObject)Camera.mainCamera.gameObject ;
		
		if( null == m_TestObject )
		{
			Debug.LogError( "ChangeScript01:InitializeCameraPtr() null == m_TestObject" ) ;
		}
		else
		{
			Debug.Log( "ChangeScript01:InitializeCameraPtr() end." ) ;
		}
	}		
	
	private void InitializeMainCharacter()
	{
		m_TargetObject = GameObject.FindGameObjectWithTag( "Player" ) ;
		
		if( null == m_TargetObject )
		{
			Debug.LogError( "ChangeScript01:InitializeMainCharacter() null == m_TargetObject" ) ;
		}
		else
		{
			Debug.Log( "ChangeScript01:InitializeMainCharacter() end." ) ;
		}
	}
}

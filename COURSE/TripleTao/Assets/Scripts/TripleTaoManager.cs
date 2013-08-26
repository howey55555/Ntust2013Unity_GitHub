/*
@file TripleTaoManager.cs
@author NDark
@date 20130826 file started.
*/
using UnityEngine;
using System.Collections.Generic;

public class TripleTaoManager : MonoBehaviour 
{
	Dictionary<int,GameObject> m_Units = new Dictionary<int, GameObject>() ;
	public GameObject m_HoldUnit = null ;
	public float m_HoldHeight = 1; 
	public int m_WidthNum = 1 ;
	public int m_HeightNum = 1 ;
	
	static int m_Iterator = 0 ;
	
	bool m_AvaliableDrop = false ;
	
	public void Drop( int i , int j , Vector3 _Center )
	{
		int index = j * m_HeightNum + i ;
		if( false == m_AvaliableDrop )
		{
			// 檢查是否是第一個(交換格)
			if( 0 == index )
			{
				EnableHoldUnitResize( false ) ;
				m_HoldUnit.transform.position = _Center ;
				GameObject temp = m_HoldUnit ;
				m_HoldUnit = m_Units[ index ] ;
				m_Units[ index ] = temp ;
			}
		}
		else
		{
			// 放下,重新產生一個
			EnableHoldUnitResize( false ) ;
			m_HoldUnit.transform.position = _Center ;
			m_Units[ index ] = m_HoldUnit ;
			m_HoldUnit = GenerateAUnit() ;
		}
		
	}
	
	// Use this for initialization
	void Start () 
	{
		m_HoldUnit = GenerateAUnit() ;
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if( null != m_HoldUnit )
		{
			Vector3 worldPos = Camera.mainCamera.ScreenToWorldPoint( Input.mousePosition ) ;
			worldPos.y = m_HoldHeight ;
			m_HoldUnit.transform.position = worldPos ;
		}
		
		// 檢查目前偵測到的stage物件
		Ray mouseRay = Camera.mainCamera.ScreenPointToRay( Input.mousePosition ) ;
		RaycastHit hitInfo ;
		if( true == Physics.Raycast( mouseRay , out hitInfo ) )
		{
			UnitData unitData = hitInfo.collider.gameObject.GetComponent<UnitData>() ;
			if( null != unitData )
			{
				int index = unitData.m_IndexJ * m_HeightNum + unitData.m_IndexI ;
				
				if( false == this.m_Units.ContainsKey( index ) )
				{
					// 可以放
					// 縮放縮放
					EnableHoldUnitResize( true ) ; 
					m_AvaliableDrop = true ;
				}
				else
				{
					// 不能放
					EnableHoldUnitResize( false ) ;
					m_AvaliableDrop = false ;
				}
			}
		}
	}
	
	private GameObject GenerateAUnit()
	{
		GameObject ret = null ;
		Object prefab = Resources.Load( "UnitBush" ) ;
		if( null == prefab )
		{
			Debug.LogError( prefab ) ;
		}
		else
		{
			ret = (GameObject)GameObject.Instantiate( prefab ) ;
			ret.name = "Unit" + m_Iterator.ToString() ;
			++m_Iterator ;
		}
		
		return ret ;
	}
	
	private void EnableHoldUnitResize( bool _Enable )
	{
		ResizeScale01 resizeScript = this.m_HoldUnit.GetComponent<ResizeScale01>() ;
		if( null == resizeScript )
		{
			resizeScript = this.m_HoldUnit.AddComponent<ResizeScale01>() ;
			
		}
		resizeScript.enabled = _Enable ;
	}
}

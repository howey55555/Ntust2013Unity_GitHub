/**
@file MinimapManager01.cs
@author NDark
@date 20130609 by NDark
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class MinimapManager01 : MonoBehaviour 
{
	// 單位與小地圖物件的組合
	[System.Serializable]
	public class ObjectPair
	{
		public GameObject UnitObj = null ;
		public GameObject MiniMapObj = null ;
		
		public ObjectPair()
		{
		}
		
		public ObjectPair( GameObject _UnitObj , 
						   GameObject _MiniMapObj )
		{
			UnitObj = _UnitObj ;
			MiniMapObj = _MiniMapObj ;
		}
	}
		
	/* 
	 小地圖物件都存在容器中
	 依照物件名稱收集的所有小地圖物件
	 */
	public Dictionary<string, ObjectPair > m_MiniMapPairs = new Dictionary<string, ObjectPair>() ;		
	private GameObject m_TrafficSignalParent = null ;	
	
	
	// Use this for initialization
	void Start () 
	{
		InitializTrafficSignalParent() ;
		InitializPairs() ;
	}
	
	// Update is called once per frame
	void Update () 
	{
		UpdatePos() ;			
	}
	
	private void InitializTrafficSignalParent()
	{
		m_TrafficSignalParent = GameObject.Find( "TrafficSignalParent" ) ;
		if( null == m_TrafficSignalParent )
		{
			Debug.LogError( "MinimapManager01:InitializTrafficSignalParent() null == m_TrafficSignalParent" ) ;
		}		
		else
		{
			Debug.Log( "MinimapManager01:InitializTrafficSignalParent() end" ) ;
		}
	}
	
	private void InitializPairs()
	{
		GameObject trafficLightParentObj = GameObject.Find( "TrafficLightParent" ) ;
		if( null == trafficLightParentObj )
		{
			Debug.LogError( "MinimapManager01:InitializPairs() null == trafficLightParentObj" ) ;
			return ;
		}
		
		ObjectPair newPair = null ;
		Transform trans = null ;
		GameObject unitObj = null ;
		string trafficLightName = "";
		for( int i = 0 ; i < 3 ; ++i )
		{
			trafficLightName = string.Format( "TrafficLight{0:00}" , i ) ;
			trans = trafficLightParentObj.transform.FindChild( trafficLightName ) ;
			if( null != trans )
			{
				Object prefabObj = Resources.Load( "Prefabs/TrafficSignal" ) ;
				if( null != prefabObj )
				{
					GameObject singalObject = (GameObject) GameObject.Instantiate( prefabObj ) ;
					singalObject.name = "TrafficSignal_" + trafficLightName ;
					if( null != m_TrafficSignalParent )
					{
						singalObject.transform.parent = m_TrafficSignalParent.transform ;
					}
					
					unitObj = trans.gameObject ;
					newPair = new ObjectPair() ;
					newPair.UnitObj = unitObj ;
					newPair.MiniMapObj = singalObject ;
					m_MiniMapPairs.Add( trafficLightName , newPair ) ;
				}
			}
			
		}
		
	}
	
	private void UpdatePos()
	{
		Dictionary<string,ObjectPair>.Enumerator ePair = m_MiniMapPairs.GetEnumerator() ;
		while( ePair.MoveNext() )
		{
			// string unitName = eInMinimap.Current.Key ;
			GameObject unitObj = ePair.Current.Value.UnitObj ;
			GameObject miniMapObj = ePair.Current.Value.MiniMapObj ;
			if( null != unitObj && 
				null != miniMapObj )
			{
				miniMapObj.transform.position = new Vector3(
					unitObj.transform.position.x ,
					miniMapObj.transform.position.y ,					 
					unitObj.transform.position.z ) ;
			}
		}
	}	
}

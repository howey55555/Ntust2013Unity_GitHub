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
	private List<string> m_RemoveList = new List<string>() ;// 準備移除的清單	
	private GameObject m_TrafficSignalParent = null ;	
	
	
	// Use this for initialization
	void Start () 
	{
		InitializTrafficLightObjects() ;
	}
	
	// Update is called once per frame
	void Update () 
	{
		UpdatePos() ;			
	}
	
	private void InitializTrafficLightObjects()
	{
		
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
					unitObj.transform.position.y ,					 
					miniMapObj.transform.position.z ) ;
			}
		}
	}	
}

/*
@file TripleTaoManager.cs
@author NDark
@date 20130826 file started.
*/

// #define DEBUG

using UnityEngine;
using System.Collections.Generic;



public class TripleTaoManager : MonoBehaviour 
{
	public enum Direction
	{
		Up = 0 ,
		Down = 1 ,
		Right = 2 ,
		Left = 3 ,
	}
	
	public int m_PreserveIndex = 0 ;
	Dictionary<int,GameObject> m_Units = new Dictionary<int, GameObject>() ;
	public GameObject m_HoldUnit = null ;
	public float m_HoldHeight = 1; 
	public int m_WidthNum = 1 ;
	public int m_HeightNum = 1 ;
	
	public HashSet<int> m_CurrentCollectAnimationList = new HashSet<int>() ;
	public bool m_CollectAnimation = false ;
	
	static int m_Iterator = 0 ;
	int m_LastIndex = -1 ;
	
	bool m_AvaliableDrop = false ;
	
	
	public void Drop( int i , int j , Vector3 _Center )
	{
		// 檢查是否在動畫中
		if( true == m_CollectAnimation )
		{
			return ;
		}
		
		int index = j * m_HeightNum + i ;
		
		// 
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
			// 放下後有否要結合動畫
			if( m_CurrentCollectAnimationList.Count >= 2 ) 
			{
#if DEBUG				
				Debug.Log( "m_CurrentCollectAnimationList.Count >= 2 " ) ;
#endif				
				m_CollectAnimation = true ;
				
				// 先暫停原本的動畫
				StopAllUnitsAnim() ;
				
				UnitType maximumType = UnitType.grass ;
				// 將目標物件都掛上結合動畫
				// 移除m_Unit上的各物件
				foreach( int collectIndex in m_CurrentCollectAnimationList )
				{
					UnitData unitData = m_Units[ collectIndex ].GetComponent<UnitData>() ;
					if( null != unitData )
					{
						if( unitData.m_UnitType > maximumType )
						{
							maximumType = unitData.m_UnitType ;
						}
					}
					
					CloseTargetAndSuicide closeScript = m_Units[ collectIndex ].GetComponent<CloseTargetAndSuicide>() ;
					if( null == closeScript )
					{
						closeScript = m_Units[ collectIndex ].AddComponent<CloseTargetAndSuicide>() ;
					}
					closeScript.m_TargetPos = _Center ;
					
					m_Units.Remove( collectIndex ) ;
				}
				
				// 放下那個要掛上升級script
				UpgradeAndEndAnimation upgradeScript = m_HoldUnit.GetComponent<UpgradeAndEndAnimation>() ;
				if( null == upgradeScript )
				{
					upgradeScript = m_HoldUnit.AddComponent<UpgradeAndEndAnimation>() ;
					upgradeScript.m_UpGradeTo = maximumType + 1 ;
				}
				
			}
			
			
			// 放下
			EnableHoldUnitResize( false ) ;
			m_HoldUnit.transform.position = _Center ;
			PlaceOnUnits( m_HoldUnit , i , j ) ;
		
			// 重新產生一個
			m_HoldUnit = GenerateAUnit() ;
		}
		
	}
	
	
	// Use this for initialization
	void Start () 
	{
		// 初始地圖
		
		
		// 放一個在預備格
		GameObject preserveObj = GenerateAUnit() ;
		PlaceOnUnits( preserveObj , m_PreserveIndex ) ;
		preserveObj.transform.position = 
			new Vector3( preserveObj.transform.position.x ,
						 0.1f ,
						 preserveObj.transform.position.z ) ;
		
		// 第二個拿在手上
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
		
		// 檢查是否在動畫中
		if( true == m_CollectAnimation )
		{
			return ;
		}
		
		// 檢查目前偵測到的stage物件
		Ray mouseRay = Camera.mainCamera.ScreenPointToRay( Input.mousePosition ) ;
		RaycastHit hitInfo ;
		if( true == Physics.Raycast( mouseRay , out hitInfo ) )
		{
			UnitData unitData = hitInfo.collider.gameObject.GetComponent<UnitData>() ;
			Vector3 mayDraopPos = hitInfo.collider.gameObject.transform.position ;
			if( null != unitData )
			{
				int index = unitData.m_IndexJ * m_HeightNum + unitData.m_IndexI ;
				
				if( false == this.m_Units.ContainsKey( index ) )
				{
					// 可以放
					// 縮放縮放
					EnableHoldUnitResize( true ) ; 
					m_AvaliableDrop = true ;
					
					// 只有當index變化時才去檢查
					if( index != m_LastIndex )
						TryAnimationNeighbor( unitData.m_IndexI , unitData.m_IndexJ , mayDraopPos ) ;
				}
				else
				{
					// 不能放
					EnableHoldUnitResize( false ) ;
					m_AvaliableDrop = false ;
				}
				
				m_LastIndex = index ;
			}
		}
	}
	
	private GameObject GenerateAUnit()
	{
		UnitType type = (UnitType) Random.Range( 0 , (int) UnitType.tree + 1 ) ;
		
		GameObject ret = null ;
		Object prefab = Resources.Load( "UnitBush" ) ;
		if( null == prefab )
		{
			Debug.LogError( prefab ) ;
		}
		else
		{
			ret = (GameObject)GameObject.Instantiate( prefab ) ;
			UnitData script = ret.AddComponent<UnitData>() ;
			script.m_UnitType = type ;
			script.SetMaterial( ret.renderer ) ;
			
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
		if( false == _Enable )
		{
			this.m_HoldUnit.transform.localScale = resizeScript.m_InitScale ;
		}
				
	}
	
	private void TryAnimationNeighbor( int _i , int _j , Vector3 _MayDropPos )
	{
#if DEBUG		
		string debugStr = string.Format( "TryAnimationNeighbor ( {0}, {1} ) " , _i , _j ) ;
		Debug.Log( debugStr ) ;		
#endif		
		
		// 檢查 目前手上的物件的 UnitType 跟 neighbor是否相同(連成3)
		UnitData unitData = m_HoldUnit.GetComponent<UnitData>() ;
		if( null == unitData )
			return ;
		
		_MayDropPos.y = 0.1f ;
			
		
		UnitType currentUnitType = unitData.m_UnitType ;
		bool collect = false ;
		HashSet<int> oneTimeCollectAnimationList = new HashSet<int>() ;
		oneTimeCollectAnimationList.Clear() ;
		m_CurrentCollectAnimationList.Clear() ;
		
		while( currentUnitType <= UnitType.flyingcastle ) 
		{
			if( true == TryCollectAllPossibleNeighborIndex( _i , _j , 
															currentUnitType , 
															ref oneTimeCollectAnimationList ) )
			{
				collect = true ;
				foreach( int collectIndex in oneTimeCollectAnimationList )
				{
					m_CurrentCollectAnimationList.Add( collectIndex ) ;
				}
				++currentUnitType ;
			}
			else
			{
				StopAllUnitsAnim() ;
				break ;
			}
		}
		
		if( true == collect )
		{
			foreach( int index in m_CurrentCollectAnimationList )
			{
				// 將相對應的 動畫script掛上 並指定好目標
	#if DEBUG				
				debugStr = string.Format( "anim[ {0} ]" , index ) ;
				Debug.Log( debugStr )  ;
	#endif				
				
				TryCloseTarget animScript = m_Units[ index ].GetComponent<TryCloseTarget>() ;
				if( null == animScript )
				{
					animScript = m_Units[ index ].AddComponent<TryCloseTarget>() ;
				}
				animScript.m_TargetPos = _MayDropPos ;
				animScript.m_InterpolateNow = 0 ;
				animScript.m_Positive = true ;
				animScript.enabled = true ;
			}		
		}
	}
	
	private bool TryCollectAllPossibleNeighborIndex( int _i ,
													 int _j ,
													 UnitType _Type ,
													 ref HashSet<int> _PossibleIndices )
	{
		int count = 0 ;
		
		bool []checkedArray = new bool[ m_WidthNum * m_HeightNum ] ;
		for( int i = 0 ; i < m_WidthNum * m_HeightNum ; ++i )
		{
			checkedArray[ i ] = false ;
		}
		
		count = 0 ;
		count += RecursiveCheck( _i , _j + 1 , Direction.Down , _Type , 
			ref _PossibleIndices , ref checkedArray ) ;
		count += RecursiveCheck( _i , _j - 1 , Direction.Up , _Type , ref _PossibleIndices , ref checkedArray ) ;
		count += RecursiveCheck( _i + 1 , _j , Direction.Left , _Type , ref _PossibleIndices , ref checkedArray ) ;
		count += RecursiveCheck( _i - 1 , _j , Direction.Right , _Type , ref _PossibleIndices , ref checkedArray ) ;
		
#if DEBUG		
		Debug.Log( "count=" + count ) ;
#endif 
		
		bool alreadyLink = ( count >= 2 ) ;
		return alreadyLink ;
	}
	
	private int RecursiveCheck( int _i ,
								int _j ,
								Direction _FromDirection ,
								UnitType _Type ,
								ref HashSet<int> _PossibleIndices ,
								ref bool [] _CheckedArray )
	{
		int ret = 0 ;
#if DEBUG		
		string debugStr = string.Format( "RecursiveCheck ( {0}, {1} ) " , _i , _j ) ;
		Debug.Log( debugStr ) ;
#endif		
		
		int index = _j * m_HeightNum + _i ;
		// 交換格不可動
		if( index == m_PreserveIndex )
		{
#if DEBUG			
			Debug.Log( "don't move preserver" ) ;
#endif			
			return ret ;
		}
			
		// 已經
		if( _i < 0 || _i >= m_WidthNum ||
			_j < 0 || _j >= m_HeightNum )
		{
#if DEBUG			
			Debug.Log( "outofmap" ) ;
#endif
			return ret ;
		}
		
		
		if( true == _CheckedArray[ index ] )
		{
#if DEBUG			
			Debug.Log( "alreadyChecked" ) ;
#endif			
			return ret ;
		}
		
		_CheckedArray[ index ] = true ;
		
		if( false == m_Units.ContainsKey( index ) )
		{
#if DEBUG			
			Debug.Log( "noUnitonIt index" + index ) ;
			foreach( int key in m_Units.Keys )
			{
				Debug.Log( "key=" + key ) ;
			}
			
#endif
			return ret ;
		}
		
		UnitData targetUnitData = m_Units[ index ].GetComponent<UnitData>() ;
		if( _Type != targetUnitData.m_UnitType )
		{
#if DEBUG
			Debug.Log( "wrong type" ) ;
#endif			
			return ret ;
		}
		
		_PossibleIndices.Add( index ) ;
		ret = 1 ;
		
		if( Direction.Up != _FromDirection )
			ret += RecursiveCheck( _i , _j + 1 , Direction.Down , _Type , 
				ref _PossibleIndices , ref _CheckedArray ) ;
		if( Direction.Down != _FromDirection )
			ret += RecursiveCheck( _i , _j - 1 , Direction.Up , _Type , 
				ref _PossibleIndices , ref _CheckedArray ) ;
		if( Direction.Right != _FromDirection )
			ret += RecursiveCheck( _i + 1 , _j , Direction.Left , _Type , 
				ref _PossibleIndices , ref _CheckedArray ) ;
		if( Direction.Left != _FromDirection )		
			ret += RecursiveCheck( _i - 1 , _j , Direction.Right , _Type , 
				ref _PossibleIndices , ref _CheckedArray ) ;

		return ret ;
	}
	
	private void StopAllUnitsAnim()
	{
		foreach( GameObject unitObj in m_Units.Values )
		{
			
			if( null != unitObj )
			{
				TryCloseTarget script = unitObj.GetComponent<TryCloseTarget>() ; 
				if( null != script )
				{
					script.enabled = false ;
					unitObj.transform.position = script.m_InitPos ;
				}
				
			}
		}
	}
	
	private void PlaceOnUnits( GameObject _Obj , int _i , int _j ) 
	{
		PlaceOnUnits( _Obj , _j * m_HeightNum + _i ) ;
	}
	
	private void PlaceOnUnits( GameObject _Obj , int _Index ) 
	{
		UnitData script = _Obj.GetComponent<UnitData>() ;
		if( null != script )
		{
			script.m_IndexI = _Index / m_HeightNum ;
			script.m_IndexJ = _Index % m_HeightNum ;				
		}
		m_Units[ _Index ] = _Obj ;
	}
}

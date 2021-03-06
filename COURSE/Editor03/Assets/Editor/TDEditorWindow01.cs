/*
@file TDEditorWindow01.cs
@author NDark

Attention!!!!
Script must be placed in the folder called "Editor" in Assets

# 收集 AlienUnit 集體掛上適當的script
# 將 AlienUnit 集體收到兵營

 
# 收集 Waypoint 集體掛上適當的script 
# 將 Waypoint 的位置集體降到 地表

# 依據目前顯示的 Waypoint 顯示 Scene 畫面中 不同的顏色線條

@date 20130824 file started.

*/

// #define ON_SCENE_GUI_DELEGATE 
#define DRAW_GIZMO_LINE

using UnityEngine;
using UnityEditor ; // add this for editor
using System.Collections.Generic;

// You don't have to put script on GameObject
public class TDEditorWindow01 : EditorWindow 
{
	
	[MenuItem ("Tower Defense/TD Manager Window 1")]
    static void ShowWindow () 
	{
        EditorWindow.GetWindow<TDEditorWindow01>() ;
    }		
	
	public GameObject m_BarractObj = null ;
	public bool m_ToggleAlienList = false ;
	public GameObject [] m_AlientList = null ;
	
	public bool m_ToggleWayPointList = false ;
	public GameObject m_WayPointParent = null ;
	public GameObject [] m_WayPointList = null ;
	 
	
	public Vector3 m_WayPointShift = new Vector3( 0 , 0.05f , 0 ) ;
	
	public int m_SelectIndexInWayPointList = -1 ;
	public GameObject m_PreviousSelectionWayPoint = null ;
	// the content of your window draw here.
	void OnGUI()
	{
		m_ToggleAlienList = EditorGUILayout.BeginToggleGroup( "Alien List" ,  m_ToggleAlienList ) ;
		if( null != m_AlientList )
			EditorGUILayout.LabelField( "AlienNum=" + 
										m_AlientList.Length ) ;
		else
			EditorGUILayout.LabelField( "AlienNum=" + 0 ) ;
				
		if( true == m_ToggleAlienList && 
			null != m_AlientList )
		{
			for( int i = 0 ; i < m_AlientList.Length ; ++i )
			{
				EditorGUILayout.ObjectField( m_AlientList[ i ] , 
											 typeof (GameObject) , 
											 true ) ;
			}
		}
		EditorGUILayout.EndToggleGroup() ;
		
		m_BarractObj = 
			(GameObject)EditorGUILayout.ObjectField( "BarrackObj" ,
												     m_BarractObj , 
													 typeof (GameObject) , 
													 true ) ;
		
		GUILayout.BeginHorizontal() ;		
		if( true == GUILayout.Button( "Collect Alien Unit" ) )
		{
			CollectAlienUnit() ;
		}
		if( true == GUILayout.Button( "Alien Unit to Barract." ) )
		{
			SetAllienToBarrack() ;
		}		
		GUILayout.EndHorizontal() ;
		
		m_ToggleWayPointList = 
			EditorGUILayout.BeginToggleGroup( "Way Point List" , 
											  m_ToggleWayPointList ) ;
		
#if DRAW_GIZMO_LINE		
		CheckSelectionWayPoint() ;
		
		
		EditorGUILayout.ObjectField( "SelectWayPoint" , 
									 m_PreviousSelectionWayPoint , 
									 typeof (GameObject) , 
									 true ) ;
		
#endif		
		
		if( null != m_WayPointList )
			EditorGUILayout.LabelField( "WaypointNum=" + 
										m_WayPointList.Length ) ;
		else
			EditorGUILayout.LabelField( "WaypointNum=" + 0 ) ;
				
		if( true == m_ToggleWayPointList && 
			null != m_WayPointList )
		{
			for( int i = 0 ; i < m_WayPointList.Length ; ++i )
			{
				EditorGUILayout.ObjectField( m_WayPointList[ i ] , 
											 typeof (GameObject) , 
										     true ) ;
				
			}
		}
		EditorGUILayout.EndToggleGroup() ;
		
		m_WayPointShift = 
			EditorGUILayout.Vector3Field( "Way Point Shift From Ground" , 
										  m_WayPointShift ) ;
		GUILayout.BeginHorizontal() ;
		if( true == GUILayout.Button( "Collect Way Point" ) )
		{
			CollectWayPoint() ;
		}
		if( true == GUILayout.Button( "Set Way Point To Ground" ) )
		{
			SetWayPointToGround() ;
		}
		GUILayout.EndHorizontal() ;
		
		
#if ON_SCENE_GUI_DELEGATE
		if(SceneView.onSceneGUIDelegate != this.OnSceneGUI)
		{
			SceneView.onSceneGUIDelegate += this.OnSceneGUI;
		}
#endif
	}
	
#if ON_SCENE_GUI_DELEGATE	
	void OnSceneGUI ( SceneView scnView )
	{
		
		if( -1 != m_SelectIndexInWayPointList &&
			m_SelectIndexInWayPointList >= 0 &&
			m_SelectIndexInWayPointList < m_WayPointList.Length )
		{
			if( m_SelectIndexInWayPointList-1 >= 0 &&
				m_SelectIndexInWayPointList < m_WayPointList.Length )
			{
				Handles.DrawLine( m_WayPointList[ m_SelectIndexInWayPointList ].transform.position , 
								  m_WayPointList[ m_SelectIndexInWayPointList-1 ].transform.position ) ;
			}
			if( m_SelectIndexInWayPointList+1 >= 0 &&
				m_SelectIndexInWayPointList < m_WayPointList.Length )
			{
				Handles.DrawLine( m_WayPointList[ m_SelectIndexInWayPointList ].transform.position , 
								  m_WayPointList[ m_SelectIndexInWayPointList+1 ].transform.position ) ;
			}
			
		}
	}
#endif
	
	
	void OnInspectorUpdate() 
	{
		Repaint();
	}	

	
	private void CollectAlienUnit()
	{
		m_AlientList = GameObject.FindGameObjectsWithTag( "AlienUnit" ) ;
	}

	private void SetAllienToBarrack()
	{
		if( null == m_BarractObj )
		{
			return ;
		}
		foreach( GameObject alien in m_AlientList )
		{
			alien.transform.position = m_BarractObj.transform.position ;
		}
	}
	
	private void CollectWayPoint()
	{
		m_WayPointParent = GameObject.Find( "WayPointParent" ) ;
		if( null == m_WayPointParent )
		{
			m_WayPointParent = new GameObject() ;
			m_WayPointParent.name = "WayPointParent" ;
		}
		
		if( null != m_WayPointParent )
		{
			List<GameObject> wayPointList = new List<GameObject>() ;
			for( int i = 0 ; i < m_WayPointParent.transform.childCount ; ++i )
			{
				Transform child = m_WayPointParent.transform.GetChild( i ) ;
				wayPointList.Add( child.gameObject ) ;
				
				DrawGizmo01 script = child.gameObject.GetComponent<DrawGizmo01>() ;
				if( null == script )
				{
					script = child.gameObject.AddComponent<DrawGizmo01>() ;
				}
				script.m_IconName = "waypoint" ;
				
				Renderer renderer = child.gameObject.GetComponent<Renderer>() ;
				if( null != renderer )
					renderer.enabled = false ;
			}
			m_WayPointList = wayPointList.ToArray() ;
		}
	}	
	
	private void SetWayPointToGround()
	{
		foreach( GameObject wayPointObject in m_WayPointList )
		{
			Vector3 pos = wayPointObject.transform.position ;
			pos.y = Camera.mainCamera.transform.position.y ;
			Ray ray = new Ray( pos , Vector3.up * -1 ) ;
			RaycastHit hitInfo ;
			if( Physics.Raycast( ray , out hitInfo ) )
			{
				pos = hitInfo.point + m_WayPointShift ;
			}
			else 
			{
				pos.y = 0 ;
				
			}
				
			wayPointObject.transform.position = pos ;
			
		}
	}
	
	private void CheckSelectionWayPoint()
	{
		// select waypoint 在清單前面
		if( null == Selection.activeGameObject ||
			null == m_WayPointList )
		{
			m_PreviousSelectionWayPoint = null ; // 清除選擇路標
			ClearAllDrawLineInGizmo01() ; // 清除線
		}
		else if( m_PreviousSelectionWayPoint != 
			  	 Selection.activeGameObject )
		{
			m_SelectIndexInWayPointList = -1 ;
			for( int i = 0 ; i < m_WayPointList.Length ; ++i )
			{
				if( m_WayPointList[ i ] == Selection.activeGameObject )
				{
					m_SelectIndexInWayPointList = i ;
					m_PreviousSelectionWayPoint = Selection.activeGameObject ;
					// Debug.Log( "m_SelectIndexInWayPointList" + m_SelectIndexInWayPointList ) ;
					
					ClearAllDrawLineInGizmo01() ; // 清除線
					SetupDrawLineInSelectWayPoint() ; // 重新設定線
					
					break ;// 只有需要檢查到一個路標
				}
			}
		}
		else
		{
			
		}

	}
	
	private void ClearAllDrawLineInGizmo01()
	{
		if( null == m_WayPointList )
			return ;
		
		foreach( GameObject wayPointObject in m_WayPointList )
		{
			DrawGizmo01 script = 
				wayPointObject.GetComponent<DrawGizmo01>() ;
			if( null != script )
			{
				script.m_DrawLine = false ;// 關閉每一個路標的線顯示
			}
		}
	}
	
	private void SetupDrawLineInSelectWayPoint()
	{
		if( null == m_PreviousSelectionWayPoint )
			return ;
		
		DrawGizmo01 script = 
			m_PreviousSelectionWayPoint.GetComponent<DrawGizmo01>() ;
		if( null != script )
		{
			script.m_DrawLine = true ;
			
			if( m_SelectIndexInWayPointList-1 >= 0 &&
				m_SelectIndexInWayPointList < m_WayPointList.Length )
			{
				script.m_PreviousObj = 
					m_WayPointList[ m_SelectIndexInWayPointList-1 ] ;
				
			}
			
			if( m_SelectIndexInWayPointList+1 >= 0 &&
				m_SelectIndexInWayPointList < m_WayPointList.Length )
			{
				script.m_NextObj = 
					m_WayPointList[ m_SelectIndexInWayPointList+1 ] ;
			}
		}		

		EditorUtility.SetDirty( m_PreviousSelectionWayPoint ) ;
		
	}
}

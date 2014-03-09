/**
 * @file ConversationManager.cs
 * @author NDark
 * @date20140308 file started.
 */
using UnityEngine;
using System.Collections.Generic ;

public enum ConversationManagerState
{
	UnActive = 0 ,
	Starting ,
	WaitEnd ,
	WaitForInput , 
	Closing ,
	Close ,
} ;

public class ConversationManager : MonoBehaviour 
{
	public ConversationManagerState State
	{
		get { return m_State ; }
	}
	private ConversationManagerState m_State = ConversationManagerState.UnActive ;
	private int m_CurrentStoryUID = 0 ;
	private int m_CurrentTakeUID = 0 ;
	private float m_StartTime = 0.0f ;

	public List<Story> Stories
	{ 
		get { return m_Stories ; } 
		set { m_Stories = value ; } 
	}
	private List<Story> m_Stories = new List<Story>() ;

	public List<Take> Takes
	{ 
		get { return m_Takes ; } 
		set { m_Takes = value ; } 
	}
	private List<Take> m_Takes = new List<Take>() ;

	private List<int> m_StartingQueue = new List<int>() ;
	
	public void ActiveConversation( int _StoryUID )
	{
		m_StartingQueue.Add( _StoryUID ) ;
	}
	
	// Use this for initialization
	void Start () 
	{
		ShowDialogUI( false ) ;
	}
	
	// Update is called once per frame
	void Update () 
	{
		switch( m_State )
		{
		case ConversationManagerState.UnActive :
			CheckQueue() ;
			break ;
		case ConversationManagerState.Starting :
			ShowDialogUI( true ) ;
			break ;
		case ConversationManagerState.WaitEnd :
			break ;
		case ConversationManagerState.WaitForInput :
			if( true == CheckIfPress() )
			{
				PlayNext() ;
			}
			break ;
		case ConversationManagerState.Closing :
			ShowDialogUI( false ) ;
			break ;
		case ConversationManagerState.Close :
			m_State = ConversationManagerState.UnActive ;
			break ;			
		}
	
	}
	
	private bool CheckIfPress()
	{
		bool ret = false ;
		return ret ; 
	}
	
	private void PlayNext()
	{
		ShowDialogUI( true ) ;
		m_State = ConversationManagerState.WaitEnd ;
	}	
	
	private void ShowDialogUI( bool _Show )
	{
		if( false == _Show )
		{

		}
		else
		{

		}
	}
	
	private void CheckQueue()
	{
		if( ConversationManagerState.UnActive != m_State )
			return ;

		if( m_StartingQueue.Count > 0 )
		{
			int retreiveStoryUID = m_StartingQueue[ 0 ] ;
			m_StartingQueue.RemoveAt( 0 ) ;


			Story s = GetStory( retreiveStoryUID ) ;
			if( null != s )
			{
				m_CurrentStoryUID = retreiveStoryUID ;
				m_CurrentTakeUID = s.StartTakeUID ;
				m_State = ConversationManagerState.Starting ;
			}

		}
	}

	Story GetStory( int _UID )
	{
		foreach( Story s in m_Stories )
		{
			if( _UID == s.UID )
			{
				return s ;
			}
		}
		return null ;
	}

	int GetTakeIndex( int _TakeUID )
	{
		for( int i = 0 ; i < m_Takes.Count ; ++i )
		{
			if( _TakeUID == m_Takes[ i ].UID )
			{
				return i ;
			}
		}
		return -1 ;
	}
}


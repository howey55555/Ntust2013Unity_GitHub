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
	public ConversationGUISystem m_ConversationGUISystemSharePointer = null ;
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
		m_ConversationGUISystemSharePointer = this.gameObject.GetComponent<ConversationGUISystem>() ;
		if( null == m_ConversationGUISystemSharePointer )
		{
			Debug.LogError( "null == m_ConversationGUISystemSharePointer" ) ;
		}

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
			m_ConversationGUISystemSharePointer.ShowDialog( false ) ;
			m_ConversationGUISystemSharePointer.ShowPotrait1( false ) ;
			m_ConversationGUISystemSharePointer.ShowPotrait2( false ) ;
		}
		else
		{
			int takeIndex = GetTakeIndex( m_CurrentTakeUID ) ;
			if( takeIndex >= m_Takes.Count )
			{
				// send warning
				Debug.LogError( "null == take" ) ;
				return ;
			}

			Take take = m_Takes[ takeIndex ] ;
			if( null == take )
			{
				// send warning
				Debug.LogError( "null == take" ) ;
				return ;
			}
			else
			{
				// potrait
				if( take.Potraits.Count <= 0 )
				{
					m_ConversationGUISystemSharePointer.ShowPotrait1( false ) ;
					m_ConversationGUISystemSharePointer.ShowPotrait2( false ) ;
				}
				else if( 1 == take.Potraits.Count )
				{
					string p1 = take.Potraits[ 0 ] ;

					m_ConversationGUISystemSharePointer.ShowPotrait1( true ) ;
				}
				else if( 2 == take.Potraits.Count )
				{
					string p1 = take.Potraits[ 0 ] ;
					if( 0 == p1.Length )
					{
						m_ConversationGUISystemSharePointer.ShowPotrait1( false ) ;
					}
					else
					{
						m_ConversationGUISystemSharePointer.ShowPotrait1( true ) ;
					}

					string p2 = take.Potraits[ 1 ] ;
					if( 0 == p2.Length )
					{
						m_ConversationGUISystemSharePointer.ShowPotrait2( false ) ;
					}
					else
					{
						m_ConversationGUISystemSharePointer.ShowPotrait2( true ) ;
					}
				}

				// content
				if( take.Contents.Count <= 0 )
				{
					m_ConversationGUISystemSharePointer.ShowDialog( false ) ;
				}
				else if( 1 == take.Contents.Count )
				{
					string contentStr = take.Contents[ 0 ] ;

					m_ConversationGUISystemSharePointer.ShowDialog( true ) ;
				}
				else if( 2 == take.Contents.Count )
				{
					string content2Str = take.Contents[ 1 ] ;
					
					m_ConversationGUISystemSharePointer.ShowDialog( true ) ;
				}
			}

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


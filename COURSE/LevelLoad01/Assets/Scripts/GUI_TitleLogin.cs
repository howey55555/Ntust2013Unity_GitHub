using UnityEngine;
using System.Collections;

public class GUI_TitleLogin : MonoBehaviour 
{
	AccessPlayerPerfs02 m_PlayerPerfsManager = null ;
	MessageQueueManager01 m_MessageQueueManager = null ;
	public string m_LevelString = "" ;
	
	// Use this for initialization
	void Start () 
	{
		if( null == m_PlayerPerfsManager )
		{
			m_PlayerPerfsManager = this.gameObject.GetComponent<AccessPlayerPerfs02>() ;
		}
		
		if( null == m_MessageQueueManager )
		{
			m_MessageQueueManager = this.gameObject.GetComponent<MessageQueueManager01>() ;
		}
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public Rect m_AccountRect = new Rect( 460 , 390 , 120 , 30 ) ;
	public string m_AccountString = "" ;
	
	public Rect m_PasswordRect = new Rect( 460 , 465 , 120 , 30 ) ;
	public string m_PasswordString = "" ;
	public string m_PasswordStarString = "" ;
	
	public Rect m_LoginButton = new Rect( 460 , 520 , 120 , 30 ) ;
	void OnGUI()
	{
		m_AccountString = GUI.TextField( m_AccountRect , m_AccountString ) ;
		
		m_PasswordStarString = "" ;
		for( int i = 0 ; i < m_PasswordString.Length ; ++i )
		{
			m_PasswordStarString += "*" ;
		}
		m_PasswordStarString = GUI.TextField( m_PasswordRect , m_PasswordStarString ) ;
		if( m_PasswordStarString.Length < m_PasswordString.Length )
		{
			Debug.Log( "m_PasswordString.Remove" ) ;
			m_PasswordString = m_PasswordString.Remove( m_PasswordStarString.Length-1 ) ;
		}
		if( m_PasswordStarString.Length > m_PasswordString.Length )
		{
			m_PasswordString += m_PasswordStarString[ m_PasswordStarString.Length-1 ]  ;
		}		
		
		if( true == GUI.Button( m_LoginButton , "" ) )
		{
			CheckLogin() ;
			
		}
	}
	
	private void CheckLogin()
	{
		if( null == m_PlayerPerfsManager ||
			null == m_MessageQueueManager )
			return ;
		
		if( true == m_PlayerPerfsManager.m_Map.ContainsKey( "PlayerAccount" ) &&
			true == m_PlayerPerfsManager.m_Map.ContainsKey( "PlayerPassword" ) )
		{
			bool passwordCorrect = true ;
			string account = m_PlayerPerfsManager.m_Map[ "PlayerAccount" ] ;
			
			if( 0 == m_AccountString.Length ||
				0 == m_PasswordString.Length )
			{
				m_MessageQueueManager.AddMessage( "Enter valid account and password, please~~." ) ;
			}
			else
			{
				if( 0 == account.Length )
				{
					// no user 
					Debug.Log( "NoUser Set One" ) ;
					// set it by current
					m_PlayerPerfsManager.m_Map[ "PlayerAccount" ] = m_AccountString ;
					m_PlayerPerfsManager.m_Map[ "PlayerPassword" ] = m_PasswordString ;
					m_PlayerPerfsManager.SetPlayerPerfs() ;
				}
				else
				{
					
					// check correct
					if( m_AccountString == m_PlayerPerfsManager.m_Map[ "PlayerAccount" ] &&
						m_PasswordString == m_PlayerPerfsManager.m_Map[ "PlayerPassword" ] )
					{
						Debug.Log( "Check Correct" ) ;
					}
					else
					{
						// show message wrong
	
						m_MessageQueueManager.AddMessage( "Your password is god damn wrong, HAHAHA!!!" ) ;
						passwordCorrect = false ;
					}
				}
			
				if( true == passwordCorrect )
					Application.LoadLevel( m_LevelString ) ;			
			}
		

		}
		
		
	}
	
}

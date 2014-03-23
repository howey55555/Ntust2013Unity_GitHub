/**
 * @file AgentManager.cs
 * @author NDark
 * @date 20140322 . file started.
 */
using UnityEngine;
using System.Collections.Generic;

public class AgentManager : MonoBehaviour 
{
	private Dictionary<string,AgentBase> m_Agents = new Dictionary<string, AgentBase>() ;

	public void RegisterAgent( string _Name , AgentBase _Add )
	{
		m_Agents.Add( _Name , _Add ) ;
	}

	// Use this for initialization
	void Start () 
	{
		// Character_TestObj1
		InfoDataCenter infoDataCenter = GlobalSingleton.GetInfoDataCenter() ;
		infoDataCenter.WriteProperty( "CHARACTER_TestObj1" , "OBJECT_NAME" , "Character_TestObj1" ) ;
		GameObject character_TestObj1 = GameObject.Find( "Character_TestObj1" ) ;
		if( null != character_TestObj1 )
		{
			AgentBase agentComponent = character_TestObj1.AddComponent<AgentBase>() ;
			agentComponent.Name = "TestObj1" ;
		}
		infoDataCenter.WriteProperty( "CHARACTER_TestObj1" , "STATE" , "Condition" ) ;
		infoDataCenter.WriteProperty( "CHARACTER_TestObj1" , "ASSIGNMENT" , "GOTOTARGET" ) ;
		infoDataCenter.WriteProperty( "CHARACTER_TestObj1" , "TARGET_POSITION" , "5,0,0" ) ;
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
}

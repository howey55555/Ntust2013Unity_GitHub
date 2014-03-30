/**
 * @file AgentManager.cs
 * @author NDark
 * @date 20140322 . file started.
 */
// #define ENABLE_TEST
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
#if ENABLE_TEST
		// Character_TestObj1
		InfoDataCenter infoDataCenter = GlobalSingleton.GetInfoDataCenter() ;
		infoDataCenter.WriteProperty( "CHARACTER_TestObj1" , "OBJECT_NAME" , "Character_TestObj1" ) ;
		GameObject character_TestObj1 = GameObject.Find( "Character_TestObj1" ) ;
		if( null != character_TestObj1 )
		{
			AgentBase agentComponent = character_TestObj1.AddComponent<AgentBase>() ;
			agentComponent.AgentName = "TestObj1" ;
		}
		infoDataCenter.WriteProperty( "CHARACTER_TestObj1" , "STATE" , "Condition" ) ;
		infoDataCenter.WriteProperty( "CHARACTER_TestObj1" , "ASSIGNMENT" , "WaitSec" ) ;
		infoDataCenter.WriteProperty( "CHARACTER_TestObj1" , "WAITSEC" , "5" ) ;
#endif // #if ENABLE_TEST
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
}

/**
 * @file AgentBase.cs
 * @author NDark
 * @date 20140322 . file started.
 */
using UnityEngine;
using System.Collections;

public enum AgentState
{
	Condition,
	Action,
	Fighting,
}



public class AgentBase: MonoBehaviour 
{
	public string Name
	{
		get { return m_Name ; } 
		set { m_Name = value ; }
	}
	private string m_Name = "" ;
	
	public bool IsValid
	{
		get { return m_IsValid ; }
		set { m_IsValid = value ; }
	}
	private bool m_IsValid = true ;

	public AgentState State
	{
		get 
		{ 
			return ReadAgentState() ; 
		}
		set 
		{ 
			WriteAgentState( value ) ;
			m_AgentState = value ; 
		}
	}
	private AgentState m_AgentState = AgentState.Condition ;

	public ConditionBase Condition
	{
		get { return m_Condition ; }
		set { m_Condition = value ; }
	}
	private ConditionBase m_Condition = null ;
	
	public ActionBase Action
	{
		get { return m_Action ; }
		set { m_Action = value ; }
	}
	private ActionBase m_Action = null ;

	// Use this for initialization
	void Start () 
	{
		GlobalSingleton.GetAgentManager().RegisterAgent( Name , this ) ;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if( false == m_IsValid )
			return  ;
		DoUpdate() ;
	}



	public virtual void DoUpdate()
	{
		ReadAgentState() ;
		switch( m_AgentState )
		{
		case AgentState.Condition :
			DoCondition() ;
			break ;
		case AgentState.Action :
			DoAction() ;
			break ;
		case AgentState.Fighting :
			break ;	
		}
	}

	private AgentState ReadAgentState()
	{
		// read from property
		InfoDataCenter infoDataCenter = GlobalSingleton.GetInfoDataCenter() ;
		string stateStr = infoDataCenter.ReadProperty( "CHARACTER_" + Name , "STATE" ) ;
		m_AgentState = AgentStateFromStr( stateStr ) ;
		return m_AgentState ;
	}

	private void WriteAgentState( AgentState _Set )
	{
		// Set to property
		InfoDataCenter infoDataCenter = GlobalSingleton.GetInfoDataCenter() ;
		infoDataCenter.WriteProperty( "CHARACTER_" + Name , "STATE" , _Set.ToString() ) ;
		m_AgentState = _Set ;
	}

	protected virtual void DoCondition()
	{
		if( null != m_Condition )
		{
			m_Condition.DoCondition() ;
		}
		// Do Check if too far from target
		{
			InfoDataCenter infoDataCenter = GlobalSingleton.GetInfoDataCenter() ;
			string currentObjectName = infoDataCenter.ReadProperty( "CHARACTER_TestObj1" , "OBJECT_NAME" ) ;
			GameObject currentObject = GameObject.Find( currentObjectName ) ;

			string assignmentStr = infoDataCenter.ReadProperty( "CHARACTER_TestObj1" , "ASSIGNMENT" ) ;
			if( "GOTOTARGET" == assignmentStr )
			{
				string targetPositionStr = infoDataCenter.ReadProperty( "CHARACTER_TestObj1" , "TARGET_POSITION" ) ;
				Vector3 targetPositon = Vector3FromFromStr( targetPositionStr ) ;
				Vector3 currentPosistion = currentObject.transform.position ;
				float distanceToTarget = Vector3.Distance( targetPositon , currentPosistion ) ;
				if( distanceToTarget > 1 )
				{
					// replace correct action object
					WriteAgentState( AgentState.Action ) ;
				}
			}
		}
	}

	protected virtual void DoAction()
	{

		if( null != m_Action )
		{
			m_Action.DoAction() ;
		}
		// Do From GOTOTARGET Current Position to ( 5 , 0 , 0 )
		{
			InfoDataCenter infoDataCenter = GlobalSingleton.GetInfoDataCenter() ;
			string currentObjectName = infoDataCenter.ReadProperty( "CHARACTER_TestObj1" , "OBJECT_NAME" ) ;
			GameObject currentObject = GameObject.Find( currentObjectName ) ;
			string assignmentStr = infoDataCenter.ReadProperty( "CHARACTER_TestObj1" , "ASSIGNMENT" ) ;
			if( "GOTOTARGET" == assignmentStr )
			{
				string targetPositionStr = infoDataCenter.ReadProperty( "CHARACTER_TestObj1" , "TARGET_POSITION" ) ;
				Vector3 targetPositon = Vector3FromFromStr( targetPositionStr ) ;
				Vector3 currentPosistion = currentObject.transform.position ;
				Vector3 distanceVec = targetPositon - currentPosistion ;
				float distanceToTarget = distanceVec.magnitude ;
				if( distanceToTarget > 1 )
				{
					// keep going
					Rigidbody2D r2d = currentObject.rigidbody2D ;
					if( null != r2d )
					{
						r2d.AddForce( distanceVec ) ;
					}
				}
				else
				{
					Rigidbody2D r2d = currentObject.rigidbody2D ;
					r2d.velocity = Vector2.zero ;
					WriteAgentState( AgentState.Condition ) ;
				}
			}
		}
	}

	public static void AgentStateToStr()
	{
		
	}
	
	public static AgentState AgentStateFromStr( string _Str )
	{
		if( _Str == AgentState.Condition.ToString() )
		{
			return AgentState.Condition ;
		}
		else if( _Str == AgentState.Action.ToString() )
		{
			return AgentState.Action ;
		}
		else // if( _Str == AgentState.Fighting.ToString() )
		{
			return AgentState.Fighting ;
		}
	}

	public static Vector3 Vector3FromFromStr( string _Str )
	{
		Vector3 ret = Vector3.zero ;
		string [] splitor = { "," } ;
		string [] strVec = _Str.Split( splitor , System.StringSplitOptions.RemoveEmptyEntries ) ;
		if( strVec.Length >= 3 )
		{
			float x = 0 ;
			float y = 0 ;
			float z = 0 ;
			if( true == float.TryParse( strVec[ 0 ] , out x ) )
			{
				ret.x = x ;
			}
			if( true == float.TryParse( strVec[ 1 ] , out y ) )
			{
				ret.y = y ;
			}
			if( true == float.TryParse( strVec[ 2 ] , out z ) )
			{
				ret.z = z ;
			}
		}
		return ret ;
	}
}




















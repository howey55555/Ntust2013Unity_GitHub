/**
 * @file Agent_GotoGladiatores.cs
 * @author NDark
 * @date 20140330 . file started.
 */
using UnityEngine;

public class Agent_GotoGladiatores : AgentBase 
{
	private string oName = "MainCharacter" ;
	private string aName = "MainCharacter" ;
	private string aCategory = "CHARACTER_MainCharacter" ;

	private float closeDistance = 0.01f ;
	private int m_TargetObjectIndex = 0 ;
	private string []m_TargetObjectNames = 
	{
		"Anchor01" ,
		"Anchor02" ,
		"Anchor03" ,
		"Anchor04" ,
		"Anchor05" ,
		"Anchor06" ,
		"Anchor07" ,
		"Anchor08" ,
		"Anchor09" ,
		"Anchor10" ,
		"Anchor11" ,
		"Anchor12" ,
		"Anchor13" ,
		"Anchor14" ,
		"Anchor15" ,
		"Anchor16" ,
		"Anchor17" ,
		"Anchor18" ,
		"Anchor19" ,
		"Anchor20" ,
		"Anchor21" ,
		"Anchor22" ,
		"Anchor23" ,
		"Anchor24" ,
		"Anchor25" ,
		"Anchor26" ,

	} ;

	// Use this for initialization
	void Start () 
	{
		this.AgentName = aName ;

		AgentStart() ;

		// MainCharacter
		InfoDataCenter infoDataCenter = GlobalSingleton.GetInfoDataCenter() ;
		infoDataCenter.WriteProperty( aCategory , "OBJECT_NAME" , oName ) ;
		infoDataCenter.WriteProperty( aCategory , "STATE" , "Condition" ) ;
		infoDataCenter.WriteProperty( aCategory , "ASSIGNMENT" , "GoToTarget" ) ;

	}
	
	// Update is called once per frame
	void Update () 
	{
		AgentUpdate() ;
	}

	protected override void DoCondition()
	{
		Conditon_DoGoToGladiatores() ;
	}
	
	protected override void DoAction()
	{
		Action_DoGoToGladiatores() ;
	}

	
	private void Conditon_DoGoToGladiatores()
	{
		InfoDataCenter infoDataCenter = GlobalSingleton.GetInfoDataCenter() ;
		string currentObjectName = infoDataCenter.ReadProperty( aCategory , "OBJECT_NAME" ) ;
		GameObject currentObject = GameObject.Find( currentObjectName ) ;
		
		string assignmentStr = infoDataCenter.ReadProperty( aCategory , "ASSIGNMENT" ) ;
		
		if( "GoToTarget" == assignmentStr )
		{
			// Debug.Log( "Conditon_DoGoToGladiatores():GoToTarget" ) ;

			bool FindNextTargetObject = false ;

			string targetPositionStr = infoDataCenter.ReadProperty( aCategory , "TARGET_POSITION" ) ;
			if( 0 == targetPositionStr.Length )
			{
				string targetObjectNameStr = infoDataCenter.ReadProperty( aCategory , "TARGET_OBJECT_NAME" ) ;
				if( 0 == targetObjectNameStr.Length )
				{
					FindNextTargetObject = true ;
				}
				else
				{
					GameObject obj = GameObject.Find( targetObjectNameStr ) ;
					targetPositionStr = string.Format( "{0},{1},{2}" , 
					                                  obj.transform.position.x , obj.transform.position.y , obj.transform.position.z ) ;
					// Debug.Log( "Conditon_DoGoToGladiatores():targetPositionStr=" + targetPositionStr ) ;
					infoDataCenter.WriteProperty( aCategory , "TARGET_POSITION" , targetPositionStr ) ;
				}
			}

			if( 0 != targetPositionStr.Length )
			{
				Vector3 targetPositon = Vector3FromFromStr( targetPositionStr ) ;
				Vector3 currentPosistion = currentObject.transform.position ;
				targetPositon.z = currentPosistion.z ;
				float distanceToTarget = Vector3.Distance( targetPositon , currentPosistion ) ;
				if( distanceToTarget > closeDistance )
				{
					// replace correct action object
					WriteAgentState( AgentState.Action ) ;
				}
				else
				{
					FindNextTargetObject = true ;
				}
			}

			if( true == FindNextTargetObject )
			{
				// next target position

				if( m_TargetObjectIndex >= m_TargetObjectNames.Length )
				{
					Debug.Log( "Conditon_DoGoToGladiatores():m_TargetObjectIndex >= m_TargetObjectNames.Length" ) ;
					infoDataCenter.WriteProperty( aCategory , "ASSIGNMENT" , "Wait" ) ;
				}
				else
				{

					string targetName = m_TargetObjectNames[ m_TargetObjectIndex ] ;
					Debug.Log( "Conditon_DoGoToGladiatores():targetName=" + targetName ) ;
					infoDataCenter.WriteProperty( aCategory , "TARGET_OBJECT_NAME" , targetName ) ;
					infoDataCenter.WriteProperty( aCategory , "TARGET_POSITION" , "" ) ;
					++m_TargetObjectIndex ;	
				}
			}
		}
	}
	
	private void Action_DoGoToGladiatores()
	{
		InfoDataCenter infoDataCenter = GlobalSingleton.GetInfoDataCenter() ;
		string currentObjectName = infoDataCenter.ReadProperty( aCategory , "OBJECT_NAME" ) ;
		GameObject currentObject = GameObject.Find( currentObjectName ) ;
		if( null == currentObject )
		{
			Debug.Log( "null == currentObject") ;
			return;
		}

		string assignmentStr = infoDataCenter.ReadProperty( aCategory , "ASSIGNMENT" ) ;

		if( "GoToTarget" == assignmentStr )
		{
			string targetPositionStr = infoDataCenter.ReadProperty( aCategory , "TARGET_POSITION" ) ;
			Vector3 targetPositon = Vector3FromFromStr( targetPositionStr ) ;


			Vector3 currentPosistion = currentObject.transform.position ;
			targetPositon.z = currentPosistion.z ;

			Vector3 distanceVec = targetPositon - currentPosistion ;
			float distanceToTarget = distanceVec.magnitude ;
//			Debug.Log( "Action_DoGoToGladiatores()::targetPositionStr=" + targetPositionStr ) ;
//			Debug.Log( "Action_DoGoToGladiatores()::currentPosistion=" + currentPosistion ) ;
//			Debug.Log( "Action_DoGoToGladiatores()::distanceVec=" + distanceVec ) ;
			if( distanceToTarget > closeDistance )
			{
				// keep going
				Rigidbody2D r2d = currentObject.rigidbody2D ;
				if( null != r2d )
				{
					distanceVec.Normalize() ;
					if( r2d.velocity.magnitude < 0.2f )
						r2d.AddForce( distanceVec * 0.1f ) ;
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

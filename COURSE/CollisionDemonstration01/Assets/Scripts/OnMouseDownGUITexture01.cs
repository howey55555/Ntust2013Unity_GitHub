using UnityEngine;
using System.Collections;

public class OnMouseDownGUITexture01 : MonoBehaviour 
{
	public GameObject m_TargetObj = null ;

	// Use this for initialization
	void Start () 
	{
	}
	
	// Update is called once per frame
	void Update () 
	{	
	}
	
	void OnMouseDown()
	{
		Debug.Log( "OnMouseDown" ) ;
		// return ;
		if( null != m_TargetObj )
		{
			Component script = m_TargetObj.GetComponent( "RotateAroundLocalX" ) ;
			if( null == script )
				m_TargetObj.AddComponent( "RotateAroundLocalX" ) ;
			else
				Component.Destroy( script ) ;
		}
	}
}

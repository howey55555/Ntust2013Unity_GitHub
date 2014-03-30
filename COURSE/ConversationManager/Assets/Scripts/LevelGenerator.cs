/*
@file LevelGenerator.cs
@author NDark
@date 20140329 file started.
*/
#define USE_XML
using UnityEngine;
using System.Collections.Generic;
using System.Xml;

public class LevelGenerator : MonoBehaviour 
{
	public string LevelFilepath 
	{
		get { return m_LevelFilepath ; } 
		set { m_LevelFilepath = value ;}
	}
	private string m_LevelFilepath = "" ;


	// Use this for initialization
	void Start () 
	{
		RetrieveLevelFilepath () ;
		LoadLevel( m_LevelFilepath ) ;
		GenerateObjectFromLevelData() ;
	}
	
	// Update is called once per frame
	void Update () 
	{
	}

	private void RetrieveLevelFilepath()
	{
		m_LevelFilepath = "TestLevel" ;
	}

	public void LoadLevel( string _LevelFilepath )
	{
		Debug.Log( "_LevelFilepath=" + _LevelFilepath ) ;
		if( 0 == _LevelFilepath.Length )
		{
			// warning
			Debug.LogWarning( "_LevelFilepath=" + _LevelFilepath ) ;
			return ;
		}

		TextAsset ta = Resources.Load<TextAsset>( _LevelFilepath );
		if( null == ta )
		{
			Debug.LogError( "LoadLevel() null == ta" ) ;
			return ;
		}
#if USE_XML
		XmlDocument doc = new XmlDocument() ;
		doc.LoadXml( ta.text ) ;
		XmlNode root = doc.FirstChild ;

		if( null == root )
		{
			Debug.LogError( "LoadLevel() : null == root" ) ;
			return ;
		}
		
		// string levelname = root.Attributes[ "name" ].Value ;
		if( false == root.HasChildNodes )
		{
			Debug.Log( "LoadLevel() : false == root.HasChildNodes" ) ;
			return ;			
		}
#endif // USE_XML

#if USE_XML
		for( int i = 0 ; i < root.ChildNodes.Count ; ++i )
		{
			XmlNode unitNode = root.ChildNodes[ i ] ;
#endif // USE_XML
			string unitName ;
			string prefabTemplateName ;
			string unitDataTemplateName ;
			string raceName ;
			string sideName ;
			Vector3 position = Vector3.zero ;
			PosAnchor posAnchor = null ;
			Quaternion orientation ;
			Dictionary<string,string> supplemental ;
			string textureName = "";

			// Debug.Log( "LoadLevel() : unitNode.Name=" + unitNode.Name ) ;
			
			if( -1 != unitNode.Name.IndexOf( "comment" ) )
			{
				// comment
			}
			else if( true == XMLParseLevelUtility.ParseUnit( unitNode , 
			                                                out unitName , 
			                                                out prefabTemplateName , 
			                                                out unitDataTemplateName ,
			                                                out raceName ,
			                                                out sideName ,				
			                                                out posAnchor , 
			                                                out orientation ,
			                                                out supplemental ) )
			{

			}
			else if( true == ParseUtility.ParseMapZoneObject( unitNode,
		                                                     ref unitName ,
		                                                     ref prefabTemplateName , 
		                                                     ref position , 
		                                                     ref orientation , 
			                                                 ref textureName ) )
			{
				GameObject obj = PrefabInstantiate.CreateByInit( prefabTemplateName , 
				                                                unitName , 
				                                                position , 
				                                                orientation ) ;
				obj.renderer.material = new Material( obj.renderer.material ) ;
				Texture tex = ResourceLoad.LoadTexture( textureName ) ;
				if( null == tex )
				{

				}
				else
				{
					obj.renderer.material.mainTexture = tex ;
				}
			}
#if USE_XML
		}
#endif // USE_XML			

	}

	private void GenerateObjectFromLevelData()
	{

	}

}

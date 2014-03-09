/**
 * @file ConversationGUISystem.cs
 * @author NDark
 * @date20140309 file started.
 */
using UnityEngine;

public class ConversationGUISystem : MonoBehaviour 
{

	private GameObject Dialog = null ;
	private GameObject Potrait1 = null ;
	private GameObject Potrait2 = null ;

	public void ShowDialog( bool _Show ) 
	{
		if( null == Dialog )
			return ;

		SpriteRenderer sr = Dialog.GetComponent<SpriteRenderer>() ;
		if( null != sr )
		{
			sr.enabled = _Show ;
		}
	}

	public void ShowPotrait1( bool _Show ) 
	{
		if( null == Potrait1 )
			return ;

		SpriteRenderer sr = Potrait1.GetComponent<SpriteRenderer>() ;
		if( null != sr )
		{
			sr.enabled = _Show ;
		}
	}

	public void ShowPotrait2( bool _Show ) 
	{
		if( null == Potrait2 )
			return ;

		SpriteRenderer sr = Potrait2.GetComponent<SpriteRenderer>() ;
		if( null != sr )
		{
			sr.enabled = _Show ;
		}
	}
	
	public void SetContent( string _SpriteLabel , string _Content ) 
	{
		if( null == Dialog )
			return ;
		
		SpriteRenderer sr = Dialog.GetComponent<SpriteRenderer>() ;
		if( null != sr )
		{
			Sprite changeSprite = Resources.Load<Sprite>( _SpriteLabel ) ;
			sr.sprite = changeSprite ;
		}

		// set text
	}

	public void SetPotrait1( string _SpriteLabel ) 
	{
		if( null == Dialog )
			return ;
		
		SpriteRenderer sr = Potrait1.GetComponent<SpriteRenderer>() ;
		if( null != sr )
		{
			Sprite changeSprite = Resources.Load<Sprite>( _SpriteLabel ) ;
			sr.sprite = changeSprite ;
		}
	}

	public void SetPotrait2( string _SpriteLabel ) 
	{
		if( null == Dialog )
			return ;
		
		SpriteRenderer sr = Potrait2.GetComponent<SpriteRenderer>() ;
		if( null != sr )
		{
			Sprite changeSprite = Resources.Load<Sprite>( _SpriteLabel ) ;
			sr.sprite = changeSprite ;
		}
	}

	// Use this for initialization
	void Start () 
	{
		Dialog = GameObject.Find( "Dialog" ) ;
		if( null == Dialog )
		{
			Debug.LogError( "null == Dialog" ) ;
		}

		Potrait1 = GameObject.Find( "Potrait1" ) ;
		if( null == Potrait1 )
		{
			Debug.LogError( "null == Potrait1" ) ;
		}

		Potrait2 = GameObject.Find( "Potrait2" ) ;
		if( null == Potrait2 )
		{
			Debug.LogError( "null == Potrait2" ) ;
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
}

/*
@file DrivingCar01.cs
@author NDark
@date 20131005 file started.
*/
using UnityEngine;

public class DrivingCar01 : MonoBehaviour 
{
	public WheelCollider m_WheelCollderFR = null ;
	public WheelCollider m_WheelCollderFL = null ;
	public GameObject m_WheelModelFR = null ;
	public GameObject m_WheelModelFL = null ;
	
	Quaternion m_OrgRotationWheelRF = Quaternion.identity ;
	Quaternion m_OrgRotationWheelLF = Quaternion.identity ;
	
	// Use this for initialization
	void Start () 
	{
		if(	null != m_WheelModelFR &&
			null != m_WheelModelFL )
		{
			m_OrgRotationWheelRF = m_WheelModelFR.transform.rotation ;
			m_OrgRotationWheelLF = m_WheelModelFL.transform.rotation ;
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		if( null != m_WheelCollderFR &&
			null != m_WheelCollderFL &&
			null != m_WheelModelFR &&
			null != m_WheelModelFL )
		{
			m_WheelCollderFR.motorTorque = 260 * Input.GetAxis("Vertical") ;
			m_WheelCollderFL.motorTorque = 260 * Input.GetAxis("Vertical") ;
		
			m_WheelCollderFR.steerAngle = 10 * Input.GetAxis("Horizontal") ;
			
			m_WheelModelFR.transform.rotation = m_OrgRotationWheelRF *
				Quaternion.AngleAxis( -1 * m_WheelCollderFR.steerAngle , m_WheelModelFR.transform.up ) ;
			
			m_WheelCollderFL.steerAngle = 10 * Input.GetAxis("Horizontal") ;
			
			m_WheelModelFL.transform.rotation = m_OrgRotationWheelLF *
				Quaternion.AngleAxis( -1 * m_WheelCollderFL.steerAngle , m_WheelModelFL.transform.up ) ;
			
		}
	}
}

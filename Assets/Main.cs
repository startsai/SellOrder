using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Main : MonoBehaviour 
{
	Text m_TargetText;
	public RectTransform m_DisplayPanelRTF;
	public GameObject m_ItemListRes;

	void Awake()
	{
		for(int i = 0;i<11;i++)
		{
			GameObject GOTS = GameObject.Instantiate(m_ItemListRes) as GameObject;
			GOTS.transform.parent = m_DisplayPanelRTF.transform;
		}
	}

	public void onClearInput()
	{
		if(m_TargetText!=null)
			m_TargetText.text = "";
	}
	public void onInputMsg(string msg)
	{
		if(m_TargetText!=null)
			m_TargetText.text += msg;
	}

}

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Main : MonoBehaviour 
{
	Text m_TargetText;
	public int m_nListSize = 20;
	public RectTransform m_DisplayPanelRTF;
	public GameObject m_ItemListRes;
	public static Main m_Instance = null;

	private ItemListBehavior m_TargetILB = null;	
	private OrderUnit m_TargetOrderUnit = null;
	void Awake()
	{
		m_Instance = this;
		for(int i = 0;i<m_nListSize;i++)
		{
			GameObject GOTS = GameObject.Instantiate(m_ItemListRes) as GameObject;
			OrderUnit ou = new OrderUnit();
			ItemListBehavior ILB = GOTS.GetComponent<ItemListBehavior>();
			ILB.setId(i+1);
			ILB.setOrderUnit(ou);
			ILB.refresh();
			GOTS.transform.parent = m_DisplayPanelRTF.transform;
		}
	}

	public void setTarget(ItemListBehavior ILB)
	{
		if(m_TargetILB !=null && m_TargetILB != ILB)
		{
			m_TargetILB.clearHighLight();
		}
		m_TargetILB = ILB;
	}

	public void onClearInput()
	{
		if(m_TargetILB!=null)
		{
			m_TargetILB.onClearValue();
		}
	}

	public void onInputMsg(string msg)
	{
		if(m_TargetILB!=null)
		{
			m_TargetILB.onInputValue(msg);
		}
	}

}

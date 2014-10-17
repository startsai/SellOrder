using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class ItemListBehavior : MonoBehaviour 
{
	public Text m_Id = null;
	public Button m_ItemNameButton = null;
	public Button m_PriceButton = null;
	public Button m_WeightButton = null;
	public Button m_TotalPriceButton = null;

	private Text m_ItemNameText = null;
	private Text m_PriceText = null;
	private Text m_WeightText = null;
	private Text m_TotalPriceText = null;
	private Color m_OriginColor = Color.white;
	private Color m_HilighColor = new Color(0.1f,1.0f,0.1f,0.4f);
	private Color m_DisableColor = new Color(1.0f,0.1f,0.1f,0.4f);
	private OrderUnit m_OrderUnit = null;

	private Button m_SelectedButton = null;
	public void setOrderUnit(OrderUnit OU)
	{
		m_OrderUnit = OU;
	}

	public void setId(int id)
	{
		m_Id.text = id.ToString().PadLeft(2,'0');
	}

	void Awake()
	{
		m_OriginColor = new Color(m_ItemNameButton.image.color.r,
		                          m_ItemNameButton.image.color.g,
		                          m_ItemNameButton.image.color.b,
		                          m_ItemNameButton.image.color.a);

		m_ItemNameButton.onClick.AddListener(onItemNameButtonClick);
		m_ItemNameText = m_ItemNameButton.transform.Find("Name").GetComponent<Text>();

		m_PriceButton.onClick.AddListener(onPriceButtonClick);
		m_PriceText = m_PriceButton.GetComponentInChildren<Text>();

		m_WeightButton.onClick.AddListener(onWeightButtonClick);
		m_WeightText = m_WeightButton.GetComponentInChildren<Text>();

		m_TotalPriceButton.onClick.AddListener(onTotalPriceButtonClick);
		m_TotalPriceText = m_TotalPriceButton.GetComponentInChildren<Text>();

	}
	void OnDestroy()
	{
		m_ItemNameButton.onClick.RemoveListener(onItemNameButtonClick);
		m_PriceButton.onClick.RemoveListener(onPriceButtonClick);
		m_WeightButton.onClick.RemoveListener(onWeightButtonClick);
		m_TotalPriceButton.onClick.RemoveListener(onTotalPriceButtonClick);
	}

	public void onTotalPriceButtonClick()
	{
		setSelectedButton(m_TotalPriceButton);
		m_OrderUnit.setFocus(OrderUnit.EFocusTarget.eTotalPrice);
		Main.m_Instance.setTarget(this);
	}

	public void onWeightButtonClick()
	{
		setSelectedButton(m_WeightButton);
		m_OrderUnit.setFocus(OrderUnit.EFocusTarget.eWeight);
		Main.m_Instance.setTarget(this);
	}

	public void onPriceButtonClick()
	{
		setSelectedButton(m_PriceButton);
		m_OrderUnit.setFocus(OrderUnit.EFocusTarget.ePrice);
		Main.m_Instance.setTarget(this);
	}

	public void onItemNameButtonClick()
	{
		setSelectedButton(m_ItemNameButton);
		m_OrderUnit.setFocus(OrderUnit.EFocusTarget.eItemName);
		Main.m_Instance.setTarget(this);
	}

	public void onInputValue(string stValue)
	{
		m_OrderUnit.inputMethod(stValue);
		refresh();
	}

	public void onClearValue()
	{
		m_OrderUnit.clearValue();
		refresh();
	}

	public void refresh()
	{
		m_ItemNameText.text = m_OrderUnit.m_Name;
		m_PriceText.text = m_OrderUnit.m_PriceDisplay;
		m_WeightText.text = m_OrderUnit.m_WeightDisplay;
		m_TotalPriceText.text = m_OrderUnit.m_TotalPriceDisplay;
	}

	public void clearHighLight()
	{
		m_SelectedButton.image.color = m_OriginColor;
	}

	private void setSelectedButton(Button btn)
	{
		if(m_SelectedButton != null){
			m_SelectedButton.image.color = m_OriginColor;
		}
		m_SelectedButton = btn;
		m_SelectedButton.image.color = m_HilighColor;
	}
}





public class OrderUnit
{
	public enum EFocusTarget
	{
		eItemName = 0,
		ePrice = 1,
		eWeight = 2,
		eTotalPrice = 3
	}
	private const int WEIGHT_UNIT = 600;
	private const float BASIC_TRADE_UNIT = 3.75f;
	private EFocusTarget m_eFocusTarget;
	private const int MAX_LOADED_GRAM= 60000;
	
	private int m_weightModeIndex = 0;
	private int[] m_weightOnDisplay = new int[]{0,0,0};
	public OrderUnit()
	{
		m_Name = "ItemName";
		m_Price = 0;
		m_Weight = 0;
		refreshDisplay();
	}
	
	
	public string m_Name;
	public int m_Price;
	public float m_Weight;
	public int m_TotalPrice;
	public string m_WeightDisplay;
	public string m_PriceDisplay;
	public string m_TotalPriceDisplay;

	public void setFocus(EFocusTarget ft)
	{
		m_eFocusTarget = ft;
	}
	
	public void refreshDisplay()
	{
		int displayWeight = (int)(m_Weight / BASIC_TRADE_UNIT);
		
		int first = displayWeight / 160;
		int calSecond = displayWeight - first * 160;
		int second = calSecond /10;
		int third = calSecond - second * 10;
		m_WeightDisplay = string.Format("{0}.{1}.{2}",first,second,third);

		m_WeightDisplay = m_WeightDisplay.ToString().PadLeft (7,' ');
		m_PriceDisplay = m_Price.ToString().PadLeft (3,' ');
		m_TotalPriceDisplay = m_TotalPrice.ToString().PadLeft (6,' ');
	}

	public void refreshDisplay(EFocusTarget eTarget)
	{
		switch(eTarget)
		{
		case EFocusTarget.ePrice:
			m_TotalPrice = (int)((float)m_Price / (float)WEIGHT_UNIT * m_Weight);
			break;
		case EFocusTarget.eTotalPrice:
			if(m_Price > 0){
				m_Weight = ((float)m_TotalPrice / ((float)m_Price/(float)WEIGHT_UNIT));
			}
			break;
		case EFocusTarget.eWeight:
			m_TotalPrice = (int)((float)m_Price / (float)WEIGHT_UNIT * m_Weight);
			break;
		}
		refreshDisplay();
	}
	
	public void clearValue()
	{
		switch(m_eFocusTarget)
		{
		case EFocusTarget.ePrice:
			m_Price = 0;
			break;
		case EFocusTarget.eWeight:
			m_Weight = 0f;
			for(int i = 0 ;i<m_weightOnDisplay.Length;i++){
				m_weightOnDisplay[i] = 0;
			}
			m_weightModeIndex = 0;
			break;
		case EFocusTarget.eTotalPrice:
			m_TotalPrice = 0;
			break;
		}
		refreshDisplay(m_eFocusTarget);
	}
	
	public void inputMethod(string value)
	{
		int nValue = 0;
		switch(m_eFocusTarget)
		{
		case EFocusTarget.eItemName:
			m_Name = value;
			break;
		case EFocusTarget.ePrice:
			if(int.TryParse(value,out nValue))
			{
				priceInput(nValue);
			}
			break;
		case EFocusTarget.eWeight:
			if(value == ".")
			{
				weightInput(-1);
			}
			else if(int.TryParse(value,out nValue))
			{
				weightInput(nValue);
			}
			break;
		case EFocusTarget.eTotalPrice:
			if(int.TryParse(value,out nValue))
			{
				totalPriceInput(nValue);
			}
			break;
		}
		refreshDisplay(m_eFocusTarget);
	}
	
	private void priceInput(int value)
	{
		if(this.m_Price < 100)
		{
			m_Price = (this.m_Price * 10) + value;
		}
	}
	
	private void totalPriceInput(int value)
	{
		if(m_Price > 0 || m_Weight >0f) // Price or weight must bigger than zero to figure out total price.
		{
			if(this.m_TotalPrice < 1000)
			{
				m_TotalPrice = this.m_TotalPrice * 10 + value;
			}
		}
	}
	
	private void weightInput(int value)
	{
		if(value >=0)
		{
			if(m_Weight < MAX_LOADED_GRAM)
			{
				int afterValue = 0;
				switch(m_weightModeIndex)
				{
				case 0:
					m_weightOnDisplay[0] = m_weightOnDisplay[0] * 10  +value;
					break;
				case 1:
					afterValue =  m_weightOnDisplay[1] * 10  +value;
					if(afterValue >15)
					{
						afterValue = value;
					}
					m_weightOnDisplay[1] = afterValue;
					break;
				case 2:
					afterValue =  m_weightOnDisplay[2] * 10  +value;
					if(afterValue >9)
					{
						afterValue = value;
					}
					m_weightOnDisplay[2] = afterValue;
					break;
				}
			}
			m_Weight = 	(float)m_weightOnDisplay[0] * (float)WEIGHT_UNIT + 
						(float)m_weightOnDisplay[1] * 10.0f * BASIC_TRADE_UNIT + 
						(float)m_weightOnDisplay[2] * BASIC_TRADE_UNIT;
		}
		else
		{
			if(m_weightModeIndex < m_weightOnDisplay.Length)
			{
				m_weightModeIndex ++;
			}
		}
	}
}

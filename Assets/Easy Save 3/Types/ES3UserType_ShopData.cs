using System;
using UnityEngine;

namespace ES3Types
{
	[UnityEngine.Scripting.Preserve]
	[ES3PropertiesAttribute("stocks", "soldOuts")]
	public class ES3UserType_ShopData : ES3ComponentType
	{
		public static ES3Type Instance = null;

		public ES3UserType_ShopData() : base(typeof(ShopData)){ Instance = this; priority = 1;}


		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			var instance = (ShopData)obj;
			
			writer.WriteProperty("stocks", instance.stocks, ES3Internal.ES3TypeMgr.GetOrCreateES3Type(typeof(System.Collections.Generic.List<Item>)));
			writer.WriteProperty("soldOuts", instance.soldOuts, ES3Type_boolArray.Instance);
		}

		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			var instance = (ShopData)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
					case "stocks":
						instance.stocks = reader.Read<System.Collections.Generic.List<Item>>();
						break;
					case "soldOuts":
						instance.soldOuts = reader.Read<System.Boolean[]>(ES3Type_boolArray.Instance);
						break;
					default:
						reader.Skip();
						break;
				}
			}
		}
	}


	public class ES3UserType_ShopDataArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3UserType_ShopDataArray() : base(typeof(ShopData[]), ES3UserType_ShopData.Instance)
		{
			Instance = this;
		}
	}
}
using System;
using UnityEngine;

namespace ES3Types
{
	[UnityEngine.Scripting.Preserve]
	[ES3PropertiesAttribute("coins")]
	public class ES3UserType_CoinManager : ES3ComponentType
	{
		public static ES3Type Instance = null;

		public ES3UserType_CoinManager() : base(typeof(CoinManager)){ Instance = this; priority = 1;}


		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			var instance = (CoinManager)obj;
			
			writer.WriteProperty("coins", instance.coins, ES3Type_int.Instance);
		}

		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			var instance = (CoinManager)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
					case "coins":
						instance.coins = reader.Read<System.Int32>(ES3Type_int.Instance);
						break;
					default:
						reader.Skip();
						break;
				}
			}
		}
	}


	public class ES3UserType_CoinManagerArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3UserType_CoinManagerArray() : base(typeof(CoinManager[]), ES3UserType_CoinManager.Instance)
		{
			Instance = this;
		}
	}
}
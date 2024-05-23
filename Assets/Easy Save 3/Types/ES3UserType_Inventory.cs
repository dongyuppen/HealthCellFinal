using System;
using UnityEngine;

namespace ES3Types
{
	[UnityEngine.Scripting.Preserve]
	[ES3PropertiesAttribute("instance", "items")]
	public class ES3UserType_Inventory : ES3ComponentType
	{
		public static ES3Type Instance = null;

		public ES3UserType_Inventory() : base(typeof(Inventory)){ Instance = this; priority = 1;}


		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			var instance = (Inventory)obj;
			
			writer.WritePropertyByRef("instance", Inventory.instance);
			writer.WriteProperty("items", instance.items, ES3Internal.ES3TypeMgr.GetOrCreateES3Type(typeof(System.Collections.Generic.List<Item>)));
		}

		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			var instance = (Inventory)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
					case "instance":
						Inventory.instance = reader.Read<Inventory>();
						break;
					case "items":
						instance.items = reader.Read<System.Collections.Generic.List<Item>>();
						break;
					default:
						reader.Skip();
						break;
				}
			}
		}
	}


	public class ES3UserType_InventoryArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3UserType_InventoryArray() : base(typeof(Inventory[]), ES3UserType_Inventory.Instance)
		{
			Instance = this;
		}
	}
}
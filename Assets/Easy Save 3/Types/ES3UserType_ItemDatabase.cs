using System;
using UnityEngine;

namespace ES3Types
{
	[UnityEngine.Scripting.Preserve]
	[ES3PropertiesAttribute("instance", "money", "itemDB", "fieldItemPrefab", "pos")]
	public class ES3UserType_ItemDatabase : ES3ComponentType
	{
		public static ES3Type Instance = null;

		public ES3UserType_ItemDatabase() : base(typeof(ItemDatabase)){ Instance = this; priority = 1;}


		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			var instance = (ItemDatabase)obj;
			
			writer.WritePropertyByRef("instance", ItemDatabase.instance);
			writer.WriteProperty("money", instance.money, ES3Type_int.Instance);
			writer.WriteProperty("itemDB", instance.itemDB, ES3Internal.ES3TypeMgr.GetOrCreateES3Type(typeof(System.Collections.Generic.List<Item>)));
			writer.WritePropertyByRef("fieldItemPrefab", instance.fieldItemPrefab);
			writer.WriteProperty("pos", instance.pos, ES3Type_Vector3Array.Instance);
		}

		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			var instance = (ItemDatabase)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
					case "instance":
						ItemDatabase.instance = reader.Read<ItemDatabase>();
						break;
					case "money":
						instance.money = reader.Read<System.Int32>(ES3Type_int.Instance);
						break;
					case "itemDB":
						instance.itemDB = reader.Read<System.Collections.Generic.List<Item>>();
						break;
					case "fieldItemPrefab":
						instance.fieldItemPrefab = reader.Read<UnityEngine.GameObject>(ES3Type_GameObject.Instance);
						break;
					case "pos":
						instance.pos = reader.Read<UnityEngine.Vector3[]>(ES3Type_Vector3Array.Instance);
						break;
					default:
						reader.Skip();
						break;
				}
			}
		}
	}


	public class ES3UserType_ItemDatabaseArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3UserType_ItemDatabaseArray() : base(typeof(ItemDatabase[]), ES3UserType_ItemDatabase.Instance)
		{
			Instance = this;
		}
	}
}
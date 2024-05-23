using System;
using UnityEngine;

namespace ES3Types
{
	[UnityEngine.Scripting.Preserve]
	[ES3PropertiesAttribute("level", "playerData", "attackSpeed", "damageableHit", "_isAlive", "timeSinceHit", "MaxHealth", "Health")]
	public class ES3UserType_Damageable : ES3ComponentType
	{
		public static ES3Type Instance = null;

		public ES3UserType_Damageable() : base(typeof(Damageable)){ Instance = this; priority = 1;}


		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			var instance = (Damageable)obj;
			
			writer.WriteProperty("level", instance.level, ES3Type_int.Instance);
			writer.WritePropertyByRef("playerData", instance.playerData);
			writer.WritePropertyByRef("attackSpeed", instance.attackSpeed);
			writer.WriteProperty("damageableHit", instance.damageableHit, ES3Internal.ES3TypeMgr.GetOrCreateES3Type(typeof(UnityEngine.Events.UnityEvent<System.Int32, UnityEngine.Vector2>)));
			writer.WritePrivateField("_isAlive", instance);
			writer.WritePrivateField("timeSinceHit", instance);
			writer.WriteProperty("MaxHealth", instance.MaxHealth, ES3Type_int.Instance);
			writer.WriteProperty("Health", instance.Health, ES3Type_int.Instance);
		}

		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			var instance = (Damageable)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
					case "level":
						instance.level = reader.Read<System.Int32>(ES3Type_int.Instance);
						break;
					case "playerData":
						instance.playerData = reader.Read<SOPlayer>();
						break;
					case "attackSpeed":
						instance.attackSpeed = reader.Read<AttackSpeed>();
						break;
					case "damageableHit":
						instance.damageableHit = reader.Read<UnityEngine.Events.UnityEvent<System.Int32, UnityEngine.Vector2>>();
						break;
					case "_isAlive":
					instance = (Damageable)reader.SetPrivateField("_isAlive", reader.Read<System.Boolean>(), instance);
					break;
					case "timeSinceHit":
					instance = (Damageable)reader.SetPrivateField("timeSinceHit", reader.Read<System.Single>(), instance);
					break;
					case "MaxHealth":
						instance.MaxHealth = reader.Read<System.Int32>(ES3Type_int.Instance);
						break;
					case "Health":
						instance.Health = reader.Read<System.Int32>(ES3Type_int.Instance);
						break;
					default:
						reader.Skip();
						break;
				}
			}
		}
	}


	public class ES3UserType_DamageableArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3UserType_DamageableArray() : base(typeof(Damageable[]), ES3UserType_Damageable.Instance)
		{
			Instance = this;
		}
	}
}
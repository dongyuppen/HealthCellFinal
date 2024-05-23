using System;
using UnityEngine;

namespace ES3Types
{
	[UnityEngine.Scripting.Preserve]
	[ES3PropertiesAttribute("walkSpeed", "runSpeed", "airWalkSpeed", "jumpImpulse", "isOnPlatform", "platformRb", "canDash", "isDashing", "dashingPower", "dashingTime", "dashingCooldown", "groundCheck", "groundLayer", "tr", "moveInput", "touchingDirections", "_isMoving", "_isRunning", "_isFacingRight", "rb", "animator", "m_CancellationTokenSource")]
	public class ES3UserType_PlayerController : ES3ComponentType
	{
		public static ES3Type Instance = null;

		public ES3UserType_PlayerController() : base(typeof(PlayerController)){ Instance = this; priority = 1;}


		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			var instance = (PlayerController)obj;
			
			writer.WriteProperty("walkSpeed", instance.walkSpeed, ES3Type_float.Instance);
			writer.WriteProperty("runSpeed", instance.runSpeed, ES3Type_float.Instance);
			writer.WriteProperty("airWalkSpeed", instance.airWalkSpeed, ES3Type_float.Instance);
			writer.WriteProperty("jumpImpulse", instance.jumpImpulse, ES3Type_float.Instance);
			writer.WriteProperty("isOnPlatform", instance.isOnPlatform, ES3Type_bool.Instance);
			writer.WritePropertyByRef("platformRb", instance.platformRb);
			writer.WritePrivateField("canDash", instance);
			writer.WritePrivateField("isDashing", instance);
			writer.WritePrivateField("dashingPower", instance);
			writer.WritePrivateField("dashingTime", instance);
			writer.WritePrivateField("dashingCooldown", instance);
			writer.WritePrivateFieldByRef("groundCheck", instance);
			writer.WritePrivateField("groundLayer", instance);
			writer.WritePrivateFieldByRef("tr", instance);
			writer.WritePrivateField("moveInput", instance);
			writer.WritePrivateFieldByRef("touchingDirections", instance);
			writer.WritePrivateField("_isMoving", instance);
			writer.WritePrivateField("_isRunning", instance);
			writer.WriteProperty("_isFacingRight", instance._isFacingRight, ES3Type_bool.Instance);
			writer.WritePrivateFieldByRef("rb", instance);
			writer.WritePrivateFieldByRef("animator", instance);
			writer.WritePrivateField("m_CancellationTokenSource", instance);
		}

		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			var instance = (PlayerController)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
					case "walkSpeed":
						instance.walkSpeed = reader.Read<System.Single>(ES3Type_float.Instance);
						break;
					case "runSpeed":
						instance.runSpeed = reader.Read<System.Single>(ES3Type_float.Instance);
						break;
					case "airWalkSpeed":
						instance.airWalkSpeed = reader.Read<System.Single>(ES3Type_float.Instance);
						break;
					case "jumpImpulse":
						instance.jumpImpulse = reader.Read<System.Single>(ES3Type_float.Instance);
						break;
					case "isOnPlatform":
						instance.isOnPlatform = reader.Read<System.Boolean>(ES3Type_bool.Instance);
						break;
					case "platformRb":
						instance.platformRb = reader.Read<UnityEngine.Rigidbody2D>();
						break;
					case "canDash":
					instance = (PlayerController)reader.SetPrivateField("canDash", reader.Read<System.Boolean>(), instance);
					break;
					case "isDashing":
					instance = (PlayerController)reader.SetPrivateField("isDashing", reader.Read<System.Boolean>(), instance);
					break;
					case "dashingPower":
					instance = (PlayerController)reader.SetPrivateField("dashingPower", reader.Read<System.Single>(), instance);
					break;
					case "dashingTime":
					instance = (PlayerController)reader.SetPrivateField("dashingTime", reader.Read<System.Single>(), instance);
					break;
					case "dashingCooldown":
					instance = (PlayerController)reader.SetPrivateField("dashingCooldown", reader.Read<System.Single>(), instance);
					break;
					case "groundCheck":
					instance = (PlayerController)reader.SetPrivateField("groundCheck", reader.Read<UnityEngine.Transform>(), instance);
					break;
					case "groundLayer":
					instance = (PlayerController)reader.SetPrivateField("groundLayer", reader.Read<UnityEngine.LayerMask>(), instance);
					break;
					case "tr":
					instance = (PlayerController)reader.SetPrivateField("tr", reader.Read<UnityEngine.TrailRenderer>(), instance);
					break;
					case "moveInput":
					instance = (PlayerController)reader.SetPrivateField("moveInput", reader.Read<UnityEngine.Vector2>(), instance);
					break;
					case "touchingDirections":
					instance = (PlayerController)reader.SetPrivateField("touchingDirections", reader.Read<TouchingDirections>(), instance);
					break;
					case "_isMoving":
					instance = (PlayerController)reader.SetPrivateField("_isMoving", reader.Read<System.Boolean>(), instance);
					break;
					case "_isRunning":
					instance = (PlayerController)reader.SetPrivateField("_isRunning", reader.Read<System.Boolean>(), instance);
					break;
					case "_isFacingRight":
						instance._isFacingRight = reader.Read<System.Boolean>(ES3Type_bool.Instance);
						break;
					case "rb":
					instance = (PlayerController)reader.SetPrivateField("rb", reader.Read<UnityEngine.Rigidbody2D>(), instance);
					break;
					case "animator":
					instance = (PlayerController)reader.SetPrivateField("animator", reader.Read<UnityEngine.Animator>(), instance);
					break;
					case "m_CancellationTokenSource":
					instance = (PlayerController)reader.SetPrivateField("m_CancellationTokenSource", reader.Read<System.Threading.CancellationTokenSource>(), instance);
					break;
					default:
						reader.Skip();
						break;
				}
			}
		}
	}


	public class ES3UserType_PlayerControllerArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3UserType_PlayerControllerArray() : base(typeof(PlayerController[]), ES3UserType_PlayerController.Instance)
		{
			Instance = this;
		}
	}
}
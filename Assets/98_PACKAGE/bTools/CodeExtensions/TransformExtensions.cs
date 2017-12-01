using UnityEngine;

namespace bTools.CodeExtensions
{
	/// <summary>
	/// Holds parent, local position, rotation and scale of a transform
	/// </summary>
	[System.Serializable]
	public struct TransformData
	{
		[SerializeField] public Vector3 localPosition;
		[SerializeField] public Quaternion localRotation;
		[SerializeField] public Vector3 localScale;
		[SerializeField] public UnityEngine.Transform parent;
	}

	public static class TransformExtensions
	{
		#region TransformData
		/// <summary>
		/// Returns the transform information (Position, Rotation, Scale, Parent) of a given transfom
		/// </summary>
		public static TransformData GetTransformData( this UnityEngine.Transform transform )
		{
			TransformData data = new TransformData()
			{
				parent = transform.parent,
				localPosition = transform.localPosition,
				localRotation = transform.localRotation,
				localScale = transform.localScale
			};

			return data;
		}

		/// <summary>
		/// Applies a TransformData obtained from GetTranformData to a transform
		/// </summary>
		public static void ApplyTransformData( this UnityEngine.Transform transform, TransformData data )
		{
			transform.parent = data.parent;
			transform.localPosition = data.localPosition;
			transform.localRotation = data.localRotation;
			transform.localScale = data.localScale;
		}
		#endregion

		#region Set Position Shorthands
		/// <summary>
		/// Shorthand to set the world X position of a transform
		/// </summary>
		public static void SetXPos( this UnityEngine.Transform transform, float X )
		{

			Vector3 newPos = transform.position;
			newPos.x = X;
			transform.position = newPos;
		}

		/// <summary>
		/// Shorthand to set the world Y position of a transform
		/// </summary>
		public static void SetYPos( this UnityEngine.Transform transform, float Y )
		{

			Vector3 newPos = transform.position;
			newPos.y = Y;
			transform.position = newPos;
		}

		/// <summary>
		/// Shorthand to set the world Z position of a transform
		/// </summary>
		public static void SetZPos( this UnityEngine.Transform transform, float Z )
		{

			Vector3 newPos = transform.position;
			newPos.z = Z;
			transform.position = newPos;
		}

		/// <summary>
		/// Shorthand to set the local X position of a transform
		/// </summary>
		public static void SetLocalXPos( this UnityEngine.Transform transform, float X )
		{

			Vector3 newPos = transform.localPosition;
			newPos.x = X;
			transform.localPosition = newPos;
		}

		/// <summary>
		/// Shorthand to set the local Y position of a transform
		/// </summary>
		public static void SetLocalYPos( this UnityEngine.Transform transform, float Y )
		{

			Vector3 newPos = transform.localPosition;
			newPos.y = Y;
			transform.localPosition = newPos;
		}

		/// <summary>
		/// Shorthand to set the local Z position of a transform
		/// </summary>
		public static void SetLocalZPos( this UnityEngine.Transform transform, float Z )
		{

			Vector3 newPos = transform.localPosition;
			newPos.z = Z;
			transform.localPosition = newPos;
		}
		#endregion

		#region Set Rotation Shorthands
		/// <summary>
		/// Shorthand to set the world X euler angle of a transform
		/// </summary>
		public static void SetXRot( this UnityEngine.Transform transform, float X )
		{

			Vector3 newPos = transform.eulerAngles;
			newPos.x = X;
			transform.eulerAngles = newPos;
		}

		/// <summary>
		/// Shorthand to set the world Y euler angle of a transform
		/// </summary>
		public static void SetYRot( this UnityEngine.Transform transform, float Y )
		{

			Vector3 newPos = transform.eulerAngles;
			newPos.y = Y;
			transform.eulerAngles = newPos;
		}

		/// <summary>
		/// Shorthand to set the world Z euler angle of a transform
		/// </summary>
		public static void SetZRot( this UnityEngine.Transform transform, float Z )
		{

			Vector3 newPos = transform.eulerAngles;
			newPos.z = Z;
			transform.eulerAngles = newPos;
		}

		/// <summary>
		/// Shorthand to set the local X euler angle of a transform
		/// </summary>
		public static void SetLocalXRot( this UnityEngine.Transform transform, float X )
		{

			Vector3 newPos = transform.localEulerAngles;
			newPos.x = X;
			transform.localEulerAngles = newPos;
		}

		/// <summary>
		/// Shorthand to set the local Y euler angle of a transform
		/// </summary>
		public static void SetLocalYRot( this UnityEngine.Transform transform, float Y )
		{

			Vector3 newPos = transform.localEulerAngles;
			newPos.y = Y;
			transform.localEulerAngles = newPos;
		}

		/// <summary>
		/// Shorthand to set the local Z euler angle of a transform
		/// </summary>
		public static void SetLocalZRot( this UnityEngine.Transform transform, float Z )
		{

			Vector3 newPos = transform.localEulerAngles;
			newPos.z = Z;
			transform.localEulerAngles = newPos;
		}
		#endregion

		#region Set Scale Shorthands
		/// <summary>
		/// Shorthand to set the local X scale of a transform
		/// </summary>
		public static void SetLocaXlScl( this UnityEngine.Transform transform, float X )
		{
			Vector3 newPos = transform.localScale;
			newPos.x = X;
			transform.localScale = newPos;
		}

		/// <summary>
		/// Shorthand to set the local Y scale of a transform
		/// </summary>
		public static void SetLocalYScl( this UnityEngine.Transform transform, float Y )
		{

			Vector3 newPos = transform.localScale;
			newPos.y = Y;
			transform.localScale = newPos;
		}

		/// <summary>
		/// Shorthand to set the local Z scale of a transform
		/// </summary>
		public static void SetLocalZScl( this UnityEngine.Transform transform, float Z )
		{

			Vector3 newPos = transform.localScale;
			newPos.z = Z;
			transform.localScale = newPos;
		}
		#endregion

		/// <summary>
		/// Returns the amount of parents to the scene root
		/// </summary>
		public static int GetParentCount( this UnityEngine.Transform t )
		{
			UnityEngine.Transform parent = t;
			int i = 0;
			do
			{
				parent = parent.parent;
				i++;
			}
			while ( parent.parent != null );

			return i;
		}

		/// <summary>
		/// Gets the parent of the object at a certain index
		/// </summary>
		public static UnityEngine.Transform GetParent( this UnityEngine.Transform t, int indiceFromObject )
		{
			if ( indiceFromObject > t.GetParentCount() ) return null;

			UnityEngine.Transform parent = t;

			for ( int i = 0 ; i < indiceFromObject ; i++ )
			{
				parent = parent.parent;
			}

			return parent;
		}

		/// <summary>
		/// Returns the total amount of children and sub-children of Transform.
		/// </summary>
		public static int RecursiveChildCount( this UnityEngine.Transform t )
		{
			int amount = 1;

			foreach ( UnityEngine.Transform child in t )
			{
				amount += child.RecursiveChildCount();
			}

			return amount;
		}

		/// <summary>
		/// Disables all children of this transform.
		/// </summary>
		public static void DisableAllChild( this UnityEngine.Transform t )
		{
			for ( int i = 0 ; i < t.childCount ; i++ )
			{
				t.GetChild( i ).gameObject.SetActive( false );
			}
		}

		/// <summary>
		/// Enables all children of this transform.
		/// </summary>
		public static void EnableAllChild( this UnityEngine.Transform t )
		{
			for ( int i = 0 ; i < t.childCount ; i++ )
			{
				t.GetChild( i ).gameObject.SetActive( true );
			}
		}
	}
}

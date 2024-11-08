/**
* Code generation. Don't modify! 
 */
using System.Runtime.CompilerServices;
using Atomic.AI;
using Game.Engine;
using UnityEngine;
namespace Game
{
    public static class BlackboardAPI
    {
        public const int Character = 1; // GameObject : class
        public const int TreeService = 2; // TreeService : class
        public const int Target = 3; // GameObject : class
        public const int NoTreesView = 4; // GameObject : class
        public const int StoppingDistance = 5; // float
        public const int Barn = 6; // GameObject : class
        public const int FullBarnView = 7; // GameObject : class
        public const int Waypoints = 8; // Transform[] : class
        public const int WaypointIndex = 9; // int
        public const int WaypointPause = 10; // float
        public const int WaypointTime = 11; // float
        public const int Enemy = 12; // GameObject : class


        ///Extensions
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasCharacter(this IBlackboard obj) => obj.HasObject(Character);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static GameObject  GetCharacter(this IBlackboard obj) => obj.GetObject<GameObject >(Character);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetCharacter(this IBlackboard obj, out GameObject  value) => obj.TryGetObject(Character, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetCharacter(this IBlackboard obj, GameObject  value) => obj.SetObject(Character, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelCharacter(this IBlackboard obj) => obj.DelObject(Character);


		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasTreeService(this IBlackboard obj) => obj.HasObject(TreeService);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static TreeService  GetTreeService(this IBlackboard obj) => obj.GetObject<TreeService >(TreeService);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetTreeService(this IBlackboard obj, out TreeService  value) => obj.TryGetObject(TreeService, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetTreeService(this IBlackboard obj, TreeService  value) => obj.SetObject(TreeService, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelTreeService(this IBlackboard obj) => obj.DelObject(TreeService);


		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasTarget(this IBlackboard obj) => obj.HasObject(Target);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static GameObject  GetTarget(this IBlackboard obj) => obj.GetObject<GameObject >(Target);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetTarget(this IBlackboard obj, out GameObject  value) => obj.TryGetObject(Target, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetTarget(this IBlackboard obj, GameObject  value) => obj.SetObject(Target, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelTarget(this IBlackboard obj) => obj.DelObject(Target);


		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasNoTreesView(this IBlackboard obj) => obj.HasObject(NoTreesView);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static GameObject  GetNoTreesView(this IBlackboard obj) => obj.GetObject<GameObject >(NoTreesView);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetNoTreesView(this IBlackboard obj, out GameObject  value) => obj.TryGetObject(NoTreesView, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetNoTreesView(this IBlackboard obj, GameObject  value) => obj.SetObject(NoTreesView, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelNoTreesView(this IBlackboard obj) => obj.DelObject(NoTreesView);


		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasStoppingDistance(this IBlackboard obj) => obj.HasFloat(StoppingDistance);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float GetStoppingDistance(this IBlackboard obj) => obj.GetFloat(StoppingDistance);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetStoppingDistance(this IBlackboard obj, out float value) => obj.TryGetFloat(StoppingDistance, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetStoppingDistance(this IBlackboard obj, float value) => obj.SetFloat(StoppingDistance, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelStoppingDistance(this IBlackboard obj) => obj.DelFloat(StoppingDistance);


		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasBarn(this IBlackboard obj) => obj.HasObject(Barn);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static GameObject  GetBarn(this IBlackboard obj) => obj.GetObject<GameObject >(Barn);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetBarn(this IBlackboard obj, out GameObject  value) => obj.TryGetObject(Barn, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetBarn(this IBlackboard obj, GameObject  value) => obj.SetObject(Barn, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelBarn(this IBlackboard obj) => obj.DelObject(Barn);


		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasFullBarnView(this IBlackboard obj) => obj.HasObject(FullBarnView);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static GameObject  GetFullBarnView(this IBlackboard obj) => obj.GetObject<GameObject >(FullBarnView);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetFullBarnView(this IBlackboard obj, out GameObject  value) => obj.TryGetObject(FullBarnView, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetFullBarnView(this IBlackboard obj, GameObject  value) => obj.SetObject(FullBarnView, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelFullBarnView(this IBlackboard obj) => obj.DelObject(FullBarnView);


		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasWaypoints(this IBlackboard obj) => obj.HasObject(Waypoints);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Transform[]  GetWaypoints(this IBlackboard obj) => obj.GetObject<Transform[] >(Waypoints);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetWaypoints(this IBlackboard obj, out Transform[]  value) => obj.TryGetObject(Waypoints, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetWaypoints(this IBlackboard obj, Transform[]  value) => obj.SetObject(Waypoints, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelWaypoints(this IBlackboard obj) => obj.DelObject(Waypoints);


		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasWaypointIndex(this IBlackboard obj) => obj.HasInt(WaypointIndex);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int GetWaypointIndex(this IBlackboard obj) => obj.GetInt(WaypointIndex);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetWaypointIndex(this IBlackboard obj, out int value) => obj.TryGetInt(WaypointIndex, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetWaypointIndex(this IBlackboard obj, int value) => obj.SetInt(WaypointIndex, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelWaypointIndex(this IBlackboard obj) => obj.DelInt(WaypointIndex);


		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasWaypointPause(this IBlackboard obj) => obj.HasFloat(WaypointPause);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float GetWaypointPause(this IBlackboard obj) => obj.GetFloat(WaypointPause);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetWaypointPause(this IBlackboard obj, out float value) => obj.TryGetFloat(WaypointPause, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetWaypointPause(this IBlackboard obj, float value) => obj.SetFloat(WaypointPause, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelWaypointPause(this IBlackboard obj) => obj.DelFloat(WaypointPause);


		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasWaypointTime(this IBlackboard obj) => obj.HasFloat(WaypointTime);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float GetWaypointTime(this IBlackboard obj) => obj.GetFloat(WaypointTime);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetWaypointTime(this IBlackboard obj, out float value) => obj.TryGetFloat(WaypointTime, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetWaypointTime(this IBlackboard obj, float value) => obj.SetFloat(WaypointTime, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelWaypointTime(this IBlackboard obj) => obj.DelFloat(WaypointTime);


		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasEnemy(this IBlackboard obj) => obj.HasObject(Enemy);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static GameObject  GetEnemy(this IBlackboard obj) => obj.GetObject<GameObject >(Enemy);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetEnemy(this IBlackboard obj, out GameObject  value) => obj.TryGetObject(Enemy, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetEnemy(this IBlackboard obj, GameObject  value) => obj.SetObject(Enemy, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelEnemy(this IBlackboard obj) => obj.DelObject(Enemy);

    }
}

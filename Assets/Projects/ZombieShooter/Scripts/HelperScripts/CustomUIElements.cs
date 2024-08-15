using UnityEditor;
using UnityEngine;

namespace ZombieShooter
{
    [CustomEditor(typeof(ActionHelper))]
    public class CustomUIElements : Editor
    {

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            var actionHelper = (ActionHelper)target;

            if (GUILayout.Button("DamageCharacter"))
            {
                actionHelper.DamageCharacter(actionHelper.damage);
            }

            if (GUILayout.Button("KillZombie"))
            {
                actionHelper.KillZombie(actionHelper._zombie);
            }
        }
    }
}
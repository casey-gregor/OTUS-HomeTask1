using System.Collections;
using UnityEngine;

namespace ZombieShooter
{
    public static class APIKeys
    {
        public const string MOVE_DIRECTION = nameof(MOVE_DIRECTION);

        public const string ROTATE_DIRECTION = nameof(ROTATE_DIRECTION);

        public const string SHOOT_ACTION = nameof(SHOOT_ACTION);
        public const string SHOOT_REQUEST = nameof(SHOOT_REQUEST);

        public const string SHOOT_BULLET_ACTION = nameof(SHOOT_BULLET_ACTION);
        public const string INITIATE_BULLET = nameof(INITIATE_BULLET);
        public const string REMOVE_BULLET = nameof(REMOVE_BULLET);

        public const string DEDUCT_HITPOINTS = nameof(DEDUCT_HITPOINTS);
        public const string IS_DEAD = nameof(IS_DEAD);
        public const string DEATH_ACTION = nameof(DEATH_ACTION);
        public const string IS_ACTIVE = nameof(IS_ACTIVE);

        public const string TRY_TAKE_DAMAGE_ACTION = nameof(TRY_TAKE_DAMAGE_ACTION);
        public const string DAMAGE = nameof(DAMAGE);
        public const string DAMAGE_INTERVAL = nameof(DAMAGE_INTERVAL);

        public const string HIT_POINTS = nameof(HIT_POINTS);
        public const string TARGET = nameof(TARGET);

        public const string INITIATE = nameof(INITIATE);
    }
}
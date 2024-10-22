using System;
using System.Collections.Generic;
using Projects.EventBus.Scripts.Components;
using UI;
using UnityEngine;

namespace EventBus
{
    public class HeroEntity
    {
        public int ID { get; }
        public PlayerEntity PlayerEntity { get; private set; }
        public HealthComponent HealthComponent { get; }
        // public readonly int MaxHealth;
        // public int CurrentHealth { get; private set; }
        public int AttackDamage { get; private set; }
        public HeroView View { get; private set; }
        public Ability Ability { get; private set; }
        
        public bool IsInvincible { get; private set; }
        public bool IsDead { get; private set; }

        public int SkipNumOfTurns { get; private set; }

        public HeroEntity(int id, PlayerEntity playerEntity, int health, int attackDamage, HeroView view, Ability ability)
        {
            ID = id;
            PlayerEntity = playerEntity;
            HealthComponent = new HealthComponent(health);
            AttackDamage = attackDamage;
            View = view;
            Ability = ability;

            IsDead = false;
        }

        // public void SetHealth(int value)
        // {
        //     CurrentHealth = value;
        // }

        public void DeductHealth(int value)
        {
            if(value < 0) return;
            int currentHealth = HealthComponent.CurrentHealth;
            currentHealth -= value;
            if(currentHealth < 0) currentHealth = 0;
            HealthComponent.SetCurrentHealth(currentHealth);
        }

        public void AddHealth(int value)
        {
            if(value < 0) return;
            int currentHealth = HealthComponent.CurrentHealth;
            currentHealth += value;
            if(currentHealth > HealthComponent.MaxHealth) currentHealth = HealthComponent.MaxHealth;
            HealthComponent.SetCurrentHealth(currentHealth);
        }

        public void SetTurnHealth(int value)
        {
            if(value < 0) return;
            HealthComponent.SetHealthAfterTurn(value);
        }

        public void SetStats(int attackValue=default, int healthValue=default)
        {
            if (attackValue == default)
            {
                attackValue = AttackDamage;
            }

            if (healthValue == default)
            {
                healthValue = HealthComponent.CurrentHealth;
            }
            
            string stats = $"{View.name}\n {attackValue}/{healthValue}";
            View.SetStats(stats);
        }

        public void SetIsDead()
        {
            IsDead = true;
        }

        public void SetInvincible(bool value)
        {
            IsInvincible = value;
        }

        public bool TryGetEffect(out IEffect effect)
        {
            if (Ability != null & Ability!.Effect != null)
            {
                effect = Ability.Effect;
                return true;
            }

            effect = null;
            return false;
        }

        public void ModifyTurnsToSkip(int value)
        {
            SkipNumOfTurns += value;
            
            if(SkipNumOfTurns < 0) SkipNumOfTurns = 0;
        }
        
        
    }
}
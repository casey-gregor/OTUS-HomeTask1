using System;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{

    public sealed class GameManager : MonoBehaviour, IGameListener
    {
        public enum State
        {
            Unknown,
            Start,
            Pause,
            Resume,
            Finish
        }

        public State state { get; private set; }

        private List<IGameListener> gameListeners = new();

        private void Awake()
        {
            IGameListener.RegisterEvent += RegisterEventHandler;
            IGameListener.UnregisterEvent += UnregisterEventHandler;
            //Debug.Log("GameManager subscribed to Register Event");
        }

        private void RegisterEventHandler(IGameListener gameListener)
        {
            gameListeners.Add(gameListener);
            
            //if (gameListeners.Contains(gameListener))
            //{
            //    IGameListener.SuccessfulRegistration();
            //}
        }
        private void UnregisterEventHandler(IGameListener gameListener)
        {
            gameListeners.Remove(gameListener);
            Debug.Log($"{gameListener} removed from the list");
        }
        private void FixedUpdate()
        {
            if(!CanUpdate()) return;

            for(int i = 0; i < gameListeners.Count; i++)
            {
                if (gameListeners[i] is IGameFixedUpdateListener fixedUpdateListener)
                {
                    fixedUpdateListener.OnFixedUpdate();
                }
            }
        }

        private void Update()
        {
            if (!CanUpdate()) return;
            
            for (int i = 0; i < gameListeners.Count; i++)
            {

                //Debug.Log("gameListener : " + gameListeners[i]);
                if (gameListeners[i] is IGameUpdateListener updateListener)
                {
                    updateListener.OnUpdate();
                    //Debug.Log("OnUpdate");
                }
            }
        }

        private void SwitchState(State state)
        {
            switch (state)
            {
                case State.Start:
                    for (int i = 0; i < gameListeners.Count; i++)
                    {

                        //Debug.Log("gameListener : " + gameListeners[i]);
                        if (gameListeners[i] is IGameStartListener listener)
                        {
                            listener.OnStart();
                            Debug.Log($"{listener} called OnStart");
                        }
                    }
                    break;
                case State.Pause:
                    for (int i = 0; i < gameListeners.Count; i++)
                    {

                        //Debug.Log("gameListener : " + gameListeners[i]);
                        if (gameListeners[i] is IGamePauseListener listener)
                        {
                            listener.OnPause();
                            //Debug.Log("OnStart");
                        }
                    }
                    break;
                case State.Resume:
                    for (int i = 0; i < gameListeners.Count; i++)
                    {

                        //Debug.Log("gameListener : " + gameListeners[i]);
                        if (gameListeners[i] is IGameResumeListener listener)
                        {
                            listener.OnResume();
                            //Debug.Log("OnStart");
                        }
                    }
                    break;

            }
        }

        public void SetState(State newState)
        {
            if (state != newState)
            {
                SwitchState(newState);
            }
            state = newState;
        }

        public void FinishGame()
        {
            Debug.Log("Game over!");
            Time.timeScale = 0;
        }

        private bool CanUpdate()
        {
            return state is State.Start or State.Resume;
        }

    }
}
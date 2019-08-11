using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BaseFramework
{
    public class GameMode : MonoBehaviour
    {
        #region Properties
        protected UiMaster uiMaster
        {
            get { return UiMaster.thisInstance; }
        }

        protected UiManager uiManager
        {
            get { return UiManager.thisInstance; }
        }

        protected GameInstance gameInstance
        {
            get { return GameInstance.thisInstance; }
        }

        public GameMaster gamemaster
        {
            get { return GameMaster.thisInstance; }
        }

        protected UnityMsgManager myUnityMsgManager
        {
            get { return UnityMsgManager.thisInstance;}
        }

        public static GameMode thisInstance
        {
            get; protected set;
        }
        #endregion

        #region UnityMessages
        protected virtual void OnEnable()
        {
            if (thisInstance != null)
                Debug.LogWarning("More than one instance of GameMode in scene.");
            else
                thisInstance = this;

            ResetGameModeStats();
            InitializeGameModeValues();
            SubscribeToEvents();
        }

        protected virtual void Start()
        {
            StartServices();

            //if (uiManager == null)
            //    Debug.LogWarning("There is no uimanager in the scene!");
        }

        protected virtual void OnDisable()
        {
            UnsubscribeFromEvents();
        }
        #endregion

        #region Handlers
        protected virtual void OnUpdateHandler()
        {

        }
        #endregion

        #region Updaters and Resetters
        protected virtual void UpdateGameModeStats()
        {

        }

        protected virtual void ResetGameModeStats()
        {

        }
        #endregion

        #region GameModeSetupFunctions
        protected virtual void InitializeGameModeValues()
        {

        }

        protected virtual void SubscribeToEvents()
        {
            myUnityMsgManager.RegisterOnUpdate(OnUpdateHandler);
        }

        protected virtual void UnsubscribeFromEvents()
        {
            myUnityMsgManager.DeregisterOnUpdate(OnUpdateHandler);
        }

        protected virtual void StartServices()
        {
            //InvokeRepeating("SE_UpdateTargetUI", 0.1f, 0.1f);
        }
        #endregion
    }
}
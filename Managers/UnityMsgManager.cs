using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BaseFramework
{
    public class UnityMsgManager : BaseSingleton<UnityMsgManager>
    {
        #region FieldsAndProperties

        private bool bUpdateActionList = true;
        private List<System.Action> UpdateActionList = new List<System.Action>();

        #endregion

        #region UnityMessages

        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            if (bUpdateActionList && UpdateActionList != null && UpdateActionList.Count > 0)
            {
                foreach (var _updateAction in UpdateActionList)
                {
                    if (_updateAction == null){ RemoveUnusableUpdateAction(_updateAction, true);}
                    else
                    {
                        try
                        {
                            _updateAction();
                        }
                        catch (System.Exception _updateErrno)
                        {
                            RemoveUnusableUpdateAction(_updateAction, false, _updateErrno.Message);
                        }
                    }
                }
            }
        }

        void OnDisable()
        {
            bUpdateActionList = false;
            UpdateActionList.Clear();
        }

        #endregion

        #region RegisterUnityEvents

        public void RegisterOnUpdate(System.Action updateMethodAction)
        {
            UpdateActionList.Add(updateMethodAction);
        }

        public void DeregisterOnUpdate(System.Action updateMethodAction)
        {
            UpdateActionList.Remove(updateMethodAction);
        }

        #endregion

        #region HelperMethods

        private void RemoveUnusableUpdateAction(System.Action updateMethodAction, bool bWasNull, string errnoMsg = "")
        {
            string _errormsg = bWasNull ? $"Update Method {updateMethodAction} was Null, Removing..." : 
                $"Update Method Error From {updateMethodAction}: {errnoMsg}. Removing...";
            Debug.LogWarning(_errormsg);
            DeregisterOnUpdate(updateMethodAction);
        }

        #endregion
    }
}
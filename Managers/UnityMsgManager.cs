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
        private List<System.Action> FixedUpdateActionList = new List<System.Action>();

        #endregion

        #region UnityMessages

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

        private void FixedUpdate()
        {
            if (bUpdateActionList && FixedUpdateActionList != null && FixedUpdateActionList.Count > 0)
            {
                foreach (var _fixedUpdateAction in FixedUpdateActionList)
                {
                    if (_fixedUpdateAction == null) { RemoveUnusableFixedUpdateAction(_fixedUpdateAction, true); }
                    else
                    {
                        try
                        {
                            _fixedUpdateAction();
                        }
                        catch (System.Exception _updateErrno)
                        {
                            RemoveUnusableFixedUpdateAction(_fixedUpdateAction, false, _updateErrno.Message);
                        }
                    }
                }
            }
        }

        void OnDisable()
        {
            bUpdateActionList = false;
            UpdateActionList.Clear();
            FixedUpdateActionList.Clear();
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

        public void RegisterOnFixedUpdate(System.Action fixedUpdateMethodAction)
        {
            FixedUpdateActionList.Add(fixedUpdateMethodAction);
        }

        public void DeregisterOnFixedUpdate(System.Action fixedUpdateMethodAction)
        {
            FixedUpdateActionList.Remove(fixedUpdateMethodAction);
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

        private void RemoveUnusableFixedUpdateAction(System.Action fixedUpdateMethodAction, bool bWasNull, string errnoMsg = "")
        {
            string _errormsg = bWasNull ? $"Fixed Update Method {fixedUpdateMethodAction} was Null, Removing..." :
                $"Fixed Update Method Error From {fixedUpdateMethodAction}: {errnoMsg}. Removing...";
            Debug.LogWarning(_errormsg);
            DeregisterOnFixedUpdate(fixedUpdateMethodAction);
        }

        #endregion
    }
}
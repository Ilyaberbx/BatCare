using UnityEngine;

namespace Workspace.Core.MVP.Abstractions
{
    [RequireComponent(typeof(CanvasGroup))]
    public abstract class BaseView : MonoBehaviour
    {
        private CanvasGroup _canvasGroup;

        protected CanvasGroup CanvasGroup
        {
            get
            {
                if (_canvasGroup == null)
                {
                    _canvasGroup =  GetComponent<CanvasGroup>();
                }

                return _canvasGroup;
            }
        }

        public bool Interactable
        {
            get => CanvasGroup.interactable;
            set => CanvasGroup.interactable = value;
        }
        
        public bool BlocksRayCasts
        {
            get => CanvasGroup.blocksRaycasts;
            set => CanvasGroup.blocksRaycasts = value;
        }
    }
}
using UnityEngine;
using Workspace.Core.Systems.Data;
using Workspace.Utilities;

namespace Workspace.Core.Systems.Environment
{
    public class SunSystem : MonoBehaviour
    {
        [SerializeField] private SunData _moon;
        [SerializeField] private SunData _sun;
        
        private float SunAngle => SunUtility.GetSunAngle();


        private void Update()
        {
            _sun.Light.transform.rotation = Quaternion.AngleAxis(SunAngle, Vector3.right);
        }
    }
}
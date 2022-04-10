using System.Collections.Generic;
using UnityEngine;

namespace Features.UIBehaviour
{
    [CreateAssetMenu(menuName = "Config/Record keeper", fileName = "RecordKeeper")]
    public class RecordKeeper : ScriptableObject
    {
        public List<float> LevelList;
    }
}

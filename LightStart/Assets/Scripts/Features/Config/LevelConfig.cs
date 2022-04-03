using UnityEngine;

namespace Features.Config
{
    
    [CreateAssetMenu(fileName = "LevelConfig", menuName = "Config/LevelConfig")]
    public class LevelConfig : ScriptableObject
    {
        [SerializeField] private float gravityValue;
        
        public float GravityValue => gravityValue;
    }
}
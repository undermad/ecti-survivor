using System.Collections.Generic;
using UnityEngine;

namespace Explorer._Scripts.Explorer.Components.ActiveTags
{
    [CreateAssetMenu(menuName = "Explorer/ActiveTags/TagsData", fileName = "ActiveTagsData")]
    public class ActiveTagsData : ScriptableObject
    {
        public List<ActiveTag> tags;
    }
}
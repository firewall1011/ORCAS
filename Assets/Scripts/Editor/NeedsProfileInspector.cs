using UnityEditor;
using UnityEngine;

namespace ORCAS
{
    [CustomEditor(typeof(NeedsProfile))]
    public class NeedsProfileInspector : Editor
    {
        private readonly GUILayoutOption _layoutOptions = GUILayout.Width(100f);
        private static bool _foldout = false;

        private SerializedProperty _decayAmounts;
        private SerializedProperty _scoringMults;
        private SerializedProperty _needTypes;

        private void OnEnable()
        {
            _decayAmounts = serializedObject.FindProperty("_decayAmounts");
            _scoringMults = serializedObject.FindProperty("_scoringMultipliers");
            _needTypes = serializedObject.FindProperty("NeedTypes");

            Validate();
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            _foldout = EditorGUILayout.Foldout(_foldout, "Profile");
            
            if (_foldout)
            {
                DisplayProfile();
            }

            serializedObject.ApplyModifiedProperties();
        }

        private void DisplayProfile()
        {
            DisplayProfileHeader();

            var needList = (NeedTypeList)serializedObject.FindProperty("NeedTypes").objectReferenceValue;

            using (new EditorGUILayout.VerticalScope())
            {
                for (int i = 0; i < needList.List.Count; i++)
                {
                    DisplayNeedProperties(needList, i);
                }
            }
        }

        private void DisplayNeedProperties(NeedTypeList needList, int i)
        {
            using (new EditorGUILayout.HorizontalScope())
            {
                EditorGUILayout.LabelField(needList.List[i].name, _layoutOptions);
                var decayAmount = _decayAmounts.GetArrayElementAtIndex(i);
                var scoringMult = _scoringMults.GetArrayElementAtIndex(i);


                decayAmount.floatValue = EditorGUILayout.FloatField(decayAmount.floatValue, _layoutOptions);
                scoringMult.floatValue = EditorGUILayout.FloatField(scoringMult.floatValue, _layoutOptions);
            }
        }

        private void DisplayProfileHeader()
        {
            using (new EditorGUILayout.HorizontalScope())
            {
                EditorGUILayout.LabelField("Need Type", _layoutOptions);
                EditorGUILayout.LabelField("Decay Amount", _layoutOptions);
                EditorGUILayout.LabelField("Scoring Multiplier", _layoutOptions);
            }
        }

        private void Validate()
        {
            var needList = ((NeedTypeList)_needTypes.objectReferenceValue);
            if (needList is null)
                return;
            
            int count = needList.List.Count;

            if (_decayAmounts.arraySize != count)
            {
                _decayAmounts.arraySize = count;
            }

            if (_scoringMults.arraySize != count)
            {
                _scoringMults.arraySize = count;
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}

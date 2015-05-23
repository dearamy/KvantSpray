//
// Custom editor class for Spray
//

using UnityEngine;
using UnityEditor;

namespace Kvant
{
    [CustomEditor(typeof(Spray))]
    public class SprayEditor : Editor
    {
        SerializedProperty propShapes;
        SerializedProperty propMaxParticles;

        SerializedProperty propEmitterCenter;
        SerializedProperty propEmitterSize;
        SerializedProperty propThrottle;

        SerializedProperty propMinLife;
        SerializedProperty propMaxLife;

        SerializedProperty propMinScale;
        SerializedProperty propMaxScale;

        SerializedProperty propDirection;
        SerializedProperty propSpread;

        SerializedProperty propMinSpeed;
        SerializedProperty propMaxSpeed;

        SerializedProperty propMinSpin;
        SerializedProperty propMaxSpin;

        SerializedProperty propNoiseFrequency;
        SerializedProperty propNoiseAmplitude;
        SerializedProperty propNoiseAnimation;

        SerializedProperty propColor;
        SerializedProperty propRandomSeed;
        SerializedProperty propDebug;

        GUIContent textCenter;
        GUIContent textSize;
        GUIContent textFrequency;
        GUIContent textAmplitude;
        GUIContent textAnimation;

        void OnEnable()
        {
            propShapes         = serializedObject.FindProperty("_shapes");
            propMaxParticles   = serializedObject.FindProperty("_maxParticles");

            propEmitterCenter  = serializedObject.FindProperty("_emitterCenter");
            propEmitterSize    = serializedObject.FindProperty("_emitterSize");
            propThrottle       = serializedObject.FindProperty("_throttle");

            propMinLife        = serializedObject.FindProperty("_minLife");
            propMaxLife        = serializedObject.FindProperty("_maxLife");

            propMinScale       = serializedObject.FindProperty("_minScale");
            propMaxScale       = serializedObject.FindProperty("_maxScale");

            propDirection      = serializedObject.FindProperty("_direction");
            propSpread         = serializedObject.FindProperty("_spread");

            propMinSpeed       = serializedObject.FindProperty("_minSpeed");
            propMaxSpeed       = serializedObject.FindProperty("_maxSpeed");

            propMinSpin        = serializedObject.FindProperty("_minSpin");
            propMaxSpin        = serializedObject.FindProperty("_maxSpin");

            propNoiseFrequency = serializedObject.FindProperty("_noiseFrequency");
            propNoiseAmplitude = serializedObject.FindProperty("_noiseAmplitude");
            propNoiseAnimation = serializedObject.FindProperty("_noiseAnimation");

            propColor          = serializedObject.FindProperty("_color");
            propRandomSeed     = serializedObject.FindProperty("_randomSeed");
            propDebug          = serializedObject.FindProperty("_debug");

            textCenter    = new GUIContent("Center");
            textSize      = new GUIContent("Size");
            textFrequency = new GUIContent("Frequency");
            textAmplitude = new GUIContent("Amplitude");
            textAnimation = new GUIContent("Animation");
        }

        void MinMaxSlider(string label, SerializedProperty propMin, SerializedProperty propMax, float minLimit, float maxLimit, string format)
        {
            var min = propMin.floatValue;
            var max = propMax.floatValue;

            EditorGUI.BeginChangeCheck();

            var text = new GUIContent(label + " (" + min.ToString(format) + "-" + max.ToString(format) + ")");
            EditorGUILayout.MinMaxSlider(text, ref min, ref max, minLimit, maxLimit);

            if (EditorGUI.EndChangeCheck()) {
                propMin.floatValue = min;
                propMax.floatValue = max;
            }
        }

        public override void OnInspectorGUI()
        {
            var targetSpray = target as Spray;

            serializedObject.Update();

            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(propMaxParticles);
            EditorGUILayout.HelpBox("Actual Number: " + targetSpray.maxParticles, MessageType.None);
            if (EditorGUI.EndChangeCheck()) targetSpray.NotifyConfigChange();

            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Emitter");
            EditorGUI.indentLevel++;
            EditorGUILayout.PropertyField(propEmitterCenter, textCenter);
            EditorGUILayout.PropertyField(propEmitterSize, textSize);
            EditorGUILayout.Slider(propThrottle, 0.0f, 1.0f);
            EditorGUI.indentLevel--;

            EditorGUILayout.Space();

            MinMaxSlider("Life", propMinLife, propMaxLife, 0.1f, 5.0f, "0.00");

            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Velocity");
            EditorGUI.indentLevel++;
            MinMaxSlider("Speed", propMinSpeed, propMaxSpeed, 0.0f, 50.0f, "0.0");
            EditorGUILayout.PropertyField(propDirection);
            EditorGUILayout.Slider(propSpread, 0.0f, 1.0f);
            MinMaxSlider("Spin", propMinSpin, propMaxSpin, 0.0f, 1000.0f, "0");
            EditorGUI.indentLevel--;

            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Turbulent Noise");
            EditorGUI.indentLevel++;
            EditorGUILayout.Slider(propNoiseFrequency, 0.01f, 1.0f, textFrequency);
            EditorGUILayout.Slider(propNoiseAmplitude, 0.0f, 50.0f, textAmplitude);
            EditorGUILayout.Slider(propNoiseAnimation, 0.0f, 10.0f, textAnimation);
            EditorGUI.indentLevel--;

            EditorGUILayout.Space();

            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(propShapes, true);
            if (EditorGUI.EndChangeCheck()) targetSpray.NotifyConfigChange();

            EditorGUILayout.Space();

            MinMaxSlider("Scale", propMinScale, propMaxScale, 0.01f, 5.0f, "0.00");

            EditorGUILayout.Space();

            EditorGUILayout.PropertyField(propColor);
            EditorGUILayout.PropertyField(propRandomSeed);
            EditorGUILayout.PropertyField(propDebug);

            serializedObject.ApplyModifiedProperties();
        }
    }
}

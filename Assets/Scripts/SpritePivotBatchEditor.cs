#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

public class BatchSetPivot : EditorWindow
{
    private float customPivotX = 0.75f;
    private float customPivotY = 0f;

    [MenuItem("Tools/Batch Set Sprite Pivot")]
    public static void ShowWindow()
    {
        GetWindow<BatchSetPivot>("Batch Pivot Setter");
    }

    [System.Obsolete]
    private void OnGUI()
    {
        GUILayout.Label("Set Pivot for Multiple Sprites", EditorStyles.boldLabel);

        if (GUILayout.Button("Set Pivot to Center Bottom"))
        {
            SetPivotCenterBottom();
        }

        GUILayout.Space(20);
        GUILayout.Label("Set Custom Pivot", EditorStyles.boldLabel);

        customPivotX = EditorGUILayout.FloatField("Custom Pivot X (0~1)", customPivotX);
        customPivotY = EditorGUILayout.FloatField("Custom Pivot Y (0~1)", customPivotY);

        if (GUILayout.Button("Set Pivot to Custom Values"))
        {
            SetPivotCustom(customPivotX, customPivotY);
        }
    }

    [System.Obsolete]
    private static void SetPivotCenterBottom()
    {
        SetPivotCustom(0.5f, 0f);
    }

    [System.Obsolete]
    private static void SetPivotCustom(float pivotX, float pivotY)
    {
        Object[] selectedTextures = Selection.GetFiltered<Texture2D>(SelectionMode.Assets);

        if (selectedTextures.Length == 0)
        {
            EditorUtility.DisplayDialog("경고", "먼저 하나 이상의 Texture2D 에셋을 선택하세요.", "확인");
            return;
        }

        int successCount = 0;
        int failCount = 0;

        foreach (Texture2D texture in selectedTextures)
        {
            string assetPath = AssetDatabase.GetAssetPath(texture);
            TextureImporter importer = AssetImporter.GetAtPath(assetPath) as TextureImporter;

            if (importer == null || importer.spriteImportMode != SpriteImportMode.Multiple)
            {
                Debug.LogWarning($"'{texture.name}'는 유효한 Texture2D(Multiple 모드) 파일이 아닙니다.");
                failCount++;
                continue;
            }

            SpriteMetaData[] metaData = importer.spritesheet;

            if (metaData == null || metaData.Length == 0)
            {
                Debug.LogWarning($"'{texture.name}'에 스프라이트 데이터가 없습니다.");
                failCount++;
                continue;
            }

            bool isModified = false;

            for (int i = 0; i < metaData.Length; i++)
            {
                metaData[i].alignment = (int)SpriteAlignment.Custom;
                metaData[i].pivot = new Vector2(pivotX, pivotY);
                isModified = true;

                Debug.Log($"'{texture.name}'의 스프라이트 '{metaData[i].name}' 피벗을 ({pivotX}, {pivotY})로 변경.");
            }

            if (isModified)
            {
                importer.spritesheet = metaData;
                EditorUtility.SetDirty(importer);
                AssetDatabase.ImportAsset(assetPath, ImportAssetOptions.ForceUpdate);
                successCount++;
            }
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        EditorUtility.DisplayDialog("완료", $"피벗 설정 완료:\n성공: {successCount}\n실패: {failCount}", "확인");
    }
}
#endif
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
            EditorUtility.DisplayDialog("���", "���� �ϳ� �̻��� Texture2D ������ �����ϼ���.", "Ȯ��");
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
                Debug.LogWarning($"'{texture.name}'�� ��ȿ�� Texture2D(Multiple ���) ������ �ƴմϴ�.");
                failCount++;
                continue;
            }

            SpriteMetaData[] metaData = importer.spritesheet;

            if (metaData == null || metaData.Length == 0)
            {
                Debug.LogWarning($"'{texture.name}'�� ��������Ʈ �����Ͱ� �����ϴ�.");
                failCount++;
                continue;
            }

            bool isModified = false;

            for (int i = 0; i < metaData.Length; i++)
            {
                metaData[i].alignment = (int)SpriteAlignment.Custom;
                metaData[i].pivot = new Vector2(pivotX, pivotY);
                isModified = true;

                Debug.Log($"'{texture.name}'�� ��������Ʈ '{metaData[i].name}' �ǹ��� ({pivotX}, {pivotY})�� ����.");
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

        EditorUtility.DisplayDialog("�Ϸ�", $"�ǹ� ���� �Ϸ�:\n����: {successCount}\n����: {failCount}", "Ȯ��");
    }
}
#endif
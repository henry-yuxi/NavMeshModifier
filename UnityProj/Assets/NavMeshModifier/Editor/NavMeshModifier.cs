using UnityEditor;
using UnityEditor.AI;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NavMeshModifier
{
    public static NavMeshProjectSettingsParamSetter GetNavMeshProjectSettings()
    {
        return new NavMeshProjectSettingsParamSetter();
    }

    public static NavMeshSettingsBakeParamSetter GetNavMeshSettings(Scene scene)
    {
        return new NavMeshSettingsBakeParamSetter(scene);
    }

    public static Object GetNavMeshData(Scene scene)
    {
        SceneManager.SetActiveScene(scene);
        var naviMeshSettings = new SerializedObject(NavMeshBuilder.navMeshSettingsObject);
        var prop = naviMeshSettings.FindProperty("m_NavMeshData");
        return prop != null ? prop.objectReferenceValue : null;
    }

    public static void SetNavMeshData(Scene scene, Object obj)
    {
        SceneManager.SetActiveScene(scene);
        var naviMeshSettings = new SerializedObject(NavMeshBuilder.navMeshSettingsObject);
        var prop = naviMeshSettings.FindProperty("m_NavMeshData");
        if (prop != null)
        {
            prop.objectReferenceValue = obj;
            naviMeshSettings.ApplyModifiedProperties();
        }
        EditorSceneManager.SaveScene(scene, scene.path);
    }

}

public class NavMeshProjectSettingsParamSetter
{
    private SerializedObject m_NavMeshProjectSettingsSo;

    internal NavMeshProjectSettingsParamSetter()
    {
        var serializedAssetInterfaceSingleton = Unsupported.GetSerializedAssetInterfaceSingleton("NavMeshProjectSettings");
        m_NavMeshProjectSettingsSo = new SerializedObject(serializedAssetInterfaceSingleton);
    }

    public NavigationAgent GetAgentByIndex(int index = 0)
    {
        return new NavigationAgent(m_NavMeshProjectSettingsSo, index);
    }

    public NavigationArea GetNavigationArea(int index = 0)
    {
        return new NavigationArea(m_NavMeshProjectSettingsSo, index);
    }

    public int GetAgentsCount()
    {
        var agents = m_NavMeshProjectSettingsSo.FindProperty("m_Settings");
        return agents.arraySize;
    }

    public string[] GetAgentNames()
    {
        var agentnamesProp = m_NavMeshProjectSettingsSo.FindProperty("m_SettingNames");
        var names = new string[agentnamesProp.arraySize];
        for (var index = 0; index < names.Length; index++)
        {
            names[index] = agentnamesProp.GetArrayElementAtIndex(index).stringValue;
        }
        return names;
    }
}

public class NavMeshSettingsBakeParamSetter
{
    private SerializedObject m_NavigationSettings;
    private SerializedProperty m_AgentRadius;
    private SerializedProperty m_AgentHeight;
    private SerializedProperty m_MaxSlope;
    private SerializedProperty m_StepHeight;
    private SerializedProperty m_DdropHeight;
    private SerializedProperty m_JumpDistance;
    private SerializedProperty m_MmanualVoxelSize;
    private SerializedProperty m_VoxelSize;
    private SerializedProperty m_MinRegionArea;
    private SerializedProperty m_HeightMesh;

    internal NavMeshSettingsBakeParamSetter(Scene scene)
    {
        SceneManager.SetActiveScene(scene);
        m_NavigationSettings = new SerializedObject(NavMeshBuilder.navMeshSettingsObject);
        m_AgentRadius = m_NavigationSettings.FindProperty("m_BuildSettings.agentRadius");
        m_AgentHeight = m_NavigationSettings.FindProperty("m_BuildSettings.agentHeight");
        m_MaxSlope = m_NavigationSettings.FindProperty("m_BuildSettings.agentSlope");
        m_StepHeight = m_NavigationSettings.FindProperty("m_BuildSettings.agentClimb");
        m_DdropHeight = m_NavigationSettings.FindProperty("m_BuildSettings.ledgeDropHeight");
        m_JumpDistance = m_NavigationSettings.FindProperty("m_BuildSettings.maxJumpAcrossDistance");
        m_MmanualVoxelSize = m_NavigationSettings.FindProperty("m_BuildSettings.manualCellSize");
        m_VoxelSize = m_NavigationSettings.FindProperty("m_BuildSettings.cellSize");
        m_MinRegionArea = m_NavigationSettings.FindProperty("m_BuildSettings.minRegionArea");
        m_HeightMesh = m_NavigationSettings.FindProperty("m_BuildSettings.accuratePlacement");
    }

    public float AgentRadius
    {
        get
        {
            return m_AgentRadius.floatValue;
        }
        set
        {
            m_AgentRadius.floatValue = value;
            m_NavigationSettings.ApplyModifiedProperties();
        }
    }

    public float AgentHeight
    {
        get
        {
            return m_AgentHeight.floatValue;
        }
        set
        {
            m_AgentHeight.floatValue = value;
            m_NavigationSettings.ApplyModifiedProperties();
        }
    }

    public float MaxSlope
    {
        get
        {
            return m_MaxSlope.floatValue;
        }
        set
        {
            m_MaxSlope.floatValue = value;
            m_NavigationSettings.ApplyModifiedProperties();
        }
    }

    public float StepHeight
    {
        get
        {
            return m_StepHeight.floatValue;
        }
        set
        {
            m_StepHeight.floatValue = value;
            m_NavigationSettings.ApplyModifiedProperties();
        }
    }

    public float DropHeight
    {
        get
        {
            return m_DdropHeight.floatValue;
        }
        set
        {
            m_DdropHeight.floatValue = value;
            m_NavigationSettings.ApplyModifiedProperties();
        }
    }

    public float JumpDistance
    {
        get
        {
            return m_JumpDistance.floatValue;
        }
        set
        {
            m_JumpDistance.floatValue = value;
            m_NavigationSettings.ApplyModifiedProperties();
        }
    }

    public bool ManualVoxelSize
    {
        get
        {
            return m_MmanualVoxelSize.boolValue;
        }
        set
        {
            m_MmanualVoxelSize.boolValue = value;
            m_NavigationSettings.ApplyModifiedProperties();
        }
    }

    public float VoxelSize
    {
        get
        {
            return m_VoxelSize.floatValue;
        }
        set
        {
            m_VoxelSize.floatValue = value;
            m_NavigationSettings.ApplyModifiedProperties();
        }
    }

    public float MinRegionArea
    {
        get
        {
            return m_MinRegionArea.floatValue;
        }
        set
        {
            m_MinRegionArea.floatValue = value;
            m_NavigationSettings.ApplyModifiedProperties();
        }
    }

    public bool HeightMesh
    {
        get
        {
            return m_HeightMesh.boolValue;
        }
        set
        {
            m_HeightMesh.boolValue = value;
            m_NavigationSettings.ApplyModifiedProperties();
        }
    }
}

public class NavigationAgent
{
    private SerializedObject m_NavMeshProjectSettingsObject;
    private int m_AgentIndex;
    private SerializedProperty m_AgentName;
    private SerializedProperty m_AgentRadius;
    private SerializedProperty m_AgentHeight;
    private SerializedProperty m_AgentStepHeight;
    private SerializedProperty m_AgentMaxSlope;

    public NavigationAgent(SerializedObject navMeshProjectSettingsObject, int agentIndex)
    {
        this.m_NavMeshProjectSettingsObject = navMeshProjectSettingsObject;
        this.m_AgentIndex = agentIndex;
        var _agents = m_NavMeshProjectSettingsObject.FindProperty("m_Settings");
        var _settingNames = m_NavMeshProjectSettingsObject.FindProperty("m_SettingNames");
        var agent = _agents.GetArrayElementAtIndex(agentIndex);
        m_AgentName = _settingNames.GetArrayElementAtIndex(agentIndex);
        m_AgentRadius = agent.FindPropertyRelative("agentRadius");
        m_AgentHeight = agent.FindPropertyRelative("agentHeight");
        m_AgentStepHeight = agent.FindPropertyRelative("agentClimb");
        m_AgentMaxSlope = agent.FindPropertyRelative("agentSlope");
    }

    public int AgentIndex
    {
        get { return m_AgentIndex; }
    }

    public string AgentName
    {
        get
        {
            return m_AgentName.stringValue;
        }
        set
        {
            m_AgentName.stringValue = value;
            m_NavMeshProjectSettingsObject.ApplyModifiedProperties();
        }
    }

    public float AgentRadius
    {
        get
        {
            return m_AgentRadius.floatValue;
        }
        set
        {
            m_AgentRadius.floatValue = value;
            m_NavMeshProjectSettingsObject.ApplyModifiedProperties();
        }
    }

    public float AgentHeight
    {
        get
        {
            return m_AgentHeight.floatValue;
        }
        set
        {
            m_AgentHeight.floatValue = value;
            m_NavMeshProjectSettingsObject.ApplyModifiedProperties();
        }
    }

    public float AgentStepHeight
    {
        get
        {
            return m_AgentStepHeight.floatValue;
        }
        set
        {
            m_AgentStepHeight.floatValue = value;
            m_NavMeshProjectSettingsObject.ApplyModifiedProperties();
        }
    }

    public float AgentMaxSlope
    {
        get
        {
            return m_AgentMaxSlope.floatValue;
        }
        set
        {
            m_AgentMaxSlope.floatValue = value;
            m_NavMeshProjectSettingsObject.ApplyModifiedProperties();
        }
    }
}

public class NavigationArea
{
    private SerializedObject m_NavMeshProjectSettingsObject;
    private int m_AreaIndex;
    private SerializedProperty m_Name;
    private SerializedProperty m_Cost;

    internal NavigationArea(SerializedObject navMeshProjectSettingsObject, int areaIndex)
    {
        this.m_NavMeshProjectSettingsObject = navMeshProjectSettingsObject;
        this.m_AreaIndex = areaIndex;
        var areas = m_NavMeshProjectSettingsObject.FindProperty("areas");

        if (areaIndex < 0 || areaIndex > areas.arraySize - 1)
        {
            throw new System.ArgumentOutOfRangeException("越界");
        }
        var area = areas.GetArrayElementAtIndex(areaIndex);
        m_Name = area.FindPropertyRelative("name");
        m_Cost = area.FindPropertyRelative("cost");
    }

    public int AreaIndex
    {
        get { return m_AreaIndex; }
    }

    public string Name
    {
        get
        {
            return m_Name.stringValue;
        }
        set
        {
            m_Name.stringValue = value;
            m_NavMeshProjectSettingsObject.ApplyModifiedProperties();
        }
    }

    public float Cost
    {
        get
        {
            return m_Cost.floatValue;
        }
        set
        {
            m_Cost.floatValue = value;
            m_NavMeshProjectSettingsObject.ApplyModifiedProperties();
        }
    }
}

using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NavMeshModifierSample
{
    [MenuItem("Tools/NavMesh/AgentTest")]
    private static void AgentTest()
    {
        var navMeshProjectSettings = NavMeshModifier.GetNavMeshProjectSettings();
        var agent = navMeshProjectSettings.GetAgentByIndex(0);
        Debug.Log($"Index={agent.AgentIndex},Name={agent.AgentName}," +
                  $"Radius={agent.AgentRadius},Height={agent.AgentHeight}," +
                  $"StepHeight={agent.AgentStepHeight},MaxSlope={agent.AgentMaxSlope}");
        agent.AgentName = "AgentTest";
        agent.AgentRadius = 1;
        agent.AgentHeight = 1;
        agent.AgentStepHeight = 1;
        agent.AgentMaxSlope = 52;
        Debug.Log($"Index={agent.AgentIndex},Name={agent.AgentName}," +
                  $"Radius={agent.AgentRadius},Height={agent.AgentHeight}," +
                  $"StepHeight={agent.AgentStepHeight},MaxSlope={agent.AgentMaxSlope}");
    }

    [MenuItem("Tools/NavMesh/AreaTest")]
    private static void AreaTest()
    {
        var navMeshProjectSettings = NavMeshModifier.GetNavMeshProjectSettings();
        var area = navMeshProjectSettings.GetNavigationArea(3);
        Debug.Log($"Index={area.AreaIndex},name={area.Name},cost={area.Cost}");
        area.Name = "AreaTest";
        area.Cost = 5;
        Debug.Log($"Index={area.AreaIndex},name={area.Name},cost={area.Cost}");
    }

    [MenuItem("Tools/NavMesh/BakeTest")]
    private static void BakeTest()
    {
        var bakeSetter = NavMeshModifier.GetNavMeshSettings(SceneManager.GetActiveScene());
        Debug.Log($"AgentRadius={bakeSetter.AgentRadius},AgentHeight={bakeSetter.AgentHeight}," +
                  $"MaxSlope={bakeSetter.MaxSlope},StepHeight={bakeSetter.StepHeight}," +
                  $"DropHeight={bakeSetter.DropHeight},JumpDistance={bakeSetter.JumpDistance}," +
                  $"ManualVoxelSize={bakeSetter.ManualVoxelSize},VoxelSize={bakeSetter.VoxelSize}," +
                  $"MinRegionArea={bakeSetter.MinRegionArea},HeightMesh={bakeSetter.HeightMesh}");
        bakeSetter.AgentRadius = 1;
        bakeSetter.AgentHeight = 1;
        bakeSetter.MaxSlope = 1;
        bakeSetter.StepHeight = 1;
        bakeSetter.DropHeight = 1;
        bakeSetter.JumpDistance = 1;
        bakeSetter.ManualVoxelSize = true;
        bakeSetter.VoxelSize = 1;
        bakeSetter.MinRegionArea = 1;
        bakeSetter.HeightMesh = true;
        Debug.Log($"AgentRadius={bakeSetter.AgentRadius},AgentHeight={bakeSetter.AgentHeight}," +
                  $"MaxSlope={bakeSetter.MaxSlope},StepHeight={bakeSetter.StepHeight}," +
                  $"DropHeight={bakeSetter.DropHeight},JumpDistance={bakeSetter.JumpDistance}," +
                  $"ManualVoxelSize={bakeSetter.ManualVoxelSize},VoxelSize={bakeSetter.VoxelSize}," +
                  $"MinRegionArea={bakeSetter.MinRegionArea},HeightMesh={bakeSetter.HeightMesh}");
    }

    [MenuItem("Tools/NavMesh/Redirect")]
    private static void Redirect()
    {
        var src = "Assets/Sample1.unity";
        var target = "Assets/Sample2.unity";
        var srcScene = EditorSceneManager.OpenScene(src, OpenSceneMode.Single);
        var navMeshData = NavMeshModifier.GetNavMeshData(srcScene);
        var targetScene = EditorSceneManager.OpenScene(target, OpenSceneMode.Single);
        NavMeshModifier.SetNavMeshData(targetScene, navMeshData);
    }
}

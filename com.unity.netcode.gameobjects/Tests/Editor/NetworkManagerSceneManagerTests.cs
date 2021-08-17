using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Unity.Netcode.EditorTests
{
    public class NetworkManagerSceneManagerTests
    {
        [Test]
        public void SceneManagerAssigned()
        {
            var gameObject = new GameObject(nameof(SceneManagerAssigned));
            var networkManager = gameObject.AddComponent<NetworkManager>();
            var transport = gameObject.AddComponent<DummyTransport>();
            networkManager.PopulateScenesInBuild(true);
            networkManager.ScenesInBuild.Scenes.Add(SceneManager.GetActiveScene().name);
            networkManager.NetworkConfig = new NetworkConfig();
            // Set dummy transport that does nothing
            networkManager.NetworkConfig.NetworkTransport = transport;

            NetworkSceneManager preManager = networkManager.SceneManager;

            // Start server to cause initialization process
            networkManager.StartServer();

            Debug.Assert(preManager == null);
            Debug.Assert(networkManager.SceneManager != null);

            Object.DestroyImmediate(gameObject);
        }
    }
}
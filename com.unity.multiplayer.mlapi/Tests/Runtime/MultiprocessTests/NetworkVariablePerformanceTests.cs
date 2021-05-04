using System;
using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace MLAPI.MultiprocessRuntimeTests
{
    public class NetworkVariablePerformanceTests : BaseMultiprocessTests
    {
        protected override int NbWorkers { get; } = 2;

        [UnityTest]
        public IEnumerator Test1()
        {
            // Call the method
            TestCoordinator.Instance.TriggerTestClientRpc(TestCoordinator.GetMethodInfo(ExecuteSimpleCoordinatorTest));

            for (int i = 0; i < NbWorkers; i++) // wait and test for the two clients
            {
                yield return new WaitUntil(TestCoordinator.SetResults());
                var resKey = TestCoordinator.Instance.CurrentResultClient;

                Debug.Log($"got results, asserting, result is {TestCoordinator.GetCurrentResult()} from key {resKey}");
                Assert.True(TestCoordinator.GetCurrentResult() > 0f);
            }
        }
    }
}

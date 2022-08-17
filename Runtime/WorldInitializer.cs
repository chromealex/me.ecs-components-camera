using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ME.ECS.Components.Camera {

    public static class WorldInitializer {

        public static DisposeStatic disposeStatic = new DisposeStatic();

        private static bool initialized = false;
        
        #if UNITY_EDITOR
        [UnityEditor.InitializeOnLoad]
        private static class EditorInitializer {
            static EditorInitializer() => WorldInitializer.Initialize();
        }
        #endif

        [UnityEngine.RuntimeInitializeOnLoadMethodAttribute(UnityEngine.RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Initialize() {
            
            if (WorldInitializer.initialized == false) {
                
                CoreComponentsInitializer.RegisterInitCallback(WorldInitializer.InitTypeId, WorldInitializer.Init, WorldInitializer.Init);
                WorldInitializer.initialized = true;
                
            }
            
        }

        public class DisposeStatic {
            ~DisposeStatic() {
                CoreComponentsInitializer.UnRegisterInitCallback(WorldInitializer.InitTypeId, WorldInitializer.Init, WorldInitializer.Init);

                WorldInitializer.initialized = false;
            }
        }
        
        public static void InitTypeId() {
            
            CameraComponentsInitializer.InitTypeId();
            
        }

        public static void Init(State state, ref World.NoState noState) {
    
            CameraComponentsInitializer.Init(state);

        }
    
        public static void Init(in Entity entity) {

            CameraComponentsInitializer.Init(in entity);
            
        }

    }

}
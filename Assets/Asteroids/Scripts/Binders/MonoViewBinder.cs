using System;
using MVVM;
using Sirenix.OdinInspector;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Asteroids.Scripts.Binders
{
    public sealed class MonoViewBinder : MonoBehaviour
    {
        private enum BindingMode
        {
            FromInstance = 0,
            FromResolve = 1,
            FromResolveId = 2
        }

        [SerializeField]
        private BindingMode viewBinding;

        [ShowIf(nameof(viewBinding), BindingMode.FromInstance)]
        [SerializeField]
        private Object view;

#if UNITY_EDITOR
        [ShowIf("@this.viewBinding == BindingMode.FromResolve || this.viewBinding == BindingMode.FromResolveId")]
        [SerializeField]
        private MonoScript viewType;
#endif
        [SerializeField, HideInInspector]
        private string viewTypeName;

        [ShowIf(nameof(viewBinding), BindingMode.FromResolveId)]
        [SerializeField]
        private string viewId;

        [Space(8)]
        [SerializeField]
        private BindingMode viewModelBinding;

        [ShowIf(nameof(viewModelBinding), BindingMode.FromInstance)]
        [SerializeField]
        private Object viewModel;

#if UNITY_EDITOR
        [ShowIf("@this.viewModelBinding == BindingMode.FromResolve || this.viewModelBinding == BindingMode.FromResolveId")]
        [SerializeField]
        private MonoScript viewModelType;
#endif
        [SerializeField, HideInInspector]
        private string viewModelTypeName;
        [ShowIf(nameof(viewModelBinding), BindingMode.FromResolveId)]
        [SerializeField]
        private string viewModelId;

        [Inject]
        private DiContainer diContainer;

        private IBinder _binder;

        private void Awake()
        {
#if UNITY_EDITOR
            SyncTypeNames();
#endif
            _binder = this.CreateBinder();
        }

        private void OnEnable()
        {
            _binder.Bind();
        }

        private void OnDisable()
        {
            _binder.Unbind();
        }

        private IBinder CreateBinder()
        {
            Type resolvedViewType = ResolveType(viewTypeName, nameof(viewTypeName));
            Type resolvedViewModelType = ResolveType(viewModelTypeName, nameof(viewModelTypeName));

            object view = this.viewBinding switch
            {
                BindingMode.FromInstance => this.view,
                BindingMode.FromResolve => this.diContainer.Resolve(resolvedViewType),
                BindingMode.FromResolveId => this.diContainer.ResolveId(resolvedViewType, this.viewId),
                _ => throw new Exception($"Binding type of view {this.viewBinding} is not found!")
            };

            object model = this.viewModelBinding switch
            {
                BindingMode.FromInstance => this.viewModel,
                BindingMode.FromResolve => this.diContainer.Resolve(resolvedViewModelType),
                BindingMode.FromResolveId => this.diContainer.ResolveId(resolvedViewModelType, this.viewModelId),
                _ => throw new Exception($"Binding type of view {this.viewBinding} is not found!")
            };

            return BinderFactory.CreateComposite(view, model);
        }

        private static Type ResolveType(string typeName, string fieldName)
        {
            if (string.IsNullOrWhiteSpace(typeName))
                return null;

            Type type = Type.GetType(typeName);

            if (type == null)
                throw new Exception($"Cannot resolve type from {fieldName}: '{typeName}'");

            return type;
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            SyncTypeNames();
        }

        private void SyncTypeNames()
        {
            viewTypeName = viewType != null
                ? viewType.GetClass()?.AssemblyQualifiedName
                : null;
            viewModelTypeName = viewModelType != null
                ? viewModelType.GetClass()?.AssemblyQualifiedName
                : null;
        }
#endif
    }
}
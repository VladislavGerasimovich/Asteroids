using System;
using MVVM;
using Sirenix.OdinInspector;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace SampleGame
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
        [ShowIf(nameof(viewModelBinding), BindingMode.FromResolveId)]
        [SerializeField]
        private string viewModelId;

        [Inject]
        private DiContainer diContainer;

        private IBinder _binder;

        private void Awake()
        {
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
            object view = this.viewBinding switch
            {
                BindingMode.FromInstance => this.view,
                BindingMode.FromResolve => this.diContainer.Resolve(this.viewType.GetClass()),
                BindingMode.FromResolveId => this.diContainer.ResolveId(this.viewType.GetClass(), this.viewId),
                _ => throw new Exception($"Binding type of view {this.viewBinding} is not found!")
            };

            object model = this.viewModelBinding switch
            {
                BindingMode.FromInstance => this.viewModel,
                BindingMode.FromResolve => this.diContainer.Resolve(this.viewModelType.GetClass()),
                BindingMode.FromResolveId => this.diContainer.ResolveId(this.viewModelType.GetClass(), this.viewModelId),
                _ => throw new Exception($"Binding type of view {this.viewBinding} is not found!")
            };

            return BinderFactory.CreateComposite(view, model);
        }
    }
}
using System;
using MVVM;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Asteroids.Scripts.Binders
{
    public sealed class ButtonBinder : IBinder
    {
        private readonly Button view;
        private readonly UnityAction modelAction;

        public ButtonBinder(Button view, Action model)
        {
            this.view = view;
            modelAction = new UnityAction(model);
        }

        void IBinder.Bind()
        {
            view.onClick.AddListener(this.modelAction);
        }

        void IBinder.Unbind()
        {
            view.onClick.RemoveListener(this.modelAction);
        }
    }
}
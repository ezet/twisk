using System;
using System.Collections.Generic;
using Caliburn.Micro;
using eZet.Twisk.Services;
using eZet.Twisk.ViewModels;

namespace eZet.Twisk {
    public class Bootstrapper : BootstrapperBase {
        
        SimpleContainer _container;

        public Bootstrapper() {
            Initialize();
        }

        protected override void Configure() {
            _container = new SimpleContainer();

            _container.Singleton<IWindowManager, WindowManager>();
            _container.Singleton<IEventAggregator, EventAggregator>();
            _container.PerRequest<ShellViewModel>();
            _container.PerRequest<OverlayViewModel>();

            _container.PerRequest<EveOnlineApiService>();

        }

        protected override object GetInstance(Type service, string key) {
            var instance = _container.GetInstance(service, key);
            if (instance != null)
                return instance;

            throw new Exception("Could not locate any instances.");
        }

        protected override IEnumerable<object> GetAllInstances(Type service) {
            return _container.GetAllInstances(service);
        }

        protected override void BuildUp(object instance) {
            _container.BuildUp(instance);
        }

        protected override void OnStartup(object sender, System.Windows.StartupEventArgs e) {
            DisplayRootViewFor<ShellViewModel>();
        }
    }
}
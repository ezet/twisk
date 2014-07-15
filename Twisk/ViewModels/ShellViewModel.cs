using System.Linq;
using Caliburn.Micro;
using eZet.EveLib.Modules.Models.Character;
using eZet.Twisk.Models;
using eZet.Twisk.Services;

namespace eZet.Twisk.ViewModels {
    public class ShellViewModel : Screen {
        private readonly IWindowManager _windowManager;
        private readonly EveOnlineApiService _apiService;
        private int _keyId;
        private string _vCode;
        private int _accountKey;
        private OverlayViewModel _overlayViewModel;
        private WalletJournal.JournalEntry _topDonation;
        private BindableCollection<WalletJournal.JournalEntry> _donations;

        public ShellViewModel(IWindowManager windowManager, EveOnlineApiService apiService) {
            _windowManager = windowManager;
            _apiService = apiService;
            DisplayName = "Twisk";
            KeyId = Properties.Settings.Default.ApiKeyId;
            VCode = Properties.Settings.Default.ApiKeyVCode;
            AccountKey = Properties.Settings.Default.AccountKey;
            Donations = new BindableCollection<WalletJournal.JournalEntry>();
            OverlayViewModel = IoC.Get<OverlayViewModel>();
            OverlayViewModel.Initialize(Donations);
        }

        public WalletJournal.JournalEntry TopDonation {
            get { return _topDonation; }
            private set {
                if (Equals(value, _topDonation)) return;
                _topDonation = value;
                NotifyOfPropertyChange();
            }
        }

        public BindableCollection<WalletJournal.JournalEntry> Donations {
            get { return _donations; }
            private set {
                if (Equals(value, _donations)) return;
                _donations = value;
                NotifyOfPropertyChange();
            }
        }

        public OverlayViewModel OverlayViewModel {
            get { return _overlayViewModel; }
            private set {
                if (Equals(value, _overlayViewModel)) return;
                _overlayViewModel = value;
                NotifyOfPropertyChange();
                NotifyOfPropertyChange(() => CanExecuteHideOverlay);
                NotifyOfPropertyChange(() => CanExecuteShowOverlay);
            }
        }


        public int KeyId {
            get { return _keyId; }
            set {
                if (value == _keyId) return;
                _keyId = value;
                NotifyOfPropertyChange();
            }
        }

        public string VCode {
            get { return _vCode; }
            set {
                if (value == _vCode) return;
                _vCode = value;
                NotifyOfPropertyChange();
            }
        }

        public int AccountKey {
            get { return _accountKey; }
            set {
                if (value == _accountKey) return;
                _accountKey = value;
                NotifyOfPropertyChange();
            }
        }

        public void ExecuteShowOverlay() {
            OverlayViewModel = IoC.Get<OverlayViewModel>();
            _windowManager.ShowWindow(OverlayViewModel);
        }

        public bool CanExecuteShowOverlay {
            get { return OverlayViewModel != null; }
        }

        public void ExecuteHideOverlay() {
            OverlayViewModel.TryClose();
        }

        public bool CanExecuteHideOverlay {
            get { return OverlayViewModel != null; }
        }

        public void ExecuteSetEntity() {
            Properties.Settings.Default.ApiKeyId = KeyId;
            Properties.Settings.Default.ApiKeyVCode = VCode;
            Properties.Settings.Default.AccountKey = AccountKey;
            Properties.Settings.Default.Save();
        }

        public async void ExecuteUpdateNow() {
            var journal = await _apiService.GetJournalEntriesAsync(KeyId, VCode, AccountKey).ConfigureAwait(false);
            Donations.AddRange(journal.Take(5));
            Donations.Refresh();
            OverlayViewModel.Initialize(Donations);
        }
    }
}

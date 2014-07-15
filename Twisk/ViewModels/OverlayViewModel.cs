using Caliburn.Micro;
using eZet.EveLib.Modules.Models.Character;
using eZet.Twisk.Models;

namespace eZet.Twisk.ViewModels {
    public class OverlayViewModel : Screen {
        private BindableCollection<WalletJournal.JournalEntry> _donations;

        public OverlayViewModel() {

        }

        public BindableCollection<WalletJournal.JournalEntry> Donations {
            get { return _donations; }
            private set {
                if (Equals(value, _donations)) return;
                _donations = value;
                NotifyOfPropertyChange();
            }
        }

        public void Initialize(BindableCollection<WalletJournal.JournalEntry> entries) {
            Donations = entries;
        }
    }
}

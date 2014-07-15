using System.Linq;
using System.Threading.Tasks;
using eZet.EveLib.Modules;
using eZet.EveLib.Modules.Models;
using eZet.EveLib.Modules.Models.Character;

namespace eZet.Twisk.Services {
    public class EveOnlineApiService {

        public async Task<EveOnlineRowCollection<WalletJournal.JournalEntry>> GetJournalEntriesAsync(int keyId, string vCode, int accountKey) {
            var key = new ApiKey(keyId, vCode);
            key = key.GetActualKey();
            EveOnlineRowCollection<WalletJournal.JournalEntry> journal;
            if (key.GetType() == typeof(CharacterKey)) {
                var response = await (key as CharacterKey).Characters.FirstOrDefault().GetWalletJournalAsync(2000);
                journal = response.Result.Journal;
            } else {
                var response = await (key as CorporationKey).Corporation.GetWalletJournalAsync(accountKey, 2000);
                journal = response.Result.Journal;
            }
            return journal;
        }

    }
}

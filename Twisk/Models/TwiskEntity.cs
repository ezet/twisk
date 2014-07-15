namespace eZet.Twisk.Models {
    public class TwiskEntity {

        public TwiskEntity(int keyId, string vCode, int accountKey) {
            AccountKey = accountKey;
            VCode = vCode;
            KeyId = keyId;
        }

        public int KeyId { get; private set; }

        public string VCode { get; private set; }

        public int AccountKey { get; private set; }

    }
}

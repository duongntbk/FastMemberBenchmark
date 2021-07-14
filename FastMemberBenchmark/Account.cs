namespace FastMemberBenchmark
{
    public class Account
    {
        private string _password;

        private int _balance;

        public string Name { get; set; }

        public string Password
        {
            set => _password = value;

            private get => _password;
        }

        public int Balance
        {
            private set => _balance = value;

            get => _balance;
        }

        public Account()
        {
            Name = "duongntbk";
            _password = "letmein";
            _balance = 1_000;
        }
    }
}

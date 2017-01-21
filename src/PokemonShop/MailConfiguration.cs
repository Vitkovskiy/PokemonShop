namespace PokemonShop
{
    public class MailConfiguration
    {
        public bool IsValid
        {
            get
            {
                return !(string.IsNullOrEmpty(SMTPServer)
                         && string.IsNullOrEmpty(Login)
                         && string.IsNullOrEmpty(Password));
            }
        }

        public string SMTPServer { get; set; }
        public int SMTPPort { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}

namespace a.Mvvm.Organic
{
    internal class MainWindowViewModel
    {
        private User user = new User();

        public string FirstName
        {
            get { return user.FirstName; }
            set
            {
                user.FirstName = value;
            }
        }

        public string LastName
        {
            get { return user.LastName; }
            set
            {
                user.LastName = value;
            }
        }

        public string FullName => $"{FirstName} {LastName}".Trim();
    }
}

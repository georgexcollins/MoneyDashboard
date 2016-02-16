namespace MoneyDashboard
{
    public interface IUserRegistrationStore
    {
        void Save(UserRegistration newReg);
        UserRegistration Load(string email);
    }
}
using DomainModel;

namespace Business.Contracts
{
    public interface IMailBusiness
    {
        public void SignInConfirmationMail(User user, string url);
    }
}
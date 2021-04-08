using ApiNCoreEApplication1.Entity;

using ApiNCoreEApplication1.Entity.UnitofWork;
namespace ApiNCoreEApplication1.Domain.Service
{
    public class EnigmaUsersServiceAsync<Tv, Te> : GenericServiceAsync<Tv, Te>
                                        where Tv : EnigmaUsersViewModel
                                        where Te : EnigmaUser
    {
        public EnigmaUsersServiceAsync(IUnitOfWork unitOfWork)
        {
            if (_unitOfWork == null)
                _unitOfWork = unitOfWork;
        }
    }
}
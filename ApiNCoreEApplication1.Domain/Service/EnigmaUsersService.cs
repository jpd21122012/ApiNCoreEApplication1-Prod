using ApiNCoreEApplication1.Entity;

using ApiNCoreEApplication1.Entity.UnitofWork;
namespace ApiNCoreEApplication1.Domain.Service
{
    public class EnigmaUsersService<Tv, Te> : GenericService<Tv, Te>
                                        where Tv : EnigmaUsersViewModel
                                        where Te : EnigmaUser
    {
        public EnigmaUsersService(IUnitOfWork unitOfWork)
        {
            if (_unitOfWork == null)
                _unitOfWork = unitOfWork;
        }
        public bool DoNothing()
        {
            return true;
        }
    }
}
using ApiNCoreEApplication1.Entity;

using ApiNCoreEApplication1.Entity.UnitofWork;
namespace ApiNCoreEApplication1.Domain.Service
{
    public class EnigmaUsersTypeService<Tv, Te> : GenericService<Tv, Te>
                                        where Tv : EnigmaUsersTypeViewModel
                                        where Te : EnigmaUsersType
    {
        public EnigmaUsersTypeService(IUnitOfWork unitOfWork)
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
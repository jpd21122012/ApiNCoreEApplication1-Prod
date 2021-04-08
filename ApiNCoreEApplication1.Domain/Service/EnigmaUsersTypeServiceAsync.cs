using ApiNCoreEApplication1.Entity;

using ApiNCoreEApplication1.Entity.UnitofWork;
namespace ApiNCoreEApplication1.Domain.Service
{
    public class EnigmaUsersTypeServiceAsync<Tv, Te> : GenericServiceAsync<Tv, Te>
                                        where Tv : EnigmaUsersTypeViewModel
                                        where Te : EnigmaUsersType
    {
        public EnigmaUsersTypeServiceAsync(IUnitOfWork unitOfWork)
        {
            if (_unitOfWork == null)
                _unitOfWork = unitOfWork;
        }
    }
}
using ApiNCoreEApplication1.Entity;
using ApiNCoreEApplication1.Entity.UnitofWork;

namespace ApiNCoreEApplication1.Domain.Service
{
    public class AwardsServiceAsync<Tv, Te> : GenericServiceAsync<Tv, Te>
                                        where Tv : AwardsViewModel
                                        where Te : Award
    {
        public AwardsServiceAsync(IUnitOfWork unitOfWork)
        {
            if (_unitOfWork == null)
                _unitOfWork = unitOfWork;
        }
    }
}
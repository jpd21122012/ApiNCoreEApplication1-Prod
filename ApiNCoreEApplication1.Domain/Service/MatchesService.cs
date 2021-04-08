using ApiNCoreEApplication1.Entity;

using ApiNCoreEApplication1.Entity.UnitofWork;
namespace ApiNCoreEApplication1.Domain.Service
{
    public class MatchesService<Tv, Te> : GenericService<Tv, Te>
                                        where Tv : MatchesViewModel
                                        where Te : Match
    {
        public MatchesService(IUnitOfWork unitOfWork)
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

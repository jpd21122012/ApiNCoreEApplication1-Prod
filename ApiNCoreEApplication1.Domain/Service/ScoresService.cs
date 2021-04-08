using ApiNCoreEApplication1.Entity;

using ApiNCoreEApplication1.Entity.UnitofWork;
namespace ApiNCoreEApplication1.Domain.Service
{
    public class ScoresService<Tv, Te> : GenericService<Tv, Te>
                                        where Tv : ScoresViewModel
                                        where Te : Score
    {
        public ScoresService(IUnitOfWork unitOfWork)
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

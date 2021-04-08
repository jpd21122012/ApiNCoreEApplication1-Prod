using ApiNCoreEApplication1.Entity;

using ApiNCoreEApplication1.Entity.UnitofWork;
namespace ApiNCoreEApplication1.Domain.Service
{
    public class PlayerStatsService<Tv, Te> : GenericService<Tv, Te>
                                        where Tv : PlayerStatsViewModel
                                        where Te : PlayerStat
    {
        public PlayerStatsService(IUnitOfWork unitOfWork)
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

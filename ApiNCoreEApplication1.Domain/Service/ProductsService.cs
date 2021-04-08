using ApiNCoreEApplication1.Entity;

using ApiNCoreEApplication1.Entity.UnitofWork;
namespace ApiNCoreEApplication1.Domain.Service
{
    public class ProductsService<Tv, Te> : GenericService<Tv, Te>
                                        where Tv : ProductsViewModel
                                        where Te : Product
    {
        public ProductsService(IUnitOfWork unitOfWork)
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
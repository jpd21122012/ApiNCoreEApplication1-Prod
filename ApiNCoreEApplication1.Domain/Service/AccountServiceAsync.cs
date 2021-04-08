﻿using ApiNCoreEApplication1.Entity;
using ApiNCoreEApplication1.Entity.UnitofWork;

namespace ApiNCoreEApplication1.Domain.Service
{
    public class AccountServiceAsync<Tv, Te> : GenericServiceAsync<Tv, Te>
                                        where Tv : AccountViewModel
                                        where Te : Account
    {
        public AccountServiceAsync(IUnitOfWork unitOfWork)
        {
            if (_unitOfWork == null)
                _unitOfWork = unitOfWork;
        }
    }
}

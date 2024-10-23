using CardManagement.Application.Dtos.Card;
using CardManagement.Application.Dtos.SysUser;
using CardManagement.Domain.Model;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardManagement.Application.IRepository
{
    public interface IAPIClientRepo
    {
        public Task<bool> PostCardApplicationCall(CardApplicationRequest request);

    }
}

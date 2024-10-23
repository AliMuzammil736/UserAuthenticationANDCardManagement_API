using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardManagement.Application.Utils
{
    public class GeneralResponse<T>
    {
        public string ErrCode { get; set; }
        public T? Data { get; set; }
        public GeneralResponse(string errCode)
        {
            ErrCode = errCode;
            Data = default(T);
        }
        public GeneralResponse(string errCode, T data)
        {
            ErrCode = errCode;
            Data = data;
        } 

    }
}

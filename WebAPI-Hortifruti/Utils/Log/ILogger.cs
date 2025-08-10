using System;
using System.Threading.Tasks;

namespace WebAPI_Hortifruti.Utils.Log
{
    internal interface ILogger
    {
        Task Log(Exception ex);
    }
}

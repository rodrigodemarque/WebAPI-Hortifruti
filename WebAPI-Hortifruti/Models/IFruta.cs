using System;

namespace WebAPI_Hortifruti.Models
{
    internal interface IFruta
    {
        int Id { get; set; }
        string Nome { get; set; }
        DateTime DataVenc {  get; set; }
    }
}

using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Dominio.Entidades
{
    public enum Status
    {
        [Description("Criado")]
        Criado = 0,
        [Description("Executando")]
        Executando = 1,
        [Description("Finalizado")]
        Finalizado = 2,
        [Description("Erro")]
        Erro = 3

    }

    
}


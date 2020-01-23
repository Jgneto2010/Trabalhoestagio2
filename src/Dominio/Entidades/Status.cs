using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Dominio.Entidades
{
    public enum Status
    {
        
        [Description("Executando")]
        Executando = 0,
        [Description("Finalizado")]
        Finalizado = 1,
        [Description("Erro")]
        Erro = 2
    }

    
}


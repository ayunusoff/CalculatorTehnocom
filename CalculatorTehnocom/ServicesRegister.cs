﻿using CalculatorTehnocom.Tokenizers;
using CalculatorTehnocom.Tokenizers.TokenReaders;
using Microsoft.Extensions.DependencyInjection;

namespace CalculatorTehnocom
{
    public static class ServicesRegister
    {
        public static IServiceCollection AddTokenizer(this IServiceCollection services)
        {
            foreach (var tokenReader in typeof(ServicesRegister).Assembly.GetTypes()
                .Where(t => !t.IsAbstract && t.IsAssignableTo(typeof(ITokenReader)))
            )
            {
                services.AddSingleton(typeof(ITokenReader), tokenReader);
            }

            services.AddSingleton<ITokenizer, Tokenizer>();
            
            return services;
        }

    }
}

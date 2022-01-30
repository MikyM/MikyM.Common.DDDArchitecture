﻿// This file is part of Lisbeth.Bot project
//
// Copyright (C) 2021 Krzysztof Kupisz - MikyM
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU Affero General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Affero General Public License for more details.
// 
// You should have received a copy of the GNU Affero General Public License
// along with this program.  If not, see <https://www.gnu.org/licenses/>.


using Autofac;
using MikyM.Common.Utilities;

namespace MikyM.Common.Application;

/// <summary>
/// Registration extension configuration
/// </summary>
public sealed class RegistrationConfiguration
{
    internal ContainerBuilder Builder { get; set; }

    internal  RegistrationConfiguration(ContainerBuilder builder)
    {
        this.Builder = builder;
    }

    /// <summary>
    /// Registers an interceptor with <see cref="ContainerBuilder"/>
    /// </summary>
    /// <param name="factoryMethod">Factory method for the registration</param>
    /// <returns>Current instance of the <see cref="RegistrationConfiguration"/></returns>
    public RegistrationConfiguration AddInterceptor<T>(Func<IComponentContext, T> factoryMethod) where T : notnull
    {
        Builder.Register(factoryMethod);

        return this;
    }

    /// <summary>
    /// Registers an async executor with the container
    /// <returns>Current instance of the <see cref="RegistrationConfiguration"/></returns>
    public RegistrationConfiguration AddAsyncExecutor()
    {
        Builder.AddAsyncExecutor();
        return this;
    }
}

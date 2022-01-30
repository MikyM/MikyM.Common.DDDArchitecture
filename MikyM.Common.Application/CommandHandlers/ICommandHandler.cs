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

using MikyM.Common.Application.CommandHandlers.Commands;
using MikyM.Common.Application.Results;

namespace MikyM.Common.Application.CommandHandlers;

public interface ICommandHandler
{
}

public interface ICommandHandler<in TCommand> : ICommandHandler where TCommand : class, ICommand
{
    Task<Result> HandleAsync(TCommand command);
}

public interface ICommandHandler<in TCommand, TResult> : ICommandHandler where TCommand : class, ICommand<TResult>
{
    Task<Result<TResult>> HandleAsync(TCommand command);
}

﻿// Copyright 2013 Nicholas Blumhardt
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using Serilog.Core;
using Serilog.Events;
using Serilog.Parsing;

namespace Serilog.Policies
{
    class NullableDestructuringPolicy : IDestructuringPolicy
    {
        public bool TryDestructure(object value, Destructuring destructuring, ILogEventPropertyValueFactory propertyValueFactory,
                                   out LogEventPropertyValue result)
        {
            var type = value.GetType();
            if (!type.IsGenericType || type.GetGenericTypeDefinition() != typeof(Nullable<>))
            {
                result = null;
                return false;
            }

            var dynamicValue = (dynamic)value;
            result = propertyValueFactory.CreatePropertyValue(
                dynamicValue.HasValue ? dynamicValue.Value : null, destructuring);

            return true;
        }
    }
}
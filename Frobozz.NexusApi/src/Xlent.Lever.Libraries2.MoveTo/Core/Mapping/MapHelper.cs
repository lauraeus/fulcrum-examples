﻿using System;
using System.Threading;
using System.Threading.Tasks;
using Xlent.Lever.Libraries2.Core.Assert;
using Xlent.Lever.Libraries2.Core.Error.Logic;

namespace Xlent.Lever.Libraries2.MoveTo.Core.Mapping
{
    public static class MapHelper
    {
        public static TTarget MapId<TTarget, TSource>(TSource sourceId)
        {
            if (sourceId == null) return default(TTarget);
            if (Equals(sourceId, default(TSource))) return default(TTarget);
            var sourceType = typeof(TSource);
            var targetType = typeof(TTarget);
            if (targetType == typeof(string))
            {
                return (TTarget) (object) sourceId.ToString();
            }
            if (targetType == typeof(Guid))
            {
                var success = Guid.TryParse(sourceId.ToString(), out Guid targetId);
                InternalContract.Require(success, $"Could not parse parameter {nameof(sourceId)} ({sourceId}) of type {sourceType.Name} into type Guid.");
                return (TTarget) (object)targetId;
            }
            throw new FulcrumNotImplementedException($"There is currently no rule on how to convert an id from type {sourceType.Name} to type {targetType.Name}.");
        }

        public static async Task<TTarget> CreateAndMapToAsync<TTarget, TSource, TLogic>(TSource source, TLogic logic, CancellationToken token = default(CancellationToken)) 
            where TTarget : IMapper<TSource, TLogic>, new()
        {
            if (Equals(source, default(TSource))) return default(TTarget);
            var target = new TTarget();
            await target.MapFromAsync(source, logic, token);
            return target;
        }
    }
}

﻿using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using Xlent.Lever.Libraries2.Core.Assert;
using Xlent.Lever.Libraries2.Core.Storage.Model;
using Xlent.Lever.Libraries2.MoveTo.Core.Translation;

namespace Xlent.Lever.Libraries2.MoveTo.Core.ClientTranslators
{
    public class ReadClientTranslator<TModel> : ClientTranslatorBase, IReadAll<TModel, string>
    where TModel : IValidatable
    {
        private readonly IReadAll<TModel, string> _storage;

        public ReadClientTranslator(IReadAll<TModel, string> storage, string idConceptName)
        :base(idConceptName)
        {
            _storage = storage;
        }

        /// <inheritdoc />
        [HttpGet]
        [Route("{id}")]
        public virtual async Task<TModel> ReadAsync(string id, CancellationToken token = default(CancellationToken))
        {
            var translator = new TranslationHelper(ClientName);
            id = translator.DecorateId(IdConceptName, id);
            var decoratedResult = await _storage.ReadAsync(id, token);
            await translator.Add(decoratedResult).ExecuteAsync();
            return translator.Translate(decoratedResult);   
        }

        /// <inheritdoc />
        [HttpGet]
        [Route("WithPaging")]
        public virtual async Task<PageEnvelope<TModel>> ReadAllWithPagingAsync(int offset, int? limit = null, CancellationToken token = default(CancellationToken))
        {
            var translator = new TranslationHelper(ClientName);
            var decoratedResult =  await _storage.ReadAllWithPagingAsync(offset, limit, token);
            await translator.Add(decoratedResult).ExecuteAsync();
            return translator.Translate(decoratedResult);
        }

        /// <inheritdoc />
        [HttpGet]
        [Route("")]
        public virtual async Task<IEnumerable<TModel>> ReadAllAsync(int limit = int.MaxValue, CancellationToken token = default(CancellationToken))
        {
            var translator = new TranslationHelper(ClientName);
            var decoratedResult = await _storage.ReadAllAsync(limit, token);
            var decoratedArray = decoratedResult as TModel[] ?? decoratedResult.ToArray();
            await translator.Add(decoratedArray).ExecuteAsync();
            return translator.Translate(decoratedArray);
        }
    }
}
﻿using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Crossover.Common.Abstractions;
using Crossover.Core.Abstractions;

namespace Crossover.WebAPI.Services.Controllers
{
    public class BlogPostController : ApiController
    {
        private readonly IPostRepository _repository;

        public BlogPostController(IPostRepository repository)
        {
            _repository = repository;
        }

        public IQueryable<IPost> Get()
        {
            return _repository.GetAll();
        }

        public IPost Get(int id)
        {
            return _repository.Get(id);
        }

        public HttpResponseMessage Post(IPost post)
        {
            _repository.Create(post);
            var response = Request.CreateResponse(HttpStatusCode.Created);
            response.StatusCode = HttpStatusCode.Created;
            var uri = Url.Link("DefaultApi", new {id = post.Id});
            response.Headers.Location = new Uri(uri);
            return response;
        }

        public HttpResponseMessage Put(int id, IPost post)
        {
            post.Id = id;
            _repository.Update(post);
            var response = Request.CreateResponse(HttpStatusCode.NoContent);
            var uri = Url.Link("DefaultApi", new {id = post.Id});
            response.Headers.Location = new Uri(uri);
            return response;
        }

        public HttpResponseMessage Delete(int id)
        {
            _repository.Delete(id);
            var response = Request.CreateResponse(HttpStatusCode.NoContent);
            return response;
        }
    }
}
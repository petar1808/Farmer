﻿using Application.Exceptions;
using Application.Models.Articles;
using Application.Models.Common;
using AutoMapper;
using Domain.Enum;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Articles
{
    public class ArticleService : IArticleService
    {
        private readonly IFarmerDbContext farmerDbContext;
        private readonly IMapper mapper;

        public ArticleService(IFarmerDbContext farmerDbContext, IMapper mapper)
        {
            this.farmerDbContext = farmerDbContext;
            this.mapper = mapper;
        }

        public async Task Add(AddArticleModel articleModel)
        {
            var article = new Article(articleModel.Name, articleModel.ArticleType);

            await farmerDbContext.AddAsync(article);
            await farmerDbContext.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var article = await farmerDbContext
                .Articles
                .FirstOrDefaultAsync(x => x.Id == id);

            if (article == null)
            {
                throw new BadRequestExeption($"Article with Id: {id}, don't exist");
            }

            farmerDbContext.Articles.Remove(article);
            await farmerDbContext.SaveChangesAsync();
        }

        public async Task Edit(EditArticleModel articleModel)
        {
            var article = await farmerDbContext
                .Articles
                .FirstOrDefaultAsync(x => x.Id == articleModel.Id);

            if (article == null)
            {
                throw new BadRequestExeption($"Article with Id: {articleModel.Id}, don't exist");
            }

            article
                .UpdateName(articleModel.Name)
                .UpdateArticleType(articleModel.ArticleType);

            farmerDbContext.Update(article);
            await farmerDbContext.SaveChangesAsync();
        }

        public async Task<GetArticleModel> Get(int id)
        {
            var article = await farmerDbContext
                .Articles
                .FirstOrDefaultAsync(x => x.Id == id);

            if (article == null)
            {
                throw new BadRequestExeption($"Article with Id: {id}, don't exist");
            }

            var result = mapper.Map<GetArticleModel>(article);
            return result;
        }

        public async Task<List<ListArticleModel>> List()
        {
            var articles = await farmerDbContext.Articles.ToListAsync();

            var result = mapper.Map<List<ListArticleModel>>(articles);
            return result;
        }

        public async Task<List<SelectionListModel>> SeedsArticlesSelectionList()
        {
            var articles = await farmerDbContext.Articles
                .Where(x => x.ArticleType == ArticleType.Seeds)
                .Select(x => new SelectionListModel(x.Id, x.Name))
                .ToListAsync();

            return articles;
        }

        public async Task<List<SelectionListModel>> TreatmentArticlesSelectionList()
        {
            var articles = await farmerDbContext.Articles
                .Where(x => x.ArticleType != ArticleType.Seeds)
                .Select(x => new SelectionListModel(x.Id, x.Name))
                .ToListAsync();

            return articles;
        }
    }     
}

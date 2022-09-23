﻿using Application.Models.Articles;
using Application.Models.Common;
using Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Articles
{
    public interface IArticleService
    {
        Task Add(AddArticleModel articleModel);

        Task Edit(EditArticleModel articleModel);

        Task<GetArticleModel> Get(int id);

        Task<List<ListArticleModel>> List();

        Task Delete(int id);

        Task<List<SelectionListModel>> SeedsArticlesSelectionList();

        Task<List<SelectionListModel>> TreatmentArticlesSelectionList();
    }
}

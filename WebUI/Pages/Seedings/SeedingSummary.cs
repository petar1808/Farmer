﻿using Microsoft.AspNetCore.Components;
using Radzen;
using WebUI.Services.Article;
using WebUI.Services.Seeding;
using WebUI.ServicesModel.Common;
using WebUI.ServicesModel.Seeding;

namespace WebUI.Pages.Seedings
{
    public partial class SeedingSummary
    {
        [Inject]
        public ISeedingService SeedingService { get; set; } = default!;

        [Inject]
        public IArticleService ArticleService { get; set; } = default!;

        [Inject]
        public DialogService DialogService { get; set; } = default!;

        [Parameter]
        public int SeedingId { get; set; }

        [Parameter]
        public bool IsModal { get; set; }

        public GetSeedingSummaryModel GetSeedingModel { get; set; } = default!;

        public List<SelectionListModel> AllArticleOfTypeSeeds { get; set; } = new List<SelectionListModel>();

        protected async override Task OnParametersSetAsync()
        {
            GetSeedingModel = await SeedingService.GetSeedingSummary(SeedingId);

            if (IsModal)
            {
                AllArticleOfTypeSeeds = await ArticleService.GetSeeds();
            }
        }
        public void OnDropDownChange(object value)
        {
            GetSeedingModel.ArticleId = (int)value;
        }

        public async Task OnEdit()
        {
            await DialogService.OpenAsync<SeedingSummary>($"Добавяне на Сеитба",
                new Dictionary<string, object>() { 
                    { "SeedingId", SeedingId },
                    { "IsModal", true}
                },
                options: new DialogOptions() { Width = "900px", Height = "370px" });

            GetSeedingModel = await SeedingService.GetSeedingSummary(SeedingId);

            this.StateHasChanged();
        }

        public async Task OnSubmit()
        {
            var response = await SeedingService.UpdateSeedingSummary(GetSeedingModel, SeedingId);

            DialogService.Close(response);
        }
    }
}
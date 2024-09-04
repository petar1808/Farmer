using Radzen;

namespace WebUI.Extensions
{
    public static class DialogServiceExtensions
    {
        public static async Task<bool?> ShowDeleteDialog(this DialogService dialogService, int recordId)
        {
            var confOptions = new ConfirmOptions()
            {
                CancelButtonText = "Отказ",
                OkButtonText = "Изтриване"
            };

            return await dialogService.Confirm(
                $"Сигурни ли сте че искате да <strong>изтриете</strong> запис с <strong>ИД:{recordId}</strong>",
                "Внимание",
                confOptions);
        }
    }
}

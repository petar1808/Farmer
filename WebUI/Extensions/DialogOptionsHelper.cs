using Radzen;

namespace WebUI.Extensions
{
    public static class DialogOptionsHelper
    {
        public static DialogOptions GetCommonDialogOptions()
        {
            return new DialogOptions()
            {
                Resizable = true,
                AutoFocusFirstElement = true,
                Draggable = true
            };
        }

        public static DialogOptions WithHeight(this DialogOptions options, string height)
        {
            options.Height = height;
            return options;
        }

        public static DialogOptions WithWidth(this DialogOptions options, string width)
        {
            options.Width = width;
            return options;
        }


        public static DialogOptions GetDeleteDialogDefaultOptions()
        {
            return new DialogOptions()
            {
                Draggable = true
            };
        }

        public static DialogOptions WithDefaultSize(this DialogOptions options)
        {
            options.Width = "500px";
            options.Height = "170px";
            return options;
        }
    }
}

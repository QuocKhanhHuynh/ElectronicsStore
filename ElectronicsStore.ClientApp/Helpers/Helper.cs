using ElectronicsStore.Models.Products;

namespace ElectronicsStore.ClientApp.Helpers
{
    public class Helper
    {
        public static void LimitNameLength(List<ProductQuickViewModel> list)
        {
            foreach (var i in list)
            {
                var subName = i.Name;
                if (i.Name.Length > 45)
                {
                    subName = i.Name.Substring(0, 45);
                    subName += "...";
                }
                i.Name = subName;
            }
        }
    }
}

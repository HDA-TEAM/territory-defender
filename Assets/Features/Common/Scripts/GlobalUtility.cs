public static class GlobalUtility
{
    public static void ResetView(ListTowerViewModel towerViewModel, ListHeroViewModel heroViewModel)
    {
        if (towerViewModel != null)
        {
            towerViewModel.ResetView();
        }

        if (heroViewModel != null)
        {
            heroViewModel.ResetView();
        }
    }

    public static void ResetRuneDetailView(ListRuneViewModel runeViewModel)
    {
        if (runeViewModel != null)
        {
            runeViewModel.SetupRuneDetailView(false);
        }
    }
}

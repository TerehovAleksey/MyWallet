namespace MyWallet.Client.UI;

public class StartScreenCarouselItem
{
    public string Title { get; init; } = default!;
    public string Description { get; init; } = default!;
    public string Image { get; init; } = default!;
}

public static class StartScreenCarousel
{
    public static List<StartScreenCarouselItem> GetItems() => new()
    {
        new()
        {
            Title = Strings.StartScreen1Title,
            Description = Strings.StartScreen1Description,
            Image = "start_screen_1.png"
        },
        new()
        {
            Title = Strings.StartScreen2Title,
            Description = Strings.StartScreen2Description,
            Image = "start_screen_2.png"
        },
        new()
        {
            Title = Strings.StartScreen3Title,
            Description = Strings.StartScreen3Description,
            Image = "start_screen_3.png"
        },
        new()
        {
            Title = Strings.StartScreen4Title,
            Description = Strings.StartScreen4Description,
            Image = "start_screen_4.png"
        },
        new()
        {
            Title = Strings.StartScreen5Title,
            Description = Strings.StartScreen5Description,
            Image = "start_screen_5.png"
        },
        new()
        {
            Title = Strings.StartScreen6Title,
            Description = Strings.StartScreen6Description,
            Image = "start_screen_6.png"
        }
    };
}
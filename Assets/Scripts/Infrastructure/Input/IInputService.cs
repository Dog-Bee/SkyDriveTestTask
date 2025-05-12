namespace Infrastructure.Input
{
    public interface IInputService
    {
        float GetHorizontal();
        bool IsBoostPressed();
        bool IsAnyKeyPressed();
    }
}
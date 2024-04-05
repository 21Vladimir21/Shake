namespace DefaultNamespace.Gates
{
    public class Gate: Food, IEatable
    {
        public void Eat(Player player)
        {
            player.TryApplyDamage(-_foodData.ValueShift);
            //player.PlayerAnimator.SetRunningTrigger();
            FoodEaten();
        }
    }
}
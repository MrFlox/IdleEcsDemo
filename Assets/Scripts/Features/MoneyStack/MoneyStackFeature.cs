using Features.MoneyStack.Systems;

namespace Features.MoneyStack
{
    public class MoneyStackFeature : UpdateFeatureWithContainer
    {
        protected override void Initialize()
        {
            AddSystem(Resolve<MoneyStackSystem>());
        }
    }
}
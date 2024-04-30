using Berries.Systems;
using Generators.Systems;
using Player.Systems;
using Scellecs.Morpeh.Addons.Feature;
using Systems;

class SimpleFeature: UpdateFeature
{
    private readonly AddGeneratorsSystem _generatorsSystem;
    private readonly SimpleFlyingBerrySystem _simpleFlyingBerrySystem;
    private readonly HilightObjectIfPlayerInRangeSystem _hilightObjectIfPlayerInRangeSystem;
    private readonly PlayerInputSystem _playerInputSystem;
    private readonly DeleteBerriesSystem _deleteBerriesSystem;
    private readonly PlayerAnimationSystem _playerAnimationSystem;
    private readonly ActivateBerriesSystem _activateBerriesSystem;
    private readonly TempClass _tempClass;


    public SimpleFeature(
        AddGeneratorsSystem generatorsSystem,
        SimpleFlyingBerrySystem simpleFlyingBerrySystem,
        HilightObjectIfPlayerInRangeSystem hilightObjectIfPlayerInRangeSystem,
        PlayerInputSystem playerInputSystem,
        DeleteBerriesSystem deleteBerriesSystem,
        PlayerAnimationSystem playerAnimationSystem,
        ActivateBerriesSystem activateBerriesSystem
    )
    {
        _generatorsSystem = generatorsSystem;
        _simpleFlyingBerrySystem = simpleFlyingBerrySystem;
        _hilightObjectIfPlayerInRangeSystem = hilightObjectIfPlayerInRangeSystem;
        _playerInputSystem = playerInputSystem;
        _deleteBerriesSystem = deleteBerriesSystem;
        _playerAnimationSystem = playerAnimationSystem;
        _activateBerriesSystem = activateBerriesSystem;
    }

    protected override void Initialize()
    {
        AddInitializer(_generatorsSystem);
        AddInitializer(new FloatingInitializer());
        
        AddSystem(_simpleFlyingBerrySystem);
        AddSystem(_hilightObjectIfPlayerInRangeSystem);
        AddSystem(_playerInputSystem);
        AddSystem(_deleteBerriesSystem);
        AddSystem(_playerAnimationSystem);
        
        AddSystem(new FloatingSystem());
        AddSystem(new BallGeneratorSystem());
        AddSystem(new SpawningBallsSystem());
        AddSystem(new GeneratrosActivatorSystem());
        AddSystem(_activateBerriesSystem);
    }
}
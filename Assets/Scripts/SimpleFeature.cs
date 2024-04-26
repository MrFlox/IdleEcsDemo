using Scellecs.Morpeh.Addons.Feature;
using Systems;

class SimpleFeature: UpdateFeature
{
    private readonly AddGeneratorsSystem _generatorsSystem;
    private readonly SimpleFlyingBerrySystem _simpleFlyingBerrySystem;
    private readonly GeneratorRadiusDrawerSystem _generatorRadiusDrawerSystem;
    private readonly PlayerInputSystem _playerInputSystem;
    private readonly DeleteBerriesSystem _deleteBerriesSystem;
    private readonly PlayerAnimationSystem _playerAnimationSystem;
    private readonly TempClass _tempClass;


    public SimpleFeature(
        AddGeneratorsSystem generatorsSystem,
        SimpleFlyingBerrySystem simpleFlyingBerrySystem,
        GeneratorRadiusDrawerSystem generatorRadiusDrawerSystem,
        PlayerInputSystem playerInputSystem,
        DeleteBerriesSystem deleteBerriesSystem,
        PlayerAnimationSystem playerAnimationSystem
    )
    {
        _generatorsSystem = generatorsSystem;
        _simpleFlyingBerrySystem = simpleFlyingBerrySystem;
        _generatorRadiusDrawerSystem = generatorRadiusDrawerSystem;
        _playerInputSystem = playerInputSystem;
        _deleteBerriesSystem = deleteBerriesSystem;
        _playerAnimationSystem = playerAnimationSystem;
    }

    protected override void Initialize()
    {
        AddSystem(_generatorsSystem);
        AddSystem(_simpleFlyingBerrySystem);
        AddSystem(_generatorRadiusDrawerSystem);
        AddSystem(_playerInputSystem);
        AddSystem(_deleteBerriesSystem);
        AddSystem(_playerAnimationSystem);
    }
}
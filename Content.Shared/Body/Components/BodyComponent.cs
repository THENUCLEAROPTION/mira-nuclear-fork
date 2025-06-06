using Content.Shared.Alert;
using Content.Shared.Body.Part;
using Content.Shared.Body.Prototypes;
using Content.Shared.Body.Systems;
using Robust.Shared.Audio;
using Robust.Shared.Containers;
using Robust.Shared.GameStates;
using Robust.Shared.Prototypes;

namespace Content.Shared.Body.Components;

[RegisterComponent, NetworkedComponent, AutoGenerateComponentState]
[Access(typeof(SharedBodySystem))]
public sealed partial class BodyComponent : Component
{
    /// <summary>
    /// Relevant template to spawn for this body.
    /// </summary>
    [DataField, AutoNetworkedField]
    public ProtoId<BodyPrototype>? Prototype;

    /// <summary>
    /// Container that holds the root body part.
    /// </summary>
    /// <remarks>
    /// Typically is the torso.
    /// </remarks>
    [ViewVariables]
    public (BodyPart BodyPart, ContainerSlot Container) RootContainer = default!;

    [ViewVariables]
    public ContainerSlot RootContainerVV
    {
        get => RootContainer.Container;
    }

    [ViewVariables]
    public string RootPartSlot => RootContainer.Container.ID;

    [DataField, AutoNetworkedField]
    public SoundSpecifier GibSound = new SoundCollectionSpecifier("gib");

    /// <summary>
    /// The amount of legs required to move at full speed.
    /// If 0, then legs do not impact speed.
    /// </summary>
    [DataField, AutoNetworkedField]
    public int RequiredLegs;

    [ViewVariables]
    [DataField, AutoNetworkedField]
    public HashSet<EntityUid> LegEntities = new();

    [DataField]
    public ProtoId<AlertPrototype> Alert = "LimbHealth";

    [ViewVariables, AutoNetworkedField]
    public Dictionary<BodyPart, BodyPartLayer> AlertLayers = new();
}

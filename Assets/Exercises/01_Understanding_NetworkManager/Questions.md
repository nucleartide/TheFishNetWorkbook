> For the questions below, assume that you start with a blank scene in Unity, and that you can build up your scene as you progress through the questions.
>
> Use the [FishNet docs](https://fish-networking.gitbook.io/docs/) to research questions, and guide your way to the answers.

### Question 1

When creating a new scene with FishNet, the first GameObject you typically need is a `NetworkManager` GameObject that contains many important manager classes from FishNet.

What MonoBehaviours might you need on this `NetworkManager`?

Why might you need each MonoBehaviour? That is, what would each MonoBehaviour do?

<details><summary>Answer</summary>

In the `Lobby and Worlds` example included with [FishNet Pro](https://fish-networking.gitbook.io/docs/master/pro-and-donating) (which you should purchase to support FishNet's developer), the `NetworkManager` GameObject contains the following MonoBehaviours:

| MonoBehaviour         | Why is it important                                                                                                                                                                                                                                                                                                                                                 |
|-----------------------|---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| `NetworkManager`      | Acts as a container for all things related to your networking session. Used to configure network settings, logging, and prefabs to spawn.                                                                                                                                                                                                                           |
| `PlayerSpawner`       | Will spawn a selected prefab for each newly connected client. Allows configuration of spawning behavior.                                                                                                                                                                                                                                                            |
| `LobbyNetworkSpawner` | Instantiates a `LobbyNetwork` GameObject (which implements the notion of lobby rooms) once the server connection is ready. Allows configuration of the spawned lobby network GameObject.                                                                                                                                                                            |
| `ObserverManager`     | Specifies global conditions for a GameObject to be visible to another GameObject in a networked environment. A common condition would be `SceneCondition` (a ScriptableObject), in which GameObjects must be in the same scene to be visible. This is important when implementing rooms via scene stacking, since we do not want to see GameObjects in other rooms. |
| `Tugboat`             | The presence of this component indicates that `Tugboat` is the selected data `Transport` to use.                                                                                                                                                                                                                                                                    |
| `TimeManager`         | Specifies the timing of game ticks within server and client. Additionally, `TimeManager` is used to configure how physics is simulated in the demo game.                                                                                                                                                                                                            |
</details>

### Question 2

As a follow-up to [Question 1](#question-1), create a new Unity project, and create the `NetworkManager` GameObject you described in a fresh Unity scene.

<details><summary>Answer</summary>

See `Answer_02.unity` for an example hierarchy.

Note that you can leave the newly added MonoBehaviours at their default settings:

1. You can assign `DefaultPrefabObjects` (from FishNet) to `NetworkManager`'s `Spawnable Prefabs` field.
2. You can leave `PlayerSpawner` empty for now.
3. You can create an empty `LobbyNetworkSpawner` script. We will fill this in down the line.
</details>

### Question 3

When the object you created in Question 1 is spawned, you will see additional MonoBehaviours added to the object.

What are the additional MonoBehaviours?

(It isn't terribly important to understand what they do now, but awareness that these MonoBehaviours are present will make it easier to identify where functionality is implemented down the line.)

<details><summary>Answer</summary>

There are several additional MonoBehaviours added at runtime:

* `DebugManager`
* `TransportManager`
* `ServerManager`
* `ClientManager`
* `SceneManager`
* `DefaultSceneProcessor`
* `RollbackManager`
* `PredictionManager`
* `StatisticsManager`
* `DefaultObjectPool`
* `NetworkWriterLoop`
* `NetworkReaderLoop`

As always, documentation for these MonoBehaviours can be gleaned from the official [FishNet docs](https://fish-networking.gitbook.io/docs/).
</details>

### Question 4

The `PlayerSpawner` MonoBehaviour is allowed to spawn a prefab for each newly connected client.

Give a few examples of MonoBehaviours that this prefab might contain.

<details><summary>Answer</summary>

In the Lobby and Worlds example included in FishNet Pro, the spawned `ClientInstance` contains the following MonoBehaviours:

* `ClientInstance`: Performs a version handshake with the server upon activation, and disconnects if there is a version mismatch.
* `LocalPlayerAnnouncer`: Exposes an `OnLocalPlayerUpdated` C# event that may be subscribed to, so that other scripts can react when `ClientInstance` is instantiated.
* `PlayerSettings`: An in-memory data container for the player's username.
* `NetworkObject`: A MonoBehaviour that is always present on objects with NetworkBehaviors. (`ClientInstance` would be a NetworkBehavior.) This is a helper class that invokes callbacks present on sibling NetworkBehaviors.
* `NetworkObserver`: Overrides global settings set by `ObserverManager`, such that a client (that simultaneously acts as the server) always sees connected clients' Renderers as visible. In the case of this GameObject, there are no renderers so this is a moot point, but the behavior is useful to understand for later use.
</details>

### Question 5

Create a `ClientInstance` prefab, and assign it to your `PlayerSpawner` in [Question 4](#question-4).

Note that you can create class stubs for each of `ClientInstance`, `LocalPlayerAnnouncer`, and `PlayerSettings`. We'll fill them in down the line.

<details><summary>Answer</summary>

See the `ClientInstance.prefab` and `Answer_05.unity` files within this directory for examples.
</details>

> For this set of exercises, the goal is to understand and deploy your FishNet architecture in the wild.

## Exercises

### Question 1: Cloud providers

What are some cloud providers that offer game server hosting?

*(Hint: It may help to peruse the FishNet Discord server, and review solutions that people have suggested.)*

<details><summary>Answer</summary>

Some solutions offered by the big players (Microsoft, Amazon, Google) would be:

- Azure PlayFab (Microsoft)
- Gamelift (Amazon, AWS)
- Google Cloud Platform
- Any service that provides a Virtual Private Server (VPS)

Other specialized game hosting solutions include [Edgegap](https://edgegap.com/) and [Hathora](https://hathora.dev/). Both have vocal supporters in the `#hosting` channel in FishNet's Discord server.
</details>

### Question 2: Dedicated server deployment

For this question, we will deploy a dedicated server to [Edgegap](https://edgegap.com/).

Edgegap is a game hosting service that allows you to run your game servers in the cloud. Additionally, FishNet 4 ships with an Edgegap setup wizard for easy integration.

Follow the instructions in the [official YouTube tutorial](https://www.youtube.com/watch?v=Tr9Y9Xn4Afk) to deploy a simple game server to Edgegap.

The instructions leave a fair amount of setup as implied, so feel free to review the written instructions below for closer guidance:

<details><summary>Step 1: Mandatory pre-requisites</summary>

1. **Project creation.** Create a new Unity project.
1. **Newtonsoft JSON.** You must add the `com.unity.nuget.newtonsoft-json` package to your project's Unity packages.
    * Otherwise, calling `.ToString()` on Newtonsoft `JObject` instances will return `Newtonsoft.Something.JObject` or similar, meaning that when the Edgegap integration serializes POST request bodies for sending to Edgegap, those API calls will return HTTP 400 — indicating an improperly formed request.
1. **Linux dedicated server modules.** You must have the Linux dedicated server modules (for Unity) installed. This includes the Mono and IL2CPP backends.
    * Ensure your platform is switched over to Linux dedicated server once the modules are installed. You may need to restart your editor to accomplish this.
1. **Test scene.** Open the `HashGrid_Demo` scene included with FishNet, and make sure this scene is included in Build Settings. That is, you don't want to be deploying the default `SampleScene`!
1. **Spawnable Prefabs.** You shouldn’t need to do this, but in case you see complaints about FishNet's `Spawnable Prefabs` being null, you should ensure that any `NetworkManager` in your test scenes has its `Spawnable Prefabs` set to the `Default Prefab Objects` asset.
1. **Docker.** Install [Docker Engine](https://docs.docker.com/engine/install/) so that you can build Docker containers locally.
</details>

<details><summary>Step 2: Build our dedicated server</summary>

1. **Dedicated server mode.** Once again, ensure you have your build platform set to `Dedicated Server (Linux)`.
1. **Test scene configuration.** In the `HashGrid_Demo` scene you are using as a test, on `NetworkManager`'s `NetworkHudCanvas`, ensure the `Auto Start Type` field is set to `Server`. This will automatically start the server connection upon starting the game.
1. **Edgegap auth.** Use the Edgegap setup wizard in the top menu bar, and login using a quick start token. It’s a little fidget-y, and you may need to re-create your token a few times before you can display the rest of the setup wizard.
1. **Transport.** Check the `Tugboat` component on `NetworkManager`. Make sure that the configured port is 7770 (by community convention), and that the client address is `localhost`.
1. **Edgegap port.** In the Edgegap setup wizard, make sure that the configured port is 7770. The number of 7770 is arbitrary, but this should be the same port that is set on the FishNet `Transport` in your scene (see the previous step).
1. **Build and push.** Finally, press the `Build and push` button in the Edgegap setup wizard. You may need to restart Unity at least once; the first time around for me, this command caused the editor to hang.
</details>

<details><summary>Step 3: Test your deployment</summary>

1. **Deploy.** Create a new deployment by clicking the `Deploy` button in Edgegap's setup wizard.
1. **Revert build settings.** Switch back to `Windows, Mac, Linux` for your build platform (or whichever build platform you are developing for). That is, the platform should no longer be `Dedicated Server`.
1. **Tugboat.** In your test scene's `Tugboat` component (which lives on the `NetworkManager`), set the client address to the address returned by the successful deployment in the setup wizard. Set the Server `Port` to the port returned by the deployment.
1. **Disable automatic connections.** On the `NetworkHudCanvas` in your test scene, ensure the `Auto Start Type` is set to `Disabled`. We’ll toggle the `Server` / `Client` buttons ourselves for testing.
1. **Test.** Enter Play Mode, toggle the `Client` button, and await a moment of truth. If all goes well, your client (the local Unity editor) should now be connected to your remote Edgegap deployment, and your circle character will be moving across a hash grid of dots.
</details>

### Question 3: WebGL

The setup instructions above work for a game whose platform is Windows, Mac, or Linux (Desktop).

How would the setup change if we were developing a WebGL game?

<details><summary>Answer</summary>

The `Tugboat` Transport would be replaced with [`Bayou`](https://fish-networking.gitbook.io/docs/manual/components/transports/bayou), which is the FishNet Transport class that supports WebGL.

Configuring your Edgegap build to support a `Bayou` Transport is left as an exercise to the reader. Note that you may need to configure the [underlying protocol](https://fish-networking.gitbook.io/docs/manual/components/transports/bayou#component-settings) in the Edgegap setup wizard for `Bayou` to work in production.
</details>

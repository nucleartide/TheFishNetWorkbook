# The FishNet Workbook

> Learn FishNet for Unity in the style of a math workbook: through hands-on exercises and application.

<br />

## Quick start

Please jump right to the [workbook exercises below](#on-math-workbooks).

Otherwise, read on to understand what this repo is... 👇

<br />

## Motivation

It is 2024, and any hit game will feature multiplayer heavily in its gameplay.

And if you're using Unity, [FishNet is a solid choice for your networking stack](https://forum.unity.com/threads/updated-free-networking-solution-comparison-chart.1359775/). It's free, the developer is friendly and responsive, and their Discord community is incredibly helpful.

But... there's 1 problem: _how do you get started?_

FishNet isn't *hard* to learn, but there are numerous concepts and not-too-many "getting started" guides.

And while YouTube videos or courses *can* help, I feel they aren't the most efficient way to learn. It feels akin to watching a pro play basketball; at some point, you need to stop watching others, and get your *own* hands dirty.

<br />

## The dream

The ultimate goal for you is to understand 3 things:

1. what tools are provided by your networking solution (FishNet), and how to use them,
2. when or *why* you should use each tool,
3. and how to use these tools to architect any multiplayer game of your choosing.

Be it peer-to-peer or dedicated server, turn-based or real-time — you want to be a **multiplayer architect**.

You want to be a creator of multiplayer experiences that bring people together. But how?

How do you learn something new — in this case, networking with FishNet — in the *least amount of time possible*?

<br />

## On math workbooks

You don't need a YouTube video.

What you need is a *math workbook*. A set of end-of-chapter exercises that:

1. get your hands dirty in multiplayer programming as soon as possible, and that
2. progressively build your understanding by posing well-designed questions: forcing you to do research, build GameObject hierarchies, and write C# code ASAP.

Thus, this repo provides a set of successive exercises that teach you multiplayer programming in Unity, using FishNet as your networking solution.

You can see links to the exercises below, which guide you through building a room-lobby system (inspired by [FishNet Pro's](https://fish-networking.gitbook.io/docs/master/pro-and-donating) Lobby and Worlds system):

| Exercise                           | Link                                                                          |
|------------------------------------|-------------------------------------------------------------------------------|
| Build your own `NetworkManager`    | [Exercise 1](./Assets/Exercises/01_Understanding_NetworkManager/Questions.md) |
| Managing server and client sockets | [Exercise 2](./Assets/Exercises/02_Server_Client_Sockets/Questions.md)        |
| Multiplayer architecture           | [Exercise 3](./Assets/Exercises/03_Architecture/Questions.md)                 |
| Deployment with Edgegap            | [Exercise 4](./Assets/Exercises/04_Deployment/Questions.md)                   |
| *(more to come soon...)*           |                                                                               |

<br />

## License

Please see FishNet's and TextMeshPro's respective licenses for usage restrictions.

The written exercises in this repo are unlicensed. Thus, you may not re-appropriate the written exercises as your own.

However, feel free to remix or modify the C# scripts as you see fit.

<br />

---

> Jason Tu · [jasont.co](https://jasont.co/) · [LinkedIn](https://linkedin.com/in/jasontu)
